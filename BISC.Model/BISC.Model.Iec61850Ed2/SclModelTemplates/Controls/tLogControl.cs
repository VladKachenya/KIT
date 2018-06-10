
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
    public class tLogControl : tControlWithTriggerOpt
    {
        private string logNameField;
        private bool logEnaField;
        private bool reasonCodeField;

        public tLogControl()
        {
            this.logEnaField = true;
            this.reasonCodeField = true;
        }

        [Required]
        [XmlAttribute(DataType = "normalizedString")]
        [Category("LogControl"), Description("Ссылка на LD, являющееся владельцем журнала")]
        public string logName
        {
            get { return this.logNameField; }
            set { this.logNameField = value; }
        }

        [XmlAttribute]
        [Category("LogControl"),
         Description("TRUE разрешает немедленную регистрацию; " +
                     "FALSE запрещает регистрацию до разрешения в онлайновом режиме")]
        public bool logEna
        {
            get { return this.logEnaField; }
            set { this.logEnaField = value; }
        }

        [XmlAttribute]
        [Category("LogControl"), Description("Код аварии")]
        public bool reasonCode
        {
            get { return this.reasonCodeField; }
            set { this.reasonCodeField = value; }
        }
    }
}