using BISC.Model.Infrastructure.Elements;

namespace BISC.Model.Infrastructure.Project.Communication
{
    public interface IDurationInMilliSec:IModelElement
    {
        string Unit { get; set; }
        string Multiplier { get; set; }
        int Value { get; set; }
    }
}