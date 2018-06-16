using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
    public class tDOI : tUnNaming, ILogicalNodeData
    {
        public tDOI()
        {
            this.SDI = new List<tSDI>();
            this.DAI = new List<tDAI>();
        }

        [XmlAttribute, ReadOnly(true), Required]
        [Category("DOI"), Description("Стандартизированное имя DO")]
        public string name { get; set; }

        [XmlAttribute]
        [Category("DOI"), Description("Индекс элемента данных в случае индексируемого типа")]
        public uint ix { get; set; }

        [XmlIgnore, Description("Разрешение на сохранение атрибута ix"), ReadOnly(true)]
        public bool ixSpecified { get; set; }

        [XmlAttribute(DataType = "normalizedString"), ReadOnly(true)]
        [Category("DOI"), Description("Определение управления доступом к этим данным")]
        public string accessControl { get; set; }

        [XmlElement("SDI")]
        [Category("DOI"), Browsable(false)]
        public List<tSDI> SDI { get; private set; }

        [XmlElement("DAI")]
        [Category("DOI"), Browsable(false)]
        public List<tDAI> DAI { get; private set; }

        public void AddSDI(tSDI newsdi)
        {
            tSDI sditoadd = CheckExisting(newsdi);
            if (sditoadd == null)
            {
                SDI.Add(newsdi);
            }
            else
            {
                sditoadd.ix = newsdi.ix;
                sditoadd.SDI.AddRange(newsdi.SDI);
                sditoadd.DAI.AddRange(newsdi.DAI);
            }

        }

        [XmlIgnore, Browsable(false)]
        public tDOType DoType { get; set; }

        private tSDI CheckExisting(tSDI newsdi)
        {
            return SDI.FirstOrDefault(sdiItem => newsdi.name == sdiItem.name);
        }

        internal tDAI FindDAI(string daiName)
        {
            foreach (tDAI dai in DAI)
            {
                if (dai.name == daiName) return dai;
            }
            return FindDaiInSdi(SDI, daiName);
        }

        private tDAI FindDaiInSdi(List<tSDI> sdis, string daiName)
        {
            if ((sdis == null) || (sdis.Count) == 0) return null;
            foreach (tSDI sdi in sdis)
            {
                foreach (tDAI dai in sdi.DAI)
                {
                    if (dai.name == daiName) return dai;

                }
                return FindDaiInSdi(sdi.SDI, daiName);
            }
            return null;
        }

        [XmlIgnore, Browsable(false)]
        public bool IsControlObject
        {
            get
            {
                if (DAI.Any((dai => dai.name == "ctlModel")))
                {
                    return ((DAI.First(dai => dai.name == "ctlModel")).Val ==
                            ctlModelsEnum.direct_with_normal_security.ToString());
                }
                return false;
            }
        }

        //#region Implementation of IHavingChildCollection
        //[XmlIgnore, Browsable(false)]
        //public List<INamableSclItem> ChildNamableCollection
        //{
        //    get
        //    {
        //        List<SetFC> setFcs = new List<SetFC>();
        //        FullSetsFc(setFcs, DAI, SDI);
        //        return setFcs.Cast<INamableSclItem>().ToList();
        //    }
        //}


        private void FullSetsFc(List<SetFC> setFcs, List<tDAI> dais, List<tSDI> sdis)
        {
            foreach (var dai in dais)
            {
                if ((dai.DataAttribute is tDA))
                {
                    if (!setFcs.Any((setfc => setfc.name == (dai.DataAttribute as tDA).fc.ToString())))
                    {
                        setFcs.Add(new SetFC() { name = (dai.DataAttribute as tDA).fc.ToString(), Parent = this });
                    }
                    setFcs.First((setfc => setfc.name == (dai.DataAttribute as tDA).fc.ToString())).DAI.Add(dai);
                }
            }
            foreach (var sdi in sdis)
            {
                if ((sdi.DataAttribute is tDA))
                {
                    if (!setFcs.Any((setfc => setfc.name == (sdi.DataAttribute as tDA).fc.ToString())))
                    {
                        setFcs.Add(new SetFC() { name = (sdi.DataAttribute as tDA).fc.ToString(), Parent = this });
                    }
                    setFcs.First((setfc => setfc.name == (sdi.DataAttribute as tDA).fc.ToString())).SDI.Add(sdi);
                }
                else
                {
                    if (sdi.Sdo != null)
                    {
                        Dictionary<string, tSDI> dictionary = DivideSdisByFc(sdi);
                        foreach (var fcKey in dictionary.Keys)
                        {
                            if (!setFcs.Any((setfc => setfc.name == fcKey)))
                            {
                                setFcs.Add(new SetFC() { name = fcKey, Parent = this });
                            }
                            setFcs.First((setfc => setfc.name == fcKey.ToString())).SDI.Add(dictionary[fcKey]);
                        }
                    }
                }
            }
        }

        private Dictionary<string, tSDI> DivideSdisByFc(tSDI sdi)
        {
            Dictionary<string, tSDI> dictionary = new Dictionary<string, tSDI>();

            foreach (var child in sdi.DAI)
            {
                if (child is IObjectData)
                {


                    if (child is tDAI)
                    {
                        if (((child as tDAI).DataAttribute is tDA))
                        {
                            if (!dictionary.ContainsKey(((child as tDAI).DataAttribute as tDA).fc.ToString()))
                            {
                                dictionary.Add(((child as tDAI).DataAttribute as tDA).fc.ToString(), sdi.Clone() as tSDI);
                            }
                            dictionary[((child as tDAI).DataAttribute as tDA).fc.ToString()].DAI.Add(child as tDAI);

                        }
                    }

                }

            }



            foreach (var child in sdi.SDI)
            {
                if (child is IObjectData)
                {
                    
                    if (child is tSDI)
                    {
                        if (!dictionary.ContainsKey(((child as tSDI).GetFc().ToString())))
                        {
                            dictionary.Add((child as tSDI).GetFc().ToString(), sdi.Clone() as tSDI);
                        }
                        dictionary[(child as tSDI).GetFc().ToString()].SDI.Add(child as tSDI);
                    }



                }

            }
            return dictionary;

        }


        

    }
}