using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Services
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tServiceWithMaxAndMaxAttributes : tServiceWithMax
    {
        private uint maxAttributesField;

        [XmlAttribute]
        [Category("ServiceWithMaxAndMaxAttributes"),
         Description("The maximum number of attributes allowed in a data set " +
                     " (an FCDA can contain several attributes)")]
        public uint maxAttributes
        {
            get { return this.maxAttributesField; }
            set { this.maxAttributesField = value; }
        }
    }
}