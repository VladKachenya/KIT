using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;

namespace BISC.Model.Global.Project
{
   public class BiscProject:IBiscProject
    {
        public BiscProject(ISclModel sclModel)
        {
            MainSclModel = sclModel;
        }


        #region Implementation of IBiscProject

        public ISclModel MainSclModel { get; set; }

        #endregion
    }
}
