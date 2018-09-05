using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;
using BISC.Model.Iec61850Ed2.DataTypeTemplates.Base;
using BISC.Model.Iec61850Ed2.SclModelTemplates;
using BISC.Modules.Connection.Infrastructure.Connection;

namespace BISC.Model.Iec61850Ed2.DataTypeTemplates
{
    [XmlInclude(typeof(tBDA))]
    [XmlInclude(typeof(tDA))]
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tAbstractDataAttribute : tUnNaming
    {
        public tAbstractDataAttribute()
        {
           this.Val = new List<tVal>();
        }

        [XmlIgnore]
        [Category("AbstractDataAttribute"), Browsable(false)]
        public List<tVal> Val { get; set; }

        [XmlIgnore]
        [Category("Value"), Browsable(true)]
        public string Value => Val[0].Value.ToString();

        [XmlAttribute, Required, ReadOnly(true)]
        [Category("AbstractDataAttribute"), Description("��� ��������")]
        public string name { get; set; }

        [XmlAttribute(DataType = "normalizedString"), ReadOnly(true)]
        [Category("AbstractDataAttribute"), Description("�������������� �������� ����� ������� �������� DO")]
        public string sAddr { get; set; }

        [XmlAttribute, Required, ReadOnly(true)]
        [Category("AbstractDataAttribute"), Description("�������� ��� ��������")]
        public tBasicTypeEnum bType { get; set; }

        [Category("AbstractDataAttribute"), XmlIgnore, ReadOnly(true),
        Description("���������� ������������� ��������, ���� ��� ������")]
        public tValKindEnum? valKind { get; set; }

        [XmlAttribute(DataType = "normalizedString")]
        [Category("AbstractDataAttribute"), ReadOnly(true),
         Description("������������, ������ ���� b���� = Enum ��� bType = Struct ��� ��������� " +
                     "� ���������������� �������������� ���� ��� ����������� DAType (��������� ��������)")]
        public string type { get; set; }

        [XmlIgnore, ReadOnly(true)]
        [Category("AbstractDataAttribute"),
         Description("������ ����� ��������� ������� ��� ��� �������, ����� ������� ���� ������")]
        public uint? count { get; set; }



        [XmlElement("count"), Browsable(false)]
        public string countAsText
        {
            get { return (count.HasValue) ? count.ToString() : null; }
            set { count = !string.IsNullOrEmpty(value) ? uint.Parse(value) : default(uint?); }
        }

        [XmlElement("valKind"), Browsable(false)]
        public string valKindAsText
        {
            get { return (valKind.HasValue) ? valKind.ToString() : null; }
            set
            {
                tValKindEnum valKindEnum;
                if (!string.IsNullOrEmpty(value))
                {
                    Enum.TryParse(value, out valKindEnum);
                    valKind = valKindEnum;
                }
            }
        }
    }
}