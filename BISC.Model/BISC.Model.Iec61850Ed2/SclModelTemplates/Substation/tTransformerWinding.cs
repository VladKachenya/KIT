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
    public class tTransformerWinding : tAbstractConductingEquipment
    {
        private tTapChanger tapChangerField;
        private tTransformerWindingEnum typeField;

        public tTransformerWinding()
        {
            this.typeField = tTransformerWindingEnum.PTW;
        }

        [Category("TransformerWinding"), Description(" ")]
        public tTapChanger TapChanger
        {
            get { return this.tapChangerField; }
            set { this.tapChangerField = value; }
        }

        [Required]
        [XmlAttribute]
        [Category("TransformerWinding"), Description("Type of Transformer Winding")]
        public tTransformerWindingEnum type
        {
            get { return this.typeField; }
            set { this.typeField = value; }
        }
    }
}