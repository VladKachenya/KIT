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
    public class tGSESettings : tServiceSettings
    {
        private tServiceSettingsEnum appIDField;
        private tServiceSettingsEnum dataLabelField;

        public tGSESettings()
        {
            this.appIDField = tServiceSettingsEnum.Fix;
            this.dataLabelField = tServiceSettingsEnum.Fix;
        }

        [XmlAttribute]
        [Category("GSESettings"), Description("Идентификатор приложения")]
        public tServiceSettingsEnum appID
        {
            get { return this.appIDField; }
            set { this.appIDField = value; }
        }

        [XmlAttribute]
        [Category("GSESettings"), Description("Значение для ссылки объекта при отправке соответствующего элемента")]
        public tServiceSettingsEnum dataLabel
        {
            get { return this.dataLabelField; }
            set { this.dataLabelField = value; }
        }
    }
}