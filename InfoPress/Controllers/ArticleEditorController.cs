using Microsoft.AspNetCore.Mvc;
using InfoPress.Memento;
using System.Text.Json;

namespace InfoPress.Controllers
{
    public class ArticleEditorController : Controller
    {
        private const string EditorSessionKey = "ArticleEditorState";
        private const string CaretakerSessionKey = "ArticleMementoBackup";

        private ArticleEditor GetEditorFromSession()
        {
            var json = HttpContext.Session.GetString(EditorSessionKey);
            if (string.IsNullOrEmpty(json))
            {
                var defaultEditor = new ArticleEditor { Title = "Titlu Nou", ContentText = "Scrie aici..." };
                SaveEditorToSession(defaultEditor);
                return defaultEditor;
            }
            return JsonSerializer.Deserialize<ArticleEditor>(json) ?? new ArticleEditor();
        }

        private void SaveEditorToSession(ArticleEditor editor)
        {
            HttpContext.Session.SetString(EditorSessionKey, JsonSerializer.Serialize(editor));
        }

        public IActionResult Index()
        {
            var editor = GetEditorFromSession();
            return View(editor);
        }

        [HttpPost]
        public IActionResult Save(string title, string content)
        {
            var editor = GetEditorFromSession();
            editor.Title = title;
            editor.ContentText = content;
            SaveEditorToSession(editor);

            var memento = editor.Save();
            var mementoJson = JsonSerializer.Serialize(memento);
            HttpContext.Session.SetString(CaretakerSessionKey, mementoJson);

            TempData["Message"] = "Versiune salvată în Sesiune (Memento)!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Restore()
        {
            var mementoJson = HttpContext.Session.GetString(CaretakerSessionKey);
            if (!string.IsNullOrEmpty(mementoJson))
            {
                var memento = JsonSerializer.Deserialize<ArticleMemento>(mementoJson);
                if (memento != null)
                {
                    var editor = GetEditorFromSession();
                    editor.Restore(memento);
                    SaveEditorToSession(editor);
                    TempData["Message"] = "Versiune restaurată din Sesiune (Memento)!";
                    return RedirectToAction("Index");
                }
            }

            TempData["Error"] = "Nu există nicio versiune salvată în această sesiune!";
            return RedirectToAction("Index");
        }
    }
}
