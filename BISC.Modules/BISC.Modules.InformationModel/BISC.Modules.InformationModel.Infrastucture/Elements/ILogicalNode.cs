using System.Collections.Generic;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.InformationModel.Infrastucture.Elements
{
    public interface ILogicalNode:IModelElement
    {
         string Name
         {
             get;
         }
        string LnClass { get; set; }
        string Inst { get; set; }
        string LnType { get; set; }
        ChildModelsList<IDoi> DoiCollection { get;  }
        string Prefix { get; set; }
    }
}