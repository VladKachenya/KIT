using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;
using BISC.Model.Iec61850Ed2.DataTypeTemplates.Base;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Communication
{
    [XmlInclude(typeof (tSMV))]
    [XmlInclude(typeof (tGSE))]
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tControlBlock : tUnNaming
    {
        private tAddress addressField;
        private string ldInstField;
        private string cbNameField;

        [Category("ControlBlock"), Browsable(true),
         Description("��������� ������, ��������� � ������ ����������")]
        public tAddress Address
        {
            get { return this.addressField; }
            set { this.addressField = value; }
        }

        [Required]
        [XmlAttribute(DataType = "normalizedString")]
        [Category("ControlBlock"),
         Description("������������� ���������� LD � �������� ������� IED-����������, �� ������� ���������� ���� ����������"),
         ReadOnly(true)]
        public string ldInst
        {
            get { return this.ldInstField; }
            set { this.ldInstField = value; }
        }

        [Required]
        [XmlAttribute(DataType = "normalizedString")]
        [Category("ControlBlock"),
         Description("��� ����� ���������� � �������� LLN0 ���������� LD"),
         ReadOnly(true)]
        public string cbName
        {
            get { return this.cbNameField; }
            set { this.cbNameField = value; }
        }
    }
}