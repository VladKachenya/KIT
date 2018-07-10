using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Xml.Serialization;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Communication
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tGSE : tControlBlock
    {
        private tDurationInMilliSec minTimeField;
        private tDurationInMilliSec maxTimeField;

        [Category("GSE"), ReadOnly(true),
         Description("ћаксимально допустима€ задержка отправки в миллисекундах при измененииданных")]
        public tDurationInMilliSec MinTime
        {
            get { return this.minTimeField; }
            set { this.minTimeField = value; }
        }

        [Category("GSE"), ReadOnly(true),
         Description("¬рем€ контрол€ источника в миллисекундах (врем€ цикла поступлени€ контрольного сигнала). " +
                     "¬ течение этого времени отказ источника должен быть обнаружен клиентом")]
        public tDurationInMilliSec MaxTime
        {
            get { return this.maxTimeField; }
            set { this.maxTimeField = value; }
        }

        public string MacAddressString
        {
            get
            {
                var mac = Address.P.FirstOrDefault((p => p.PType.typeEnum == tPTypeEnum.MAC_Address));
                if (mac != null)
                {
                    return mac.Value;
                }
                return String.Empty;
            }
        }

        public int Appid
        {
            get
            {
                var appid = Address.P.FirstOrDefault((p => p.PType.typeEnum == tPTypeEnum.APPID));
                if (appid != null)
                {
                    try
                    {
                        return int.Parse(appid.Value, NumberStyles.HexNumber);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }


                }
                return 0;
            }
        }





        public void InitAddess()
        {
            if (Address == null)
                Address = new tAddress();

            Address.P = new tP[4];
            tP_APPID appid = new tP_APPID();
            appid.type = "APPID";
            appid.Value = "0";
            Address.P[0] = appid;
            tP_MACAddress mac = new tP_MACAddress();
            mac.Value = "01-0C-CD-01-00-00";
            Address.P[1] = mac;
            tP_VLANID vlanid = new tP_VLANID();
            vlanid.type = "VLAN-ID";

            vlanid.Value = "0";
            Address.P[2] = vlanid;
            tP_VLANPRIORITY vlanp = new tP_VLANPRIORITY();
            vlanp.type = "VLAN-PRIORITY";

            vlanp.Value = "4";
            Address.P[3] = vlanp;
        }
    }
}