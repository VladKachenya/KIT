using System.Collections.Generic;

namespace BISC.Modules.InformationModel.Infrastucture.DataTypeTemplate.TemplatesBase
{
    public interface ITemplateWithId
    {
        string Id { get; set; }
        List<IDataEntityWithType> GetAllITypes();
    }
}