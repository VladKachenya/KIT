using BISC.Model.Infrastructure.Elements;

namespace BISC.Model.Infrastructure.Project.Communication
{
    public interface IAddressProperty:IModelElement
    {
        string Type { get; set; }
        string Value { get; set; }
    }
}