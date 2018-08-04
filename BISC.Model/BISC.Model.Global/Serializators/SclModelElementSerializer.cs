using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;

namespace BISC.Model.Global.Serializators
{
    public class SclModelElementSerializer : DefaultModelElementSerializer<ISclModel>
    {
        #region Overrides of DefaultModelElementSerializer<ISclModel>

        public override IModelElement GetConcreteObject()
        {
            return new SclModel();
        }

        #endregion
    }
}
