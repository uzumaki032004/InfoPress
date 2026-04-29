using InfoPress.Interfaces;
using InfoPress.Models;
using InfoPress.Decorator;
using InfoPress.Observer;
using System.Collections.Generic;
using System;

namespace InfoPress.Services
{
    public class NewsService : INewsService
    {
        private List<IArticol> articles = new List<IArticol>();
        private NewsSubject _newsSubject = new NewsSubject();

        public NewsService()
        {
            // Abonați implicit pentru demonstrație
            _newsSubject.Subscribe(new UserSubscriber("Cititor Standard"));
            _newsSubject.Subscribe(new UserSubscriber("Abonat Premium"));

            // Seed data
            articles.Add(new NewsArticle { Title = "Lansarea InfoPress", ContentText = "...", Author = "Admin", Category = "Editorial", CreatedDate = DateTime.Now.AddDays(-5) });
            var tech1 = new NewsArticle { Title = "Inovații în AI", ContentText = "...", Author = "Ionescu", Category = "Tehnologie", CreatedDate = DateTime.Now.AddDays(-1) };
            articles.Add(new PremiumArticolDecorator(tech1));
            articles.Add(new NewsArticle { Title = "Economia 2026", ContentText = "...", Author = "Popescu", Category = "Economie", CreatedDate = DateTime.Now });
        }

        public List<IArticol> GetAllArticles() => articles;
        public IArticol GetArticleById(int id) => articles.Count > 0 ? articles[0] : null;

        public void PublishArticle(NewsArticle article)
        {
            article.Publish();
            articles.Add(article);
            
            // OBSERVER: Notificare abonați
            _newsSubject.Notify($"Un articol nou a fost publicat: {article.Title}");
        }
    }
}