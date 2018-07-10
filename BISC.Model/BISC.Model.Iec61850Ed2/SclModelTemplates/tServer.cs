using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.DataTypeTemplates.Base;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tServer : tUnNaming
    {
        private ObservableCollection<tLDevice> _lDevice;
        public tServer()
        {
            this.timeout = 30;
            this.LDevice = new ObservableCollection<tLDevice>();
            this.Association = new List<tAssociation>();
        }

        [XmlElement("Authentication")]
        [Category("Server"), Browsable(false)]
        public tServerAuthentication Authentication { get; set; }

        [XmlElement("LDevice")]
        [Category("Server"), Browsable(false)]
        public ObservableCollection<tLDevice> LDevice
        {
            get { return _lDevice; }
            set
            {
                _lDevice = value;
          
            }
        }

        [XmlElement("Association")]
        [Category("Server"), Browsable(false)]
        public List<tAssociation> Association { get; private set; }

        [XmlAttribute]
        [Category("Server"), Description("¬рем€ ожидани€ в секундах: " +
                                         "если начата€ транзакци€ не завершена в течение данного времени, " +
                                         "выполн€ютс€ сброс и перегрузка")]
        public uint timeout { get; set; }

        public bool AddLDevice(tLDevice ld)
        {
            if (this.LDevice.Any(ldevice => ldevice.inst == ld.inst)) return false;
            this.LDevice.Add(ld);
            return true;
        }

        [XmlIgnore, Browsable(false)]
        public List<string> LDCollection
        {
            get
            {
                List<string> col = new List<string>();
                foreach (tLDevice ld in LDevice)
                {
                    col.Add(ld.inst);
                }
                return col;
            }
        }


        //#region Implementation of IHavingChildCollection

        //[XmlIgnore]
        //public List<INamableSclItem> ChildNamableCollection
        //{
        //    get
        //    {
        //        List<INamableSclItem> namables = new List<INamableSclItem>();
        //        LDevice.ForEach((ld =>
        //           {
        //               namables.Add(ld);
        //           }));

        //        return namables;
        //    }
        //}
        //#endregion
    }
}