using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    [XmlRoot("LN", Namespace = "http://www.iec.ch/61850/2003/SCL", IsNullable = false)]
    public class tLN : tAnyLN
    {
        private string lnClassString;
        private tLNClassEnum lnClassField;
        public tLN()
        {
            this.prefix = string.Empty;
            this.lnClass = tLNClassEnum.Custom.ToString();
            this.inst = null;
        }

        public tLN(string lnstr)
        {
            uint i;
            prefix=string.Empty;
            var lnNames=Enum.GetNames(typeof(tLNClassEnum));
            var all—oncurrences = new List<string>();
            foreach (string str in lnNames)
            {
                if (lnstr.Contains(str))
                {
                   all—oncurrences.Add(str);
                }

            }
            string lnClassString = all—oncurrences[0];

            if (all—oncurrences.Count > 1)
            {
                foreach (var concurrence in all—oncurrences)
                {
                    if (lnstr.IndexOf(concurrence) > lnstr.IndexOf(lnClassString))
                    {
                        lnClassString = concurrence;
                    }
                }
            }

            string[] s = new string[1];
            s[0] = lnClassString;
            this.lnClass = lnClassString;
            string[] substr = new string[2];
            substr = lnstr.Split(s, 2, StringSplitOptions.None);
            if (substr[0].Length > 0)
                this.prefix = substr[0];
            if (substr[1].Length > 0)
            {
                this.inst = uint.TryParse(substr[1], out i) ? i : 1;
            }
            else this.inst = 1;
            return;

            if (uint.TryParse(lnstr.Substring(lnstr.IndexOf(lnstr.First(Char.IsDigit))), out i))
            {
                this.inst = i;
                this.lnClass = lnstr.Substring(lnstr.Length - 5, 4);
                if (lnstr.Length > 5)
                {
                    this.prefix = lnstr.Substring(0, lnstr.Length - 5);
                }
            }
            else
            {
                if (lnstr.Length > 4)
                {
                    this.lnClass = lnstr.Substring(lnstr.Length - 4, 4);
                    this.prefix = lnstr.Substring(0, lnstr.Length - 4);
                }
                else
                {
                    this.lnClass = lnstr;
                }
            }
        }

        [Required]
        [XmlAttribute]
        [Category("LN"), Browsable(false), Description("LN ÍÎ‡ÒÒ")]
        public string lnClass
        {
            get { return this.lnClassString; }
            set
            {
                this.lnClassString = value;
                if (Enum.IsDefined(typeof (tLNClassEnum), this.lnClass))
                {
                    this.lnClassEnum = (tLNClassEnum) Enum.Parse(typeof (tLNClassEnum), this.lnClass);
                }
                else
                {
                    this.lnClassEnum = tLNClassEnum.Custom;
                }
            }
        }

        [XmlIgnore, ReadOnly(true), Required]
        [Category("LN"), DisplayName("lnClass"), Description("LN ÍÎ‡ÒÒ ËÁ ÔÂÂ˜ËÒÎÂÌËˇ")]
        public tLNClassEnum lnClassEnum
        {
            get { return this.lnClassField; }
            set
            {
                this.lnClassField = value;
                if (this.lnClassField != tLNClassEnum.Custom)
                {
                    this.lnClassString = this.lnClassField.ToString();
                }
            }
        }



        [XmlAttribute("inst"), Browsable(false)]
        public uint instAsText
        {
            get {
                return inst.HasValue ? inst.Value : 1;
            }
            set { inst = value; }
        }

       
        [Required,XmlIgnore,ReadOnly(true)]
        [Category("LN"), Description("»‰ÂÌÚËÙËÍ‡ÚÓ ‰‡ÌÌÓ„Ó LN")]
        public uint? inst { get; set; }

        [XmlAttribute(DataType = "normalizedString"), ReadOnly(true)]
        [Category("LN"), Description("œÂÙËÍÒ LN")]
        public string prefix { get; set; }

        [XmlIgnore, Browsable(false)]
        public string name =>prefix+lnClass+inst.ToString();

        //#region Implementation of IStronglyNamed
        //[XmlIgnore]
        //public string StrongName => SclGlobalNames.TreeItems.LN_TREE_ITEM_NAME;

        //#endregion


        #region Overrides of Object

        public override string ToString()
        {
            return GetIedParent().name + "." + prefix + lnClass + inst;
        }


       

        #endregion
    }
}