namespace InfoPress.Facade
{
    public class PublicareArticolFacade
    {
        private ArticolService articolService = new ArticolService();
        private ImagineService imagineService = new ImagineService();
        private NotificareService notificareService = new NotificareService();

        public void PublicaArticol()
        {
            articolService.SalveazaArticol();
            imagineService.IncarcaImagine();
            notificareService.TrimiteNotificare();
        }
    }

}