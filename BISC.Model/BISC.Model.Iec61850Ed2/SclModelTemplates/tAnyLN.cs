using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;
using BISC.Model.Iec61850Ed2.DataTypeTemplates;
using BISC.Model.Iec61850Ed2.DataTypeTemplates.Base;
using BISC.Model.Iec61850Ed2.SclModelTemplates.Controls;
using BISC.Model.Iec61850Ed2.SclModelTemplates.DataSet;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates
{
    [XmlInclude(typeof (tLN0))]
    [XmlInclude(typeof (tLN))]
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tAnyLN : tUnNaming
    {
        private List<tDataSet> _dataSet;
        private List<tDOI> _doi;
        private string _name;

        public tAnyLN()
        {
            this.DataSet = new List<tDataSet>();
            this.ReportControl = new List<tReportControl>();
            this.LogControl = new List<tLogControl>();
            this._doi = new List<tDOI>();
        }

        [XmlElement("DataSet")]
        public List<tDataSet> DataSet
        {
            get { return _dataSet; }
            set
            {
                _dataSet = value;
            }
        }

        [XmlElement("ReportControl")]
        public List<tReportControl> ReportControl { get; private set; }

        [XmlElement("LogControl")]
        public List<tLogControl> LogControl { get; private set; }

        [XmlElement("DOI")]
        public List<tDOI> DOI
        {
            get { return _doi; }
            set { _doi = value; }
        }
        

        [XmlElement("Inputs")]
        public tInputs Inputs { get; set; }

        [XmlAttribute(DataType = "normalizedString"), Required, ReadOnly(true)]
        [Category("LN"), Description("Ссылка на определение типа данного LN")]
        public string lnType { get; set; }

        public bool AddDataSet(tDataSet ds)
        {
            if (this.DataSet.Any(datSet => datSet.name == ds.name)) return false;
            this.DataSet.Add(ds);
            ds.Parent = this;
            return true;
        }

        public bool AddReportControl(tReportControl rcInDevice)
        {
            tReportControl repcOld = new tReportControl();
            repcOld = ReportControl.Find(repc => repc.rptID.Contains(rcInDevice.rptID));
            if (repcOld != null)
            {
                rcInDevice.Parent = repcOld;
                rcInDevice.IsInDevice = true;
                repcOld.ReportControlsInDevice.Add(rcInDevice);
                repcOld.RptEnabled =new tRptEnabled(++repcOld.RptEnabled.max);
                return false;
            }
            if (repcOld == null)
            {
                tReportControl newReportControl = new tReportControl();
                newReportControl = (tReportControl) rcInDevice.Clone();
                IEnumerable<char> enumerable =
                    newReportControl.name.Substring(rcInDevice.name.Length - 2, 2)
                        .ToCharArray()
                        .Where(c => Char.IsDigit(c));
                if (enumerable.Count() == 2)
                {
                    newReportControl.name = newReportControl.name.Substring(0, rcInDevice.name.Length - 2);
                    newReportControl.IsInDevice = false;
                }
                ReportControl.Add(newReportControl);
                rcInDevice.Parent = newReportControl;
                rcInDevice.IsInDevice = true;
                newReportControl.ReportControlsInDevice.Add(rcInDevice);
            }

            return true;
        }



        public void AddDOI(tDOI doi)
            // проверка на то, есть ли уже DOI с таким именем, если есть, то проверяется и SDI внутри него
        {
            tDOI findedDoi = this.DOI.Find(a => a.name == doi.name);
            if (findedDoi != null)
            {
                if ((doi.SDI.Count != 0) && (findedDoi.SDI.Count != 0))
                {
                    foreach (tSDI sdi in doi.SDI)
                    {
                        tSDI findedSdi = findedDoi.SDI.Find(b => b.name == sdi.name);
                        if (findedSdi != null)
                        {
                            AddDAIandSDItoSDI(findedSdi, sdi); //в случае присутствия таких же SDI

                        }
                    }
                    return;
                }
                AddDAIandSDItoDOI(doi, findedDoi); //в случае отсутствия таких же SDI
                return;
            }
            if (doi != null)
                this.DOI.Add(doi);
        }

        private void AddDAIandSDItoSDI(tSDI findedSdi, tSDI sdiToAdd) //объединение внутренних списков
        {
            if (sdiToAdd.SDI.Count != 0)
            {
                findedSdi.SDI.AddRange(sdiToAdd.SDI.ToArray());
            }
            if (sdiToAdd.DAI.Count != 0)
            {
                findedSdi.DAI.AddRange(sdiToAdd.DAI.ToArray());
            }
        }

        public void AddDAIandSDItoDOI(tDOI doiToAdd, tDOI findedDoi)
        {
            if (doiToAdd.SDI.Count != 0)
            {
                findedDoi.SDI.AddRange(doiToAdd.SDI.ToArray());
            }
            if (doiToAdd.DAI.Count != 0)
            {
                findedDoi.DAI.AddRange(doiToAdd.DAI.ToArray());
            }
        }

        [XmlIgnore, Browsable(false)]
        public tLNodeType NodeType { get; set; }

        //#region Implementation of IHavingChildCollection
        //[XmlIgnore, Browsable(false)]
        //public List<INamableSclItem> ChildNamableCollection
        //{
        //    get
        //    {


        //        List<INamableSclItem> namables = new List<INamableSclItem>();
              
        //        DOI.ForEach((doi =>
        //        {
        //            namables.Add(doi);
        //        }));

        //        return namables;
        //    }
        //}

        //#endregion


    }
}