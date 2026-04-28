using InfoPress.Interfaces;
using InfoPress.Models;

namespace InfoPress.Services
{
    public class NewsService : INewsService
    {
        private List<NewsArticle> articles = new List<NewsArticle>();

        public NewsService()
        {
            // Seed data
            articles.Add(new NewsArticle
            {
                Id = 1,
                Title = "Lansarea InfoPress - O nouă eră în jurnalism",
                ContentText = "Astăzi am lansat oficial platforma InfoPress, un portal dedicat știrilor de calitate și integrității jurnalistice. Ne propunem să aducem informații verificate și analize profunde pentru cititorii noștri.",
                Author = "Echipa InfoPress",
                Category = "Editorial",
                CreatedDate = DateTime.Now.AddDays(-2)
            });

            articles.Add(new NewsArticle
            {
                Id = 2,
                Title = "Inovații în Inteligența Artificială",
                ContentText = "Noile modele de limbaj natural revoluționează modul în care interacționăm cu tehnologia. Companiile de top anunță progrese majore în automatizarea proceselor creative.",
                Author = "Andrei Ionescu",
                Category = "Tehnologie",
                CreatedDate = DateTime.Now.AddDays(-1)
            });

            articles.Add(new NewsArticle
            {
                Id = 3,
                Title = "Economia globală în 2026: Perspective și provocări",
                ContentText = "Analiștii prevăd o creștere moderată a piețelor emergente, în timp ce inflația continuă să fie un punct de monitorizare pentru băncile centrale.",
                Author = "Maria Popescu",
                Category = "Economie",
                CreatedDate = DateTime.Now
            });
        }

        public List<NewsArticle> GetAllArticles()
        {
            return articles;
        }

        public NewsArticle GetArticleById(int id)
        {
            return articles.FirstOrDefault(a => a.Id == id);
        }

        public void PublishArticle(NewsArticle article)
        {
            article.Publish();
            articles.Add(article);
        }
    }
}