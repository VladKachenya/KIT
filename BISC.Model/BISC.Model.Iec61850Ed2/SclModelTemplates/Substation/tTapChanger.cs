using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Substation
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tTapChanger : tPowerSystemResource
    {
        private string typeField;
        private bool virtualField;

        public tTapChanger()
        {
            this.typeField = "LTC";
            this.virtualField = false;
        }

        [Required]
        [XmlAttribute(DataType = "Name")]
        [Category("TapChanger"), Description("Type of Tap Changer")]
        public string type
        {
            get { return this.typeField; }
            set { this.typeField = value; }
        }

        [XmlAttribute]
        [Category("TapChanger"), Description("Virtual equipment")]
        public bool @virtual
        {
            get { return this.virtualField; }
            set { this.virtualField = value; }
        }
    }
}