using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure;

namespace BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType
{
   public interface ISdo:IModelElement
    {
        string Name { get; set; }
        string Type { get; set; }
    }
}
