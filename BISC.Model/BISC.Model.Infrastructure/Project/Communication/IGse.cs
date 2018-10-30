using BISC.Model.Infrastructure.Elements;

namespace BISC.Model.Infrastructure.Project.Communication
{
    public interface IGse : IModelElement
    {
        string VlanId { get; set; }
        string MacAddress { get; set; }
        int VlanPriority { get; set; }
        string AppId { get; set; }
        string AppIdDec { get; set; }

        string LdInst { get; set; }
        string CbName { get; set; }

        ChildModelProperty<IDurationInMilliSec> MinTime { get; }
        ChildModelProperty<IDurationInMilliSec> MaxTime { get;  }
        ChildModelProperty<ISclAddress> SclAddress { get; }
    }
}