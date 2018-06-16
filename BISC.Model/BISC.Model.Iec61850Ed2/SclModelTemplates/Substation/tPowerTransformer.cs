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
    public class tPowerTransformer : tEquipment
    {
        private tTransformerWinding[] transformerWindingField;
        private tPowerTransformerEnum typeField;

        public tPowerTransformer()
        {
            this.typeField = tPowerTransformerEnum.PTR;
        }

        [XmlElement("TransformerWinding")]
        [Category("PowerTransformer"), Browsable(false)]
        public tTransformerWinding[] TransformerWinding
        {
            get { return this.transformerWindingField; }
            set { this.transformerWindingField = value; }
        }

        [Required]
        [XmlAttribute]
        [Category("PowerTransformer"), Description("Type of Power Transformer")]
        public tPowerTransformerEnum type
        {
            get { return this.typeField; }
            set { this.typeField = value; }
        }
    }
}