using InfoPress.Interfaces;

namespace InfoPress.Models
{
    public abstract class Content : IArticol
    {
        public string Title { get; set; } = "";
        public string ContentText { get; set; } = "";
        public string Author { get; set; } = "";
        public string Category { get; set; } = "";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsPremium { get; set; }
        public int ViewCount { get; set; }

        public abstract void Publish();
        public virtual void AfiseazaArticol()
        {
            // Logica de baza
        }
    }
}