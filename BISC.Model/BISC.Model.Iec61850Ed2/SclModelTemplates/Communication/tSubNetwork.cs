using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.DataTypeTemplates.Base;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Communication
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tSubNetwork : tNaming
    {
        public tSubNetwork():this("NONE", "8-MMS")
        {
        }

        public tSubNetwork(string snName):this(snName, "8-MMS")
        {
        }

        public tSubNetwork(string snName, string snType)
        {
            this.ConnectedAP = new List<tConnectedAP>();
            name = snName;
            this.type = snType;
        }

        [XmlElement]
        [Category("SubNetwork"), Browsable(false)]
        public tBitRateInMbPerSec BitRate { get; set; }

        [XmlElement("ConnectedAP")]
        [Category("SubNetwork"), Browsable(false)]
        public List<tConnectedAP> ConnectedAP { get;  set; }
        [XmlAttribute(DataType = "normalizedString")]
        [Category("SubNetwork"), Description("“ип протокола SubNetwork; типы протокола определ€ют на уровне SCSM")]
        public string type { get; set; }

        public bool AddConnectedAP(tConnectedAP ap)
        {
            if (this.ConnectedAP.Any(cAP => cAP.apName == ap.apName)) return false;
            this.ConnectedAP.Add(ap);
            return true;
        }
    }
}