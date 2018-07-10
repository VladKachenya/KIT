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
    public class tBay : tEquipmentContainer
    {
        private tConductingEquipment[] conductingEquipmentField;
        private tConnectivityNode[] connectivityNodeField;

        [XmlElement("ConductingEquipment")]
        [Category("Bay"), Browsable(false)]
        public tConductingEquipment[] ConductingEquipment
        {
            get { return this.conductingEquipmentField; }
            set { this.conductingEquipmentField = value; }
        }

        [XmlElement("ConnectivityNode")]
        [Category("Bay"), Browsable(false)]
        public tConnectivityNode[] ConnectivityNode
        {
            get { return this.connectivityNodeField; }
            set { this.connectivityNodeField = value; }
        }
    }
}