using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplate.TemplatesBase;

namespace BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DaType
{
    public interface IDaType : IModelElement, ITemplateWithId
    {
        ChildModelsList<IBda> Bdas { get; }
    }
}