namespace InfoPress.Models
{
    public class NewsArticle : Content
    {
        public int Id { get; set; }
        public string Summary { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public int ReadingTimeMinutes { get; set; }
        public string Tags { get; set; } = "";
        public bool IsPublished { get; set; } = true;

        public override void Publish()
        {
            Console.WriteLine($"Articolul '{Title}' a fost publicat.");
        }

        public override void AfiseazaArticol()
        {
            Console.WriteLine($"Se afișează articolul: {Title}");
        }
    }
}