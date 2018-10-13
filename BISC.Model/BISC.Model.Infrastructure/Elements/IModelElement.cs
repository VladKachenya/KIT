using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace BISC.Model.Infrastructure.Elements
{
    public interface IModelElement : IModelElementComparable
    {
       
        string Namespace { get; set; }
        List<IModelElement> ChildModelElements { get; }
        List<XAttribute> ModelElementAttributes { get; }
        string ElementName { get; }
        IModelElement ParentModelElement { get; set; }
    }
}