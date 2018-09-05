using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Keys;
using BISC.Model.Infrastructure.Project;

namespace BISC.Model.Global.Project
{
   public class BiscProject:ModelElement,IBiscProject
    {
        
        public BiscProject()
        {
            ElementName = ModelKeys.BiscProjectKey;
        }
       
        #region Implementation of IBiscProject

        public ISclModel MainSclModel { get; set; }

        #endregion
    }
}
