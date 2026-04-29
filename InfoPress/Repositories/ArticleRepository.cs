using InfoPress.Data;
using InfoPress.Models;
using Microsoft.EntityFrameworkCore;

namespace InfoPress.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly AppDbContext _db;

        public ArticleRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<NewsArticle>> GetAllAsync(string? category = null, string? search = null, int page = 1, int pageSize = 9)
        {
            var query = _db.Articles.AsQueryable();

            if (!string.IsNullOrEmpty(category))
                query = query.Where(a => a.Category == category);

            if (!string.IsNullOrEmpty(search))
                query = query.Where(a => a.Title.Contains(search) || a.ContentText.Contains(search));

            return await query
                .OrderByDescending(a => a.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(string? category = null, string? search = null)
        {
            var query = _db.Articles.AsQueryable();

            if (!string.IsNullOrEmpty(category))
                query = query.Where(a => a.Category == category);

            if (!string.IsNullOrEmpty(search))
                query = query.Where(a => a.Title.Contains(search) || a.ContentText.Contains(search));

            return await query.CountAsync();
        }

        public async Task<NewsArticle?> GetByIdAsync(int id)
        {
            return await _db.Articles.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<NewsArticle>> GetRelatedAsync(string category, int excludeId, int count = 3)
        {
            return await _db.Articles
                .Where(a => a.Category == category && a.Id != excludeId)
                .Take(count)
                .ToListAsync();
        }

        public async Task<int> CreateAsync(NewsArticle article)
        {
            _db.Articles.Add(article);
            await _db.SaveChangesAsync();
            return article.Id;
        }

        public async Task UpdateAsync(NewsArticle article)
        {
            _db.Articles.Update(article);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var article = await GetByIdAsync(id);
            if (article != null)
            {
                _db.Articles.Remove(article);
                await _db.SaveChangesAsync();
            }
        }
    }
}
