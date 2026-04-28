using InfoPress.Interfaces;
using System.Collections.Generic;

namespace InfoPress.Models
{
    public class ArticolPrototype : IPrototype<ArticolPrototype>
    {
        public string Titlu { get; set; }
        public string Continut { get; set; }
        public string Autor { get; set; }
        public List<string> Etichete { get; set; } = new List<string>();

        // Shallow Copy
        public ArticolPrototype Clone()
        {
            return (ArticolPrototype)this.MemberwiseClone();
        }

        // Deep Copy
        public ArticolPrototype DeepClone()
        {
            ArticolPrototype clone = (ArticolPrototype)this.MemberwiseClone();
            clone.Etichete = new List<string>(this.Etichete); // Clona listei pentru a nu partaja referința
            return clone;
        }
    }
}