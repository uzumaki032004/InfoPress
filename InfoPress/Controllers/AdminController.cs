using Microsoft.AspNetCore.Mvc;
using InfoPress.Interfaces;
using InfoPress.Models;
using InfoPress.Facade;
using InfoPress.Builder;
using InfoPress.Singleton;
using InfoPress.Observer;

namespace InfoPress.Controllers
{
    public class AdminController : Controller
    {
        private readonly INewsService _newsService;

        public AdminController(INewsService newsService)
        {
            _newsService = newsService;
        }

        public IActionResult Index()
        {
            // SINGLETON: Afișare configurație
            ViewData["SiteName"] = ManagerConfigurare.GetInstance().NumeSite;
            ViewData["Logs"] = NotificationLog.Logs;
            
            return View();
        }

        // FACADE: Publicare simplificată
        [HttpPost]
        public IActionResult PublishFacade(string title, string content)
        {
            var facade = new PublicareArticolFacade(_newsService);
            var article = new NewsArticle { Title = title, ContentText = content, Author = "Admin", Category = "General", CreatedDate = System.DateTime.Now };
            
            facade.PublicaArticolComplet(article);
            TempData["Message"] = "Articol publicat prin Façade!";
            return RedirectToAction("Index");
        }

        // BUILDER: Construcție pas cu pas
        [HttpPost]
        public IActionResult PublishBuilder()
        {
            var builder = new ArticolBuilder();
            var director = new DirectorArticol();
            
            director.ConstruiesteArticolStire(builder);
            var article = builder.GetArticol();
            
            // Convertim modelul Builder (Articol) în modelul Domain (NewsArticle) pentru simplitate în demo
            var newsArticle = new NewsArticle { 
                Title = article.Titlu, 
                ContentText = article.Continut, 
                Author = article.Autor, 
                Category = article.Categorie, 
                CreatedDate = System.DateTime.Now 
            };
            
            _newsService.PublishArticle(newsArticle);
            TempData["Message"] = "Articol construit cu Builder și publicat!";
            return RedirectToAction("Index");
        }
    }
}
