using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tExtRef
    {
        private string iedNameField;
        private string ldInstField;
        private string prefixField;
        private string lnClassField;
        private string lnInstField;
        private string doNameField;
        private string daNameField;
        private string intAddrField;

        [Required]
        [XmlAttribute(DataType = "normalizedString")]
        [Category("ExtRef"), Description("��� IED-����������, �� �������� ��������� ������� ������")]
        public string iedName
        {
            get { return this.iedNameField; }
            set { this.iedNameField = value; }
        }

        [Required]
        [XmlAttribute(DataType = "normalizedString")]
        [Category("ExtRef"), Description("��� ���������� LD, �� �������� ��������� ������� ������")]
        public string ldInst
        {
            get { return this.ldInstField; }
            set { this.ldInstField = value; }
        }

        [XmlAttribute(DataType = "normalizedString")]
        [Category("ExtRef"), Description("������� LN")]
        public string prefix
        {
            get { return this.prefixField; }
            set { this.prefixField = value; }
        }

        [Required]
        [XmlAttribute]
        [Category("ExtRef"), Description("����� LN")]
        public string lnClass
        {
            get { return this.lnClassField; }
            set { this.lnClassField = value; }
        }

        [Required]
        [XmlAttribute(DataType = "normalizedString")]
        [Category("ExtRef"), Description("������������� id ���������� ������� ���������� LN ������������ ������ LN � IED-����������")]
        public string lnInst
        {
            get { return this.lnInstField; }
            set { this.lnInstField = value; }
        }

        [Required]
        [XmlAttribute(DataType = "normalizedString")]
        [Category("ExtRef"), Description("���, ���������������� DO (� �������� LN)")]
        public string doName
        {
            get { return this.doNameField; }
            set { this.doNameField = value; }
        }

        [XmlAttribute(DataType = "normalizedString")]
        [Category("ExtRef"), Description("�������, ������������ ������� ������")]
        public string daName
        {
            get { return this.daNameField; }
            set { this.daNameField = value; }
        }

        [XmlAttribute(DataType = "normalizedString")]
        [Category("ExtRef"), Description("���������� ������, � ������� ��������� ������� ������"), ReadOnly(true)]
        public string intAddr
        {
            get { return this.intAddrField; }
            set { this.intAddrField = value; }
        }
    }
}