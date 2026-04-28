namespace InfoPress.Models
{
    public class NewsArticle : Content
    {
        public int Id { get; set; }

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