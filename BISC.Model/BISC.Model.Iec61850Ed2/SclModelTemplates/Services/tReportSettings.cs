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
    public class tReportSettings : tServiceSettings
    {
        private tServiceSettingsEnum rptIDField;
        private tServiceSettingsEnum optFieldsField;
        private tServiceSettingsEnum bufTimeField;
        private tServiceSettingsEnum trgOpsField;
        private tServiceSettingsEnum intgPdField;
        
        [XmlAttribute, DefaultValue(tServiceSettingsEnum.Fix)]
        [Category("ReportSettings"), Description("Идентификатор отчетов")]
        public tServiceSettingsEnum rptID
        {
            get { return this.rptIDField; }
            set { this.rptIDField = value; }
        }

        [XmlAttribute, DefaultValue(tServiceSettingsEnum.Fix)]
        [Category("ReportSettings"), Description("Дополнительные поля для включения в отчет")]
        public tServiceSettingsEnum optFields
        {
            get { return this.optFieldsField; }
            set { this.optFieldsField = value; }
        }

        [XmlAttribute, DefaultValue(tServiceSettingsEnum.Fix)]
        [Category("ReportSettings"), Description("Буферное время")]
        public tServiceSettingsEnum bufTime
        {
            get { return this.bufTimeField; }
            set { this.bufTimeField = value; }
        }

        [XmlAttribute, DefaultValue(tServiceSettingsEnum.Fix)]
        [Category("ReportSettings"), Description("Разрешение опции пуска")]
        public tServiceSettingsEnum trgOps
        {
            get { return this.trgOpsField; }
            set { this.trgOpsField = value; }
        }

        [XmlAttribute, DefaultValue(tServiceSettingsEnum.Fix)]
        [Category("ReportSettings"), Description("Период сохранности")]
        public tServiceSettingsEnum intgPd
        {
            get { return this.intgPdField; }
            set { this.intgPdField = value; }
        }
    }
}