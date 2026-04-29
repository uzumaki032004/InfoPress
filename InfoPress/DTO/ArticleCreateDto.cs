namespace InfoPress.DTO
{
    public class ArticleCreateDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Summary { get; set; } = "";
        public string ContentText { get; set; } = "";
        public string Category { get; set; } = "";
        public bool IsPremium { get; set; }
        public IFormFile? Image { get; set; }
        public string? Template { get; set; } // For Builder pattern
    }
}
