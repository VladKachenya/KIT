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
    public class tText : tAnyContentFromOtherNamespace
    {
        [XmlAttribute(DataType = "anyURI")]
        [Browsable(false)]
        public string source { get; set; }
    }
}