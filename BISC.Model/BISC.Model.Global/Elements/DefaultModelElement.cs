using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Modularity;

namespace BISC.Model.Global.Elements
{
   public class DefaultModelElement:IModelElement
    {
        public DefaultModelElement()
        {
            ChildElements=new List<IModelElement>();
            ModelElementAttributes=new List<ModelElementAttribute>();
        }
        public string ElementName { get; set; }
        public List<IModelElement> ChildElements { get; }
        public List<ModelElementAttribute> ModelElementAttributes { get; }
    }


    public class ModelElementAttribute
    {
        public ModelElementAttribute(string attributeName, string attributeValue)
        {
            AttributeName = attributeName;
            AttributeValue = attributeValue;
        }
        public string AttributeName { get; }
        public string AttributeValue { get; }
    }
}
