namespace InfoPress.Models
{
    public class NewsArticle : Content
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public string Category { get; set; }

        public override void Publish()
        {
            Console.WriteLine("Article published.");
        }
    }
}