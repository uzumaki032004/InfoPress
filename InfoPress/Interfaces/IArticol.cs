namespace InfoPress.Interfaces
{
    public interface IArticol
    {
        string Title { get; set; }
        string ContentText { get; set; }
        string Author { get; set; }
        string Category { get; set; }
        DateTime CreatedDate { get; set; }
        bool IsPremium { get; set; }
        int ViewCount { get; set; }
        void AfiseazaArticol();
    }
}