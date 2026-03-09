using Microsoft.AspNetCore.Mvc;
using InfoPress.Interfaces;
using InfoPress.Models;

namespace InfoPress.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        public IActionResult Index()
        {
            var articles = _newsService.GetAllArticles();
            return View(articles);
        }

        public IActionResult Publish()
        {
            var article = new NewsArticle
            {
                Id = 1,
                Title = "First News",
                ContentText = "Welcome to InfoPress",
                Author = "Admin",
                Category = "General",
                CreatedDate = DateTime.Now
            };

            _newsService.PublishArticle(article);

            return RedirectToAction("Index");
        }
    }
}