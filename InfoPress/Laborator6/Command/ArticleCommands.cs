using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Collections.Generic;

namespace InfoPress.Command
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    public class BookmarkArticleCommand : ICommand
    {
        private readonly int _articleId;
        private readonly ISession _session;
        private const string BookmarkSessionKey = "UserBookmarks";

        public BookmarkArticleCommand(int articleId, ISession session)
        {
            _articleId = articleId;
            _session = session;
        }

        private List<int> GetBookmarks()
        {
            var json = _session.GetString(BookmarkSessionKey);
            if (string.IsNullOrEmpty(json)) return new List<int>();
            return JsonSerializer.Deserialize<List<int>>(json) ?? new List<int>();
        }

        private void SaveBookmarks(List<int> list)
        {
            _session.SetString(BookmarkSessionKey, JsonSerializer.Serialize(list));
        }

        public void Execute()
        {
            var bookmarks = GetBookmarks();
            if (!bookmarks.Contains(_articleId))
            {
                bookmarks.Add(_articleId);
                SaveBookmarks(bookmarks);
            }
            System.Console.WriteLine($"[Comandă Session] Articolul {_articleId} adăugat la favorite.");
        }

        public void Undo()
        {
            var bookmarks = GetBookmarks();
            if (bookmarks.Contains(_articleId))
            {
                bookmarks.Remove(_articleId);
                SaveBookmarks(bookmarks);
            }
            System.Console.WriteLine($"[Comandă Session] Articolul {_articleId} eliminat de la favorite (Undo).");
        }
    }

    public class CommandHistory
    {
        private readonly ISession _session;
        private const string HistorySessionKey = "BookmarkCommandHistory";

        public CommandHistory(ISession session)
        {
            _session = session;
        }

        private List<int> GetHistory()
        {
            var json = _session.GetString(HistorySessionKey);
            if (string.IsNullOrEmpty(json)) return new List<int>();
            return JsonSerializer.Deserialize<List<int>>(json) ?? new List<int>();
        }

        private void SaveHistory(List<int> history)
        {
            _session.SetString(HistorySessionKey, JsonSerializer.Serialize(history));
        }

        public void ExecuteCommand(ICommand command, int articleId)
        {
            command.Execute();
            var history = GetHistory();
            history.Add(articleId);
            SaveHistory(history);
        }

        public void Undo()
        {
            var history = GetHistory();
            if (history.Count > 0)
            {
                int lastArticleId = history[history.Count - 1];
                history.RemoveAt(history.Count - 1);
                SaveHistory(history);

                var undoCommand = new BookmarkArticleCommand(lastArticleId, _session);
                undoCommand.Undo();
            }
        }
    }
}
