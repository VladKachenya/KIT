using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Substation
{
    [XmlInclude(typeof (tTransformerWinding))]
    [XmlInclude(typeof (tConductingEquipment))]
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tAbstractConductingEquipment : tEquipment
    {
        private tTerminal[] terminalField;
        private tSubEquipment[] subEquipmentField;

        [XmlElement("Terminal")]
        [Category("AbstractConductingEquipment"), Browsable(false)]
        public tTerminal[] Terminal
        {
            get { return this.terminalField; }
            set { this.terminalField = value; }
        }

        [XmlElement("SubEquipment")]
        [Category("AbstractConductingEquipment"), Browsable(false)]
        public tSubEquipment[] SubEquipment
        {
            get { return this.subEquipmentField; }
            set { this.subEquipmentField = value; }
        }
    }
}

