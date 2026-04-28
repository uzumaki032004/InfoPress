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
            // Seed data - Editorial
            articles.Add(new NewsArticle
            {
                Title = "Lansarea InfoPress - O nouă eră",
                ContentText = "Am lansat oficial platforma InfoPress, un portal dedicat integrității jurnalistice.",
                Author = "Echipa InfoPress",
                Category = "Editorial",
                CreatedDate = DateTime.Now.AddDays(-5)
            });

            // Seed data - Tehnologie
            var tech1 = new NewsArticle
            {
                Title = "Inovații în Inteligența Artificială",
                ContentText = "Noile modele de limbaj natural revoluționează modul în care interacționăm cu tehnologia.",
                Author = "Andrei Ionescu",
                Category = "Tehnologie",
                CreatedDate = DateTime.Now.AddDays(-1)
            };
            articles.Add(new PremiumArticolDecorator(tech1)); // Premium

            articles.Add(new NewsArticle
            {
                Title = "Viitorul Quantum Computing",
                ContentText = "Calculatoarele cuantice promit să rezolve probleme imposibile pentru hardware-ul actual.",
                Author = "Andrei Ionescu",
                Category = "Tehnologie",
                CreatedDate = DateTime.Now.AddDays(-2)
            });

            // Seed data - Politica
            articles.Add(new NewsArticle
            {
                Title = "Alegerile Parlamentare 2026",
                ContentText = "Sondajele recente arată o cursă strânsă între principalele formațiuni politice.",
                Author = "Elena Radu",
                Category = "Politica",
                CreatedDate = DateTime.Now.AddDays(-3)
            });

            articles.Add(new NewsArticle
            {
                Title = "Reforma Administrativă în Discuție",
                ContentText = "Guvernul a propus o nouă serie de măsuri pentru descentralizarea serviciilor publice.",
                Author = "Elena Radu",
                Category = "Politica",
                CreatedDate = DateTime.Now.AddDays(-4)
            });

            // Seed data - Economie
            articles.Add(new NewsArticle
            {
                Title = "Piața Imobiliară: Tendințe 2026",
                ContentText = "Analiștii observă o stabilizare a prețurilor în marile centre urbane după o perioadă de volatilitate.",
                Author = "Maria Popescu",
                Category = "Economie",
                CreatedDate = DateTime.Now.AddDays(-6)
            });

            articles.Add(new NewsArticle
            {
                Title = "Creștere Economică în Sectorul Tech",
                ContentText = "Sectorul tehnologic continuă să fie principalul motor de creștere al economiei naționale.",
                Author = "Maria Popescu",
                Category = "Economie",
                CreatedDate = DateTime.Now
            });
        }

        public List<IArticol> GetAllArticles()
        {
            return articles;
        }

        public IArticol GetArticleById(int id)
        {
            return articles.Count > 0 ? articles[0] : null;
        }

        public void PublishArticle(NewsArticle article)
        {
            article.Publish();
            articles.Add(article);
        }
    }
}