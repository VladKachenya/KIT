using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Infrastructure.Keys;
using BISC.Model.Infrastructure.Project;

namespace BISC.Model.Global.Model
{
   public class SclModel: DefaultModelElement,ISclModel
    {
        public SclModel()
        {
            ElementName = ModelKeys.SclModelKey;
        }
        
    }
}
