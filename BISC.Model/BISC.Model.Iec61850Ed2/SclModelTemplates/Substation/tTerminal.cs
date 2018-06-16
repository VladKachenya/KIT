using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;
using BISC.Model.Iec61850Ed2.DataTypeTemplates.Base;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Substation
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tTerminal : tUnNaming
    {
        private string nameField;
        private string connectivityNodeField;
        private string substationNameField;
        private string voltageLevelNameField;
        private string bayNameField;
        private string cNodeNameField;

        public tTerminal()
        {
            this.nameField = "";
        }

        [XmlAttribute(DataType = "normalizedString")]
        [Category("Terminal"), Description("The optional relative name of the terminal at this Equipment.")]
        public string name
        {
            get { return this.nameField; }
            set { this.nameField = value; }
        }

        [Required]
        [XmlAttribute(DataType = "normalizedString")]
        [Category("Terminal"), Description("The pathname of the connectivity node to which this terminal connects.")]
        public string connectivityNode
        {
            get { return this.connectivityNodeField; }
            set { this.connectivityNodeField = value; }
        }

        [Required]
        [XmlAttribute(DataType = "normalizedString")]
        [Category("Terminal"), Description("The name of the substation containing the connectivityNode.")]
        public string substationName
        {
            get { return this.substationNameField; }
            set { this.substationNameField = value; }
        }

        [Required]
        [XmlAttribute(DataType = "normalizedString")]
        [Category("Terminal"), Description("The name of the voltage level containing the connectivityNode.")]
        public string voltageLevelName
        {
            get { return this.voltageLevelNameField; }
            set { this.voltageLevelNameField = value; }
        }

        [Required]
        [XmlAttribute(DataType = "normalizedString")]
        [Category("Terminal"), Description("The name of the bay containing the connectivityNode.")]
        public string bayName
        {
            get { return this.bayNameField; }
            set { this.bayNameField = value; }
        }

        [Required]
        [XmlAttribute(DataType = "normalizedString")]
        [Category("Terminal"), Description("The (relative) name of the connectivityNode within its bay.")]
        public string cNodeName
        {
            get { return this.cNodeNameField; }
            set { this.cNodeNameField = value; }
        }
    }
}