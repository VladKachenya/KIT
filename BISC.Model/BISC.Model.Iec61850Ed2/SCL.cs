using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.DataTypeTemplates;
using BISC.Model.Iec61850Ed2.DataTypeTemplates.Base;
using BISC.Model.Iec61850Ed2.SclModelTemplates;
using BISC.Model.Iec61850Ed2.SclModelTemplates.Communication;
using BISC.Model.Iec61850Ed2.SclModelTemplates.Controls;
using BISC.Model.Iec61850Ed2.SclModelTemplates.DataSet;
using BISC.Model.Iec61850Ed2.SclModelTemplates.Substation;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Connection;

namespace BISC.Model.Iec61850Ed2
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.iec.ch/61850/2003/SCL")]
    [XmlRoot(Namespace = "http://www.iec.ch/61850/2003/SCL", IsNullable = false)]
    public class SCL : tBaseElement
    {
        private ObservableCollection<tIED> _ied;
        private string _projectDesc;

        public SCL()
        {
            this.Header = new tHeader();
            this.Substation = new List<tSubstation>();
            this.IED = new ObservableCollection<tIED>();
            _projectDesc = "New Project";
            DataTypeTemplates = new tDataTypeTemplates();
        }

        [XmlElement, Category("SCL"), Browsable(false)]
        public tHeader Header { get; set; }

        [XmlElement("Substation")]
        [Category("SCL"), Browsable(false)]
        public List<tSubstation> Substation { get; set; }

        [XmlElement, Category("SCL"), Browsable(false)]
        public tCommunication Communication { get; set; }

        [XmlElement("IED")]
        [Category("SCL"), Browsable(false)]
        public ObservableCollection<tIED> IED
        {
            get { return _ied; }
            set
            {
                _ied = value;
            }
        }

        [XmlElement, Category("SCL"), Browsable(false)]
        public tDataTypeTemplates DataTypeTemplates { get; set; }


        public tConnectedAP CreateConnectionAccsessPoint(string ip)
        {
            if (this.Communication == null)
            {
                tConnectedAP ap = new tConnectedAP("P1");
                ap.CreateAddress(ip);
                tSubNetwork sn = new tSubNetwork();
                sn.AddConnectedAP(ap);
                tCommunication communication = new tCommunication();
                communication.AddSubNetwork(sn);
                this.Communication = communication;
                return ap;
            }
            else
            {
                tConnectedAP ap = this.Communication.GetConnectedApOnIp(ip);
                if (ap == null)
                {
                    ap = new tConnectedAP("P1");
                    ap.CreateAddress(ip);
                    this.Communication.SubNetwork.First().AddConnectedAP(ap);
                }
                return ap;
            }
        }

        public void AddIed(tIED ied,bool isToReplace)
        {
            if (this.IED.All(i => i.name != ied.name))
            {
                this.IED.Add(ied);
            }
            else if(isToReplace) // TODO сделано для того,чтобы не было повторяющихся устройств, перезаписываются данные в существующем
            {
                tIED curIed = this.IED.First(i => i.name == ied.name);
                this.IED.Remove(curIed);
                this.IED.Add(ied);
            }

        }


        public void RemoveIed(string iedName)
        {
            var iedTodel = IED.FirstOrDefault((ied => ied.name == iedName));
            if (iedTodel != null)
            {
                IED.Remove(iedTodel);
                RemoveUnusedDataTypeTemplates();
                Communication.SubNetwork[0].ConnectedAP =
                    Communication.SubNetwork[0].ConnectedAP.Where((ap => ap.iedName != iedName)).ToList();
            }

        }

        public void MergeCommunication(tCommunication communication, string iedName)
        {
            if (!IED.Any((ied => ied.name == iedName)))
            {
                Communication.SubNetwork[0].ConnectedAP.Add(communication.SubNetwork[0].ConnectedAP
                    .First((ap => ap.iedName == iedName)));
            }
        }

        public void MergeDataTypeTemplates(tDataTypeTemplates dataTypeTemplates)
        {
            foreach (var daType in dataTypeTemplates.DAType)
            {
                DataTypeTemplates.AddDAType(daType);
            }

            foreach (var doType in dataTypeTemplates.DOType)
            {
                DataTypeTemplates.AddDOType(doType);
            }

            foreach (var enumType in dataTypeTemplates.EnumType)
            {
                DataTypeTemplates.AddEnumType(enumType);
            }

            foreach (var lNodeType in dataTypeTemplates.LNodeType)
            {
                DataTypeTemplates.AddLNodeType(lNodeType);
            }
        }

        public void RemoveUnusedDataTypeTemplates()
        {

            BindTypes();
            DataTypeTemplates.RemoveUnused();
        }


        public void BindTypes()
        {
            DataTypeTemplates.MarkAllAsUnused();
            foreach (tIED ied in IED)
            {
                try
                {
                    ied.Parent = this;
                    //   tControl.datSetCollection=new ObservableCollection<string>();
                    foreach (tLDevice ld in ied.AccessPoint[0].Server.LDevice)
                    {
                        ld.LN0.NodeType = DataTypeTemplates.LNodeType.Find(t => t.id == ld.LN0.lnType);
                        ld.LN0.NodeType.IsInUse = true;
                        ld.LN0.Parent = ld;
                        ld.Parent = ied;
                        foreach (tLN ln in ld.LN)
                        {
                            ln.NodeType = DataTypeTemplates.LNodeType.Find(t => t.id == ln.lnType);
                            ln.NodeType.IsInUse = true;
                            ln.Parent = ld;
                            foreach (tDOI doi in ln.DOI)
                            {


                                doi.Parent = ln;

                                // string path = ied.name + ld.inst + "." + ln.lnClass + ln.inst + "." + doi.name;
                                try
                                {
                                    tDO dot = ln.NodeType.DO.Find(t => t.name == doi.name);

                                    doi.DoType = DataTypeTemplates.DOType.Find(t => t.id == dot.type);
                                    doi.DoType.IsInUse = true;
                                    BindSdiAndDai(doi.DAI, doi.SDI, null, doi.DoType, doi);
                                }
                                catch (Exception e)
                                {


                                }
                            }
                            if (ln.ReportControl.Count() != 0)
                            {
                                foreach (tReportControl rc in ln.ReportControl)
                                {
                                    rc.Parent = ln;
                                }
                            }
                        }
                        if (ld.LN0.ReportControl.Count() != 0)
                        {
                            foreach (tReportControl rc in ld.LN0.ReportControl)
                            {
                                rc.Parent = ld.LN0;
                            }
                        }
                        foreach (tDOI doi in ld.LN0.DOI)
                        {


                            doi.Parent = ld.LN0;
                            tDO dot = ld.LN0.NodeType.DO.Find(t => t.name == doi.name);
                            doi.DoType = DataTypeTemplates.DOType.Find(t => t.id == dot.type);
                            doi.DoType.IsInUse = true;

                            BindSdiAndDai(doi.DAI, doi.SDI, null, doi.DoType, doi);

                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                }
            }
            foreach (var ied in IED)
            {
                foreach (var lDevice in ied.AccessPoint[0].Server.LDevice)
                {
                    if (lDevice.LN0.DataSet.Count() != 0)
                    {
                        tControl.datSetCollection = new ObservableCollection<string>();
                        foreach (tDataSet ds in lDevice.LN0.DataSet)
                        {
                            ds.Parent = lDevice.LN0;
                            tControl.datSetCollection.Add(ds.name);
                            ds.FCDA.ForEach((fcda) => FullFcdaSetsOfFc(fcda, ied));
                            ds.FCDA.ForEach((fcda =>
                            {
                                fcda.Parent = ds;
                            }));
                        }
                    }
                }
            }
        }


        private void FullFcdaSetsOfFc(tFCDA fcda, tIED ied)
        {

            tLDevice ld = null;
            try
            {
                ld = ied.AccessPoint[0].Server.LDevice.First(c => c.inst == fcda.ldInst);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
            tAnyLN anyLn;
            if (fcda.lnClass == "LLN0")
            {
                anyLn = ld.LN0;
            }
            else
            {
                anyLn =
                    ld.LN.Find(
                        c =>
                            ((c.lnClass == fcda.lnClass) && (c.inst.ToString() == fcda.lnInst) &&
                             (c.prefix == fcda.prefix)));
            }
            List<string> strings = new List<string>();
            if (fcda.doName.Contains("."))
            {
                strings = fcda.doName.Split('.').ToList();
            }
            else
            {
                strings.Add(fcda.doName);
            }

            List<tDAI> dais = new List<tDAI>();
            List<tSDI> sdis = new List<tSDI>();
            tDOI doi = null;
            if (anyLn == null) return;
            if (anyLn.DOI.Count == 0) return;
            doi = anyLn.DOI.FirstOrDefault(c => c.name == strings[0]);


            if (doi == null) return;
            {
                if (strings.Count > 1)
                {
                    tSDI sdi = FindSdiInDoiByListString(strings, doi.SDI, 1);
                    dais = sdi?.DAI;
                    sdis = sdi?.SDI;
                }
                else if (strings.Count == 1)
                {
                    dais = doi.DAI;
                    sdis = doi.SDI;
                }
                try
                {

                    if (fcda.daName != null)
                    {
                        fcda.ChildNodes.Add(dais.First((dai => dai.name.Equals(fcda.daName))));
                    }
                    else
                    {
                        foreach (tDAI dai in dais)
                        {
                            if (dai.DataAttribute != null)
                            {
                                if ((dai.DataAttribute as tDA).fc == fcda.fc)
                                {
                                    fcda.ChildNodes.Add(dai);
                                }
                            }
                        }
                        foreach (tSDI sdi in sdis)
                        {
                            if (sdi.DataAttribute != null)
                            {
                                if ((sdi.DataAttribute as tDA).fc == fcda.fc)
                                {
                                    fcda.ChildNodes.Add(sdi);
                                }
                            }
                        }
                    }



                }
                catch (Exception u)
                {
                }
            }


        }

        private tSDI FindSdiInDoiByListString(List<string> strings, List<tSDI> sdis, int index)
        {
            if (sdis.Count == 0) return null;
            tSDI sdi = sdis.Find(c => c.name == strings[index]);
            if (sdi == null) return null;
            if (strings.Count == index + 1)
                return sdi;
            else
            {
                return FindSdiInDoiByListString(strings, sdi.SDI, index + 1);
            }
        }


        private void BindSdiAndDai(List<tDAI> dais, List<tSDI> sdis, tDAType daType, tDOType doType, object parent)
        {


            foreach (tDAI dai in dais)
            {
                dai.Parent = parent;
                if (doType != null)
                {
                    tDA da = doType.DA.Find(t => t.name == dai.name);
                    if (da != null)
                    {
                        dai.DataAttribute = da;

                        if (dai.DataAttribute != null)
                        {
                            FillDaiByTemplate(dai.DataAttribute, dai);
                            if (dai.DataAttribute.bType == tBasicTypeEnum.Struct)
                            {
                                tDAType newDaType = DataTypeTemplates.DAType.Find(t => t.id == da.type);
                                if (newDaType != null)
                                {
                                    dai.DataAttribute = newDaType.BDA.Find(t => t.name == dai.name);
                                    newDaType.IsInUse = true;
                                }
                                dai.DaType = DataTypeTemplates.DAType.Find(t => t.id == dai.DataAttribute.type);
                                dai.DaType.IsInUse = true;

                            }
                        }
                    }
                }
                if (daType != null)
                {
                    tBDA bda = daType.BDA.Find(t => t.name == dai.name);
                    if (bda != null)
                    {
                        dai.DataAttribute = bda;
                        FillDaiByTemplate(dai.DataAttribute, dai);
                    }
                }
            }

            foreach (tSDI sdi in sdis)
            {

                sdi.Parent = parent;
                if (doType != null)
                {
                    tDA da = doType.DA.Find(t => t.name == sdi.name);
                    if (da != null)
                    {
                        sdi.DataAttribute = da;
                        if (sdi.DataAttribute != null)
                        {
                            if (sdi.DataAttribute.bType == tBasicTypeEnum.Struct)
                            {
                                tDAType newDaType = DataTypeTemplates.DAType.Find(t => t.id == da.type);

                                if (newDaType != null)
                                {
                                    sdi.DaType = newDaType;
                                    newDaType.IsInUse = true;
                                }
                                BindSdiAndDai(sdi.DAI, sdi.SDI, sdi.DaType, null, sdi);
                            }
                        }
                    }
                    if (doType.SDO.Count != 0)
                    {
                        if (doType != null) sdi.Sdo = doType.SDO.Find(t => t.name == sdi.name);
                        if (sdi.Sdo != null)
                        {
                            tDOType newDoType = DataTypeTemplates.DOType.Find(t => t.id == sdi.Sdo.type);
                            if (newDoType != null)
                            {
                                newDoType.IsInUse = true;
                            }
                            BindSdiAndDai(sdi.DAI, sdi.SDI, null, newDoType, sdi);
                        }
                    }
                }
                if (daType != null)
                {
                    tBDA bda = daType.BDA.Find(t => t.name == sdi.name);
                    if (bda != null)
                    {
                        sdi.DataAttribute = bda;
                        if (bda.bType == tBasicTypeEnum.Struct)
                        {
                            tDAType newDaType = DataTypeTemplates.DAType.Find(t => t.id == bda.type);
                            if (newDaType != null)
                            {
                                BindSdiAndDai(sdi.DAI, sdi.SDI, newDaType, null, sdi);
                                newDaType.IsInUse = true;
                            }
                        }
                    }

                }

            }
        }
        [XmlIgnore]
        public string PathToFile { get; set; }

        [XmlIgnore]
        public bool IsLoadedFromFile { get; set; }


        private void FillDaiByTemplate(tAbstractDataAttribute daOfDai, tDAI dai)
        {
            try
            {
                if (daOfDai.bType == tBasicTypeEnum.Enum)
                //заполняются типы Enum для того, чтобы в поле Value в таблице свойств отображалось значение из Enum а не просто число
                {
                    dai.ValEnumDictionary = new Dictionary<string, string>();
                    tEnumType enumType = this.DataTypeTemplates.EnumType.Find(t => t.id == daOfDai.type);
                    enumType.IsInUse = true;
                    foreach (tEnumVal eval in enumType.EnumVal)
                    {
                        dai.ValEnumDictionary.Add(eval.ord, eval.Value);
                    }
                }


            }
            catch (Exception tException)
            {

            }

            if (daOfDai.valKind != null) dai.valKind = daOfDai.valKind.Value;
        }



    }
}