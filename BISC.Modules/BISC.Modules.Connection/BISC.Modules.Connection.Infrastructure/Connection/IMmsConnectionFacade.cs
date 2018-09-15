using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;

namespace BISC.Modules.Connection.Infrastructure.Connection
{
    public interface IMmsConnectionFacade
    {

        Task<bool> TryOpenConnection(string ip);
        bool CheckConnection();
        void StopConnection();
        Task<OperationResult<List<string>>> IdentifyAsync();
        Task<OperationResult<List<string>>> GetLdListAsync();
        Task<OperationResult<List<string>>> GetListValiablesAsync(string ldInst,bool acceptCache);
        Task<OperationResult<MmsTypeDescription>> GetMmsTypeDescription(string ldName, string lnName);
        Task<OperationResult<List<string>>> GetListDataSetsAsync(string ldInst, bool acceptCache);
        Task<OperationResult<DataSetDto>> GetListDataSetInfoAsync(string ldInst,string lnName,string datasetName, bool acceptCache);

    }

    public class DataSetDto
    {
        public bool IsDynamic { get; set; }
        public List<string> FcdaList { get; set; }

    }

    public class MmsTypeDescription
    {
        public MmsTypeDescription()
        {
            Components=new List<MmsTypeDescription>();
        }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public bool IsStructure { get; set; }
        public bool IsArray { get; set; }
        public List<MmsTypeDescription> Components { get; }
        public tBasicTypeEnum? BasicType { get; set; }
    }


    public enum tBasicTypeEnum
    {
        //tPredefinedBasicTypeEnum
        BOOLEAN,
        INT8,
        INT16,
        INT24,
        INT32,
        INT128,
        INT8U,
        INT16U,
        INT24U,
        INT64,

        INT32U,
        FLOAT32,
        FLOAT64,
        Enum,
        Dbpos,
        Tcmd,
        Quality,
        Timestamp,
        VisString32,
        VisString64,
        VisString255,
        VisString129,
        Octet64,
        Struct,
        EntryTime,
        Unicode255,
        //tExtensionBasicTypeEnum is missing	
        VisString65,
        Check,
        Extension, // Used for custom types
        bit_string,
        ObjRef,
        OptFlds, TrgOps, EntryID, PhyComAddr,
        //unset value
        Unset
    }
}