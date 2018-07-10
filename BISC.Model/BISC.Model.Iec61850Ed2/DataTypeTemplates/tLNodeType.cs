using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;
using BISC.Model.Iec61850Ed2.DataTypeTemplates.Base;

namespace BISC.Model.Iec61850Ed2.DataTypeTemplates
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlInclude(typeof(CommonLogicalNode))]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]

    public class tLNodeType : tIDNaming
    {
        [XmlElement("DO")]
        [Category("LNodeType"), Browsable(false)]
        public List<tDO> DO { get; set; }
        [XmlIgnore]
        public bool IsInUse { get; set; }
        public tLNodeType()
        {
            this.DO = new List<tDO>();
        }

        public tLNodeType(string iedType, string lnClass, string id)
        {
            this.DO = new List<tDO>();
            this.lnClass = lnClass;
            this.iedType = iedType;
            this.id = id;
        }        
        
        [XmlAttribute(DataType = "normalizedString"), ReadOnly(true)]
        [Category("LNodeType"), Description("Тип производителя IED-устройства, к которому принадлежит данный LN type")]
        public string iedType { get; set; }

        [Required, XmlAttribute, ReadOnly(true)]
        [Category("LNodeType"), Description("Базовый класс LN данного типа")]
        public string lnClass { get; set; }

        public void AddDO(tDO tdo)
        {
            if (tdo != null)
            {
                tDO findedDO = null;
                findedDO = this.DO.Find(a => a.name == tdo.name);
                if (findedDO != null)
                {
                    findedDO.type = tdo.type;
                }
                else
                {
                    this.DO.Add(tdo);
                }
            }
        }


    }
}

