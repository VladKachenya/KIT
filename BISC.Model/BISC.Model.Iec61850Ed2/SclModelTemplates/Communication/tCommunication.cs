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
    [XmlRoot("Communication", Namespace = "http://www.iec.ch/61850/2003/SCL", IsNullable = false)]
    public class tCommunication : tUnNaming
    {
        public tCommunication()
        {
            this.SubNetwork = new List<tSubNetwork>();
        }

        [XmlElement("SubNetwork")]
        [Category("Communication"), Browsable(false)]
        public List<tSubNetwork> SubNetwork { get; private set; }

        public bool AddSubNetwork(tSubNetwork sn)
        {
            if (this.SubNetwork.Any(subn => subn.name == sn.name)) return false;
            this.SubNetwork.Add(sn);
            return true;
        }

        public tConnectedAP GetConnectedApOnIp(string ip)
        {
            return this.SubNetwork.SelectMany(subNetwork => subNetwork.ConnectedAP.Where(ap => ap.Address.FindIp(ip))).FirstOrDefault();
        }


        public List<string> GetMacAddressesList()
        {
            List<string> macAddressesList = new List<string>();
            SubNetwork.ForEach((subNetwork =>
            {
                subNetwork.ConnectedAP.ForEach((ap =>
                {
                    ap.GSE.ForEach((gse =>
                    {
                        var mac = gse.Address.P.FirstOrDefault((p => p.PType.typeEnum == tPTypeEnum.MAC_Address));
                        if (mac != null)
                        {
                            macAddressesList.Add(mac.Value);

                        }
                    }));
                }));
            }));
            return macAddressesList;
        }

    }
}

