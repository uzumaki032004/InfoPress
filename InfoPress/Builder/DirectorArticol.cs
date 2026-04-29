namespace InfoPress.Builder
{
    public class DirectorArticol
    {
        public void ConstruiesteArticolStire(IArticolBuilder builder)
        {
            builder.SetTitlu("Știre de ultimă oră")
                   .SetAutor("Redacția InfoPress")
                   .SetContinut("Introducere știre... [Conținut complet]")
                   .SetCategorie("Actualitate")
                   .SetImagine("/images/stire-default.jpg");
        }

        public void ConstruiesteArticolSportiv(IArticolBuilder builder)
        {
            builder.SetTitlu("Rezultate Sportive")
                   .SetAutor("Redacția Sport")
                   .SetContinut("Meciul de ieri a fost... [Conținut sport]")
                   .SetCategorie("Sport")
                   .SetImagine("/images/sport-default.jpg");
        }

        public void ConstruiesteArticolEditorial(IArticolBuilder builder)
        {
            builder.SetTitlu("Opinie: Viitorul Presei")
                   .SetAutor("Editor Șef")
                   .SetContinut("În contextul actual... [Conținut editorial]")
                   .SetCategorie("Editorial")
                   .SetImagine("/images/editorial-default.jpg");
        }
    }
}