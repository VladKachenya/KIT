using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Global;
using BISC.Model.Iec61850Ed2.Common;
using BISC.Model.Iec61850Ed2.DataTypeTemplates;
using BISC.Model.Iec61850Ed2.DataTypeTemplates.Base;
using BISC.Model.Iec61850Ed2.TreeHelpers;
using BISC.Modules.Connection.Infrastructure.Connection;

namespace BISC.Model.Iec61850Ed2.SclModelTemplates
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tDAI : tUnNaming, IObjectData, ILogicalNodeData
    {
        private List<tVal> _valList;

        public tDAI()
        {
            this.valKind = tValKindEnum.Set;
            this.ValList = new List<tVal>();
            this.ValFromDevice = new List<tVal>();
        }
        [XmlIgnore]
        public List<tVal> ValFromDevice { get; set; }

        [XmlIgnore]
        public List<tVal> ValList
        {
            get { return _valList; }
            set
            {
                _valList = value;
            }
        }






        public bool ShouldSerializeVal()
        {
            return StaticSerializingDirectives.IsValuesShouldBeSerialized;
        }
        [XmlIgnore]
        public string ValueFromDevice
        {
            get
            {
                if ((ValFromDevice == null) || (ValFromDevice.Count == 0)) return null;
                if (ValFromDevice[0].Value == null) return null;

                if (DataAttribute != null)
                {
                    if (DataAttribute.bType == tBasicTypeEnum.Dbpos)
                    {
                        if (ValFromDevice[0].Value is string)
                        {
                            if (ValFromDevice[0].Value == "00") return dbPosEnum.intermediate.ToString();
                            if (ValFromDevice[0].Value == "01") return dbPosEnum.off.ToString();
                            if (ValFromDevice[0].Value == "10") return dbPosEnum.on.ToString();
                            if (ValFromDevice[0].Value == "11") return dbPosEnum.bad.ToString();
                        }
                    }
                }


                if (ValEnumDictionary != null&&ValEnumDictionary.Count>0)
                {
                    if (ValEnumDictionary.ContainsKey(ValFromDevice[0].Value))
                    {
                        return ValEnumDictionary[ValFromDevice[0].Value];
                    }
                }
                return ValFromDevice[0].Value?.ToString();
            }

            set
            {
                if (ValFromDevice.Count > 0)
                {
                    ValFromDevice[0].Value = value;
                }
                else
                {
                    ValFromDevice.Add(new tVal() { Value = value });
                }
            }
        }

        public string Val
        {
            get
            {

                if ((ValList == null) || (ValList.Count == 0)) return null;
                if (ValList[0].Value == null) return null;
                if (DataAttribute != null)
                {
                    if (DataAttribute.bType == tBasicTypeEnum.Dbpos)
                    {
                        if (ValList[0].Value is string)
                        {
                            if (ValList[0].Value == "00") return dbPosEnum.intermediate.ToString();
                            if (ValList[0].Value == "01") return dbPosEnum.off.ToString();
                            if (ValList[0].Value == "10") return dbPosEnum.on.ToString();
                            if (ValList[0].Value == "11") return dbPosEnum.bad.ToString();
                        }
                    }
                }


                if (ValEnumDictionary != null && ValEnumDictionary.Count > 0)
                {
                    if (ValEnumDictionary.ContainsKey(ValList[0].Value))
                    {
                        return ValEnumDictionary[ValList[0].Value];
                    }
                }
                return ValList[0].Value?.ToString();
            }

            set
            {
                if (ValList.Count > 0)
                {
                    ValList[0].Value = value;
                }
                else
                {
                    ValList.Add(new tVal() { Value = value });
                }
            }
        }

        [XmlAttribute, Required]
        [Category("DAI"), Description("»м€ атрибута Data с заданным значением"), ReadOnly(true)]
        public string name { get; set; }

        [XmlAttribute(DataType = "normalizedString"), ReadOnly(true)]
        [Category("DAI"), Description(" ороткий адрес этого атрибута Data")]
        public string sAddr { get; set; }

        [Category("DAI"), ReadOnly(true), XmlAttribute,
         Description("≈сли задано любое им€, то его смысловое значение " +
                     "задаетс€ на этапе разработки и проектировани€")]
        public tValKindEnum valKind { get; set; }

        [XmlIgnore, Description("–азрешение на сохранение атрибута valKind"), Browsable(false)]
        public bool valKindSpecified { get; set; }

        [XmlAttribute]
        [Category("DAI"), Description("»ндекс элемента DAI в случае индексируемого типа"), ReadOnly(true)]
        public uint ix { get; set; }

        [XmlIgnore, Description("–азрешение на сохранение атрибута ix"), ReadOnly(true)]
        public bool ixSpecified { get; set; }

        [XmlIgnore, Browsable(false)]
        public string FC { get; set; }

        [XmlIgnore, Browsable(false)]
        public Dictionary<string, string> ValEnumDictionary { get; set; }
        [XmlIgnore, Browsable(false)]
        public List<tDAI> DAI
        {
            get { return null; }

            set { }
        }
        [XmlIgnore, Browsable(false)]
        public List<tSDI> SDI
        {
            get { return null; }
            set { }
        }
        [XmlIgnore, Browsable(false)]
        public tDAType DaType { get; set; }
        [XmlIgnore, Browsable(false)]
        public tAbstractDataAttribute DataAttribute { get; set; }


    
    }
}