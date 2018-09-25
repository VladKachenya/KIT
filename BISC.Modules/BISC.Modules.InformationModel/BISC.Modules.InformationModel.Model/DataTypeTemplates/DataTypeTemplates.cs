using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture;
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
            ElementName = InfoModelKeys.DataTypeTemplateKeys.DataTypeTemplatesModelItemKey;
        }

        #region Implementation of IDataTypeTemplates

        public ChildModelsList<ILNodeType> LNodeTypes =>new ChildModelsList<ILNodeType>(this,InfoModelKeys.DataTypeTemplateKeys.LNodeTypeModelItemKey);
        public ChildModelsList<IDoType> DoTypes => new ChildModelsList<IDoType>(this, InfoModelKeys.DataTypeTemplateKeys.DOTypeModelItemKey);
        public ChildModelsList<IDaType> DaTypes => new ChildModelsList<IDaType>(this, InfoModelKeys.DataTypeTemplateKeys.DATypeModelItemKey);
        public ChildModelsList<IEnumType> EnumTypes => new ChildModelsList<IEnumType>(this, InfoModelKeys.DataTypeTemplateKeys.EnumTypeModelItemKey);

        #endregion
    }
}
