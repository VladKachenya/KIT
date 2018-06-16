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
    public class tConfLNs
    {
        private bool fixPrefixField;
        private bool fixLnInstField;

        [XmlAttribute]
        [Category("ConfLNs"), Description("Если логический нуль (false), префиксы могут быть заданы/изменены")]
        public bool fixPrefix
        {
            get { return this.fixPrefixField; }
            set { this.fixPrefixField = value; }
        }

        [XmlAttribute]
        [Category("ConfLNs"), Description("если логический нуль (false), может быть изменено количество экземпляров LN")]
        public bool fixLnInst
        {
            get { return this.fixLnInstField; }
            set { this.fixLnInstField = value; }
        }
    }
}