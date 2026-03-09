using InfoPress.Interfaces;

namespace InfoPress.Models
{
    public class ArticolRecomandat : IContinut
    {
        public string Titlu { get; set; }

        public void Publica()
        {
            Console.WriteLine("Se publică articolul recomandat: " + Titlu);
        }
    }
}