using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Services
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tSMVSettings : tServiceSettings
    {
        private decimal[] smpRateField;
        private tServiceSettingsEnum svIDField;
        private tServiceSettingsEnum optFieldsField;
        private tServiceSettingsEnum smpRateField1;
        
        [XmlElement]
        [Category("SMVSettings"), Description("Скорости")]
        public decimal[] SmpRate
        {
            get { return this.smpRateField; }
            set { this.smpRateField = value; }
        }

        [XmlAttribute]
        [Category("SMVSettings"), Description("Идентификатор выборочного значения"), DefaultValue(tServiceSettingsEnum.Fix)]
        public tServiceSettingsEnum svID
        {
            get { return this.svIDField; }
            set { this.svIDField = value; }
        }

        [XmlAttribute]
        [Category("SMVSettings"), Description("Дополнительные поля для включения в сообщение о выборочных значениях"), DefaultValue(tServiceSettingsEnum.Fix)]
        public tServiceSettingsEnum optFields
        {
            get { return this.optFieldsField; }
            set { this.optFieldsField = value; }
        }

        [XmlAttribute]
        [Category("SMVSettings"), Description("Скорость выборки"), DefaultValue(tServiceSettingsEnum.Fix)]
        public tServiceSettingsEnum smpRate
        {
            get { return this.smpRateField1; }
            set { this.smpRateField1 = value; }
        }
    }
}