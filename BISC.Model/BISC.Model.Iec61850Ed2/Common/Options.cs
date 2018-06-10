using System;

namespace BISC.Model.Iec61850Ed2.Common
{
    [Flags]
    public enum TriggerOptions
    {
        NONE = 0,

        /** send report when value of data changed */
        DATA_CHANGED = 1,

        /** send report when quality of data changed */
        QUALITY_CHANGED = 2,

        /** send report when data or quality is updated */
        DATA_UPDATE = 4,

        /** periodic transmission of all data set values */
        INTEGRITY = 8,

        /** general interrogation (on client request) */
        GI = 16
    }

    [Flags]
    public enum ReportOptions
    {
        NONE = 0,
        SEQ_NUM = 1,
        TIME_STAMP = 2,
        REASON_FOR_INCLUSION = 4,
        DATA_SET = 8,
        DATA_REFERENCE = 16,
        BUFFER_OVERFLOW = 32,
        ENTRY_ID = 64,
        CONF_REV = 128,
        SEGMENTATION=256,
    }

}
