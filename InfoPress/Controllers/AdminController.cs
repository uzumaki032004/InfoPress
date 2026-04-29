using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using InfoPress.Interfaces;
using InfoPress.Models;
using InfoPress.Facade;
using InfoPress.Builder;
using InfoPress.Singleton;
using InfoPress.Observer;
using InfoPress.Repositories;
using InfoPress.DTO;

namespace InfoPress.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IArticleRepository _repository;
        private readonly UserManager<AppUser> _userManager;
        private readonly NewsSubject _newsSubject;

        public AdminController(IArticleRepository repository, UserManager<AppUser> userManager, NewsSubject newsSubject)
        {
            _repository = repository;
            _userManager = userManager;
            _newsSubject = newsSubject;
        }

        public IActionResult Index()
        {
            ViewData["SiteName"] = ManagerConfigurare.GetInstance().NumeSite;
            return View();
        }

        public async Task<IActionResult> Articles()
        {
            var articles = await _repository.GetAllAsync(pageSize: 100);
            return View(articles);
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

                await _repository.CreateAsync(article);
                _newsSubject.Notify($"Admin a creat articolul: {article.Title}");
                
                TempData["Message"] = "Articol creat cu succes!";
                return RedirectToAction("Articles");
            }
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var article = await _repository.GetByIdAsync(id);
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
                var article = await _repository.GetByIdAsync(dto.Id);
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

                await _repository.UpdateAsync(article);
                _newsSubject.Notify($"Admin a editat articolul: {article.Title}");
                
                TempData["Message"] = "Articol actualizat!";
                return RedirectToAction("Articles");
            }
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
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
            else if (type == "Editorial") director.ConstruiesteArticolEditorial(builder); // Assume this exists
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
