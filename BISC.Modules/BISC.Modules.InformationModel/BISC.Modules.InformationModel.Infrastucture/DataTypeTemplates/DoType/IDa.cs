using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType
{
    public interface IDa : IModelElement
    {
        string Name { get; set; }
        string BType { get; set; }
        string Fc { get; set; }
        string Type { get; set; }

    }
}