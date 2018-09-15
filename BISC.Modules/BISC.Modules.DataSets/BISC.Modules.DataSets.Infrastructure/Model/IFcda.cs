using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.DataSets.Infrastructure.Model
{ 
    public interface IFcda:IModelElement
    {
      
        string LdInst { get; set; }
        string Prefix { get; set; }
        string LnClass { get; set; }
        string LnInst { get; set; }
        string DoName { get; set; }
        string DaName { get; set; }
        string Fc { get; set; }


    }
}