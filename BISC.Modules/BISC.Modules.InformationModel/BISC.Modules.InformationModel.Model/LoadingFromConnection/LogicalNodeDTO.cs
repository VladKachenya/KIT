using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Iec61850Ed2.DataTypeTemplates;
using BISC.Modules.Connection.Infrastructure.Connection;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.LNodeType;

namespace BISC.Modules.InformationModel.Model.LoadingFromConnection
{
    public class LogicalNodeDTO
    {
        public string ShortName { get; set; }
        public string Path { get; set; }
        public string LDName { get; set; }
        public string IedName { get; set; }
        public List<string> LnDefinitions { get; set; }
        public ILNodeType RelatedLNodeType { get; set; }
        public CommonLogicalNode RelatedCommonLogicalNode { get; set; }

        public MmsTypeDescription DoiTypeDescription { get; set; }
        //   public GetVariableAccessAttributes_Response AccessAttributes { get; set; }

    }

    public class InnerDoData
    {
        public string Name;
        public bool IsStructure;
        public List<InnerDoData> MembersList = new List<InnerDoData>();
        public string Fc;
    }

    public class DoiDto
    {
        public string Name;
        public List<InnerDoData> MembersList = new List<InnerDoData>();
        public string FullPath;
        public MmsTypeDescription DoiTypeDescription;

    }

 

}
