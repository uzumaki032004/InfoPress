using InfoPress.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfoPress.Interfaces
{
    public interface INewsService
    {
        Task<List<IArticol>> GetAllArticlesAsync(string? category = null, string? search = null, int page = 1, int pageSize = 9);
        Task<int> GetTotalCountAsync(string? category = null, string? search = null);
        Task<IArticol?> GetArticleByIdAsync(int id);
        Task<List<IArticol>> GetRelatedArticlesAsync(string category, int excludeId, int count = 3);
        Task PublishArticleAsync(NewsArticle article);
        Task UpdateArticleAsync(NewsArticle article);
        Task DeleteArticleAsync(int id);
    }
}