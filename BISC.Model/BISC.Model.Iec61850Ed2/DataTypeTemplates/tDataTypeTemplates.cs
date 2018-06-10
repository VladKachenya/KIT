using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace BISC.Model.Iec61850Ed2.DataTypeTemplates
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.iec.ch/61850/2003/SCL")]
    [XmlRoot("DataTypeTemplates", Namespace = "http://www.iec.ch/61850/2003/SCL", IsNullable = false)]
    public class tDataTypeTemplates
    {
        public tDataTypeTemplates()
        {
            LNodeType = new List<tLNodeType>();
            DAType = new List<tDAType>();
            DOType = new List<tDOType>();
            EnumType = new List<tEnumType>();
        }


        [XmlElement("LNodeType")]
        [Category("DataTypeTemplates"), Browsable(false)]
        public List<tLNodeType> LNodeType { get; private set; }

        [XmlElement("DOType")]
        [Category("DataTypeTemplates"), Browsable(false)]
        public List<tDOType> DOType { get; private set; }

        [XmlElement("DAType")]
        [Category("DataTypeTemplates"), Browsable(false)]
        public List<tDAType> DAType { get; private set; }

        [XmlElement("EnumType")]
        [Category("DataTypeTemplates"), Browsable(false)]
        public List<tEnumType> EnumType { get; private set; }

        /// <summary>
        /// Добавляет или возвращает уже существующий tLNodeType
        /// </summary>
        /// <param name="lnt">
        /// A <see cref="tLNodeType"/> tLNodeType, который надо добавить
        /// </param>
        /// <returns>
        /// Возвращает добавленный или существующий tLNodeType
        /// </returns>
        public tLNodeType AddLNodeType(tLNodeType lnt)
        {
            if (lnt == null)
                return null;
            tLNodeType existing = CheckExisting(lnt);
            if (existing == null)
            {
                tLNodeType toadd = new tLNodeType();
                toadd.id = lnt.id;
                toadd.iedType = lnt.iedType;
                toadd.lnClass = lnt.lnClass;
                toadd.DO = lnt.DO;
                LNodeType.Add(toadd);
                return toadd;
            }
            return existing;
        }

        public tDOType AddDOType(tDOType dotype) //возвращает tDOType которые или есть или который добавлен
        {
            if (dotype == null)
                return null;
            tDOType existing = CheckExistingUsual(dotype);
            if (existing == null)
            {
                tDOType toadd = new tDOType();
                toadd.id = dotype.id;
                toadd.cdc = dotype.cdc;
                toadd.iedType = dotype.iedType;
                toadd.DA.AddRange(dotype.DA);
                toadd.SDO.AddRange(dotype.SDO);
                DOType.Add(toadd);
                return dotype;
            }
            else
            {
                foreach (tDA da in dotype.DA)
                    existing.AddDA(da);
                return existing;
            }
              
        }

        private tLNodeType CheckExisting(tLNodeType nodetype)
        {
            foreach (tLNodeType nodetypetypeitem in LNodeType)
            {  
               // if ((nodetype.DO.Count == nodetypetypeitem.DO.Count) &&
               // (nodetype.id.Substring(nodetype.id.LastIndexOf('.')+1, nodetype.id.Length - nodetype.id.LastIndexOf('.') - 2) ==
               //nodetypetypeitem.id.Substring(nodetypetypeitem.id.LastIndexOf('.')+1, nodetypetypeitem.id.Length - nodetypetypeitem.id.LastIndexOf('.') - 2))) return nodetypetypeitem;      
               // //проверка только по количеству     
                bool isExist = true;
                foreach (tDO newDo in nodetype.DO)
                {
                    if (!nodetypetypeitem.DO.Exists(oldDo => oldDo.type == newDo.type))
                    {
                        isExist = false;
                        break;
                    }
                }
                foreach (tDO newDo in nodetype.DO)
                {
                    if (!nodetypetypeitem.DO.Exists(oldDo => oldDo.name == newDo.name))
                    {
                        isExist = false;
                        break;
                    }
                }
                foreach (tDO oldDo in nodetypetypeitem.DO)
                {
                    if (!nodetype.DO.Exists(newDo => newDo.type == oldDo.type))
                    {
                        isExist = false;
                        break;
                    }
                }

                foreach (tDO oldDo in nodetypetypeitem.DO)
                {
                    if (!nodetype.DO.Exists(newDo => newDo.name == oldDo.name))
                    {
                        isExist = false;
                        break;
                    }
                }

                if (isExist)
                {
                    return nodetypetypeitem;
                }
              
            }
            return null;
        }

        private tDOType CheckExistingUsual(tDOType dotype)
        {
            foreach (tDOType dotypeitem in DOType)
            {
                if (dotypeitem.id == dotype.id)
                    return dotypeitem;
            }
            foreach (tDOType dotypeitem in DOType)
            {
                if((dotypeitem.DA.Count == dotype.DA.Count)&&(dotypeitem.SDO.Count==dotype.SDO.Count))
                {
                    bool b = true;
                    foreach (tDA da in dotype.DA)
                        if (!dotypeitem.CheckIfExist(da))
                            b = false;
                    foreach (tSDO sdo in dotype.SDO)
                        if (!dotypeitem.CheckIfExist(sdo))
                            b = false;

                    if ((dotype.cdc == dotypeitem.cdc) && (b))
                        return dotypeitem;
                }
            }
            return null;
        }

        private tDAType CheckExistingUsual(tDAType datype)
        {
            foreach (tDAType datypeitem in DAType)
            {
                if (((datype.BDA!=null)&&(datype.BDA.Count == datypeitem.BDA.Count)) &&
                    (datype.id.Substring(datype.id.LastIndexOf('.') + 1) == datypeitem.id.Substring(datypeitem.id.LastIndexOf('.') + 1)))
                {
                    bool b = true;
                    foreach (tBDA bda in datypeitem.BDA)
                    {
                        if (!datype.CheckIfBDAExist(bda)) b = false;
                    }
                    if (b)
                        return datypeitem;
                }

            }
            return null;
        }


        public tDAType AddDAType(tDAType datype)
        {
            if (datype == null)
                return null;
            tDAType existing = CheckExistingUsual(datype);
            if (existing == null)
            {
                tDAType toadd = new tDAType();
                toadd.id = datype.id;
                toadd.iedType = datype.iedType;
                toadd.BDA.AddRange(datype.BDA);
                DAType.Add(toadd);
                return datype;
            }
            return existing;
        }

        public tEnumType AddEnumType(tEnumType enumtype)
        {
            if (enumtype == null)
                return null;
            tEnumType existing = CheckIfExist(enumtype);
            if (existing == null)
            {
                EnumType.Add(enumtype);
                return enumtype;
            }
            return existing;
        }

        private tEnumType CheckIfExist(tEnumType enumtype)
        {
           foreach(tEnumType enumtypeitem in EnumType)
            {                
                if (tEnumType.IsEqual( enumtypeitem, enumtype)) return enumtypeitem;
            }
            return null;
        }

        public void RemoveUnused()
        {
            DAType = DAType.Where((type => type.IsInUse)).ToList();
            DOType = DOType.Where((type => type.IsInUse)).ToList();
            EnumType = EnumType.Where((type => type.IsInUse)).ToList();
            LNodeType = LNodeType.Where((type => type.IsInUse)).ToList();
        }

        public void MarkAllAsUnused()
        {
            foreach (var daType in DAType)
            {
                daType.IsInUse = false;
            }

            foreach (var doType in DOType)
            {
                doType.IsInUse = false;
            }

            foreach (var enumType in EnumType)
            {
                enumType.IsInUse = false;
            }

            foreach (var lNodeType in LNodeType)
            {
                lNodeType.IsInUse = false;
            }
        }
    }
}