using InfoPress.Composite;
using System.Collections.Generic;

namespace InfoPress.Helpers
{
    public static class CategoryManager
    {
        public static CategorieCompusa GetMenuHierarchy()
        {
            var menu = new CategorieCompusa("Meniu Principal");
            
            var stiri = new CategorieCompusa("Știri");
            stiri.Adauga(new CategorieSimpla("Politica"));
            stiri.Adauga(new CategorieSimpla("Economie"));
            stiri.Adauga(new CategorieSimpla("Tehnologie"));
            
            menu.Adauga(stiri);
            
            return menu;
        }

        // Helper to flatten for the simple navbar in _Layout
        public static List<string> GetFlatCategories()
        {
            return new List<string> { "Politica", "Economie", "Tehnologie" };
        }
    }
}
