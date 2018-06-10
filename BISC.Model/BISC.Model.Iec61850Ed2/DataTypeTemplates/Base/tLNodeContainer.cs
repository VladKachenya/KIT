using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.SclModelTemplates.Substation;

namespace BISC.Model.Iec61850Ed2.DataTypeTemplates.Base
{
    [XmlInclude(typeof (tConnectivityNode))]
    [XmlInclude(typeof (tPowerSystemResource))]
    [XmlInclude(typeof (tSubFunction))]
    [XmlInclude(typeof (tFunction))]
    [XmlInclude(typeof (tTapChanger))]
    [XmlInclude(typeof (tSubEquipment))]
    [XmlInclude(typeof (tEquipment))]
    [XmlInclude(typeof (tGeneralEquipment))]
    [XmlInclude(typeof (tPowerTransformer))]
    [XmlInclude(typeof (tAbstractConductingEquipment))]
    [XmlInclude(typeof (tTransformerWinding))]
    [XmlInclude(typeof (tConductingEquipment))]
    [XmlInclude(typeof (tEquipmentContainer))]
    [XmlInclude(typeof (tBay))]
    [XmlInclude(typeof (tVoltageLevel))]
    [XmlInclude(typeof (tSubstation))]
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tLNodeContainer : tNaming
    {
        private tLNode[] lNodeField;
        
        [XmlElement("LNode")]
        [Category("LNodeContainer"), Browsable(false)]
        public tLNode[] LNode
        {
            get { return this.lNodeField; }
            set { this.lNodeField = value; }
        }
    }
}
