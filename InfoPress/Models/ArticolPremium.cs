using InfoPress.Interfaces;

namespace InfoPress.Models
{
    public class ArticolPremium : Content
    {
        public override void Publish()
        {
            // Logica de publicare premium
        }

        public override void AfiseazaArticol()
        {
            Console.WriteLine("[PREMIUM] Articol de știri cu acces exclusiv.");
        }
    }
}
