using InfoPress.Interfaces;

namespace InfoPress.Models
{
    public class ArticolPrototype : IPrototype<ArticolPrototype>
    {
        public string Titlu { get; set; }

        public string Continut { get; set; }

        public string Autor { get; set; }

        public ArticolPrototype Clone()
        {
            return (ArticolPrototype)this.MemberwiseClone();
        }
    }
}