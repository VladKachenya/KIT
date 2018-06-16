using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Substation
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tSubEquipment : tPowerSystemResource
    {
        private tPhaseEnum phaseField;
        private bool virtualField;

        public tSubEquipment()
        {
            this.phaseField = tPhaseEnum.none;
            this.virtualField = false;
        }

        [XmlAttribute]
        [Category("SubEquipment"), Description("The phase to which the subdevice belongs.")]
        public tPhaseEnum phase
        {
            get { return this.phaseField; }
            set { this.phaseField = value; }
        }

        [XmlAttribute]
        [Category("SubEquipment"), Description("A virtual equipment")]
        public bool @virtual
        {
            get { return this.virtualField; }
            set { this.virtualField = value; }
        }
    }
}