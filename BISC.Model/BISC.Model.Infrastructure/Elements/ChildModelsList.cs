using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BISC.Model.Infrastructure.Elements
{
    public class ChildModelsList<T> : IEnumerable<T> where T:IModelElement
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

        //public bool Contains(T item)
        //{
        //    throw new NotImplementedException();
        //}

        //public void CopyTo(T[] array, int arrayIndex)
        //{
        //    array[arrayIndex]=(T)_parent.ChildModelElements.Where((element =>
        //        element.ElementName == _childElementName && element is T)).ToList()[arrayIndex];
        //}

        public bool Remove(T item)
        {
            var existing = _parent.ChildModelElements.FirstOrDefault((element =>
                element.Equals(item)));
            _parent.ChildModelElements.Remove(existing);
            return true;
        }

        public int Count
        {
            get
            {
                return _parent.ChildModelElements.Where((element =>
                    element.ElementName == _childElementName && element is T)).Count();
            }
        }


        //public bool IsReadOnly => false;

        //#endregion

        //#region Implementation of IList<T>

        public int IndexOf(T item)
        {
            var existing = _parent.ChildModelElements.FirstOrDefault((element =>
                element.Equals(item)));
            return _parent.ChildModelElements.Where((element =>
                element.ElementName == _childElementName && element is T)).ToList().IndexOf(existing);
        }

        //public void Insert(int index, T item)
        //{
        //    throw new NotImplementedException();
        //}

        //public void RemoveAt(int index)
        //{
        //    throw new NotImplementedException();
        //}

        public T this[int index]
        {
            get { return this.ToList()[index]; }
            set
            {
               var existing= _parent.ChildModelElements.Where((element =>
                    element.ElementName == _childElementName && element is T)).ToList()[index];
                _parent.ChildModelElements.Insert(index, value);
                _parent.ChildModelElements.Remove(existing);

            }
        }

        public void  AddRange(IEnumerable<T> collection)
        {
            foreach (var el in collection)
            {
                Add(el);
            }
        }

        #endregion

        public void Insert(int i, T item)
        {
            var existingAtI = _parent.ChildModelElements.Where((element =>
                element.ElementName == _childElementName && element is T)).ToList()[i];
          var index=  _parent.ChildModelElements.IndexOf(existingAtI);

            _parent.ChildModelElements.Insert(index, item);
        }

      
    }
}
