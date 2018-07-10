using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Substation
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tConductingEquipment : tAbstractConductingEquipment
    {
        private tCommonConductingEquipmentEnum typeField;

        [XmlAttribute]
        [Required]
        [Category("ConductingEquipment"), Description("Type of Conducting Equipment.")]
        public tCommonConductingEquipmentEnum type
        {
            get { return this.typeField; }
            set { this.typeField = value; }
        }
    }
}

