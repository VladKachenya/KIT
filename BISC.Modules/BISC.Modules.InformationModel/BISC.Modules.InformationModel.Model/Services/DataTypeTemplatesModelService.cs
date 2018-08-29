using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.LNodeType;

namespace BISC.Modules.InformationModel.Model.Services
{
   public class DataTypeTemplatesModelService: IDataTypeTemplatesModelService
    {
        public IDataTypeTemplates MergeDataTypeTemplates(IDataTypeTemplates dataTypeTemplates1, IDataTypeTemplates dataTypeTemplates2)
        {
            throw new NotImplementedException();
        }

        public void AddLnodeType(ILNodeType lNodeType, ISclModel sclModel)
        {
            throw new NotImplementedException();
        }

        public void AddDoType(IDoType doType, ISclModel sclModel)
        {
            throw new NotImplementedException();
        }
    }
}
