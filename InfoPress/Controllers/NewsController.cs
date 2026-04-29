using InfoPress.Interfaces;
using InfoPress.Models;
using InfoPress.Strategy;
using InfoPress.Command;
using InfoPress.Iterator;
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
            if (!string.IsNullOrEmpty(category))
            {
                articles = articles.Where(a => a.Category.Equals(category, System.StringComparison.OrdinalIgnoreCase)).ToList();
                ViewData["CurrentCategory"] = category;
            }
            else ViewData["CurrentCategory"] = "Toate";

            ISortingStrategy strategy = sortBy?.ToLower() switch { "title" => new SortByTitleStrategy(), _ => new SortByDateStrategy() };
            var sortedArticles = strategy.Sort(articles);
            ViewData["CurrentSort"] = sortBy;
            return View(sortedArticles);
        }

        // ITERATOR: Parcurgere secvențială cu Iterator Pattern
        public IActionResult Browse()
        {
            var articles = _newsService.GetAllArticles();
            var iterator = new NewsIterator(articles);
            var items = new List<IArticol>();
            
            while (!iterator.IsDone())
            {
                items.Add(iterator.CurrentItem());
                iterator.Next();
            }

            return View(items);
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