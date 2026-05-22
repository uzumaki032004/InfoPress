using InfoPress.Interfaces;
using InfoPress.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfoPress.Proxy
{
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

        public Task<List<IArticol>> GetAllArticlesAsync(string? category = null, string? search = null, int page = 1, int pageSize = 9)
        {
            return _realService.GetAllArticlesAsync(category, search, page, pageSize);
        }

        public Task<int> GetTotalCountAsync(string? category = null, string? search = null)
        {
            return _realService.GetTotalCountAsync(category, search);
        }

        public Task<IArticol?> GetArticleByIdAsync(int id)
        {
            return _realService.GetArticleByIdAsync(id);
        }

        public Task<List<IArticol>> GetRelatedArticlesAsync(string category, int excludeId, int count = 3)
        {
            return _realService.GetRelatedArticlesAsync(category, excludeId, count);
        }

        public Task PublishArticleAsync(NewsArticle article)
        {
            if (_userRole == "Admin")
            {
                return _realService.PublishArticleAsync(article);
            }
            throw new System.UnauthorizedAccessException("Doar administratorii pot publica articole!");
        }

        public Task UpdateArticleAsync(NewsArticle article)
        {
            if (_userRole == "Admin")
            {
                return _realService.UpdateArticleAsync(article);
            }
            throw new System.UnauthorizedAccessException("Doar administratorii pot edita articole!");
        }

        public Task DeleteArticleAsync(int id)
        {
            if (_userRole == "Admin")
            {
                return _realService.DeleteArticleAsync(id);
            }
            throw new System.UnauthorizedAccessException("Doar administratorii pot șterge articole!");
        }
    }
}
