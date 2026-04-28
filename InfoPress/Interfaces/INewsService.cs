using InfoPress.Interfaces;
using InfoPress.Models;
using System.Collections.Generic;

namespace InfoPress.Interfaces
{
    public interface INewsService
    {
        List<IArticol> GetAllArticles();
        IArticol GetArticleById(int id);
        void PublishArticle(NewsArticle article);
    }
}