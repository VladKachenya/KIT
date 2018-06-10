using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Controls
{
    [XmlInclude(typeof (tSampledValueControl))]
    [XmlInclude(typeof (tGSEControl))]
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tControlWithIEDName : tControl
    {
        private string[] _IEDNameField;
        private uint confRevField;
        
        [XmlElement("IEDName", DataType = "normalizedString")]
        [Category("ControlWithIEDName"), Description("Имя IED-устройства"), Browsable(false)]
        public string[] IEDName
        {
            get { return this._IEDNameField; }
            set { this._IEDNameField = value; }
        }

        [XmlAttribute]
        [Category("ControlWithIEDName"), Description("Номер конфигурации ревизии блока управления")]
        public uint confRev
        {
            get { return this.confRevField; }
            set { this.confRevField = value; }
        }
    }
}