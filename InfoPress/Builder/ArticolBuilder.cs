using InfoPress.Models;

namespace InfoPress.Builder
{
    public class ArticolBuilder : IArticolBuilder
    {
        private Articol articol = new Articol();

        public void SetTitlu(string titlu)
        {
            articol.Titlu = titlu;
        }

        public void SetAutor(string autor)
        {
            articol.Autor = autor;
        }

        public void SetContinut(string continut)
        {
            articol.Continut = continut;
        }

        public void SetCategorie(string categorie)
        {
            articol.Categorie = categorie;
        }

        public void SetImagine(string imagine)
        {
            articol.Imagine = imagine;
        }

        public Articol GetArticol()
        {
            return articol;
        }
    }
}