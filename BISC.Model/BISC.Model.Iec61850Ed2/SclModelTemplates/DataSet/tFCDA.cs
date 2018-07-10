using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;
using BISC.Model.Iec61850Ed2.TreeHelpers;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Controls;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates.DataSet
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tFCDA : IFcda, IParentable
    {
        private List<ILogicalNodeData> _chilNodes = new List<ILogicalNodeData>();

        public tFCDA()
        {
            this.ldInst = string.Empty;
            this.prefix = string.Empty;
        }


        public tFCDA(string iecAddress, string doName, string daName, string fc)
        {
            if (doName != null)
            {
                this.doName = doName;
            }
            if (daName != null)
            {
                this.daName = daName;
            }
            tFCEnum fce;
            tFCEnum.TryParse(fc, true, out fce);
            this.fc = fce;
            this.ldInst = iecAddress.Substring(0, iecAddress.IndexOf('/'));

            string lnstr = iecAddress.Substring(iecAddress.IndexOf('/') + 1,
                iecAddress.IndexOf('.') - iecAddress.IndexOf('/') - 1);
            uint i;
            foreach (string str in Enum.GetNames(typeof(tLNClassEnum)))
            {
                if (lnstr.Contains(str))
                {
                    string[] s = new string[1];
                    s[0] = str;
                    this.lnClass = str;
                    string[] substr = new string[2];
                    substr = lnstr.Split(s, 2, StringSplitOptions.None);
                    if (substr[0].Length > 0)
                        this.prefix = substr[0];
                    if (substr[1].Length > 0)
                    {
                        if (uint.TryParse(substr[1], out i))
                            this.lnInst = i.ToString();
                        else
                            this.lnInst = "";
                    }
                    else this.lnInst = "";

                    if (this.prefix == null)
                        this.prefix = "";
                    return;
                }
            }
            if (uint.TryParse(lnstr.Substring(lnstr.IndexOf(lnstr.First(char.IsDigit))), out i))
            {
                this.lnInst = i.ToString();
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

            if (this.prefix == null)
                this.prefix = "";
        }



        public tFCDA(string fcdastr)
        {
            SetFcdaString(fcdastr);
        }

        [XmlAttribute(DataType = "normalizedString"), ReadOnly(true)]
        [Category("FCDA"), Description("Ссылка на LD, где содержится данный DO")]
        public string ldInst { get; set; }

        [XmlAttribute(DataType = "normalizedString"), ReadOnly(true)]
        [Category("FCDA"), Description("Префикс, определяющий LD и LN, где содержится данный DO")]
        public string prefix { get; set; }

        [XmlAttribute, ReadOnly(true)]
        [Category("FCDA"), Description("Класс LN, где содержится DO")]
        public string lnClass { get; set; }

        [XmlAttribute(DataType = "normalizedString"), ReadOnly(true)]
        [Category("FCDA"), Description("Номер LN, где содержится DO")]
        public string lnInst { get; set; }

        [XmlAttribute(DataType = "normalizedString"), ReadOnly(true)]
        [Category("FCDA"), Description("Имя DO")]
        public string doName { get; set; }

        [XmlAttribute(DataType = "normalizedString"), ReadOnly(true)]
        [Category("FCDA"), Description("Имя атрибута")]
        public string daName { get; set; }

        [Required, XmlAttribute, ReadOnly(true)]
        [Category("FCDA"), Description("FC")]
        public tFCEnum fc { get; set; }
        [XmlIgnore, Browsable(false)]
        public string FcdaString => ToString();
        [XmlIgnore, Browsable(false)]
        public tFCDA Self => this;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ldInst);
            sb.Append("/");
            sb.Append(prefix);
            sb.Append(lnClass);
            sb.Append(lnInst);
            sb.Append(".");
            sb.Append(doName);
            if (daName != null)
            {
                sb.Append(".");
                sb.Append(daName);
            }
            sb.Append("[");
            sb.Append(fc);
            sb.Append("]");
            return sb.ToString();
        }

        public object Clone()
        {
            tFCDA newFcda = new tFCDA(ToString());
            return newFcda;
        }

    


        [XmlIgnore]
        public List<ILogicalNodeData> ChildNodes
        {
            get { return _chilNodes; }
            set { _chilNodes = value; }
        }

        #region Implementation of IHavingChildCollection

        [XmlIgnore]
        public List<INameableItem> ChildNamableCollection
        {
            get { return _chilNodes.Cast<INameableItem>().ToList(); }
        }

        #endregion

        #region Implementation of INamableSclItem

        [XmlIgnore]
        public string name => FcdaString;

        public void SetFcdaString(string fcdastr)
        {
            //  AA1J1Q01A1LD0 / ZMQAPDIS3.Op[ST]
            if (!fcdastr.Contains("/"))
            {
                int t = fcdastr.IndexOf(".");
                fcdastr = fcdastr.Remove(t, 1).Insert(t, "/");
            }
            this.ldInst = fcdastr.Substring(0, fcdastr.IndexOf('/'));
            string buf = fcdastr.Substring(fcdastr.LastIndexOf('.') + 1);

            this.doName = buf.Remove(buf.LastIndexOf('['));
            tFCEnum fce;
            tFCEnum.TryParse(fcdastr.Substring(fcdastr.LastIndexOf('[') + 1, 2), true, out fce);

            this.fc = fce;

            string lnstr = fcdastr.Substring(fcdastr.IndexOf('/') + 1, fcdastr.IndexOf('.') - fcdastr.IndexOf('/') - 1);
            string mid = fcdastr.Replace(lnstr, "").Replace(ldInst, "").Replace(buf, "").Trim('/').Remove(0, 1);
            if (mid != "") doName = mid + doName;
            if (doName.EndsWith(".")) doName = doName.Remove(doName.Length - 1);
            uint i;
            foreach (string str in Enum.GetNames(typeof(tLNClassEnum)))
            {
                if (lnstr.Contains(str))
                {
                    string[] s = new string[1];
                    s[0] = str;
                    this.lnClass = str;
                    string[] substr = new string[2];
                    substr = lnstr.Split(s, 2, StringSplitOptions.None);
                    if (substr[0].Length > 0)
                        this.prefix = substr[0];
                    if (substr[1].Length > 0)
                    {
                        if (uint.TryParse(substr[1], out i))
                            this.lnInst = i.ToString();
                        else
                            this.lnInst = "";
                    }
                    else this.lnInst = "";

                    if (this.prefix == null)
                        this.prefix = "";
                    return;
                }
            }
            if (uint.TryParse(lnstr.Substring(lnstr.IndexOf(lnstr.First(char.IsDigit))), out i))
            {
                this.lnInst = i.ToString();
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

            if (this.prefix == null)
                this.prefix = "";
        }

        #endregion

        #region Implementation of IParentable
        [XmlIgnore]
        public object Parent { get; set; }

        #endregion
    }
}