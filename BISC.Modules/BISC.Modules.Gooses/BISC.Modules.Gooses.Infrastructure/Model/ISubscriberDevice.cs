using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.Gooses.Infrastructure.Model
{
    public interface ISubscriberDevice:IModelElement
    {
        string LdInst { get; set; }
        string ApRef { get; set; }
        string LnClass { get; set; }
        string DeviceName { get; set; }

    }
}