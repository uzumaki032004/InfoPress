using Microsoft.AspNetCore.Mvc;
using InfoPress.Memento;

namespace InfoPress.Controllers
{
    public class ArticleEditorController : Controller
    {
        private static ArticleEditor _editor = new ArticleEditor { Title = "Titlu Nou", ContentText = "Scrie aici..." };
        private static ArticleCaretaker _caretaker = new ArticleCaretaker();

        public IActionResult Index()
        {
            return View(_editor);
        }

        [HttpPost]
        public IActionResult Save(string title, string content)
        {
            _editor.Title = title;
            _editor.ContentText = content;
            _caretaker.Backup = _editor.Save();
            TempData["Message"] = "Versiune salvată (Memento)!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Restore()
        {
            if (_caretaker.Backup != null)
            {
                _editor.Restore(_caretaker.Backup);
                TempData["Message"] = "Versiune restaurată (Memento)!";
            }
            else
            {
                TempData["Error"] = "Nu există nicio versiune salvată!";
            }
            return RedirectToAction("Index");
        }
    }
}
