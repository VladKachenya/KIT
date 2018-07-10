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
    public class tEnumType : tIDNaming
    {
        [XmlIgnore]
        public bool IsInUse { get; set; }
        public tEnumType()
        {
            EnumVal = new List<tEnumVal>();
        }

        [XmlElement("EnumVal")]
        [Category("EnumType"), Browsable(false)]
        public List<tEnumVal> EnumVal { get; set; }


        public static bool IsEqual(tEnumType obj1, tEnumType obj2)
        {
            try
            {
                if ((obj1.id != obj2.id) && !obj1.id.Contains(obj2.id) && !obj2.id.Contains(obj1.id))return false;


            if (obj1.EnumVal.Count != obj2.EnumVal.Count) return false;
            int i = 0;
            foreach(tEnumVal enumvalitem in obj1.EnumVal)
            {
                if ((enumvalitem.ord != obj2.EnumVal[i].ord) || (enumvalitem.Value != obj2.EnumVal[i].Value)) return false;
                    i++;
            }
            return true;

            }
            catch (Exception e)
            {

               
            }
            return true;
         
        }

        public static bool IsNotEqual(tEnumType obj1, tEnumType obj2)
        {
            if (obj1.id == obj2.id) return false;
            if (obj1.EnumVal.Count == obj2.EnumVal.Count) return false;
            int i = 0;
            foreach (tEnumVal enumvalitem in obj1.EnumVal)
            {
                if ((enumvalitem.ord != obj2.EnumVal[i].ord) || (enumvalitem.Value != obj2.EnumVal[i].Value)) return true;
                i++;
            }
            return false;
        }


    }
}