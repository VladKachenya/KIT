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
    public class tLogSettings : tServiceSettings
    {
        private tServiceSettingsEnum logEnaField;
        private tServiceSettingsEnum trgOpsField;
        private tServiceSettingsEnum intgPdField;
        
        [XmlAttribute, DefaultValue(tServiceSettingsEnum.Fix)]
        [Category("LogSettings"), Description("Разрешение журнала")]
        public tServiceSettingsEnum logEna
        {
            get { return this.logEnaField; }
            set { this.logEnaField = value; }
        }

        [XmlAttribute, DefaultValue(tServiceSettingsEnum.Fix)]
        [Category("LogSettings"), Description("Разрешение опции пуска")]
        public tServiceSettingsEnum trgOps
        {
            get { return this.trgOpsField; }
            set { this.trgOpsField = value; }
        }

        [XmlAttribute, DefaultValue(tServiceSettingsEnum.Fix)]
        [Category("LogSettings"), Description("Период сохранности")]
        public tServiceSettingsEnum intgPd
        {
            get { return this.intgPdField; }
            set { this.intgPdField = value; }
        }
    }
}