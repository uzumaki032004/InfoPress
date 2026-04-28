using InfoPress.Interfaces;
using InfoPress.Models;
using InfoPress.Decorator;
using System.Collections.Generic;
using System;

namespace InfoPress.Services
{
    public class NewsService : INewsService
    {
        private List<IArticol> articles = new List<IArticol>();

        public NewsService()
        {
            // Seed data
            var a1 = new NewsArticle
            {
                Id = 1,
                Title = "Lansarea InfoPress - O nouă eră în jurnalism",
                ContentText = "Astăzi am lansat oficial platforma InfoPress, un portal dedicat știrilor de calitate și integrității jurnalistice.",
                Author = "Echipa InfoPress",
                Category = "Editorial",
                CreatedDate = DateTime.Now.AddDays(-2)
            };

            var a2 = new NewsArticle
            {
                Id = 2,
                Title = "Inovații în Inteligența Artificială (Exclusiv)",
                ContentText = "Noile modele de limbaj natural revoluționează modul în care interacționăm cu tehnologia. Acest articol conține detalii tehnice exclusive.",
                Author = "Andrei Ionescu",
                Category = "Tehnologie",
                CreatedDate = DateTime.Now.AddDays(-1)
            };

            var a3 = new NewsArticle
            {
                Id = 3,
                Title = "Economia globală în 2026: Perspective",
                ContentText = "Analiștii prevăd o creștere moderată a piețelor emergente în acest an.",
                Author = "Maria Popescu",
                Category = "Economie",
                CreatedDate = DateTime.Now
            };

            articles.Add(a1);
            // Aplicăm DECORATOR-ul pentru a face articolul 2 Premium
            articles.Add(new PremiumArticolDecorator(a2));
            articles.Add(a3);
        }

        public List<IArticol> GetAllArticles()
        {
            return articles;
        }

        public IArticol GetArticleById(int id)
        {
            // Simulare simplă pentru id
            return articles.Find(a => a.Title.Contains(id.ToString()));
        }

        public void PublishArticle(NewsArticle article)
        {
            article.Publish();
            articles.Add(article);
        }
    }
}