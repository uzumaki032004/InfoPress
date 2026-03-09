using InfoPress.Interfaces;
using InfoPress.Models;

namespace InfoPress.Services
{
    public class NewsService : INewsService
    {
        private List<NewsArticle> articles = new List<NewsArticle>();

        public List<NewsArticle> GetAllArticles()
        {
            return articles;
        }

        public NewsArticle GetArticleById(int id)
        {
            return articles.FirstOrDefault(a => a.Id == id);
        }

        public void PublishArticle(NewsArticle article)
        {
            article.Publish();
            articles.Add(article);
        }
    }
}