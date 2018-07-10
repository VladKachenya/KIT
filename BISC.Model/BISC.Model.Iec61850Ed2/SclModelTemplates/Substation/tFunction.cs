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
    public class tFunction : tPowerSystemResource
    {
        private tSubFunction[] subFunctionField;
        private tGeneralEquipment[] generalEquipmentField;

        [XmlElement("SubFunction")]
        [Category("Function"), Browsable(false)]
        public tSubFunction[] SubFunction
        {
            get { return this.subFunctionField; }
            set { this.subFunctionField = value; }
        }

        [XmlElement("GeneralEquipment")]
        [Category("Function"), Browsable(false)]
        public tGeneralEquipment[] GeneralEquipment
        {
            get { return this.generalEquipmentField; }
            set { this.generalEquipmentField = value; }
        }
    }
}