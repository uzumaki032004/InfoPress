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
        private int _articleId;
        public BookmarkArticleCommand(int articleId) => _articleId = articleId;

        public void Execute()
        {
            System.Console.WriteLine($"[Comandă] Articolul {_articleId} a fost adăugat la favorite.");
        }

        public void Undo()
        {
            System.Console.WriteLine($"[Comandă] Articolul {_articleId} a fost eliminat de la favorite (Undo).");
        }
    }

    public class CommandHistory
    {
        private Stack<ICommand> _history = new Stack<ICommand>();

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            _history.Push(command);
        }

        public void Undo()
        {
            if (_history.Count > 0)
            {
                _history.Pop().Undo();
            }
        }
    }
}
