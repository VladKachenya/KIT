using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Substation
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tGeneralEquipment : tEquipment
    {
        private tGeneralEquipmentEnum typeField;

        [Required]
        [XmlAttribute]
        [Category("GeneralEquipment"), Description("Type of General Equipment")]
        public tGeneralEquipmentEnum type
        {
            get { return this.typeField; }
            set { this.typeField = value; }
        }
    }
}