using BISC.Model.Iec61850Ed2.Common;

namespace BISC.Model.Iec61850Ed2.TreeHelpers
{
    public static class OptionsEnumExtensions
    {


        //public const byte OptFldsReservedForFile = 0x80;
        public const byte OptFldsSeqNumForFile = 1;
        public const byte OptFldsTimeOfEntryForFile = 2;
        public const byte OptFldsReasonCodeForFile = 4;
        public const byte OptFldsDataSetForFile = 8;
        public const byte OptFldsDataReferenceForFile = 16;
        public const byte OptFldsOvflForFile = 32;
        public const byte OptFldsEntryIDForFile = 64;
        public const byte OptFldsConfRevForFile = 128;
       // public const byte OptFldsMoreSegmentsForFile = 0x40; // bit "10" in MMS interpretation

        // TrgOps - report Trigger Options
   //     public const byte TrgOpsReservedForFile = 0x80;
        public const byte TrgOpsDataChangeForFile = 1;
        public const byte TrgOpsQualChangeForFile = 2;
        public const byte TrgOpsDataActualForFile = 4;
        public const byte TrgOpsIntegrityForFile = 8;
        public const byte TrgOpsGIForFile = 16;




        // Protocol IEC6850 - definitions
        // OptFlds - report Optional Fields
        // 1st Byte

        public const byte OptFldsReserved = 0x80;
        public const byte OptFldsSeqNum = 0x40;
        public const byte OptFldsTimeOfEntry = 0x20;
        public const byte OptFldsReasonCode = 0x10;
        public const byte OptFldsDataSet = 0x08;
        public const byte OptFldsDataReference = 0x04;
        public const byte OptFldsOvfl = 0x02;
        public const byte OptFldsEntryID = 0x01;
        public const byte OptFldsConfRev = 0x80;
        public const byte OptFldsMoreSegments = 0x40; // bit "10" in MMS interpretation

        // TrgOps - report Trigger Options
        public const byte TrgOpsReserved = 0x80;
        public const byte TrgOpsDataChange = 0x40;
        public const byte TrgOpsQualChange = 0x20;
        public const byte TrgOpsDataActual = 0x10;
        public const byte TrgOpsIntegrity = 0x08;
        public const byte TrgOpsGI = 0x04; 

        // DatQual - Data Quality Codes
        public const byte DatQualValidity0 = 0x80; // bit "0" in MMS interpretation
        public const byte DatQualValidity1 = 0x40;
        public const byte DatQualOverflow = 0x20;
        public const byte DatQualOutOfRange = 0x10;
        public const byte DatQualBadReference = 0x08;
        public const byte DatQualOscillatory = 0x04;
        public const byte DatQualFailure = 0x02;
        public const byte DatQualOldData = 0x01;
        // 2nd Byte
        public const byte DatQualInconsistent = 0x80;
        public const byte DatQualInaccurate = 0x40;
        public const byte DatQualSource = 0x20;
        public const byte DatQualTest = 0x10;
        public const byte DatQualOperatorBlocked = 0x08; // bit "12" in MMS interpretation

        // TimQual - Time Quality Codes
        public const byte TimQualExtraSeconds = 0x80; // bit "0" in MMS interpretation
        public const byte TimQualTimeBaseErr = 0x40;
        public const byte TimQualNotSynchronized = 0x20; // bit "2". Bits 3-7=time precision encoding


        public static ReportOptions ReportOptionsFromBytes(this byte[] value)
        {
            ReportOptions res = ReportOptions.NONE;
            if (value == null || value.Length < 1) return res;
            if ((value[0] & OptFldsSeqNum) == OptFldsSeqNum) res |= ReportOptions.SEQ_NUM;
            if ((value[0] & OptFldsTimeOfEntry) == OptFldsTimeOfEntry) res |= ReportOptions.TIME_STAMP;
            if ((value[0] & OptFldsReasonCode) == OptFldsReasonCode) res |= ReportOptions.REASON_FOR_INCLUSION;
            if ((value[0] & OptFldsDataSet) == OptFldsDataSet) res |= ReportOptions.DATA_SET;
            if ((value[0] & OptFldsDataReference) == OptFldsDataReference) res |= ReportOptions.DATA_REFERENCE;
            if ((value[0] & OptFldsOvfl) == OptFldsOvfl) res |= ReportOptions.BUFFER_OVERFLOW;
            if ((value[0] & OptFldsEntryID) == OptFldsEntryID) res |= ReportOptions.ENTRY_ID;
            if (value.Length < 2) return res;
            if ((value[1] & OptFldsConfRev) == OptFldsConfRev) res |= ReportOptions.CONF_REV;
            if ((value[1] & OptFldsMoreSegments) == OptFldsMoreSegments) res |= ReportOptions.SEGMENTATION;

            return res;
        }

