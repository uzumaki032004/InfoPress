using InfoPress.Interfaces;
using InfoPress.Models;
using InfoPress.Strategy;
using InfoPress.Command;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace InfoPress.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private static CommandHistory _commandHistory = new CommandHistory();

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        public IActionResult Index(string sortBy = "date", string category = null)
        {
            var articles = _newsService.GetAllArticles();

            // Filtrare după categorie (dacă este specificată)
            if (!string.IsNullOrEmpty(category))
            {
                articles = articles.Where(a => a.Category.Equals(category, System.StringComparison.OrdinalIgnoreCase)).ToList();
                ViewData["CurrentCategory"] = category;
            }
            else
            {
                ViewData["CurrentCategory"] = "Toate";
            }

            // STRATEGY: Alegerea algoritmului de sortare
            ISortingStrategy strategy = sortBy?.ToLower() switch
            {
                "title" => new SortByTitleStrategy(),
                _ => new SortByDateStrategy()
            };

            var sortedArticles = strategy.Sort(articles);
            ViewData["CurrentSort"] = sortBy;

            return View(sortedArticles);
        }

        [HttpPost]
        public IActionResult Bookmark(int id)
        {
            var command = new BookmarkArticleCommand(id);
            _commandHistory.ExecuteCommand(command);
            return Json(new { success = true, message = "Articol salvat în favorite!" });
        }

        [HttpPost]
        public IActionResult UndoBookmark()
        {
            _commandHistory.Undo();
            return Json(new { success = true, message = "Acțiune anulată cu succes!" });
        }

        public IActionResult Publish()
        {
            var article = new NewsArticle
            {
                Title = "Știre Nouă",
                ContentText = "Conținut generat pentru testarea fluxului de publicare.",
                Author = "Admin",
                Category = "General",
                CreatedDate = System.DateTime.Now
            };

            _newsService.PublishArticle(article);
            return RedirectToAction("Index");
        }
    }
}