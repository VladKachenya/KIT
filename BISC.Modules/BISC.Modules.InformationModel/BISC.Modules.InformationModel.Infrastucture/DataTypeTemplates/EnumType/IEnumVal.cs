using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.EnumType
{
    public interface IEnumVal:IModelElement
    {
        int Ord { get; set; }
        string Value { get; set; }
    }
}