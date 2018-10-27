using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Model.Model;

namespace IEC61850DeviceInteractions.Helpers
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


        public static IOptFields ReportOptionsFromBytes(this byte[] value)
        {
            IOptFields res = new OptFields();
            if (value == null || value.Length < 1) return res;
            if ((value[0] & OptFldsSeqNum) == OptFldsSeqNum) res.SeqNum=true;
            if ((value[0] & OptFldsTimeOfEntry) == OptFldsTimeOfEntry) res.TimeStamp=true;
            if ((value[0] & OptFldsReasonCode) == OptFldsReasonCode) res.ReasonCode = true;
            if ((value[0] & OptFldsDataSet) == OptFldsDataSet) res.DataSet = true;
            if ((value[0] & OptFldsDataReference) == OptFldsDataReference) res.DataRef = true;
            if ((value[0] & OptFldsOvfl) == OptFldsOvfl) res.BufOvfl = true;
            if ((value[0] & OptFldsEntryID) == OptFldsEntryID) res.EntryID = true;
            if (value.Length < 2) return res;
            if ((value[1] & OptFldsConfRev) == OptFldsConfRev) res.ConfigRef = true;
            if ((value[1] & OptFldsMoreSegments) == OptFldsMoreSegments) res.Segmentation = true;

            return res;
        }

        public static byte[] ReportOptionsToBytes(this IOptFields inp)
        {
            byte[] res = new byte[2];

            if (inp.SeqNum) res[0] |= OptFldsSeqNum;
            if (inp.TimeStamp) res[0] |= OptFldsTimeOfEntry;
            if (inp.ReasonCode) res[0] |= OptFldsReasonCode;
            if (inp.DataSet) res[0] |= OptFldsDataSet;
            if (inp.DataRef) res[0] |= OptFldsDataReference;
            if (inp.BufOvfl) res[0] |= OptFldsOvfl;
            if (inp.EntryID) res[0] |= OptFldsEntryID;
            if (inp.ConfigRef) res[1] |= OptFldsConfRev;
            if (inp.Segmentation) res[1] |= OptFldsMoreSegments;

            return res;
        }
        public static int ReportOptionsToInt(this IOptFields inp)
        {
            int res = 0;

            if (inp.SeqNum) res |= OptFldsSeqNumForFile;
            if (inp.TimeStamp) res |= OptFldsTimeOfEntryForFile;
            if (inp.ReasonCode) res |= OptFldsReasonCodeForFile;
            if (inp.DataSet) res |= OptFldsDataSetForFile;
            if (inp.DataRef) res |= OptFldsDataReferenceForFile;
            if (inp.BufOvfl) res |= OptFldsOvflForFile;
            if (inp.EntryID) res |= OptFldsEntryIDForFile;
            if (inp.ConfigRef) res |= OptFldsConfRevForFile;
            return res;
        }
        public static ITrgOps TriggerOptionsFromBytes(this byte[] value)
        {
            ITrgOps res = new TrgOps();
            if (value == null || value.Length < 1) return res;
            if ((value[0] & TrgOpsDataChange) == TrgOpsDataChange) res.Dchg=true;
            if ((value[0] & TrgOpsQualChange) == TrgOpsQualChange) res.Qchg = true;
            if ((value[0] & TrgOpsDataActual) == TrgOpsDataActual) res.Dupd = true;
            if ((value[0] & TrgOpsIntegrity) == TrgOpsIntegrity) res.Period = true;
            if ((value[0] & TrgOpsGI) == TrgOpsGI) res.Gi = true;
            return res;
        }

        public static byte[] TriggerOptionsToBytes(this ITrgOps inp)
        {
            byte[] res = new byte[1];

            if (inp.Dchg) res[0] |= TrgOpsDataChange;
            if (inp.Qchg) res[0] |= TrgOpsQualChange;
            if (inp.Dupd) res[0] |= TrgOpsDataActual;
            if (inp.Period) res[0] |= TrgOpsIntegrity;
            if (inp.Gi) res[0] |= TrgOpsGI;
            return res;
        }
        public static int TriggerOptionsToInt(this ITrgOps inp)
        {
            int res = 0;

            if (inp.Dchg) res |= TrgOpsDataChangeForFile;
            if (inp.Qchg) res |= TrgOpsQualChangeForFile;
            if (inp.Dupd) res |= TrgOpsDataActualForFile;
            if (inp.Period) res |= TrgOpsIntegrityForFile;
            if (inp.Gi) res |= TrgOpsGIForFile;
            return res;
        }
    }
}
