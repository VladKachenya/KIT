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
    public class tClientLN
    {
        private string iedNameField;
        private string ldInstField;
        private string prefixField;
        private string lnClassField;
        private string lnInstField;

        [Required]
        [XmlAttribute(DataType = "normalizedString")]
        [Category("ClientLN"), Description("Название IED-устройства, в котором содержится данный LN")]
        public string iedName
        {
            get { return this.iedNameField; }
            set { this.iedNameField = value; }
        }

        [Required]
        [XmlAttribute(DataType = "normalizedString")]
        [Category("ClientLN"), Description("Ссылка на LD, где содержится данный LN")]
        public string ldInst
        {
            get { return this.ldInstField; }
            set { this.ldInstField = value; }
        }

        [XmlAttribute(DataType = "normalizedString")]
        [Category("ClientLN"), Description("Префикс LN"), DefaultValue("")]
        public string prefix
        {
            get { return this.prefixField; }
            set { this.prefixField = value; }
        }

        [Required]
        [XmlAttribute]
        [Category("ClientLN"), Description("Класс LN")]
        public string lnClass
        {
            get { return this.lnClassField; }
            set { this.lnClassField = value; }
        }

        [Required]
        [XmlAttribute(DataType = "normalizedString")]
        [Category("ClientLN"), Description("Ссылка на LN")]
        public string lnInst
        {
            get { return this.lnInstField; }
            set { this.lnInstField = value; }
        }
    }
}