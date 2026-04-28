using InfoPress.Models;

namespace InfoPress.Builder
{
    public interface IArticolBuilder
    {
        IArticolBuilder SetTitlu(string titlu);
        IArticolBuilder SetAutor(string autor);
        IArticolBuilder SetContinut(string continut);
        IArticolBuilder SetCategorie(string categorie);
        IArticolBuilder SetImagine(string imagine);
        Articol GetArticol();
    }
}