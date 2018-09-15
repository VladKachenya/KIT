using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Global;
using BISC.Model.Iec61850Ed2.Common;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.Controls
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tReportControl : tControlWithTriggerOpt, ICloneable{
        private ObservableCollection<tReportControl> _reportControlsInDevice;
        private bool _isDynamic;

        public tReportControl()
        {
            this.confRev = 0;
            this.buffered = false;
            this.bufTime = 0;
            IsInDevice = false;
            RptEnabled = new tRptEnabled();
            TrgOps=new tTrgOps();
            OptFields=new tReportControlOptFields();
            ReportControlsInDevice = new ObservableCollection<tReportControl>();
        }

        [XmlElement, ReadOnly(true)]
        [Category("ReportControl"), Description("Опциональные поля, включаемые в отчет"),
         Browsable(false)]
        public tReportControlOptFields OptFields { get; set; }

        [XmlElement, ReadOnly(true)]
        [Category("ReportControl"),
         Description("Разрешение выдачи отчета"),
         Browsable(false)]
        public tRptEnabled RptEnabled { get; set; }

        [Required]
        [XmlAttribute(DataType = "normalizedString"), ReadOnly(true)]
        [Category("ReportControl"), Description("Идентификатор отчета")]
        public string rptID { get; set; }

        [Required]
        [XmlAttribute, ReadOnly(true), DisplayName("Config Revision")]
        [Category("ReportControl"), Description("Ревизия конфигурации")]
        public uint confRev { get; set; }


        [Required]
        [XmlAttribute, ReadOnly(false), DisplayName("GI")]
        public bool GI { get; set; }

        [XmlAttribute, ReadOnly(true), DisplayName("Buffered")]
        public bool buffered { get; set; }

        [XmlAttribute]
        [Category("ReportControl"), Description("Буферное время"), DisplayName("Buffer Time")]
        public uint bufTime { get; set; }

        [XmlIgnore, ReadOnly(false), Browsable(true)]
        public bool RptEna { get; set; }

        [XmlIgnore, Browsable(false)]
        public bool IsInDevice { get; set; }

        [XmlIgnore, ReadOnly(false), Browsable(false)]
        public ObservableCollection<tReportControl> ReportControlsInDevice
        {
            get { return _reportControlsInDevice; }
            set
            {
                _reportControlsInDevice = value;
            }
        }


        [XmlIgnore]
        public bool seqNum
        {
            get { return this.OptFields.seqNum; }
            set { this.OptFields.seqNum = value; }
        }

        [XmlIgnore]
        [Category("Optional Fields"),
         Description("UTC-время"), DisplayName("TimeStamp")]
        public bool timeStamp
        {
            get { return this.OptFields.timeStamp; }
            set { this.OptFields.timeStamp = value; }
        }

        [XmlIgnore]
        [Category("Optional Fields"), Description("Имя DATASET"), DisplayName("Data Set")]
        public bool dataSet
        {
            get { return this.OptFields.dataSet; }
            set { this.OptFields.dataSet = value; }
        }

        [XmlIgnore]
        [Category("Optional Fields"), Description("Причина включения в отчет"), DisplayName("Reason Code")]
        public bool reasonCode
        {
            get { return this.OptFields.reasonCode; }
            set { this.OptFields.reasonCode = value; }
        }
        [XmlIgnore]
        public bool dataRef
        {
            get { return this.OptFields.dataRef; }
            set { this.OptFields.dataRef = value; }
        }

        [XmlIgnore]
        public bool entryID
        {
            get { return this.OptFields.entryID; }
            set { this.OptFields.entryID = value; }
        }

        [XmlIgnore]
        [Category("Optional Fields"), Description("Ссылка конфигурации"), DisplayName("Config Reference")]
        public bool configRef
        {
            get { return this.OptFields.configRef; }
            set { this.OptFields.configRef = value; }
        }

        [XmlIgnore]
        [Category("Optional Fields"), Description("Переполнение буфера"), DisplayName("Buffer Overflow")]
        public bool bufOvfl
        {
            get { return this.OptFields.bufOvfl; }
            set { this.OptFields.bufOvfl = value; }
        }

        [XmlIgnore]
        [Category("Optional Fields"), Description("Сегментация"), DisplayName("Segmentation"), ReadOnly(true),
         Browsable(false)]
        public bool segmentation
        {
            get { return this.OptFields.segmentation; }
            set { this.OptFields.segmentation = value; }
        }






        [XmlIgnore]
        [Category("Trigger Options"),
         Description("Флаг того, что изменение значения атрибута должно идти в отчет"), DisplayName("Data Changed")]
        public bool dchg
        {
            get { return this.TrgOps.dchg; }
            set { this.TrgOps.dchg = value; }
        }

        [XmlIgnore]
        [Category("Trigger Options"),
         Description("Флаг того, что изменение значения качества атрибута должно идти в отчет"),
         DisplayName("Quality Changed")]
        public bool qchg
        {
            get { return this.TrgOps.qchg; }
            set { this.TrgOps.qchg = value; }
        }


        [XmlIgnore]
        [Category("Trigger Options"),
         Description("Флаг, который обозначает, что отчет или запись в журнале должны быть сформированы " +
                     "по фиксации значения устанновленного атрибута или по изменению любого другого атрибута"),
         DisplayName("Data Update")]
        public bool dupd
        {
            get { return this.TrgOps.dupd; }
            set { this.TrgOps.dupd = value; }
        }

        [XmlIgnore]
        [Category("Trigger Options"), Description("Актуальность целостности периода"), DisplayName("Cyclic")]
        public bool period
        {
            get { return this.TrgOps.period; }
            set { this.TrgOps.period = value; }
        }
        

        public object Clone()
        {
            tReportControl newrc = new tReportControl();
            newrc.RptEnabled = (tRptEnabled) RptEnabled.Clone();
            newrc.rptID = rptID;
            newrc.TrgOps = (tTrgOps) TrgOps.Clone();
            newrc.OptFields = (tReportControlOptFields) OptFields.Clone();
            newrc.RptEna = RptEna;
            newrc.name = name;
            newrc.dataSet = dataSet;
            newrc.configRef = configRef;
            newrc.Parent = Parent;
            newrc.datSet = datSet;
            newrc.intgPd = intgPd;
            newrc.desc = desc;
            newrc.Text = Text;
            newrc.bufTime = bufTime;
            newrc.confRev = confRev;
            newrc.buffered = buffered;
            newrc.IsInDevice = IsInDevice;
            newrc.GI = GI;
            newrc.IsDynamic = IsDynamic;
            return newrc;
        }

     

      
        public bool ShouldSerializeIsDynamic()
        {
            return StaticSerializingDirectives.IsStaticDynamicItemsDemarcationShouldBeSerialized;
        }


        #region Implementation of IReportControl
        [XmlAttribute]
        public bool IsDynamic
        {
            get { return _isDynamic; }
            set { _isDynamic = value; }
        }

        #endregion
    }
}