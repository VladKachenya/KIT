using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Substation
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tVoltageLevel : tEquipmentContainer
    {
        private tVoltage voltageField;
        private tBay[] bayField;

        [Category("VoltageLevel"), Description("It can be used to state the voltage."), Browsable(false)]
        public tVoltage Voltage
        {
            get { return this.voltageField; }
            set { this.voltageField = value; }
        }

        [Category("VoltageLevel"), Browsable(false)]
        [XmlElement("Bay")]
        public tBay[] Bay
        {
            get { return this.bayField; }
            set { this.bayField = value; }
        }
    }
}