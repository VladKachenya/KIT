using System.Collections.Generic;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.InformationModel.Infrastucture.Elements
{
    public interface ILDevice:IModelElement
    {
        string Inst { get; set; }
        ILogicalNodeZero LogicalNodeZero{get; set; }
        List<ILogicalNode> LogicalNodes { get; }
    }
}