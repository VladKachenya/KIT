using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BISC.Model.Iec61850Ed2.DataTypeTemplates.Base
{

    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tPrivate : tAnyContentFromOtherNamespace
    {
        private string typeField;
        private string sourceField;
        
        [XmlAttribute(DataType = "normalizedString")]
        [Category("Private"), 
        Description("��������� ��������� ������� ���������� ����������� ���������. " +
                    "� type ������ ���� �������� ��� ������������ ��� �������� �������� ����������������, " +
                    "�������������� ��� ������������")]
        public string type
        {
            get { return this.typeField; }
            set { this.typeField = value; }
        }
        
        [XmlAttribute(DataType = "anyURI")]
        [Category("Private"), Description("URL (������) ������� �����, ����������� ������� ����������.")]
        public string source
        {
            get { return this.sourceField; }
            set { this.sourceField = value; }
        }
    }
}