namespace InfoPress.Composite
{
    public class CategorieCompusa : ICategorie
    {
        private List<ICategorie> subcategorii = new List<ICategorie>();

        private string nume;

        public CategorieCompusa(string nume)
        {
            this.nume = nume;
        }

        public void Adauga(ICategorie categorie)
        {
            subcategorii.Add(categorie);
        }

        public void Afiseaza()
        {
            Console.WriteLine("Categorie principala: " + nume);

            foreach (var categorie in subcategorii)
            {
                categorie.Afiseaza();
            }
        }
    }
}