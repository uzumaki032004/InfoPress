using InfoPress.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace InfoPress.Strategy
{
    public interface ISortingStrategy
    {
        List<IArticol> Sort(List<IArticol> articles);
    }

    public class SortByDateStrategy : ISortingStrategy
    {
        public List<IArticol> Sort(List<IArticol> articles)
        {
            return articles.OrderByDescending(a => a.CreatedDate).ToList();
        }
    }

    public class SortByTitleStrategy : ISortingStrategy
    {
        public List<IArticol> Sort(List<IArticol> articles)
        {
            return articles.OrderBy(a => a.Title).ToList();
        }
    }
}
