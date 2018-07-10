using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;

namespace BISC.Model.Iec61850Ed2.DataTypeTemplates.Base
{
    [XmlInclude(typeof (tEnumType))]
    [XmlInclude(typeof (tDAType))]
    [XmlInclude(typeof (tDOType))]
    [XmlInclude(typeof (tLNodeType))]
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tIDNaming : tBaseElement
    {
        private string idField;
        private string descField;

        [Required]
        [XmlAttribute(DataType = "normalizedString")]
        public string id
        {
            get { return this.idField; }
            set { this.idField = value; }
        }

        [XmlAttribute(DataType = "normalizedString")]
        public string desc
        {
            get { return this.descField; }
            set { this.descField = value; }
        }
    }
}