using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Modules.Connection.Infrastructure.Connection.Dto;
using BISC.Modules.Reports.Infrastructure.Model;

namespace BISC.Modules.Connection.Infrastructure.Connection
{
    public interface IMmsConnectionFacade
    {
        Task<bool> TryOpenConnection(string ip);
        bool CheckConnection();
        void StopConnection();
        Task<OperationResult<List<string>>> IdentifyAsync();
        Task<OperationResult<List<string>>> GetLdListAsync();
        Task<OperationResult<List<string>>> GetListValiablesAsync(string ldInst, bool acceptCache);
        Task<OperationResult<MmsTypeDescription>> GetMmsTypeDescription(string ldName, string lnName, bool acceptCache);
        Task<OperationResult<MmsTypeDescription>> GetMmsTypeDescriptionByFcs(string ldName, string lnName, bool acceptCache, string[] lnFcs);
        Task<OperationResult<List<string>>> GetListDataSetsAsync(string ldInst, bool acceptCache);

        Task<OperationResult<DataSetDto>> GetListDataSetInfoAsync(string ldInst, string lnName, string datasetName,
            bool acceptCache);

        Task<OperationResult<List<GooseDto>>> GetListGoosesAsync(string fullLdPath, string lnName, string deviceName);

        Task<OperationResult<List<IReportControl>>> GetListReportsAsync(string fullLdPath, string lnName,
            string deviceName, string reportType);

        Task<OperationResult> WriteReportDataAsync(string ldFullPath, string rptId, string itemValueName,
            object valueToSave);

        Task<OperationResult> DeleteDataSet(string ln, string ld, string ied, string name);
        Task<OperationResult> AddDataSet(string ln, string ld, string ied, string nameDataSet, List<FcdaDto> fcdaDtos);

        Task<SettingsControlDto> GetSettingsControl(MmsTypeDescription lnMmsTypeDescription, string fc, string iedName,
            string lnName, string ldName);

        Task<bool> SetSettingsControl(string fc, string iedName, string lnName,
            string ldName, string newVal);

        Task<bool> WriteDaiValueAsync(string fc, string iedName, string lnName, string ldName, List<string> daPath, string newVal);


        Task<OperationResult<ValueDescription>> ReadValuesAsync(string fc, string iedName, string lnName, string ldName, List<string> customItemPathParts = null);
    }

    public class FcdaDto
    {
        public string Ln;
        public string Ld;
        public string Ied;
        public string Fc;
        public string[] DaDoPathParts;

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


    public class ValueDescription
    {
        public ValueDescription()
        {
            Components = new List<ValueDescription>();

        }
        public string Value { get; set; }
        public string TypeName { get; set; }
        public bool IsStructure { get; set; }
        public List<ValueDescription> Components { get; set; }
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