using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplate.TemplatesBase;

namespace BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DaType
{
    public interface IBda : IModelElement, IDataEntityWithType
    {
        string Name { get; set; }
        string BType { get; set; }
    }
}