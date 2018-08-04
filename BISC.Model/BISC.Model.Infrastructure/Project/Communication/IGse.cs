using BISC.Model.Infrastructure.Elements;

namespace BISC.Model.Infrastructure.Project.Communication
{
    public interface IGse:IModelElement
    {
        string VlanId { get; set; }
        string MacAddress { get; set; }
        int VlanPriority { get; set; }
        string AppId { get; set; }
        string LdInst { get; set; }
        string CbName { get; set; }

        IDurationInMilliSec MinTime { get; set; }
        IDurationInMilliSec MaxTime { get; set; }
        ISclAddress SclAddress { get; set; }
    }
}