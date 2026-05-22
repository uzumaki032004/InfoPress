using InfoPress.Interfaces;
using InfoPress.Models;
using InfoPress.Decorator;
using InfoPress.Observer;
using InfoPress.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace InfoPress.Services
{
    public class NewsService : INewsService
    {
        private readonly IArticleRepository _repository;
        private readonly NewsSubject _newsSubject;

        public NewsService(IArticleRepository repository, NewsSubject newsSubject)
        {
            _repository = repository;
            _newsSubject = newsSubject;
        }

        public async Task<List<IArticol>> GetAllArticlesAsync(string? category = null, string? search = null, int page = 1, int pageSize = 9)
        {
            var articles = await _repository.GetAllAsync(category, search, page, pageSize);
            var results = new List<IArticol>();
            foreach (var article in articles)
            {
                if (article.IsPremium)
                {
                    results.Add(new PremiumArticolDecorator(article));
                }
                else
                {
                    results.Add(article);
                }
            }
            return results;
        }

        public async Task<int> GetTotalCountAsync(string? category = null, string? search = null)
        {
            return await _repository.GetTotalCountAsync(category, search);
        }

        public async Task<IArticol?> GetArticleByIdAsync(int id)
        {
            var article = await _repository.GetByIdAsync(id);
            if (article == null) return null;

            if (article.IsPremium)
            {
                return new PremiumArticolDecorator(article);
            }
            return article;
        }

        public async Task<List<IArticol>> GetRelatedArticlesAsync(string category, int excludeId, int count = 3)
        {
            var articles = await _repository.GetRelatedAsync(category, excludeId, count);
            return articles.Cast<IArticol>().ToList();
        }

        public async Task PublishArticleAsync(NewsArticle article)
        {
            article.Publish();
            await _repository.CreateAsync(article);
            
            // OBSERVER: Notificare abonați
            _newsSubject.Notify($"Un articol nou a fost publicat: {article.Title}");
        }

        public async Task UpdateArticleAsync(NewsArticle article)
        {
            await _repository.UpdateAsync(article);
            _newsSubject.Notify($"Articolul a fost actualizat: {article.Title}");
        }

        public async Task DeleteArticleAsync(int id)
        {
            var article = await _repository.GetByIdAsync(id);
            if (article != null)
            {
                await _repository.DeleteAsync(id);
                _newsSubject.Notify($"Articolul a fost șters: {article.Title}");
            }
        }
    }
}