using InfoPress.Interfaces;
using System.Collections.Generic;

namespace InfoPress.Iterator
{
    public interface IIterator<T>
    {
        T First();
        T Next();
        bool IsDone();
        T CurrentItem();
    }

    public class NewsIterator : IIterator<IArticol>
    {
        private List<IArticol> _collection;
        private int _current = 0;

        public NewsIterator(List<IArticol> collection) => _collection = collection;

        public IArticol First()
        {
            _current = 0;
            return _collection.Count > 0 ? _collection[0] : null;
        }

        public IArticol Next()
        {
            _current++;
            return !IsDone() ? _collection[_current] : null;
        }

        public bool IsDone() => _current >= _collection.Count;

        public IArticol CurrentItem() => _collection[_current];
    }
}
