namespace InfoPress.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = "";
        public string AuthorId { get; set; } = "";
        public int ArticleId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        // Navigation properties
        public virtual NewsArticle? Article { get; set; }
        public virtual AppUser? Author { get; set; }
    }
}
