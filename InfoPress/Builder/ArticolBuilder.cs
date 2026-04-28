using InfoPress.Models;

namespace InfoPress.Builder
{
    public class ArticolBuilder : IArticolBuilder
    {
        private Articol _articol = new Articol();

        public IArticolBuilder SetTitlu(string titlu)
        {
            _articol.Titlu = titlu;
            return this;
        }

        public IArticolBuilder SetAutor(string autor)
        {
            _articol.Autor = autor;
            return this;
        }

        public IArticolBuilder SetContinut(string continut)
        {
            _articol.Continut = continut;
            return this;
        }

        public IArticolBuilder SetCategorie(string categorie)
        {
            _articol.Categorie = categorie;
            return this;
        }

        public IArticolBuilder SetImagine(string imagine)
        {
            _articol.Imagine = imagine;
            return this;
        }

        public Articol GetArticol()
        {
            Articol finalProduct = _articol;
            _articol = new Articol(); // Reset for next build
            return finalProduct;
        }
    }
}