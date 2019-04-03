using System.Collections.Generic;

namespace BISC.Modules.InformationModel.Infrastucture.DataTypeTemplate.TemplatesBase
{
    public interface IParentOfIds
    {
        List<ITemplateWithId> GetAllIds();
    }
}