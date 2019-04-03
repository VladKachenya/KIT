using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplate.TemplatesBase;

namespace BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType
{
   public interface ISdo:IModelElement, IDataEntityWithType
    {
        string Name { get; set; }
    }
}
