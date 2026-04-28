namespace InfoPress.Builder
{
    public class DirectorArticol
    {
        public void ConstruiesteArticolStire(IArticolBuilder builder)
        {
            builder.SetTitlu("Știre importantă")
                   .SetAutor("Admin")
                   .SetContinut("Acesta este conținutul articolului construit pas cu pas.")
                   .SetCategorie("Actualitate")
                   .SetImagine("stire.jpg");
        }

        public void ConstruiesteArticolSportiv(IArticolBuilder builder)
        {
            builder.SetTitlu("Rezultate meciuri")
                   .SetAutor("Redacția Sport")
                   .SetCategorie("Sport");
        }
    }
}