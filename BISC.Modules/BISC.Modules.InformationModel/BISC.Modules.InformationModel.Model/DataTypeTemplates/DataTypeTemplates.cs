using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DaType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.EnumType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.LNodeType;

namespace BISC.Modules.InformationModel.Model.DataTypeTemplates
{
   public class DataTypeTemplates:ModelElement,IDataTypeTemplates
    {
        public DataTypeTemplates()
        {
            LNodeTypes=new List<ILNodeType>();
            DoTypes=new List<IDoType>();
            DaTypes=new List<IDaType>();
            EnumTypes=new List<IEnumType>();
        }

        #region Implementation of IDataTypeTemplates

        public List<ILNodeType> LNodeTypes { get; }
        public List<IDoType> DoTypes { get; }
        public List<IDaType> DaTypes { get; }
        public List<IEnumType> EnumTypes { get; }

        #endregion
    }
}
