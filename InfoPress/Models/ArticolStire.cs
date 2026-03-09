using InfoPress.Interfaces;

namespace InfoPress.Models
{
    public class ArticolStire : IContinut
    {
        public string Titlu { get; set; }

        public void Publica()
        {
            Console.WriteLine("Se publică articolul de știri: " + Titlu);
        }
    }
}