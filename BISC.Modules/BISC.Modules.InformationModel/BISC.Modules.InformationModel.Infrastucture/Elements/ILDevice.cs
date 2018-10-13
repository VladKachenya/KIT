using System.Collections.Generic;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.InformationModel.Infrastucture.Elements
{
    public interface ILDevice:IModelElement
    {
        string Inst { get; set; }
        ChildModelProperty<ILogicalNodeZero> LogicalNodeZero{get; }
        ChildModelsList<ILogicalNode> LogicalNodes { get; }
        List<ILogicalNode> AlLogicalNodes { get; }
    }
}