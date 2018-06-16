using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;
using BISC.Model.Iec61850Ed2.SclModelTemplates.Controls;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tLN0 : tAnyLN
    {
        private tLNClassEnum _lnClass;

        public tLN0()
        {
            this.lnClass = tLNClassEnum.LLN0;
            this.inst = string.Empty;
            this.GSEControl = new List<tGSEControl>();
            this.SampledValueControl = new List<tSampledValueControl>();
        }


        public tLN0(string ln0str)
        {
            this.lnClass = tLNClassEnum.LLN0;
            this.inst = string.Empty;
            this.GSEControl = new List<tGSEControl>();
            this.SampledValueControl = new List<tSampledValueControl>();
            lnType = ln0str;
        }


        [XmlElement("GSEControl")]
        [Category("LN0"), Browsable(false), Description("У правление GSE-сообщениями")]
        public List<tGSEControl> GSEControl { get; private set; }

        [XmlElement("SampledValueControl")]
        [Category("LN0"), Browsable(false), Description("Управление выборочными значениями")]
        public List<tSampledValueControl> SampledValueControl { get; private set; }

        [XmlElement]
        [Category("LN0"), Description("Управление уставками"), Browsable(false)]
        public tSettingControl SettingControl { get; set; }

        [XmlElement]
        [Category("LN0"), Description("Управление SCL"), Browsable(false)]
        public tSCLControl SCLControl { get; set; }

        [XmlElement]
        [Category("LN0"), Description("Журнал")]
        public tLog Log { get; set; }

        [XmlAttribute, Required, ReadOnly(true)]
        [Category("LN0"), Description("Класс LN0. Всегда является LLN0")]
        public tLNClassEnum lnClass
        {
            get { return _lnClass; }
            set
            {
                _lnClass = value;
            }
        }

        [Required]
        [XmlAttribute(DataType = "normalizedString"), ReadOnly(true)]
        [Category("LN0"), Description("Идентификация данного LN")]
        public string inst { get; set; }

        [XmlIgnore, Browsable(false)]
        public string name =>lnClass + inst.ToString();


        public void AddGoose(tGSEControl tGseControl)
        {
            GSEControl.Add(tGseControl);
        }

        public void SetSettingControl(tSettingControl settingControl)
        {
            if(SettingControl==null){SettingControl=new tSettingControl();}
            this.SettingControl = settingControl;
        }

        //#region Implementation of IStronglyNamed
        //[XmlIgnore]
        //public string StrongName => SclGlobalNames.TreeItems.LN0_TREE_ITEM_NAME;

        //#endregion
    }
}