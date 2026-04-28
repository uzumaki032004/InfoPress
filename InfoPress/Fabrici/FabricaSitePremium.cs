using InfoPress.Interfaces;
using InfoPress.Models;

namespace InfoPress.Fabrici
{
    public class FabricaSitePremium : IFabricaSiteStiri
    {
        public IArticol CreeazaArticol()
        {
            return new ArticolPremium();
        }

        public IComentariu CreeazaComentariu()
        {
            return new ComentariuPremium();
        }
    }
}
