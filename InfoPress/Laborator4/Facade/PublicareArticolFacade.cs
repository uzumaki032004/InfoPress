using InfoPress.Models;
using InfoPress.Interfaces;

namespace InfoPress.Facade
{
    public class PublicareArticolFacade
    {
        private readonly INewsService _newsService;
        private readonly ArticolService _articolService = new ArticolService();
        private readonly ImagineService _imagineService = new ImagineService();
        private readonly NotificareService _notificareService = new NotificareService();

        public PublicareArticolFacade(INewsService newsService)
        {
            _newsService = newsService;
        }

        public void PublicaArticolComplet(NewsArticle articol)
        {
            // 1. Salvare în baza de date (sau serviciu)
            _newsService.PublishArticle(articol);
            
            // 2. Procesare imagini (subsistem complex)
            _imagineService.IncarcaImagine();
            
            // 3. Notificare abonați (subsistem complex)
            _notificareService.TrimiteNotificare();
            
            // 4. Logare acțiune
            _articolService.SalveazaArticol();
        }
    }
}