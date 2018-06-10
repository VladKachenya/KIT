using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Substation
{
    [XmlInclude(typeof (tGeneralEquipment))]
    [XmlInclude(typeof (tPowerTransformer))]
    [XmlInclude(typeof (tAbstractConductingEquipment))]
    [XmlInclude(typeof (tTransformerWinding))]
    [XmlInclude(typeof (tConductingEquipment))]
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tEquipment : tPowerSystemResource
    {
        private bool virtualField;

        public tEquipment()
        {
            this.virtualField = false;
        }

        [XmlAttribute]
        [Category("Equipment"), Description("Virtual equipment configured")]
        public bool @virtual
        {
            get { return this.virtualField; }
            set { this.virtualField = value; }
        }
    }
}