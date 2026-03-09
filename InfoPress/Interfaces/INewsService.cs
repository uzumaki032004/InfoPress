using InfoPress.Models;

namespace InfoPress.Interfaces
{
    public interface INewsService
    {
        List<NewsArticle> GetAllArticles();

        NewsArticle GetArticleById(int id);

        void PublishArticle(NewsArticle article);
    }
}