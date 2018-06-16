using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Communication
{
    [XmlInclude(typeof (tP_VLANID))]
    [XmlInclude(typeof (tP_VLANPRIORITY))]
    [XmlInclude(typeof (tP_APPID))]
    [XmlInclude(typeof (tP_MACAddress))]
    [XmlInclude(typeof (tP_OSIAEInvoke))]
    [XmlInclude(typeof (tP_OSIAEQualifier))]
    [XmlInclude(typeof (tP_OSIAPInvoke))]
    [XmlInclude(typeof (tP_OSIAPTitle))]
    [XmlInclude(typeof (tP_OSIPSEL))]
    [XmlInclude(typeof (tP_OSISSEL))]
    [XmlInclude(typeof (tP_OSITSEL))]
    [XmlInclude(typeof (tP_OSINSAP))]
    [XmlInclude(typeof (tP_IPGATEWAY))]
    [XmlInclude(typeof (tP_IPSUBNET))]
    [XmlInclude(typeof (tP_IP))]
    [XmlInclude(typeof (tP_MmsPort))]
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tP
    {
        private tPType _typeField;
        private string _valueField;

        public tP()
        {
            this._typeField = new tPType();
        }

        [XmlIgnore]
        public tPType PType
        {
            get { return this._typeField; }
            set { this._typeField = value; }
        }
        
        [Required]
        [XmlAttribute]
        [Category("P"), Description("јтрибут, определ€ющий значение"), ReadOnly(true)]
        public string type
        {
            get { return this._typeField.type; }
            set { this._typeField.type = value; }
        }

        [XmlText(DataType = "normalizedString")]
        [Category("P"), Description("«начение")]
        public string Value
        {
            get { return this._valueField; }
            set { this._valueField = value; }
        }
    }
    
    public class tPType
    {
        public tPType()
        {
            this.type = tPTypeEnum.IP.ToString();
        }

        public tPType(tPTypeEnum t)
        {
            this.type = t.ToString().Replace('_', '-');
        }

        public string type { get; set; }

        public tPTypeEnum typeEnum
        {
            get
            {
                try
                {
                    return (tPTypeEnum)Enum.Parse(typeof(tPTypeEnum), this.type.Replace('-','_'));
                }
                catch (Exception)
                {
                    return tPTypeEnum.EXTENSION;
                }
            }
            set { this.type = value.ToString().Replace('_', '-'); }
        }
    }

    [Serializable]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public enum tPTypeEnum
    {
        IP,
        [XmlEnum("IP-SUBNET")] IP_SUBNET,
        [XmlEnum("IP-GATEWAY")] IP_GATEWAY,
        [XmlEnum("OSI-NSAP")] OSI_NSAP,
        [XmlEnum("OSI-TSEL")] OSI_TSEL,
        [XmlEnum("OSI-SSEL")] OSI_SSEL,
        [XmlEnum("OSI-PSEL")] OSI_PSEL,
        [XmlEnum("OSI-AP-Title")] OSI_AP_Title,
        [XmlEnum("OSI-AP-Invoke")] OSI_AP_Invoke,
        [XmlEnum("OSI-AE-Qualifier")] OSI_AE_Qualifier,
        [XmlEnum("OSI-AE-Invoke")] OSI_AE_Invoke,
        [XmlEnum("MAC-Address")] MAC_Address,
        [XmlEnum("APPID")] APPID,
        [XmlEnum("VLAN-PRIORITY")] VLAN_PRIORITY,
        [XmlEnum("VLAN-ID")] VLAN_ID,
        [XmlEnum("MMS-Port")] MMS_PORT,
        EXTENSION // For custom types
    }

    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(TypeName = "tP_VLAN-ID", Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tP_VLANID : tP
    {
        public tP_VLANID()
        {
            PType = new tPType(tPTypeEnum.VLAN_ID);
        }
    }

    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(TypeName = "tP_VLAN-PRIORITY", Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tP_VLANPRIORITY : tP
    {
        public tP_VLANPRIORITY()
        {
            PType = new tPType(tPTypeEnum.VLAN_PRIORITY);
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]


    public class tP_APPID : tP
    {
        public tP_APPID()
        {
            PType = new tPType(tPTypeEnum.APPID);
        }
    }

    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(TypeName = "tP_MAC-Address", Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tP_MACAddress : tP
    {
        public tP_MACAddress()
        {
            PType = new tPType(tPTypeEnum.MAC_Address);
        }
    }

    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(TypeName = "tP_OSI-AE-Invoke", Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tP_OSIAEInvoke : tP
    {
        public tP_OSIAEInvoke()
        {
            PType = new tPType(tPTypeEnum.OSI_AE_Invoke);
        }
    }

    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(TypeName = "tP_OSI-AE-Qualifier", Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tP_OSIAEQualifier : tP
    {
        public tP_OSIAEQualifier()
        {
            PType = new tPType(tPTypeEnum.OSI_AE_Qualifier);
        }
    }

    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(TypeName = "tP_OSI-AP-Invoke", Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tP_OSIAPInvoke : tP
    {
        public tP_OSIAPInvoke()
        {
            PType = new tPType(tPTypeEnum.OSI_AP_Invoke);
        }
    }

    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(TypeName = "tP_OSI-AP-Title", Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tP_OSIAPTitle : tP
    {
        public tP_OSIAPTitle()
        {
            PType = new tPType(tPTypeEnum.OSI_AP_Title);
        }
    }

    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(TypeName = "tP_OSI-PSEL", Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tP_OSIPSEL : tP
    {
        public tP_OSIPSEL()
        {
            PType = new tPType(tPTypeEnum.OSI_PSEL);
        }
    }

    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(TypeName = "tP_OSI-SSEL", Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tP_OSISSEL : tP
    {
        public tP_OSISSEL()
        {
            PType = new tPType(tPTypeEnum.OSI_SSEL);
        }
    }

    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(TypeName = "tP_OSI-TSEL", Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tP_OSITSEL : tP
    {
        public tP_OSITSEL()
        {
            PType = new tPType(tPTypeEnum.OSI_TSEL);
        }
    }

    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(TypeName = "tP_OSI-NSAP", Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tP_OSINSAP : tP
    {
        public tP_OSINSAP()
        {
            PType = new tPType(tPTypeEnum.OSI_NSAP);
        }
    }

    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(TypeName = "tP_IP-GATEWAY", Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tP_IPGATEWAY : tP
    {
        public tP_IPGATEWAY()
        {
            PType = new tPType(tPTypeEnum.IP_GATEWAY);
        }
    }

    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(TypeName = "tP_IP-SUBNET", Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tP_IPSUBNET : tP
    {
        public tP_IPSUBNET()
        {
            PType = new tPType(tPTypeEnum.IP_SUBNET);
        }
    }

    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tP_IP : tP
    {
        public tP_IP()
        {
            PType = new tPType(tPTypeEnum.IP);
        }
    }

    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(TypeName = "tP_MMS-Port", Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tP_MmsPort : tP
    {
        public tP_MmsPort()
        {
            PType = new tPType(tPTypeEnum.MMS_PORT);
        }
    }
}