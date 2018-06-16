using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tVal
    {

    


        private uint sGroupField;
        private string valueField;

        [XmlIgnore]
        public uint sGroup
        {
            get { return this.sGroupField; }
            set { this.sGroupField = value; }
        }
        [XmlText]
        public string Value
        {
            get { return this.valueField; }
            set
            {
                this.valueField = value; 
            }
        }
    }
}