using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Communication
{
    public class tAddress
    {
        private tP[] pField;

        [XmlElement("P", IsNullable = false)]
        [Category("Address"), Browsable(true)]
        public tP[] P
        {
            get { return this.pField; }
            set { this.pField = value; }
        }
        public tAddress()
        {
            this.pField =new tP[0];
        }

        public bool AddP(tP p)
        {
            if (this.Plist.All(p1 => p1.type != p.type))
            {
                List<tP> l = this.Plist;
                l.Add(p);
                this.Plist = l;
                return true;
            }
            return false;
        }

        public bool FindIp(string ip)
        {
            return this.Plist.OfType<tP_IP>().Any(p => p.Value == ip);
        }
        [XmlIgnore]
        public List<tP> Plist
        {
            get {
                List<tP> plist = new List<tP>();
                plist.AddRange(this.pField);
                return plist;
            }
            set { this.pField = value.ToArray(); }
        }


    }
}