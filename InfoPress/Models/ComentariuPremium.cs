using InfoPress.Interfaces;

namespace InfoPress.Models
{
    public class ComentariuPremium : IComentariu
    {
        public void AfiseazaComentariu()
        {
            Console.WriteLine("[PREMIUM] Comentariu evidențiat de un abonat Gold.");
        }
    }
}
