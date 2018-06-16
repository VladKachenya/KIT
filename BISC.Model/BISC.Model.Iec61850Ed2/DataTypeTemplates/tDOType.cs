using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using BISC.Model.Iec61850Ed2.Common;
using BISC.Model.Iec61850Ed2.DataTypeTemplates.Base;

namespace BISC.Model.Iec61850Ed2.DataTypeTemplates
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]

    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    public class tDOType : tIDNaming
    {
        private string iedTypeField;
        private tCDCEnumEd2 cdcField;
        [XmlIgnore]
        public bool IsInUse { get; set; }
        public tDOType()
        {
            SDO = new List<tSDO>();
            DA = new List<tDA>();
        }

        [XmlElement("SDO")]
        [Category("DOType"), Browsable(false)]
        public List<tSDO> SDO { get; set; }

        [XmlElement("DA")]
        [Category("DOType"), Browsable(false)]
        public List<tDA> DA
        { get; set; }

        [XmlAttribute(DataType = "normalizedString")]
        [Category("DOType"), ReadOnly(true), DefaultValue(""),
        Description("“ип IED-устройства, к которому принадлежит данный DOType. " +
                    "ѕуста€ строка позвол€ет ссылки дл€ всех типов IED-устройств или из секции Substation")]
        public string iedType
        {
            get { return this.iedTypeField; }
            set { this.iedTypeField = value; }
        }

        [Required, XmlAttribute]
        [Category("DOType"), Description("Ѕазисный CDC (Common Data Class - класс общих данных)"), ReadOnly(true)]
        public tCDCEnumEd2 cdc
        {
            get { return this.cdcField; }
            set { this.cdcField = value; }
        }
        public void AddDA(tDA tda)
        {
            if (DA == null) DA = new List<tDA>();
            if (tda == null) return;
            if (CheckIfExist(tda)) return;
            tDA daToadd = new tDA
            {
                bType = tda.bType,
                name = tda.name,
                valKind = tda.valKind,
                count = tda.count,
                qchg = tda.qchg,
                dchg = tda.dchg,
                dupd = tda.dupd,
                fc = tda.fc,
                type = tda.type
            };
            if (tda.name == "stVal")
                // костыль дл€ того, чтобы stVal был первым в списке,чтобы контроллер читал файл 
                //(он глупый и думает, что stVal по пор€дку первый)
                
            {
                DA.Insert(0, daToadd);
                return;
            }
            DA.Add(daToadd);
        }

        public bool CheckIfExist(tDA tda)
        {
            if (DA.Contains(tda)) return true;
            foreach (tDA daitem in DA)
            {
                if ((tda.name == daitem.name) &&
                    (tda.bType == daitem.bType) &&
                     (tda.type == daitem.type) &&
                    (tda.count == daitem.count) &&
                    (tda.dchg == daitem.dchg) &&
                    (tda.qchg == daitem.qchg) &&
                    (tda.dupd == daitem.dupd) &&
                    (tda.fc == daitem.fc)) return true;
            }
            return false;
        }

        public bool CheckIfExist(tSDO sdo)
        {
            return SDO.Any(sdoitem => (sdoitem.name == sdo.name) && (sdoitem.type == sdo.type));
        }


        public void AddSDOtoDOType(tSDO sdo)
        {
            if (sdo == null) return;

            tSDO findedSDO = null;
            findedSDO = this.SDO.Find(a => a.name == sdo.name);
            if (findedSDO != null)
            {
                findedSDO.type = sdo.type;
            }

            if (!CheckIfExist(sdo))
                this.SDO.Add(sdo);
        }




    }
}