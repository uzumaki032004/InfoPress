namespace InfoPress.Builder
{
    public class DirectorArticol
    {
        public void ConstruiesteArticolStire(IArticolBuilder builder)
        {
            builder.SetTitlu("Știre importantă");
            builder.SetAutor("Admin");
            builder.SetContinut("Acesta este conținutul articolului.");
            builder.SetCategorie("Actualitate");
            builder.SetImagine("stire.jpg");
        }
    }
}