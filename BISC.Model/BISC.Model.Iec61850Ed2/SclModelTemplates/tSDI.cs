using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;
using BISC.Model.Iec61850Ed2.DataTypeTemplates;
using BISC.Model.Iec61850Ed2.DataTypeTemplates.Base;
using BISC.Model.Iec61850Ed2.TreeHelpers;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tSDI : tUnNaming, IObjectData,ILogicalNodeData,ICloneable
    {
        private string _fc;

        public tSDI()
        {
            this.SDI = new List<tSDI>();
            this.DAI = new List<tDAI>();
        }

        [Required]
        [XmlAttribute]
        [Category("SDI"), Description("Имя SDI (часть структуры)"), ReadOnly(true)]
        public string name { get; set; }

        [XmlAttribute]
        [Category("SDI"), Description("Индекс элемента SDI в случае индексируемого типа")]
        public uint ix { get; set; }

        [XmlIgnore, Description("Разрешение на сохранение атрибута ix"), Browsable(false)]
        public bool ixSpecified { get; set; }

        [XmlElement("SDI")]
        [Category("SDI"), Browsable(false)]
        public List<tSDI> SDI { get; set; }

        [XmlElement("DAI")]
        [Category("SDI"), Browsable(false)]
        public List<tDAI> DAI { get; set; }

        [XmlIgnore, Browsable(false)]
        public string FC
        {
            get { return GetFc().ToString(); }
            set { _fc = value; }
        }

        [XmlIgnore, Browsable(false)]
        public tAbstractDataAttribute DataAttribute { get; set; }

        [XmlIgnore, Browsable(false)]
        public tDAType DaType { get; set; }

        [XmlIgnore, Browsable(false)]
        public tSDO Sdo { get; set; }

        //#region Implementation of IHavingChildCollection
        //[XmlIgnore, Browsable(false)]
        //public List<INamableSclItem> ChildNamableCollection
        //{
        //    get
        //    {
        //        List<INamableSclItem> namables = new List<INamableSclItem>();

        //        DAI.ForEach((dai =>
        //        {
        //            namables.Add(dai);
        //        }));
        //        SDI.ForEach((sdi =>
        //        {
        //            namables.Add(sdi);
        //        }));
        //        return namables;
        //    }
        //}

        //#endregion

        //#region Implementation of IStronglyNamed

        //[XmlIgnore]
        //public string StrongName => SclGlobalNames.TreeItems.SDI_TREE_ITEM_NAME;

        //#endregion


        public tFCEnum GetFc()
        {
            if (this.DataAttribute is tDA)
            {
                return (DataAttribute as tDA).fc;
            }
            else
            {
                //return tFCEnum.NONE;
            }



                if ((DAI.Count > 0))
            {
                tFCEnum functionalConstraint;
                tFCEnum.TryParse(DAI[0].FC, out functionalConstraint);
               
                
                return functionalConstraint;
            }


            else if ((SDI.Count > 0))
            {
                return SDI[0].GetFc();
            }
            return tFCEnum.ALL;
        }

        #region Implementation of ICloneable

        public object Clone()
        {
            tSDI sdi=new tSDI();
            sdi.DaType = DaType;
            sdi.DataAttribute = DataAttribute;
            sdi.Sdo = Sdo;
            sdi.Parent = Parent;
            sdi.name = name;
            return sdi;
        }

        #endregion
    }
}