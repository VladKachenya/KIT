using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Communication
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tPhysConn
    {
        private tP[] pField;
        private string typeField;

        [XmlElement("P")]
        [Category("PhysConn"), Browsable(false)]
        public tP[] P
        {
            get { return this.pField; }
            set { this.pField = value; }
        }

        [Required]
        [XmlAttribute(DataType = "normalizedString")]
        [Category("PhysConn"), Description("Атрибут идентифицирует значение переменной")]
        public string type
        {
            get { return this.typeField; }
            set { this.typeField = value; }
        }
    }
}