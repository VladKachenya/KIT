using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Controls
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tSampledValueControl : tControlWithIEDName
    {
        private SmvOpts smvOptsField;
        private string smvIDField;
        private bool multicastField;
        private uint smpRateField;
        private uint nofASDUField;

        public tSampledValueControl()
        {
            this.multicastField = true;
        }

        [Category("SampledValueControl"), Description("Опции выборочных значений")]
        public SmvOpts SmvOpts
        {
            get { return this.smvOptsField; }
            set { this.smvOptsField = value; }
        }

        [Required]
        [XmlAttribute(DataType = "normalizedString")]
        [Category("SampledValueControl"),
        Description("Идентификатор SMV (блок управления многоадресными или одноадресными сообщениями)")]
        public string smvID
        {
            get { return this.smvIDField; }
            set { this.smvIDField = value; }
        }

        [XmlAttribute]
        [Category("SampledValueControl"), Description("Являются ли сообщения блока управления многоадресными(Тrue) или одноадресными(False")]
        public bool multicast
        {
            get { return this.multicastField; }
            set { this.multicastField = value; }
        }

        [Required]
        [XmlAttribute]
        [Category("SampledValueControl"), Description("Частота опроса")]
        public uint smpRate
        {
            get { return this.smpRateField; }
            set { this.smpRateField = value; }
        }

        [Required]
        [XmlAttribute]
        [Category("SampledValueControl"), Description("Номер ASDU (блок данных прикладных услуг)")]
        public uint nofASDU
        {
            get { return this.nofASDUField; }
            set { this.nofASDUField = value; }
        }
    }
}