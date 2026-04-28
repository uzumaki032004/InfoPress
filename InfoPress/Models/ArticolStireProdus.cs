using InfoPress.Interfaces;

namespace InfoPress.Models
{
    public class ArticolStireProdus : Content
    {
        public override void Publish()
        {
            // Logica de publicare
        }

        public override void AfiseazaArticol()
        {
            Console.WriteLine("Articol de stiri InfoPress");
        }
    }
}