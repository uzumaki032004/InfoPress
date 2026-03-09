namespace InfoPress.Models
{
    public abstract class Content
    {
        public string Title { get; set; }

        public string ContentText { get; set; }

        public DateTime CreatedDate { get; set; }

        public abstract void Publish();
    }
}