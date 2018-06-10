using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;
using BISC.Model.Iec61850Ed2.DataTypeTemplates.Base;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Substation
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tConnectivityNode : tLNodeContainer
    {
        private string pathNameField;

        [Required]
        [XmlAttribute(DataType = "normalizedString")]
        [Category("ConnectivityNode"), Description("This acts as a key")]
        public string pathName
        {
            get { return this.pathNameField; }
            set { this.pathNameField = value; }
        }
    }
}

