using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Services
{
    [XmlInclude(typeof (tSMVSettings))]
    [XmlInclude(typeof (tGSESettings))]
    [XmlInclude(typeof (tLogSettings))]
    [XmlInclude(typeof (tReportSettings))]
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tServiceSettings
    {
        private tServiceSettingsEnum cbNameField;
        private tServiceSettingsEnum datSetField;
        
        [XmlAttribute, DefaultValue(tServiceSettingsEnum.Fix)]
        [Category("ServiceSettings"), Description("»м€ блока управлени€")]
        public tServiceSettingsEnum cbName
        {
            get { return this.cbNameField; }
            set { this.cbNameField = value; }
        }

        [XmlAttribute, DefaultValue(tServiceSettingsEnum.Fix)]
        [Category("ServiceSettings"), Description("—сылка на набор данных")]
        public tServiceSettingsEnum datSet
        {
            get { return this.datSetField; }
            set { this.datSetField = value; }
        }
    }
}