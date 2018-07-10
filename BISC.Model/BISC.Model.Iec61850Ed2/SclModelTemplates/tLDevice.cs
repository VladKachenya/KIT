using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;
using BISC.Model.Iec61850Ed2.DataTypeTemplates.Base;
using BISC.Model.Infrastructure.Common;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tLDevice : tUnNaming
    {
        private string _inst ;
        private tLN0 _ln0;
        private List<tLN> _ln;
        public tLDevice()
        {
            this.inst = string.Empty;
            this.LN = new List<tLN>();
        }

        [Category("LDevice"), Description("Данный LN всегда является LLN0"), Browsable(false)]
        public tLN0 LN0 { get { return _ln0; }
            set
            {
                _ln0 = value;
            }
        }

        [XmlElement("LN")]
        [Category("LDevice"), Browsable(false)]
        public List<tLN> LN
        {
            get { return _ln; }
            set
            {
                _ln = value;
            }
        }

        [Category("LDevice"), Browsable(false)]
        public tAccessControl AccessControl { get; set; }

        [XmlAttribute(DataType = "normalizedString"), Required, ReadOnly(true)]
        [Category("LDevice"), Description("Идентификация LDevice в пределах IED-устройства")]
        public string inst
        {
            get { return _inst; }
            set
            {
                _inst = value;
            }
        }

        public bool AddLN(tAnyLN ln)
        {
            if (ln == null ) return false;
            if (ln is tLN)
            {
                this.LN.Add((tLN) ln);
                return true;
            }
            if (ln is tLN0)
            {
                this.LN0 = (tLN0) ln;
                return true;
            }
            return false;
        }
        [XmlIgnore,Browsable(false)]
        public List<string> LNColldection
        {
            get
            {
                List<string> col = new List<string>();
                col.Add(this.inst+"."+LN0.lnClass+LN0.inst);
                foreach(tLN ln in LN)
                {
                    col.Add(this.inst + "." + ln.lnClass + ln.inst);
                }
                return col;
            }
        }

     

        //#region Implementation of IHavingChildCollection
        //[XmlIgnore, Browsable(false)]
        //public List<INamableSclItem> ChildNamableCollection
        //{
        //    get
        //    {
        //        List<INamableSclItem> namables=new List<INamableSclItem>();
        //        if (LN0!=null)
        //        {
        //             namables.Add(LN0);
        //        }
        //       LN.ForEach((ln =>
        //       {
        //           namables.Add(ln);
        //       }));

        //        return namables;
        //    }
        //}

        //#endregion
        //#region Implementation of IStronglyNamed

        //[XmlIgnore]
        //public string StrongName => SclGlobalNames.TreeItems.LDEVICE_TREE_ITEM_NAME;


        


        //#endregion


        //public Maybe<tAnyLN> GetAnyLnByName(string lnName)
        //{
        //    Maybe<tAnyLN> anulnMaybe=new Maybe<tAnyLN>();
        //    if (lnName == "LLN0")
        //    {
        //        anulnMaybe.AddValue(LN0);
        //        return anulnMaybe;
        //    }

        //    try
        //    {
        //        anulnMaybe.AddValue(LN.First((ln => ln.name == lnName)));
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine(e.Message);
        //    }
        //    return anulnMaybe;
        //}


    
    }
}