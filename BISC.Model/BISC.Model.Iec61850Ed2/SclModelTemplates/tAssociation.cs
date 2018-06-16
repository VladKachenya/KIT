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
    public class tAssociation
    {
        private tAssociationKindEnum kindField;
        private string associationIDField;
        private string iedNameField;
        private string ldInstField;
        private string prefixField;
        private string lnClassField;
        private string lnInstField;
        
        [Required]
        [XmlAttribute]
        [Category("Association"), Description("��� ��������������������� ����������")]
        [DefaultValue(tAssociationKindEnum.preestablished)]
        public tAssociationKindEnum kind
        {
            get { return this.kindField; }
            set { this.kindField = value; }
        }

        [XmlAttribute(DataType = "normalizedString")]
        [Category("Association"), Description("������������� ��������������������� ����������")]
        public string associationID
        {
            get { return this.associationIDField; }
            set { this.associationIDField = value; }
        }

        [Required]
        [XmlAttribute(DataType = "normalizedString")]
        [Category("Association"), Description("������, ���������������� IED-����������, " +
                                              "�� ������� ���������� ���������� ������")]
        public string iedName
        {
            get { return this.iedNameField; }
            set { this.iedNameField = value; }
        }

        [Required]
        [XmlAttribute(DataType = "normalizedString")]
        [Category("Association"), Description("������ �� ���������� ���������� �������")]
        public string ldInst
        {
            get { return this.ldInstField; }
            set { this.ldInstField = value; }
        }

        [XmlAttribute(DataType = "normalizedString"), DefaultValue("")]
        [Category("Association"), Description("������� LN")]
        public string prefix
        {
            get { return this.prefixField; }
            set { this.prefixField = value; }
        }

        [Required]
        [XmlAttribute]
        [Category("Association"), Description("����� LN �������")]
        public string lnClass
        {
            get { return this.lnClassField; }
            set { this.lnClassField = value; }
        }

        [Required]
        [XmlAttribute(DataType = "normalizedString")]
        [Category("Association"), Description("����� ���������� LN")]
        public string lnInst
        {
            get { return this.lnInstField; }
            set { this.lnInstField = value; }
        }
    }
}