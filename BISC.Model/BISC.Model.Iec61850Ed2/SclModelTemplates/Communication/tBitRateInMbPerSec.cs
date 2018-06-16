using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.DataTypeTemplates.Base;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Communication
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tBitRateInMbPerSec : tValueWithUnit
    {
        public tBitRateInMbPerSec()
        {
            unit = tSIUnitEnum.bs;
            multiplier = tUnitMultiplierEnum.M;
        }
    }
}