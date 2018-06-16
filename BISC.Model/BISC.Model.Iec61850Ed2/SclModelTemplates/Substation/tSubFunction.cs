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
    public class tSubFunction : tPowerSystemResource
    {
        private tGeneralEquipment[] generalEquipmentField;

        [XmlElement("GeneralEquipment")]
        [Category("PowerSystemResource"), Browsable(false)]
        public tGeneralEquipment[] GeneralEquipment
        {
            get { return this.generalEquipmentField; }
            set { this.generalEquipmentField = value; }
        }
    }
}
