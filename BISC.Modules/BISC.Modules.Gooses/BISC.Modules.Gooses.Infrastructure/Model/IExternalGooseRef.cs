using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.Gooses.Infrastructure.Model
{
    public interface IExternalGooseRef:IModelElement
    {
        string IedName { get; set; }
        string LdInst { get; set; }
        string Prefix { get; set; }
        string LnClass { get; set; }
        string LnInst { get; set; }
        string DoName { get; set; }
        string DaName { get; set; }
    }
}