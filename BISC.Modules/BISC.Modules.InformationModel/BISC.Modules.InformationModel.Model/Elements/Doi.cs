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
    public  class Doi:ModelElement,IDoi
    {
        public Doi()
        {
            SdiCollection=new List<ISdi>();
            DaiCollection=new List<IDai>();
            ElementName = InfoModelKeys.ModelKeys.DoiKey;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public List<ISdi> SdiCollection { get; }
        public List<IDai> DaiCollection { get; }
    }
}
