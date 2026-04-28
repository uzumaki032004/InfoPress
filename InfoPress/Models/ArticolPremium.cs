using InfoPress.Interfaces;

namespace InfoPress.Models
{
    public class ArticolPremium : IArticol
    {
        public void AfiseazaArticol()
        {
            Console.WriteLine("[PREMIUM] Articol de știri cu acces exclusiv.");
        }
    }
}
