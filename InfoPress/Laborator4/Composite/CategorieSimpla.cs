namespace InfoPress.Composite
{
    public class CategorieSimpla : ICategorie
    {
        private string nume;

        public CategorieSimpla(string nume)
        {
            this.nume = nume;
        }

        public void Afiseaza()
        {
            Console.WriteLine("Categorie: " + nume);
        }
    }
}