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
    [XmlType(AnonymousType = true, Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tReportControlOptFields : ICloneable
    {

        #region C'tors

        public tReportControlOptFields()
        {
            this.seqNum = false;
            this.timeStamp = false;
            this.dataSet = false;
            this.reasonCode = false;
            this.dataRef = false;
            this.entryID = false;
            this.configRef = false;
            this.bufOvfl = false;
            this.segmentation = false;
        }

        public tReportControlOptFields(ReportOptions reportOptions)
        {

            this.seqNum = false;
            this.timeStamp = false;
            this.dataSet = false;
            this.reasonCode = false;
            this.dataRef = false;
            this.entryID = false;
            this.configRef = false;
            this.bufOvfl = false;
            this.segmentation = false;
            //if (reportOptions.HasFlag(IEDExplorer.ReportOptions.NONE))
            //{
            //    return;
            //}
            //if (reportOptions.HasFlag(ReportOptions.ALL))
            //{
            //    this.seqNum = true;
            //    this.timeStamp = true;
            //    this.dataSet = true;
            //    this.reasonCode = true;
            //    this.dataRef = true;
            //    this.entryID = true;
            //    this.configRef = true;
            //    this.bufOvfl = true;
            //    this.segmentation = true;
            //    return;
            //}
            if (reportOptions.HasFlag(ReportOptions.BUFFER_OVERFLOW))
            {
                this.bufOvfl = true;
            }
            if (reportOptions.HasFlag(ReportOptions.CONF_REV))
            {
                this.configRef = true;
            }
            if (reportOptions.HasFlag(ReportOptions.DATA_REFERENCE))
            {
                this.dataRef = true;
            }
            if (reportOptions.HasFlag(ReportOptions.DATA_SET))
            {
                this.dataSet = true;
            }
            if (reportOptions.HasFlag(ReportOptions.ENTRY_ID))
            {
                this.entryID = true;
            }
            if (reportOptions.HasFlag(ReportOptions.REASON_FOR_INCLUSION))
            {
                this.reasonCode = true;
            }
            if (reportOptions.HasFlag(ReportOptions.SEQ_NUM))
            {
                this.seqNum = true;
            }
            if (reportOptions.HasFlag(ReportOptions.TIME_STAMP))
            {
                this.timeStamp = true;
            }
            if (reportOptions.HasFlag(ReportOptions.SEGMENTATION))
            {
                this.segmentation = true;
            }
        }

        #endregion

        [XmlIgnore, Browsable(false)]
        public ReportOptions AsReportOptionsEnum
        {
            get
            {
                ReportOptions ro = ReportOptions.NONE;
                if (bufOvfl) ro |= ReportOptions.BUFFER_OVERFLOW;
                if (configRef) ro |= ReportOptions.CONF_REV;
                if (dataRef) ro |= ReportOptions.DATA_REFERENCE;
                if (dataSet) ro |= ReportOptions.DATA_SET;
                if (entryID) ro |= ReportOptions.ENTRY_ID;
                if (reasonCode) ro |= ReportOptions.REASON_FOR_INCLUSION;
                if (timeStamp) ro |= ReportOptions.TIME_STAMP;
                if (seqNum) ro |= ReportOptions.SEQ_NUM;
                if (segmentation) ro |= ReportOptions.SEGMENTATION;

                return ro;
            }
        }


        [XmlAttribute]
        [Category("ReportControlOptFields"), Description("Текущий номер отчета")]
        public bool seqNum { get; set; }

        [XmlAttribute]
        [Category("ReportControlOptFields"),
         Description("Представляет UTC-время")]
        public bool timeStamp { get; set; }

        [XmlAttribute]
        [Category("ReportControlOptFields"), Description("Имя DATASET")]
        public bool dataSet { get; set; }

        [XmlAttribute]
        [Category("ReportControlOptFields"), Description("Причина включения в отчет")]
        public bool reasonCode { get; set; }

        [XmlAttribute]
        [Category("ReportControlOptFields"), Description("Ссылка на DO")]
        public bool dataRef { get; set; }

        [XmlAttribute]
        [Category("ReportControlOptFields"),
         Description("Используется для идентификации записи")]
        public bool entryID { get; set; }

        [XmlAttribute]
        [Category("ReportControlOptFields"), Description("Ссылка конфигурации")]
        public bool configRef { get; set; }

        [XmlAttribute]
        [Category("ReportControlOptFields"), Description("Переполнение буфера")]
        public bool bufOvfl { get; set; }

        [XmlAttribute]
        [Category("ReportControlOptFields"), Description("Сегментация")]
        public bool segmentation { get; set; }

        public object Clone()
        {
            tReportControlOptFields newOptF = new tReportControlOptFields(AsReportOptionsEnum);
            return newOptF;
        }
    }
}