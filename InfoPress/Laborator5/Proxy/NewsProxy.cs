using InfoPress.Interfaces;
using InfoPress.Models;
using System.Collections.Generic;

namespace InfoPress.Proxy
{
    // Real Subject
    public class RealNewsService : INewsService
    {
        public List<IArticol> GetAllArticles() => new List<IArticol>();
        public IArticol GetArticleById(int id) => null;
        public void PublishArticle(NewsArticle article) { }
    }

    // Protection Proxy
    public class NewsAccessProxy : INewsService
    {
        private readonly INewsService _realService;
        private readonly string _userRole;

        public NewsAccessProxy(INewsService realService, string userRole)
        {
            _realService = realService;
            _userRole = userRole;
        }

        public List<IArticol> GetAllArticles()
        {
            return _realService.GetAllArticles();
        }

        public IArticol GetArticleById(int id)
        {
            return _realService.GetArticleById(id);
        }

        public void PublishArticle(NewsArticle article)
        {
            if (_userRole == "Admin")
            {
                _realService.PublishArticle(article);
            }
        }
    }
}
