using InfoPress.Interfaces;

namespace InfoPress.Fabrici
{
    public interface IFabricaSiteStiri
    {
        IArticol CreeazaArticol();

        IComentariu CreeazaComentariu();
    }
}