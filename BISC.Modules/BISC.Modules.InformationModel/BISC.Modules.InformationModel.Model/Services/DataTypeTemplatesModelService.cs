using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Iec61850Ed2.DataTypeTemplates;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DaType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.EnumType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.LNodeType;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Model.DataTypeTemplates.DoType;
using BISC.Modules.InformationModel.Model.DataTypeTemplates.LNodeType;

namespace BISC.Modules.InformationModel.Model.Services
{
   public class DataTypeTemplatesModelService: IDataTypeTemplatesModelService
    {
        public IDataTypeTemplates MergeDataTypeTemplates(IDataTypeTemplates dataTypeTemplates1, IDataTypeTemplates dataTypeTemplates2)
        {
            throw new NotImplementedException();
        }

        private IDataTypeTemplates GetDataTypeTemplates(ISclModel sclModel)
        {
            if (sclModel.TryGetFirstChildOfType(out IDataTypeTemplates dataTypeTemplates))
            {
                return dataTypeTemplates;
            }
            else
            {
                IDataTypeTemplates newdataTypeTemplates=new DataTypeTemplates.DataTypeTemplates();
                sclModel.ChildModelElements.Add(newdataTypeTemplates);
                return newdataTypeTemplates;
            }
        }


        public string AddLnodeType(ILNodeType lNodeType, ISclModel sclModel)
        {
            IDataTypeTemplates dataTypeTemplates = GetDataTypeTemplates(sclModel);
            if (lNodeType == null)
                return null;
            var existing = GetExisting(lNodeType,dataTypeTemplates);
            if (existing == null)
            {
                var toadd = new LNodeType();
                toadd.Id = lNodeType.Id;
                toadd.LnClass = lNodeType.LnClass;
           
                toadd.DoList.AddRange(lNodeType.DoList);
                dataTypeTemplates.LNodeTypes.Add(toadd);
                return toadd.Id;
            }
            return existing.Id;
        }

