using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Model.Infrastructure.Elements
{
    public class ChildModelProperty<T> where T : IModelElement
    {
        private readonly IModelElement _parentModelElement;
        private readonly string _elementName;

        public ChildModelProperty(IModelElement parentModelElement, string elementName)
        {
            _parentModelElement = parentModelElement;
            _elementName = elementName;
        }

        public T Value
        {
            get
            {
                var val= _parentModelElement.ChildModelElements.FirstOrDefault((element =>
                    element.ElementName == _elementName && element is T));
                if (val != null) return (T) val;
                return default(T);
            }

            set
            {
                var val = _parentModelElement.ChildModelElements.FirstOrDefault((element =>
                    element.ElementName == _elementName && element is T));
                if (val != null)
                {
                    _parentModelElement.ChildModelElements.Remove(val);
                }
                _parentModelElement.ChildModelElements.Add(value);
            }
        }


    }
}