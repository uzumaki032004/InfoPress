using InfoPress.Interfaces;
using InfoPress.Models;

namespace InfoPress.Fabrici
{
    public class FabricaSiteStiri : IFabricaSiteStiri
    {
        public IArticol CreeazaArticol()
        {
            return new ArticolStireProdus();
        }

        public IComentariu CreeazaComentariu()
        {
            return new ComentariuArticol();
        }
    }
}