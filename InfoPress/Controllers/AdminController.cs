using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using InfoPress.Interfaces;
using InfoPress.Models;
using InfoPress.Facade;
using InfoPress.Builder;
using InfoPress.Singleton;
using InfoPress.Observer;
using InfoPress.DTO;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.IO;

namespace InfoPress.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly INewsService _newsService;
        private readonly UserManager<AppUser> _userManager;

        public AdminController(INewsService newsService, UserManager<AppUser> userManager)
        {
            _newsService = newsService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewData["SiteName"] = ManagerConfigurare.GetInstance().NumeSite;
            return View();
        }

        public async Task<IActionResult> Articles()
        {
            var articles = await _newsService.GetAllArticlesAsync(pageSize: 100);
            
            // Map back to NewsArticles for the View (unwrapping decorated premium ones if necessary)
            var mappedArticles = articles.Select(a => {
                if (a is NewsArticle na) return na;
                if (a is InfoPress.Decorator.ArticolDecorator dec && dec.WrappedArticle is NewsArticle wrapped) return wrapped;
                return null;
            }).Where(a => a != null).Cast<NewsArticle>().ToList();

            return View(mappedArticles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ArticleCreateDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArticleCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                var article = new NewsArticle
                {
                    Title = dto.Title,
                    Summary = dto.Summary,
                    ContentText = dto.ContentText,
                    Category = dto.Category,
                    IsPremium = dto.IsPremium,
                    Author = User.Identity?.Name ?? "Admin",
                    CreatedDate = DateTime.Now
                };

                // Process Image Upload
                if (dto.Image != null)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
                    
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + dto.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await dto.Image.CopyToAsync(fileStream);
                    }
                    article.ImageUrl = "/uploads/" + uniqueFileName;
                }

                // FACADE Pattern for complete article publishing
                var facade = new PublicareArticolFacade(_newsService);
                await facade.PublicaArticolCompletAsync(article);
                
                TempData["Message"] = "Articol publicat cu succes prin Facade!";
                return RedirectToAction("Articles");
            }
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var articleInterface = await _newsService.GetArticleByIdAsync(id);
            if (articleInterface == null) return NotFound();

            NewsArticle? article = articleInterface as NewsArticle;
            if (article == null && articleInterface is InfoPress.Decorator.ArticolDecorator dec)
            {
                article = dec.WrappedArticle as NewsArticle;
            }

            if (article == null) return NotFound();

            var dto = new ArticleCreateDto
            {
                Id = article.Id,
                Title = article.Title,
                Summary = article.Summary,
                ContentText = article.ContentText,
                Category = article.Category,
                IsPremium = article.IsPremium
            };
            ViewData["ExistingImageUrl"] = article.ImageUrl;
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ArticleCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                var articleInterface = await _newsService.GetArticleByIdAsync(dto.Id);
                if (articleInterface == null) return NotFound();

                NewsArticle? article = articleInterface as NewsArticle;
                if (article == null && articleInterface is InfoPress.Decorator.ArticolDecorator dec)
                {
                    article = dec.WrappedArticle as NewsArticle;
                }

                if (article == null) return NotFound();

                article.Title = dto.Title;
                article.Summary = dto.Summary;
                article.ContentText = dto.ContentText;
                article.Category = dto.Category;
                article.IsPremium = dto.IsPremium;

                if (dto.Image != null)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
                    
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + dto.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await dto.Image.CopyToAsync(fileStream);
                    }
                    article.ImageUrl = "/uploads/" + uniqueFileName;
                }

                await _newsService.UpdateArticleAsync(article);
                
                TempData["Message"] = "Articol actualizat!";
                return RedirectToAction("Articles");
            }
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _newsService.DeleteArticleAsync(id);
            TempData["Message"] = "Articol șters!";
            return RedirectToAction("Articles");
        }

        public IActionResult Users()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        public IActionResult Logs()
        {
            return View(NotificationLog.Logs);
        }

        // BUILDER: Use builder to populate DTO via AJAX or just return preset data
        [HttpGet]
        public IActionResult GetTemplate(string type)
        {
            var builder = new ArticolBuilder();
            var director = new DirectorArticol();

            if (type == "Stire") director.ConstruiesteArticolStire(builder);
            else if (type == "Sport") director.ConstruiesteArticolSportiv(builder);
            else if (type == "Editorial") director.ConstruiesteArticolEditorial(builder);
            else director.ConstruiesteArticolStire(builder);

            var result = builder.GetArticol();
            return Json(new { 
                title = result.Titlu, 
                content = result.Continut, 
                category = result.Categorie,
                author = result.Autor
            });
        }
    }
}