        private ILNodeType GetExisting(ILNodeType nodetype, IDataTypeTemplates dataTypeTemplates)
        {
            foreach (var nodetypetypeitem in dataTypeTemplates.LNodeTypes)
            {
                // if ((nodetype.DO.Count == nodetypetypeitem.DO.Count) &&
                // (nodetype.id.Substring(nodetype.id.LastIndexOf('.')+1, nodetype.id.Length - nodetype.id.LastIndexOf('.') - 2) ==
                //nodetypetypeitem.id.Substring(nodetypetypeitem.id.LastIndexOf('.')+1, nodetypetypeitem.id.Length - nodetypetypeitem.id.LastIndexOf('.') - 2))) return nodetypetypeitem;      
                // //проверка только по количеству     
                bool isExist = true;
                foreach (IDo newDo in nodetype.DoList)
                {
                    if (!nodetypetypeitem.DoList.Exists(oldDo => oldDo.Type == newDo.Type))
                    {
                        isExist = false;
                        break;
                    }
                }
                foreach (var newDo in nodetype.DoList)
                {
                    if (!nodetypetypeitem.DoList.Exists(oldDo => oldDo.Name == newDo.Name))
                    {
                        isExist = false;
                        break;
                    }
                }
                foreach (IDo oldDo in nodetypetypeitem.DoList)
                {
                    if (!nodetype.DoList.Exists(newDo => newDo.Type == oldDo.Type))
                    {
                        isExist = false;
                        break;
                    }
                }

                foreach (IDo oldDo in nodetypetypeitem.DoList)
                {
                    if (!nodetype.DoList.Exists(newDo => newDo.Name == oldDo.Name))
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



        public string AddDoType(IDoType dotype, ISclModel sclModel)
        {
            IDataTypeTemplates dataTypeTemplates = GetDataTypeTemplates(sclModel);
            if (dotype == null)
                return null;
            var existing = CheckExisting(dotype,dataTypeTemplates);
            if (existing == null)
            {
                var toadd = new DoType();
                toadd.Id = dotype.Id;
                toadd.Cdc = dotype.Cdc;
              
                toadd.DaList.AddRange(dotype.DaList);
                toadd.SdoList.AddRange(dotype.SdoList);
                dataTypeTemplates.DoTypes.Add(toadd);
                return dotype.Id;
            }
            else
            {
                foreach (IDa da in dotype.DaList)
                {
                 
                    if (CheckIfExist(dotype,da)) continue;
                 
                    if (da.Name == "stVal")
                        // костыль для того, чтобы stVal был первым в списке,чтобы контроллер читал файл 
                        //(он глупый и думает, что stVal по порядку первый)

                    {
                        existing.DaList.Insert(0, da);
                        continue;
                    }
                    existing.DaList.Add(da);
                }
                return existing.Id;
            }
        }

        private IDoType CheckExisting(IDoType dotype,IDataTypeTemplates dataTypeTemplates)
        {
            foreach (IDoType dotypeitem in dataTypeTemplates.DoTypes)
            {
                if (dotypeitem.Id == dotype.Id)
                    return dotypeitem;
            }
            foreach (IDoType dotypeitem in dataTypeTemplates.DoTypes)
            {
                if ((dotypeitem.DaList.Count == dotype.DaList.Count) && (dotypeitem.SdoList.Count == dotype.SdoList.Count))
                {
                    bool b = true;
                    foreach (var da in dotype.DaList)
                        if (!CheckIfExist(dotypeitem,da))
                            b = false;
                    foreach (var sdo in dotype.SdoList)
                        if (!CheckIfExist(dotypeitem, sdo))
                            b = false;

                    if ((dotype.Cdc == dotypeitem.Cdc) && (b))
                        return dotypeitem;
                }
            }
            return null;
        }

        public bool CheckIfExist(IDoType do1,IDa newDa)
        {
            if (do1.DaList.Contains(newDa)) return true;
            foreach (var daitem in do1.DaList)
            {
                if ((newDa.Type == daitem.Type) &&
                    (newDa.BType == daitem.BType) &&
                  //  (newDa.type == daitem.type) &&
                    //(newDa.count == daitem.count) &&
                    //(newDa.dchg == daitem.dchg) &&
                    //(newDa.qchg == daitem.qchg) &&
                    //(newDa.dupd == daitem.dupd) &&
                    (newDa.Fc == daitem.Fc)) return true;
            }
            return false;
        }
        public bool CheckIfExist(IDoType do1, ISdo sdo)
        {
            return do1.SdoList.Any(sdoitem => (sdoitem.Type == sdo.Type) && (sdoitem.Name == sdo.Name));
        }


        public string AddDaType(IDaType datype, ISclModel sclModel)
        {
            if (datype == null)
                return null;
            IDataTypeTemplates dataTypeTemplates = GetDataTypeTemplates(sclModel);
            IDaType existing = CheckExistingUsual(datype,dataTypeTemplates);
            if (existing == null)
            {
                dataTypeTemplates.DaTypes.Add(datype);
                return datype.Id;
            }
            return existing.Id;
        }

        private IDaType CheckExistingUsual(IDaType datype,IDataTypeTemplates dataTypeTemplates)
        {
            foreach (IDaType datypeitem in dataTypeTemplates.DaTypes)
            {
                if (((datype.Bdas != null) && (datype.Bdas.Count == datypeitem.Bdas.Count)) &&
                    (datype.Id.Substring(datype.Id.LastIndexOf('.') + 1) == datypeitem.Id.Substring(datypeitem.Id.LastIndexOf('.') + 1)))
                {
                    bool b = true;
                    foreach (IBda bda in datypeitem.Bdas)
                    {
                        if (!CheckIfBDAExist(datypeitem,bda)) b = false;
                    }
                    if (b)
                        return datypeitem;
                }

            }
            return null;
        }
        private bool CheckIfBDAExist(IDaType daType,IBda bda)
        {
            foreach (IBda bdaitem in daType.Bdas)
            {
                if ((bda.Type == bdaitem.Type) &&
                    (bda.BType == bdaitem.BType) &&
                    (bda.Name == bdaitem.Name))
                     return true;
            }
            return false;
        }
        public string AddEnumType(IEnumType enumtype, ISclModel sclModel)
        {
            IDataTypeTemplates dataTypeTemplates = GetDataTypeTemplates(sclModel);

            if (enumtype == null)
                return null;
            IEnumType existing = CheckIfExist(enumtype,dataTypeTemplates);
            if (existing == null)
            {
                dataTypeTemplates.EnumTypes.Add(enumtype);
                return enumtype.Id;
            }
            return existing.Id;
        }

        private IEnumType CheckIfExist(IEnumType enumtype,IDataTypeTemplates dataTypeTemplates)
        {
            foreach (IEnumType enumtypeitem in dataTypeTemplates.EnumTypes)
            {
                if (IsEqual(enumtypeitem, enumtype)) return enumtypeitem;
            }
            return null;
        }

        private  bool IsEqual(IEnumType obj1, IEnumType obj2)
        {
            try
            {
                if ((obj1.Id != obj2.Id) && !obj1.Id.Contains(obj2.Id) && !obj2.Id.Contains(obj1.Id)) return false;


                if (obj1.EnumValList.Count != obj2.EnumValList.Count) return false;
                int i = 0;
                foreach (IEnumVal enumvalitem in obj1.EnumValList)
                {
                    if ((enumvalitem.Ord != obj2.EnumValList[i].Ord) || (enumvalitem.Value != obj2.EnumValList[i].Value)) return false;
                    i++;
                }
                return true;

            }
            catch (Exception e)
            {


            }
            return true;

        }
    }
}
