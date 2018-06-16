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
    [XmlRoot("Substation", Namespace = "http://www.iec.ch/61850/2003/SCL", IsNullable = false)]
    public class tSubstation : tEquipmentContainer
    {
        private tVoltageLevel[] voltageLevelField;
        private tFunction[] functionField;

        [XmlElement("VoltageLevel")]
        [Category("Substation"), Browsable(false)]
        public tVoltageLevel[] VoltageLevel
        {
            get { return this.voltageLevelField; }
            set { this.voltageLevelField = value; }
        }

        [XmlElement("Function")]
        [Category("Substation"), Browsable(false)]
        public tFunction[] Function
        {
            get { return this.functionField; }
            set { this.functionField = value; }
        }
    }
}