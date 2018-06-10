using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;

namespace BISC.Model.Iec61850Ed2.DataTypeTemplates.Base
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tLNode : tUnNaming
    {
        private string lnInstField;
        private string lnClassField;
        private string iedNameField;
        private string ldInstField;
        private string prefixField;
        private string lnTypeField;
        
        [XmlAttribute(DataType = "normalizedString")]
        [Category("LNode"), Description("Ссылка на логический узел")]
        public string lnInst
        {
            get { return this.lnInstField; }
            set { this.lnInstField = value; }
        }

        [Required]
        [XmlAttribute]
        [Category("LNode"), Description("Класс логического узла")]
        public string lnClass
        {
            get { return this.lnClassField; }
            set { this.lnClassField = value; }
        }

        [XmlAttribute(DataType = "normalizedString")]
        [Category("LNode"), Description("IED-устройство, которое содержит данный логический узел")]
        public string iedName
        {
            get { return this.iedNameField; }
            set { this.iedNameField = value; }
        }

        [XmlAttribute(DataType = "normalizedString")]
        [Category("LNode"), Description("Ссылка на IED-устройство, которое содержит данный логический узел")]
        public string ldInst
        {
            get { return this.ldInstField; }
            set { this.ldInstField = value; }
        }

        [XmlAttribute(DataType = "normalizedString")]
        [Category("LNode"), Description("Префикс IED-устройства")]
        public string prefix
        {
            get { return this.prefixField; }
            set { this.prefixField = value; }
        }

        [XmlAttribute(DataType = "normalizedString")]
        [Category("LNode"), Description("Определение типа логического узла, содержащее более подробную функциональную спецификацию")]
        public string lnType
        {
            get { return this.lnTypeField; }
            set { this.lnTypeField = value; }
        }
    }
}