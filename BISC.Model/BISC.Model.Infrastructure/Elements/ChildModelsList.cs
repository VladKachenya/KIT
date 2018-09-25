using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BISC.Model.Infrastructure.Elements
{
    public class ChildModelsList<T> : IList<T> where T:IModelElement
    {
        private readonly IModelElement _parent;
        private readonly string _childElementName;

        public ChildModelsList(IModelElement parent, string childElementName)
        {
            _parent = parent;
            _childElementName = childElementName;
        }


        #region Implementation of IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            return _parent.ChildModelElements.Where((element =>
                element.ElementName == _childElementName && element is T)).Cast<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<T>

        public void Add(T item)
        {
            var lastIndex = _parent.ChildModelElements.Where((element =>
                element.ElementName == _childElementName && element is T)).Count();
            _parent.ChildModelElements.Insert(lastIndex,item);
        }

        public void Clear()
        {
          
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get
            {
                return 0;

            }
        }

        public bool IsReadOnly { get; }

        #endregion

        #region Implementation of IList<T>

        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public T this[int index]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public void  AddRange(IEnumerable<T> collection)
        {
            foreach (var el in collection)
            {
                Add(el);
            }
        }

        #endregion
    }
}
