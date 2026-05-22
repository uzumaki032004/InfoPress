using InfoPress.Interfaces;
using InfoPress.Models;
using InfoPress.Strategy;
using InfoPress.Command;
using InfoPress.Iterator;
using InfoPress.Observer;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace InfoPress.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly NewsSubject _newsSubject;

        public NewsController(INewsService newsService, NewsSubject newsSubject)
        {
            _newsService = newsService;
            _newsSubject = newsSubject;
        }

        public async Task<IActionResult> Index(string sortBy = "date", string? category = null, int page = 1)
        {
            int pageSize = 9;
            var articles = await _newsService.GetAllArticlesAsync(category, search: null, page, pageSize);
            var totalCount = await _newsService.GetTotalCountAsync(category);

            // STRATEGY Pattern for sorting
            ISortingStrategy strategy = sortBy?.ToLower() switch
            {
                "title" => new SortByTitleStrategy(),
                "views" => new SortByViewsStrategy(),
                _ => new SortByDateStrategy()
            };

            // Cast to IArticol for strategy
            var sortedArticles = strategy.Sort(articles);

            ViewData["CurrentCategory"] = category ?? "Toate";
            ViewData["CurrentSort"] = sortBy;
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling((double)totalCount / pageSize);

            return View(sortedArticles);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var articleInterface = await _newsService.GetArticleByIdAsync(id);
            if (articleInterface == null) return NotFound();

            // Unwrap decorator if present to get original NewsArticle for the view
            NewsArticle? article = articleInterface as NewsArticle;
            if (article == null && articleInterface is InfoPress.Decorator.ArticolDecorator dec)
            {
                article = dec.WrappedArticle as NewsArticle;
            }

            if (article == null) return NotFound();

            // OBSERVER: Notify about view and update count
            article.ViewCount++;
            await _newsService.UpdateArticleAsync(article);
            _newsSubject.Notify($"Articolul '{article.Title}' a fost citit.");

            // Related articles
            var related = await _newsService.GetRelatedArticlesAsync(article.Category, article.Id, 3);
            ViewData["Related"] = related.Cast<NewsArticle>().ToList();

            return View(article);
        }

        public async Task<IActionResult> Browse(int index = 0)
        {
            var articles = await _newsService.GetAllArticlesAsync(category: null, search: null, page: 1, pageSize: 100);
            if (articles.Count == 0)
            {
                ViewData["Index"] = 0;
                ViewData["Total"] = 0;
                ViewData["HasNext"] = false;
                ViewData["HasPrev"] = false;
                return View((IArticol?)null);
            }

            var iterator = new NewsIterator(articles);
            
            // Advance iterator to the requested index
            IArticol? current = iterator.First();
            int currentIdx = 0;
            while (currentIdx < index && !iterator.IsDone())
            {
                current = iterator.Next();
                currentIdx++;
            }

            ViewData["Index"] = index;
            ViewData["Total"] = articles.Count;
            ViewData["HasNext"] = index < articles.Count - 1;
            ViewData["HasPrev"] = index > 0;

            return View(current);
        }

        public async Task<IActionResult> Search(string q, int page = 1)
        {
            if (string.IsNullOrWhiteSpace(q)) return RedirectToAction("Index");

            int pageSize = 9;
            var results = await _newsService.GetAllArticlesAsync(category: null, search: q, page: page, pageSize: pageSize);
            var total = await _newsService.GetTotalCountAsync(category: null, search: q);

            ViewData["Query"] = q;
            ViewData["TotalResults"] = total;
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling((double)total / pageSize);

            // Map back to NewsArticles for the Search View (unwrapping if necessary)
            var mappedResults = results.Select(r => {
                if (r is NewsArticle na) return na;
                if (r is InfoPress.Decorator.ArticolDecorator dec && dec.WrappedArticle is NewsArticle wrapped) return wrapped;
                return null;
            }).Where(r => r != null).Cast<NewsArticle>().ToList();

            return View(mappedResults);
        }

        [HttpPost]
        public IActionResult Bookmark(int id)
        {
            var command = new BookmarkArticleCommand(id, HttpContext.Session);
            var history = new CommandHistory(HttpContext.Session);
            history.ExecuteCommand(command, id);
            return Json(new { success = true, message = "Articol salvat!" });
        }

        [HttpPost]
        public IActionResult UndoBookmark()
        {
            var history = new CommandHistory(HttpContext.Session);
            history.Undo();
            return Json(new { success = true, message = "Acțiune anulată!" });
        }
    }
}