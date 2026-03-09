using InfoPress.Interfaces;

namespace InfoPress.Models
{
    public class GalerieFoto : IContinut
    {
        public string Titlu { get; set; }

        public void Publica()
        {
            Console.WriteLine("Se publică galeria foto: " + Titlu);
        }
    }
}