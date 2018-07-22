using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.LNodeType
{
    public interface IDo:IModelElement
    {
        string Name { get; set; }
        string Type { get; set; }

    }
}
