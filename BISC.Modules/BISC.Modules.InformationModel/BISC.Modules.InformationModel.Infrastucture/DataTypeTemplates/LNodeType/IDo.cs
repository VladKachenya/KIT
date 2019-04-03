using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplate.TemplatesBase;

namespace BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.LNodeType
{
    public interface IDo : IModelElement, IDataEntityWithType
    {
        string Name { get; set; }
    }
}
