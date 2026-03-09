using InfoPress.Interfaces;

namespace InfoPress.Models
{
    public class ArticolStireProdus : IArticol
    {
        public void AfiseazaArticol()
        {
            Console.WriteLine("Articol de stiri InfoPress");
        }
    }
}