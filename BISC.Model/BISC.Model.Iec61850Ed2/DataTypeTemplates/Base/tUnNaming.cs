using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.SclModelTemplates;
using BISC.Model.Iec61850Ed2.SclModelTemplates.Communication;
using BISC.Model.Iec61850Ed2.SclModelTemplates.Controls;
using BISC.Model.Iec61850Ed2.SclModelTemplates.Substation;

namespace BISC.Model.Iec61850Ed2.DataTypeTemplates.Base
{
  
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tUnNaming : tBaseElement
    {
        [XmlAttribute(DataType = "normalizedString")]
        [Category("Description"), Description("Описательный текст для атрибута")]
        public string desc { get; set; }
    }
}