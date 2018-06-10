using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;
using BISC.Model.Iec61850Ed2.SclModelTemplates.Communication;
using BISC.Model.Iec61850Ed2.SclModelTemplates.Substation;

namespace BISC.Model.Iec61850Ed2.DataTypeTemplates.Base
{
    [XmlInclude(typeof (tDurationInMilliSec))]
    [XmlInclude(typeof (tDurationInSec))]
    [XmlInclude(typeof (tBitRateInMbPerSec))]
    [XmlInclude(typeof (tVoltage))]
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tValueWithUnit
    {
        private tSIUnitEnum unitField;
        private tUnitMultiplierEnum multiplierField;
        private decimal valueField;
        
        [Required]
        [XmlAttribute]
        [Category("ValueWithUnit"), Description("Единица измерения")]
        public tSIUnitEnum unit
        {
            get { return this.unitField; }
            set { this.unitField = value; }
        }

        [XmlAttribute]
        [Category("ValueWithUnit"), Description("Множитель значения"), DefaultValue(tUnitMultiplierEnum.Item)]
        public tUnitMultiplierEnum multiplier
        {
            get { return this.multiplierField; }
            set { this.multiplierField = value; }
        }

        [XmlText]
        [Category("ValueWithUnit"), Description("Числовое значение")]
        public decimal Value
        {
            get { return this.valueField; }
            set { this.valueField = value; }
        }
    }
}