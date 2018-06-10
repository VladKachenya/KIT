using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Substation
{
    [XmlInclude(typeof (tBay))]
    [XmlInclude(typeof (tVoltageLevel))]
    [XmlInclude(typeof (tSubstation))]
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tEquipmentContainer : tPowerSystemResource
    {
        private tPowerTransformer[] powerTransformerField;
        private tGeneralEquipment[] generalEquipmentField;

        [XmlElement("PowerTransformer")]
        [Category("EquipmentContainer"), Browsable(false)]
        public tPowerTransformer[] PowerTransformer
        {
            get { return this.powerTransformerField; }
            set { this.powerTransformerField = value; }
        }

        [XmlElement("GeneralEquipment")]
        [Category("EquipmentContainer"), Browsable(false)]
        public tGeneralEquipment[] GeneralEquipment
        {
            get { return this.generalEquipmentField; }
            set { this.generalEquipmentField = value; }
        }
    }
}