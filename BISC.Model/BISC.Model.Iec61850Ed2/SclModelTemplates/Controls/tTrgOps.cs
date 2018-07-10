using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Controls
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tTrgOps:ICloneable
    {

        public tTrgOps()
        {
            this.dchg = false;
            this.dupd = false;
            this.qchg = false;
            this.period = false;
        }

        public tTrgOps(TriggerOptions triggerOptions)
        {
            this.dchg = false;
            this.dupd = false;
            this.qchg = false;
            this.period = false;
            if (triggerOptions.HasFlag(TriggerOptions.DATA_CHANGED))
            {
                this.dchg = true;
            }
            if (triggerOptions.HasFlag(TriggerOptions.QUALITY_CHANGED))
            {
                this.qchg = true;
            }
            if (triggerOptions.HasFlag(TriggerOptions.DATA_UPDATE))
            {
                this.dupd = true;
            }
            if (triggerOptions.HasFlag(TriggerOptions.INTEGRITY))
            {
                this.period = true;
            }
            if (triggerOptions.HasFlag(TriggerOptions.GI))
            {
                this.gi = true;
            }
        }

        [XmlAttribute]
        [Category("TrgOps"),
         Description("Флаг того, что изменение значения атрибута должно идти в отчет")]
        public bool dchg { get; set; }

        [XmlAttribute]
        [Category("TrgOps"),
         Description("Флаг того, что изменение значения качества атрибута должно идти в отчет")]
        public bool qchg { get; set; }

        [XmlAttribute]
        [Category("TrgOps"),
         Description("Флаг, который обозначает, что отчет или запись в журнале должны быть сформированы " +
                     "по фиксации значения устанновленного аттрибута или по изменению любого другого атрибута")]
        public bool dupd { get; set; }

        [XmlAttribute]
        [Category("TrgOps"), Description("Актуальность целостности периода")]
        public bool period { get; set; }
        [XmlAttribute]
        public bool gi { get; set; }
        public TriggerOptions AsTriggerOptEnum
        {
            get
            {
                TriggerOptions to = TriggerOptions.NONE;
                if(dchg)to|= TriggerOptions.DATA_CHANGED;
                if(dupd)to|= TriggerOptions.DATA_UPDATE;
                if(period)to|= TriggerOptions.INTEGRITY;
                if(qchg)to|= TriggerOptions.QUALITY_CHANGED;
                if (gi) to |= TriggerOptions.GI;
                return to;
            }
        }

        public object Clone()
        {
           tTrgOps newTrgOps=new tTrgOps(AsTriggerOptEnum);
            return newTrgOps;
        }
    }
}