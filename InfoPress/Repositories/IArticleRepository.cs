using InfoPress.Models;

namespace InfoPress.Repositories
{
    public interface IArticleRepository
    {
        Task<List<NewsArticle>> GetAllAsync(string? category = null,
                                            string? search = null,
                                            int page = 1,
                                            int pageSize = 9);
        Task<int> GetTotalCountAsync(string? category = null,
                                     string? search = null);
        Task<NewsArticle?> GetByIdAsync(int id);
        Task<List<NewsArticle>> GetRelatedAsync(string category,
                                                int excludeId,
                                                int count = 3);
        Task<int> CreateAsync(NewsArticle article);
        Task UpdateAsync(NewsArticle article);
        Task DeleteAsync(int id);
    }
}
