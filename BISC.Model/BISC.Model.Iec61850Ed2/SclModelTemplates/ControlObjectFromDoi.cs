using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.TreeHelpers;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates
{
    //декоратор для tdoi
    public class ControlObjectFromDoi : IControlObject
    {
        private tDOI _doi;

        public ControlObjectFromDoi(tDOI doi)
        {
            _doi = doi;
            Parent = doi.Parent;
            name = doi.name;
            tDAI orCatDai = _doi.FindDAI("orCat");
            if (orCatDai != null)
            {
                OrCat = orCatDai.Val;
                OrCatCollection = orCatDai.ValEnumDictionary.Values.ToList();
            }

            tDAI ctlValDai = _doi.FindDAI("ctlVal");
            if (ctlValDai != null)
            {
                CtlVal = ctlValDai.Val;
                CtlValCollection = ctlValDai.ValEnumDictionary.Values.ToList();
            }

            tDAI ctlNumdai = _doi.FindDAI("ctlNum");
            if (ctlNumdai != null)
            {
                CtlNum = ctlNumdai.Val;
            }
            tDAI orIdent = _doi.FindDAI("orIdent");
            if (orIdent != null)
            {
                OrIdent = orIdent.Val;
            }
        }

        [XmlIgnore, Browsable(false)]
        public ObjectItemPath ObjectItemPath { get; set; }

        [XmlIgnore, Browsable(false)]
        public ctlModelsEnum? CtlModelValue
        {
            get
            {
                if (!_doi.IsControlObject) return null;
                tDAI daiToCheck = _doi.DAI.FirstOrDefault((dai => dai.name == "ctlModel"));
                if (daiToCheck?.Val == null) return null;
                if (!Enum.GetNames(typeof (ctlModelsEnum)).Contains(daiToCheck.Val)) return null;
                ctlModelsEnum toRet;
                if (Enum.TryParse(daiToCheck.Val, out toRet)) return toRet;
                return null;
            }
        }

        [XmlIgnore, Browsable(false)]
        public Action<ctlModelsEnum> CtlModelValueChanged { get; set; }

        public void Subscribe()
        {
            if (!_doi.IsControlObject) return;
            tDAI daiToCheck = _doi.DAI.FirstOrDefault((dai => dai.name == "ctlModel"));

            if (daiToCheck == null) return;
        }

        public string OrCat { get; set; }
        public string OrIdent { get; set; }
        public List<string> OrCatCollection { get; set; }
        public string CtlVal { get; set; }
        public List<string> CtlValCollection { get; set; }
     
        public string StVal
        {
            get
            {
                if (!_doi.IsControlObject) return null;
                tDAI daiToCheck = _doi.DAI.FirstOrDefault((dai => dai.name == "stVal"));
                return daiToCheck?.Val;
            }
        }

        public string CtlNum { get; set; }
        public bool SynchroCheck { get; set; }
        public bool InterlockCheck { get; set; }
        public bool TestMode { get; set; }
        public object Parent { get; set; }
        public string name { get; }
    }
}
