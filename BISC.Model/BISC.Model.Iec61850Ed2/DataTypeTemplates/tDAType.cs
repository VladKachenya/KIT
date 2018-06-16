using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.DataTypeTemplates.Base;

namespace BISC.Model.Iec61850Ed2.DataTypeTemplates
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]

    public class tDAType : tIDNaming
    {
        public tDAType()
        {
            this.iedType = string.Empty;
            this.BDA = new List<tBDA>();
        }

        [XmlElement("BDA")]
        [Category("DAType"), Browsable(false)]
        public List<tBDA> BDA { get; set; }

        [XmlAttribute(DataType = "normalizedString"), ReadOnly(true)]
        [Category("DAType"), Description("Поле используется для определения отношения определенного LN к типу IED")]
        public string iedType { get; set; }

        public void AddAbstrAtrAsBDA(tAbstractDataAttribute bdaAbs)
        {
            tBDA bda = new tBDA();
            bda.name = bdaAbs.name;
            bda.bType = bdaAbs.bType;
            bda.valKind = bdaAbs.valKind;
            bda.type = bdaAbs.type;
            bda.count = bdaAbs.count;
            if (BDA.Count == 0)
            {
                BDA.Add(bda);
                return;
            }
            if (!CheckIfBDAExist(bda))
            {
                BDA.Add(bda);
                return;
            }
        }

        public bool CheckIfBDAExist(tBDA bda)
        {
          foreach(tBDA bdaitem in BDA)
            {
                if ((bda.name == bdaitem.name) &&
               (bda.bType == bdaitem.bType) &&
                (bda.valKind == bdaitem.valKind) &&
               (bda.type == bdaitem.type) &&
                (bda.count == bdaitem.count)) return true;
            }
            return false;
        }
        [XmlIgnore]
        public bool IsInUse { get; set; }
    }
}