        public static byte[] ReportOptionsToBytes(this ReportOptions inp)
        {
            byte[] res = new byte[2];

            if ((inp & ReportOptions.SEQ_NUM) == ReportOptions.SEQ_NUM) res[0] |= OptFldsSeqNum;
            if ((inp & ReportOptions.TIME_STAMP) == ReportOptions.TIME_STAMP) res[0] |= OptFldsTimeOfEntry;
            if ((inp & ReportOptions.REASON_FOR_INCLUSION) == ReportOptions.REASON_FOR_INCLUSION) res[0] |= OptFldsReasonCode;
            if ((inp & ReportOptions.DATA_SET) == ReportOptions.DATA_SET) res[0] |= OptFldsDataSet;
            if ((inp & ReportOptions.DATA_REFERENCE) == ReportOptions.DATA_REFERENCE) res[0] |= OptFldsDataReference;
            if ((inp & ReportOptions.BUFFER_OVERFLOW) == ReportOptions.BUFFER_OVERFLOW) res[0] |= OptFldsOvfl;
            if ((inp & ReportOptions.ENTRY_ID) == ReportOptions.ENTRY_ID) res[0] |= OptFldsEntryID;
            if ((inp & ReportOptions.CONF_REV) == ReportOptions.CONF_REV) res[1] |= OptFldsConfRev;
            if ((inp & ReportOptions.SEGMENTATION) == ReportOptions.SEGMENTATION) res[1] |= OptFldsMoreSegments;

            return res;
        }
        public static int ReportOptionsToInt(this ReportOptions inp)
        {
            int res = 0;

            if ((inp & ReportOptions.SEQ_NUM) == ReportOptions.SEQ_NUM) res |= OptFldsSeqNumForFile;
            if ((inp & ReportOptions.TIME_STAMP) == ReportOptions.TIME_STAMP) res |= OptFldsTimeOfEntryForFile;
            if ((inp & ReportOptions.REASON_FOR_INCLUSION) == ReportOptions.REASON_FOR_INCLUSION) res |= OptFldsReasonCodeForFile;
            if ((inp & ReportOptions.DATA_SET) == ReportOptions.DATA_SET) res |= OptFldsDataSetForFile;
            if ((inp & ReportOptions.DATA_REFERENCE) == ReportOptions.DATA_REFERENCE) res |= OptFldsDataReferenceForFile;
            if ((inp & ReportOptions.BUFFER_OVERFLOW) == ReportOptions.BUFFER_OVERFLOW) res |= OptFldsOvflForFile;
            if ((inp & ReportOptions.ENTRY_ID) == ReportOptions.ENTRY_ID) res |= OptFldsEntryIDForFile;
            if ((inp & ReportOptions.CONF_REV) == ReportOptions.CONF_REV) res|= OptFldsConfRevForFile;

            return res;
        }
        public static TriggerOptions TriggerOptionsFromBytes(this byte[] value)
        {
            TriggerOptions res = TriggerOptions.NONE;
            if (value == null || value.Length < 1) return res;
            if ((value[0] & TrgOpsDataChange) == TrgOpsDataChange) res |= TriggerOptions.DATA_CHANGED;
            if ((value[0] & TrgOpsQualChange) == TrgOpsQualChange) res |= TriggerOptions.QUALITY_CHANGED;
            if ((value[0] & TrgOpsDataActual) == TrgOpsDataActual) res |= TriggerOptions.DATA_UPDATE;
            if ((value[0] & TrgOpsIntegrity) == TrgOpsIntegrity) res |= TriggerOptions.INTEGRITY;
            if ((value[0] & TrgOpsGI) == TrgOpsGI) res |= TriggerOptions.GI;
            return res;
        }

        public static byte[] TriggerOptionsToBytes(this TriggerOptions inp)
        {
            byte[] res = new byte[1];

            if ((inp & TriggerOptions.DATA_CHANGED) == TriggerOptions.DATA_CHANGED) res[0] |= TrgOpsDataChange;
            if ((inp & TriggerOptions.QUALITY_CHANGED) == TriggerOptions.QUALITY_CHANGED) res[0] |= TrgOpsQualChange;
            if ((inp & TriggerOptions.DATA_UPDATE) == TriggerOptions.DATA_UPDATE) res[0] |= TrgOpsDataActual;
            if ((inp & TriggerOptions.INTEGRITY) == TriggerOptions.INTEGRITY) res[0] |= TrgOpsIntegrity;
            if ((inp & TriggerOptions.GI) == TriggerOptions.GI) res[0] |= TrgOpsGI;
            return res;
        }
        public static int TriggerOptionsToInt(this TriggerOptions inp)
        {
            int res = 0;

            if ((inp & TriggerOptions.DATA_CHANGED) == TriggerOptions.DATA_CHANGED) res |= TrgOpsDataChangeForFile;
            if ((inp & TriggerOptions.QUALITY_CHANGED) == TriggerOptions.QUALITY_CHANGED) res |= TrgOpsQualChangeForFile;
            if ((inp & TriggerOptions.DATA_UPDATE) == TriggerOptions.DATA_UPDATE) res |= TrgOpsDataActualForFile;
            if ((inp & TriggerOptions.INTEGRITY) == TriggerOptions.INTEGRITY) res |= TrgOpsIntegrityForFile;
            if ((inp & TriggerOptions.GI) == TriggerOptions.GI) res |= TrgOpsGIForFile;
            return res;
        }
    }
}
