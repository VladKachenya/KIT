using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;
using BISC.Model.Iec61850Ed2.DataTypeTemplates.Base;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Communication
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tConnectedAP : tUnNaming
    {
        public tConnectedAP():this("P1")
        {
        }

        public tConnectedAP(string apName)
        {
            this.apName = apName;
            this.Address = new tAddress();
            this.GSE = new List<tGSE>();
            this.SMV = new List<tSMV>();
            this.PhysConn = new List<tPhysConn>();
        }

        [Category("ConnectedAP"), Browsable(false)]
        public tAddress Address { get; set; }

        [XmlElement("GSE")]
        [Category("ConnectedAP"), Browsable(false)]
        public List<tGSE> GSE { get; private set; }

        [XmlElement("SMV")]
        [Category("ConnectedAP"), Browsable(false)]
        public List<tSMV> SMV { get; private set; }

        [XmlElement("PhysConn")]
        [Category("ConnectedAP"), Browsable(false)]
        public List<tPhysConn> PhysConn { get; private set; }

        [Required, XmlAttribute(DataType = "normalizedString"), ReadOnly(true)]
        [Category("ConnectedAP"), Description("»м€, идентифицирующее IED-устройство")]
        public string iedName { get; set; }

        [Required, XmlAttribute(DataType = "normalizedString"), ReadOnly(true)]
        [Category("ConnectedAP"), Description("»м€, определ€ющее данную точку доступа в пределах IED-устройства")]
        public string apName { get; set; }
        
        public void CreateAddress(string ip)
        {
            if (this.Address == null)
                this.Address = new tAddress();
            this.Address.AddP(new tP_IP { Value = ip });
            this.Address.AddP(new tP_OSIAEQualifier { Value = "1" });
            this.Address.AddP(new tP_OSIAPTitle { Value = "1,1,1,999,1" });
            this.Address.AddP(new tP_OSIPSEL { Value = "00000001" });
            this.Address.AddP(new tP_OSISSEL { Value = "0001" });
            this.Address.AddP(new tP_OSITSEL { Value = "0001" });
            //this.Address.AddP(new tP_IPSUBNET { Value = "255.255.255.0" });
            //this.Address.AddP(new tP_IPGATEWAY { Value = "192.168.0.1" });
            this.Address.AddP(new tP_OSIAPInvoke { Value = "0" });
            this.Address.AddP(new tP_OSIAEQualifier { Value = "12" });
            this.Address.AddP(new tP_OSIAEInvoke { Value = "0" });


        }
    }
}

