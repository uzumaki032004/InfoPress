using InfoPress.Interfaces;
using InfoPress.Models;
using InfoPress.Strategy;
using InfoPress.Command;
using InfoPress.Iterator;
using InfoPress.Repositories;
using InfoPress.Observer;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace InfoPress.Controllers
{
    public class NewsController : Controller
    {
        private readonly IArticleRepository _repository;
        private readonly NewsSubject _newsSubject;
        private static CommandHistory _commandHistory = new CommandHistory();

        public NewsController(IArticleRepository repository, NewsSubject newsSubject)
        {
            _repository = repository;
            _newsSubject = newsSubject;
        }

        public async Task<IActionResult> Index(string sortBy = "date", string? category = null, int page = 1)
        {
            int pageSize = 9;
            var articles = await _repository.GetAllAsync(category, search: null, page, pageSize);
            var totalCount = await _repository.GetTotalCountAsync(category);

            // STRATEGY Pattern for sorting
            ISortingStrategy strategy = sortBy?.ToLower() switch
            {
                "title" => new SortByTitleStrategy(),
                "views" => new SortByViewsStrategy(), // We'll create this
                _ => new SortByDateStrategy()
            };

            // Cast to IArticol for strategy
            var sortedArticles = strategy.Sort(articles.Cast<IArticol>().ToList());

            ViewData["CurrentCategory"] = category ?? "Toate";
            ViewData["CurrentSort"] = sortBy;
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling((double)totalCount / pageSize);

            return View(sortedArticles);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var article = await _repository.GetByIdAsync(id);
            if (article == null) return NotFound();

            // OBSERVER: Notify about view
            article.ViewCount++;
            await _repository.UpdateAsync(article);
            _newsSubject.Notify($"Articolul '{article.Title}' a fost citit.");

            // Related articles for ITERATOR
            var related = await _repository.GetRelatedAsync(article.Category, article.Id, 3);
            ViewData["Related"] = related;

            return View(article);
        }

        public async Task<IActionResult> Search(string q, int page = 1)
        {
            if (string.IsNullOrWhiteSpace(q)) return RedirectToAction("Index");

            int pageSize = 9;
            var results = await _repository.GetAllAsync(search: q, page: page, pageSize: pageSize);
            var total = await _repository.GetTotalCountAsync(search: q);

            ViewData["Query"] = q;
            ViewData["TotalResults"] = total;
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling((double)total / pageSize);

            return View(results);
        }

        [HttpPost]
        public IActionResult Bookmark(int id)
        {
            var command = new BookmarkArticleCommand(id);
            _commandHistory.ExecuteCommand(command);
            return Json(new { success = true, message = "Articol salvat!" });
        }

        [HttpPost]
        public IActionResult UndoBookmark()
        {
            _commandHistory.Undo();
            return Json(new { success = true, message = "Acțiune anulată!" });
        }
    }
}