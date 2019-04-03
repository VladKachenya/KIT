using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplate.TemplatesBase;

namespace BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.LNodeType
{
    public interface ILNodeType : IModelElement, ITemplateWithId
    {
        string LnClass { get; set; }

        ChildModelsList<IDo> DoList { get; }

    }
}