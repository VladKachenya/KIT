using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.DataTypeTemplates.Base;
using BISC.Model.Iec61850Ed2.SclModelTemplates.Controls;
using BISC.Model.Iec61850Ed2.SclModelTemplates.DataSet;
using BISC.Model.Iec61850Ed2.SclModelTemplates.Services;
using BISC.Model.Iec61850Ed2.TreeHelpers;
using BISC.Model.Infrastructure.Device;
using Microsoft.Practices.ObjectBuilder2;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    [XmlRoot("IED", Namespace = "http://www.iec.ch/61850/2003/SCL", IsNullable = false)]
    public class tIED : tNaming
    {
        private ObservableCollection<tAccessPoint> _accessPoint;
        public tIED()
        {
            name = "TEMPLATE";
            this.AccessPoint = new ObservableCollection<tAccessPoint>();
        }

        [Category("IED"), Description("Все сервисы данного IED-продукта"), Browsable(false)]
        public tServices Services { get; set; }

        [XmlElement("AccessPoint")]
        [Category("IED"), Browsable(false)]
        public ObservableCollection<tAccessPoint> AccessPoint
        {
            get { return _accessPoint; }
            set
            {
                _accessPoint = value;
            }

        }

        [XmlAttribute(DataType = "normalizedString"), ReadOnly(true)]
        [Category("IED"), Description("Тип IED-продукта. Определяется производителем")]
        public string type { get; set; }

        [XmlAttribute(DataType = "normalizedString"), ReadOnly(true)]
        [Category("IED"), Description("Наименование производителя")]
        public string manufacturer { get; set; }

        [XmlAttribute(DataType = "normalizedString"), ReadOnly(true)]
        [Category("IED"), Description("Версия базовой конфигураии данного устройства")]
        public string configVersion { get; set; }


        /// <summary>
        /// Добавляет новую точку доступа к IED 
        /// </summary>
        /// <param name="apName">
        /// Название точки доступа. Название должно быть уникальным и не дублироваться с уже имеющимися
        /// </param>
        /// <returns>
        /// A <see cref="System.Boolean"/> with the index of the new Access Point added.
        /// </returns>
        public bool AddAP(string apName)
        {
            if (string.IsNullOrEmpty(apName)) return false;
            tAccessPoint ap = new tAccessPoint();
            ap.name = apName;
            ap.Server = new tServer();
            ap.Server.Authentication = new tServerAuthentication();
            if (this.AccessPoint.Any(t => t.name.Equals(apName)))
            {
                return false;
            }
            this.AccessPoint.Add(ap);
            return true;
        }

        public bool AddLd(tLDevice lDevice, string apName)
        {
            var ap = AccessPoint.FirstOrDefault((point => point.name == apName));
            if (ap == null) return false;
            return ap.Server.AddLDevice(lDevice);
        }


        public List<string> GetDataSetNamesList()
        {
            List < string > dataSetList=new List<string>();
            foreach (var ap in AccessPoint)
            {
                ap.Server.LDevice.ForEach((device =>
                {
                    device.LN0.DataSet.ForEach((set =>
                    {
                        dataSetList.Add(set.name);
                    } ));
                }));
            }
            return dataSetList;
        }

       


        public void TransferValuesFromDeviceToLocal()
        {
            DataTransferingHelper.TransferValuesFromDeviceToLocal(this);
            IsLocalDataTransferred = true;
        }
        [XmlIgnore]
        public bool IsLocalDataTransferred { get; set; }


        public List<tReportControl> GetAllReportsOfIed()
        {
            List<tReportControl> resultReportControls=new List<tReportControl>();
            foreach (var lDevice in AccessPoint[0].Server.LDevice)
            {
                foreach (var ln in lDevice.LN)
                {
                    if (ln.ReportControl.Count > 0)
                    {
                        resultReportControls.AddRange(ln.ReportControl);
                    }
                }
                if (lDevice.LN0.ReportControl.Count > 0)
                {
                    resultReportControls.AddRange(lDevice.LN0.ReportControl);
                }
            }
            return resultReportControls;
        }

        public List<tDataSet> GetAllDataSetsOfIed()
        {
            List<tDataSet> dataSets=new List<tDataSet>();
            foreach (var lDevice in AccessPoint[0].Server.LDevice)
            {
                foreach (var ln in lDevice.LN)
                {
                    if (ln.DataSet.Count > 0)
                    {
                        dataSets.AddRange(ln.DataSet);
                    }
                }
                if (lDevice.LN0.DataSet.Count > 0)
                {
                    dataSets.AddRange(lDevice.LN0.DataSet);
                }
            }
            return dataSets;
        }


        public List<tGSEControl> GetAllGoCbsOfIed()
        {
            List<tGSEControl> gseControls=new List<tGSEControl>();

            foreach (var lDevice in AccessPoint[0].Server.LDevice)
            {
                if (lDevice.LN0.GSEControl.Count > 0)
                {
                    gseControls.AddRange(lDevice.LN0.GSEControl);
                }
            }
            return gseControls;
        }
    }
}