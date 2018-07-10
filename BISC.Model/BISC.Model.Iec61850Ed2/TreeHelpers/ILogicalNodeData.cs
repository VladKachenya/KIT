using BISC.Model.Infrastructure.Common;

namespace BISC.Model.Iec61850Ed2.TreeHelpers
{
    public interface ILogicalNodeData: INameableItem
    {
         string name { get; set; }
    }
}