using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;

namespace BISC.Model.Iec61850Ed2.DataTypeTemplates
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tEnumVal
    {

        [Required]
        [XmlAttribute(DataType = "integer")]
        [Category("EnumVal"), Description("Числовой порядок перечисления")]
        public string ord { get; set; }

        [XmlText(DataType = "normalizedString")]
        [Category("EnumVal"), Description("Значение из символьной строки")]
        public string Value { get; set; }
    }
}