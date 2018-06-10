using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;

namespace BISC.Model.Iec61850Ed2.DataTypeTemplates
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlInclude(typeof(DADataType))]
    [XmlInclude(typeof(SDIDADataTypeBDA))]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tDA : tAbstractDataAttribute
    {
      public tDA()
        {
            fc = tFCEnum.NONE;
        }


        [XmlIgnore]
        [Category("DA"), Description("Опция срабатывания по изменению качества")]
        public bool? qchg { get; set; }

        [XmlAttribute("qchg"), Browsable(false)]
        public string qchgAsText
        {
            get
            {
                if (this.qchg.HasValue)
                    return this.qchg.Value.ToString();
                else
                    return null;
            }
            set
            {
                if (value != null)
                    this.qchg = bool.Parse(value);
                else
                    this.qchg = null;
            }
        }




        [XmlIgnore]
        [Category("DA"), Description("Опция срабатывания по изменению данных")]
        public bool? dchg { get; set; }
        


        [XmlAttribute("dchg"), Browsable(false)]
        public string dchgAsText
        {
            get
            {
                if (this.dchg.HasValue)
                    return this.dchg.Value.ToString();
                else
                    return null;
            }
            set
            {
                if (value != null)
                    this.dchg = bool.Parse(value);
                else
                    this.dchg = null;
            }
        }








        [XmlIgnore]
        [Category("DA"), Description("Опция срабатывания по обновлению значения данных")]
        public bool? dupd { get; set; }

        [XmlAttribute("dupd"), Browsable(false)]
        public string dupdAsText
        {
            get
            {
                if (this.dupd.HasValue)
                    return this.dupd.Value.ToString();
                else
                    return null;
            }
            set
            {
                if (value != null)
                    this.dupd = bool.Parse(value);
                else
                    this.dupd = null;
            }
        }






        [Required, XmlAttribute, ReadOnly(true)]
        [Category("DA"), Description("Функциональная связь для данного атрибута")]
        public tFCEnum fc { get; set; }
    }
}