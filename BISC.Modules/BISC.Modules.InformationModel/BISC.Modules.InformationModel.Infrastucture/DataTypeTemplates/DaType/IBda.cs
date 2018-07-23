using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DaType
{
    public interface IBda:IModelElement
    {
        string Name { get; set; }
        string BType { get; set; }
        string Type { get; set; }
    }
}