using System.Collections.Generic;

namespace InfoPress.Flyweight
{
    // Flyweight interface
    public interface ICategoryFlyweight
    {
        void Display(string articleTitle);
    }

    // Concrete Flyweight
    public class CategoryFlyweight : ICategoryFlyweight
    {
        private string _name; // Intrinsic state (shared)

        public CategoryFlyweight(string name)
        {
            _name = name;
        }

        public void Display(string articleTitle)
        {
            // articleTitle is Extrinsic state (not shared)
            System.Console.WriteLine($"Articolul '{articleTitle}' aparține categoriei partajate: {_name}");
        }
    }

    // Flyweight Factory
    public class CategoryFlyweightFactory
    {
        private Dictionary<string, ICategoryFlyweight> _categories = new Dictionary<string, ICategoryFlyweight>();

        public ICategoryFlyweight GetCategory(string name)
        {
            if (!_categories.ContainsKey(name))
            {
                _categories[name] = new CategoryFlyweight(name);
                System.Console.WriteLine($"[Flyweight] Creată instanță nouă pentru categoria: {name}");
            }
            return _categories[name];
        }

        public int GetCount() => _categories.Count;
    }
}
