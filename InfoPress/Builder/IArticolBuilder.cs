using InfoPress.Models;

namespace InfoPress.Builder
{
    public interface IArticolBuilder
    {
        void SetTitlu(string titlu);

        void SetAutor(string autor);

        void SetContinut(string continut);

        void SetCategorie(string categorie);

        void SetImagine(string imagine);

        Articol GetArticol();
    }
}