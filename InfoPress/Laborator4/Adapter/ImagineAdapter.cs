using InfoPress.Interfaces;

namespace InfoPress.Adapter
{
    public class ImagineAdapter : IImagineService
    {
        private ServiciuExternImagini serviciuExtern;

        public ImagineAdapter()
        {
            serviciuExtern = new ServiciuExternImagini();
        }

        public void IncarcaImagine(string numeImagine)
        {
            serviciuExtern.UploadImage(numeImagine);
        }
    }
}