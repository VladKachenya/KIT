using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Services
{
    [XmlInclude(typeof (tServiceWithMaxAndMaxAttributes))]
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tServiceWithMax
    {
        private uint maxField;

        public tServiceWithMax()
        {
            this.maxField = 0;
        }

        [Required]
        [XmlAttribute]
        [Category("ServiceWithMax"), Description("Максимальное количество DataSet")]
        public uint max
        {
            get { return this.maxField; }
            set { this.maxField = value; }
        }
    }
}
