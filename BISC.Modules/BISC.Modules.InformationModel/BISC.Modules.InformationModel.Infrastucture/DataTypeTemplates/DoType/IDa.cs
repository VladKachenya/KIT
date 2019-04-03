using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplate.TemplatesBase;

namespace BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType
{
    public interface IDa : IModelElement, IDataEntityWithType
    {
        string Name { get; set; }
        string BType { get; set; }
        string Fc { get; set; }

    }
}