using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Modules.InformationModel.Infrastucture.Elements;

namespace BISC.Modules.InformationModel.Model.Elements
{
   public class SettingControl:ModelElement,ISettingControl
    {
        public SettingControl()
        {
            ElementName = InfoModelKeys.ModelKeys.SettingControlKey;
        }

        #region Implementation of ISettingControl

        public int NumOfSGs { get; set; }
        public int ActSG { get; set; }

        #endregion
    }
}
