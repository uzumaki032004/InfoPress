namespace InfoPress.Memento
{
    // Memento
    public class ArticleMemento
    {
        public string Title { get; private set; }
        public string ContentText { get; private set; }

        public ArticleMemento(string title, string content)
        {
            Title = title;
            ContentText = content;
        }
    }

    // Originator
    public class ArticleEditor
    {
        public string Title { get; set; }
        public string ContentText { get; set; }

        public ArticleMemento Save()
        {
            return new ArticleMemento(Title, ContentText);
        }

        public void Restore(ArticleMemento memento)
        {
            Title = memento.Title;
            ContentText = memento.ContentText;
        }
    }

    // Caretaker
    public class ArticleCaretaker
    {
        public ArticleMemento Backup { get; set; }
    }
}
