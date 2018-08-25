using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.Device.Infrastructure.Model
{
    public interface IDevice:IModelElement
    {
        string Name { get; set; }
        string Ip { get; set; }
    }
}
