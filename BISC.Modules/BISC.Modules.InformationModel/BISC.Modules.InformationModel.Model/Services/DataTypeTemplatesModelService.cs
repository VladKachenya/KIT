using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DaType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.EnumType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.LNodeType;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Model.DataTypeTemplates.DoType;
using BISC.Modules.InformationModel.Model.DataTypeTemplates.LNodeType;
using BISC.Modules.InformationModel.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BISC.Modules.InformationModel.Model.Services
{
    public class DataTypeTemplatesModelService : IDataTypeTemplatesModelService
    {
        private IDataTypeTemplates GetDataTypeTemplates(ISclModel sclModel)
        {
            if (sclModel.TryGetFirstChildOfType(out IDataTypeTemplates dataTypeTemplates))
            {
                return dataTypeTemplates;
            }
            else
            {
                IDataTypeTemplates newdataTypeTemplates = new DataTypeTemplates.DataTypeTemplates();
                sclModel.ChildModelElements.Add(newdataTypeTemplates);
                return newdataTypeTemplates;
            }
        }


        public void MergeDataTypeTemplates(ISclModel sclModelTo, ISclModel sclModelFrom)
        {
            var dttFrom = GetDataTypeTemplates(sclModelFrom);
            foreach (var doType in dttFrom.DoTypes)
            {
                var resid = AddDoType(doType, sclModelTo);
                if (resid != doType.Id)
                {
                    List<IDo> doList = new List<IDo>();
                    dttFrom.GetAllChildrenOfType(ref doList);
                    foreach (var node in doList.Where(node => node.Type == doType.Id))
                    {
                        node.Type = resid;
                    }
                    //var t = doList.Where(node => node.Type == doType.Id).ToList();
                }

            }
            foreach (var daType in dttFrom.DaTypes)
            {
                var resId = AddDaType(daType, sclModelTo);
                if (daType.Id != resId)
                {
                    List<IDa> daList = new List<IDa>();
                    dttFrom.GetAllChildrenOfType(ref daList);
                    foreach (var node in daList.Where(node => node.Type == daType.Id))
                    {
                        if(resId == "MR5PO50N245CTRL.CSWI1.Pos.Oper")
                        {
                        }
                        node.Type = resId;
                    }
                    //var t = daList.Where(node => node.Type == daType.Id).ToList();
                }
            }
            foreach (var lNodeType in dttFrom.LNodeTypes)
            {
                var resId = AddLnodeType(lNodeType, sclModelTo);
                if (lNodeType.Id != resId)
                {
                    List<ILogicalNode> lnList = new List<ILogicalNode>();
                    sclModelFrom.GetAllChildrenOfType(ref lnList);
                    foreach (var node in lnList.Where(node => node.LnType == lNodeType.Id))
                    {
                        node.LnType = resId;
                    }
                    //var t = lnList.Where(node => node.LnClass == lNodeType.Id).ToList();
                }
            }
            foreach (var enumType in dttFrom.EnumTypes)
            {
                var resId = AddEnumType(enumType, sclModelTo);
                if (enumType.Id != resId)
                {

                }
            }
        }

        public void FilterDataTypeTemplates(IDataTypeTemplates dataTypeTemplates, List<ILDevice> lDevicesToExclude,
            List<ILDevice> lDevicesToLeave)
        {
            var lntypesToExclude = GetAllLnypesOfDevices(lDevicesToExclude);
            var lntypesToLeave = GetAllLnypesOfDevices(lDevicesToLeave);

            var lnTypesToExcludeFiltered =
                lntypesToExclude.Where((exclude => !lntypesToLeave.Any((leave => leave == exclude)))).Distinct().ToList();

            List<string> doTypesToExclude = new List<string>();


            foreach (var lnTypeToExcludeFiltered in lnTypesToExcludeFiltered)
            {
                var removeItem = dataTypeTemplates.LNodeTypes.FirstOrDefault((type => type.Id == lnTypeToExcludeFiltered));
                if (removeItem == null)
                {
                    continue;
                }

                doTypesToExclude.AddRange(removeItem.DoList.Select((ddo => ddo.Type)));
                dataTypeTemplates.LNodeTypes.Remove(removeItem);
            }



            RemoveDoTypes(dataTypeTemplates, doTypesToExclude);


        }


        private void RemoveDoTypes(IDataTypeTemplates dataTypeTemplates, List<string> doTypesToExclude)
        {
            List<string> doTypesToExcludeNew = new List<string>();
            List<string> doTypesToLeave = new List<string>();
            var enumTypesToExclude = new List<string>();

            dataTypeTemplates.LNodeTypes.ToList().ForEach((type =>
            {
                type.DoList.ToList().ForEach((ddo =>
                {

                    doTypesToLeave.Add(ddo.Type);
                }));
            }));

            var doTypesToExcludeFiltered =
                doTypesToExclude.Where((exclude => !doTypesToLeave.Any((leave => leave == exclude)))).Distinct().ToList();


            var daTypesToExclude = new List<string>();
            foreach (var doTypeToExcludeFiltered in doTypesToExcludeFiltered)
            {
                var removeItem = dataTypeTemplates.DoTypes.FirstOrDefault((type => type.Id == doTypeToExcludeFiltered));
                if (removeItem != null)
                {
                    enumTypesToExclude.AddRange(removeItem.DaList.Where((da => da.BType == "Enum"))
                        .Select((da => da.Type)));
                    daTypesToExclude.AddRange(removeItem.DaList.Select((da => da.Type)));
                    doTypesToExcludeNew.AddRange(removeItem.SdoList.Select((sdo => sdo.Type)));

                    dataTypeTemplates.DoTypes.Remove(removeItem);
                }
            }
            if (doTypesToExcludeNew.Count > 0)
            {
                RemoveDoTypes(dataTypeTemplates, doTypesToExcludeNew);
            }

            List<string> daTypesToLeave = new List<string>();
            dataTypeTemplates.DoTypes.ToList().ForEach((type => type.DaList.ToList().ForEach((da => daTypesToLeave.Add(da.Type)))));

            var daTypesToExcludeFiltered =
                daTypesToExclude.Where((exclude => !daTypesToLeave.Any((leave => leave == exclude)))).Distinct().ToList();
            RemoveDaTypes(dataTypeTemplates, daTypesToExcludeFiltered);
            RemoveEnums(dataTypeTemplates, enumTypesToExclude);

        }

        private void RemoveDaTypes(IDataTypeTemplates dataTypeTemplates, List<string> daTypesToExcludeFiltered)
        {
            var enumTypesToExclude = new List<string>();
            var daToExcludeNew = new List<string>();


            foreach (var daTypeToExcludeFiltered in daTypesToExcludeFiltered)
            {
                if (daTypeToExcludeFiltered != null && daTypeToExcludeFiltered.Contains("Mod.Oper"))
                {

                }
                var removeItem = dataTypeTemplates.DaTypes.FirstOrDefault((type => type.Id == daTypeToExcludeFiltered));
                if (removeItem != null)
                {
                    enumTypesToExclude.AddRange(removeItem.Bdas.Where((bda => bda.BType == "Enum"))
                        .Select((bda => bda.Type)));
                    daToExcludeNew.AddRange(removeItem.Bdas.Where((bda => bda.BType == "Struct"))
                        .Select((bda => bda.Type)));
                    dataTypeTemplates.DaTypes.Remove(removeItem);
                }
            }

            if (daToExcludeNew.Count > 0)
            {
                RemoveDaTypes(dataTypeTemplates, daToExcludeNew);
            }
            RemoveEnums(dataTypeTemplates, enumTypesToExclude);

        }

        private void RemoveEnums(IDataTypeTemplates dataTypeTemplates, List<string> enumTypesToExclude)
        {
            var enumTypesToLeave = new List<string>();
            dataTypeTemplates.DaTypes.ToList().ForEach((type => type.Bdas.ToList().ForEach((bda =>
            {
                if (bda.BType == "Enum")
                {
                    enumTypesToLeave.Add(bda.Type);
                }
            }))));
            var enumTypesToExcludeFiltered =
                enumTypesToExclude.Where((exclude => !enumTypesToLeave.Any((leave => leave == exclude)))).Distinct().ToList();

            foreach (var enumTypeToExcludeFiltered in enumTypesToExcludeFiltered)
            {
                var removeItem = dataTypeTemplates.EnumTypes.FirstOrDefault((type => type.Id == enumTypeToExcludeFiltered));
                if (removeItem != null)
                {
                    dataTypeTemplates.EnumTypes.Remove(removeItem);
                }
            }

        }


        private List<string> GetAllLnypesOfDevices(List<ILDevice> lDevices)
        {
            var lnTypesToExclude = new List<string>();
            foreach (var lDevice in lDevices)
            {
                foreach (var logicalNode in lDevice.LogicalNodes)
                {
                    lnTypesToExclude.Add(logicalNode.LnType);
                }
                lnTypesToExclude.Add(lDevice.LogicalNodeZero.Value.LnType);
            }

            return lnTypesToExclude;
        }




        public string AddLnodeType(ILNodeType lNodeType, ISclModel sclModel)
        {
            IDataTypeTemplates dataTypeTemplates = GetDataTypeTemplates(sclModel);
            if (lNodeType == null)
            {
                return null;
            }

            var existing = GetExisting(lNodeType, dataTypeTemplates);
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
                    if (!nodetypetypeitem.DoList.ToList().Exists(oldDo => oldDo.Type == newDo.Type))
                    {
                        isExist = false;
                        break;
                    }
                }
                foreach (var newDo in nodetype.DoList)
                {
                    if (!nodetypetypeitem.DoList.ToList().Exists(oldDo => oldDo.Name == newDo.Name))
                    {
                        isExist = false;
                        break;
                    }
                }
                foreach (IDo oldDo in nodetypetypeitem.DoList)
                {
                    if (!nodetype.DoList.ToList().Exists(newDo => newDo.Type == oldDo.Type))
                    {
                        isExist = false;
                        break;
                    }
                }

                foreach (IDo oldDo in nodetypetypeitem.DoList)
                {
                    if (!nodetype.DoList.ToList().Exists(newDo => newDo.Name == oldDo.Name))
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
            {
                return null;
            }

            var existing = CheckExisting(dotype, dataTypeTemplates);
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

                    if (CheckIfExist(dotype, da))
                    {
                        continue;
                    }

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

        private IDoType CheckExisting(IDoType dotype, IDataTypeTemplates dataTypeTemplates)
        {
            if (dotype.Id.Contains("BlkZ"))
            {

            }
            foreach (IDoType dotypeitem in dataTypeTemplates.DoTypes)
            {
                if (dotypeitem.Id == dotype.Id)
                {
                    return dotypeitem;
                }
            }
            //foreach (IDoType dotypeitem in dataTypeTemplates.DoTypes)
            //{
            //    if ((dotypeitem.DaList.Count == dotype.DaList.Count) && (dotypeitem.SdoList.Count == dotype.SdoList.Count))
            //    {
            //        bool b = true;
            //        foreach (var da in dotype.DaList)
            //            if (!CheckIfExist(dotypeitem, da))
            //                b = false;
            //        foreach (var sdo in dotype.SdoList)
            //            if (!CheckIfExist(dotypeitem, sdo))
            //                b = false;

            //        if ((dotype.Cdc == dotypeitem.Cdc) && (b))
            //            return dotypeitem;
            //    }
            //}
            return null;
        }

        public bool CheckIfExist(IDoType do1, IDa newDa)
        {
            if (do1.DaList.Contains(newDa))
            {
                return true;
            }

            foreach (var daitem in do1.DaList)
            {
                if ((newDa.Type == daitem.Type) &&
                    (newDa.BType == daitem.BType) &&
                      (newDa.Name == daitem.Name) &&
                    //(newDa.count == daitem.count) &&
                    //(newDa.dchg == daitem.dchg) &&
                    //(newDa.qchg == daitem.qchg) &&
                    //(newDa.dupd == daitem.dupd) &&
                    (newDa.Fc == daitem.Fc))
                {
                    return true;
                }
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
            {
                return null;
            }

            IDataTypeTemplates dataTypeTemplates = GetDataTypeTemplates(sclModel);
            IDaType existing = CheckExistingUsual(datype, dataTypeTemplates);
            if (existing == null)
            {
                dataTypeTemplates.DaTypes.Add(datype);
                return datype.Id;
            }
            return existing.Id;
        }

        private IDaType CheckExistingUsual(IDaType datype, IDataTypeTemplates dataTypeTemplates)
        {
            foreach (IDaType datypeitem in dataTypeTemplates.DaTypes)
            {
                if (((datype.Bdas != null) && (datype.Bdas.Count == datypeitem.Bdas.Count)) &&
                    (datype.Id.Substring(datype.Id.LastIndexOf('.') + 1) == datypeitem.Id.Substring(datypeitem.Id.LastIndexOf('.') + 1)) &&
                    datype.Id == datypeitem.Id
                    )
                {
                    bool b = true;
                    foreach (IBda bda in datypeitem.Bdas)
                    {
                        if (!CheckIfBDAExist(datypeitem, bda))
                        {
                            b = false;
                        }
                    }
                    if (b)
                    {
                        return datypeitem;
                    }
                }

            }
            return null;
        }
        private bool CheckIfBDAExist(IDaType daType, IBda bda)
        {
            foreach (IBda bdaitem in daType.Bdas)
            {
                if ((bda.Type == bdaitem.Type) &&
                    (bda.BType == bdaitem.BType) &&
                    (bda.Name == bdaitem.Name))
                {
                    return true;
                }
            }
            return false;
        }
        public string AddEnumType(IEnumType enumtype, ISclModel sclModel)
        {
            IDataTypeTemplates dataTypeTemplates = GetDataTypeTemplates(sclModel);

            if (enumtype == null)
            {
                return null;
            }

            IEnumType existing = CheckIfExist(enumtype, dataTypeTemplates);
            if (existing == null)
            {
                dataTypeTemplates.EnumTypes.Add(enumtype);
                return enumtype.Id;
            }
            return existing.Id;
        }

        public IDa GetDaOfDai(IDai dai, ISclModel sclModel)
        {
            ILDevice parentLDevice = null;
            ILogicalNode parentLogicalNode = null;
            IDoi parentDoi = null;
            List<object> recursiveParents = new List<object>();
            IModelElement currentElement = dai;
            recursiveParents.Add(currentElement);

            do
            {
                currentElement = currentElement.ParentModelElement;

                if (currentElement is ILDevice)
                {
                    parentLDevice = currentElement as ILDevice;
                    continue;
                }

                if (currentElement is ILogicalNode)
                {
                    parentLogicalNode = currentElement as ILogicalNode;
                    continue;
                }

                if (currentElement is IDoi)
                {
                    parentDoi = currentElement as IDoi;
                    continue;
                }

                recursiveParents.Insert(0, currentElement);
            } while (parentLDevice == null);

            IDataTypeTemplates dataTypeTemplates = GetDataTypeTemplates(sclModel);
            ILNodeType lNodeType =
                dataTypeTemplates.LNodeTypes.FirstOrDefault((type => type.Id == parentLogicalNode.LnType));
            IDo parentDo = lNodeType.DoList.FirstOrDefault(parentDoToFind => parentDoToFind.Name == parentDoi.Name);

            IDoType parentDoType = dataTypeTemplates.DoTypes.FirstOrDefault((type => type.Id == parentDo.Type));
            foreach (var recursiveParent in recursiveParents)
            {
                if (recursiveParent is IDai daiParent)
                {
                    return parentDoType.DaList.FirstOrDefault((da => da.Name == daiParent.Name));
                }

                if (recursiveParent is ISdi sdiParent)
                {
                    var parentSdo = parentDoType.SdoList.FirstOrDefault((sdo => sdo.Name == sdiParent.Name));
                    if (parentSdo != null)
                    {
                        parentDoType = dataTypeTemplates.DoTypes.First((type => type.Id == parentSdo.Type));
                    }
                    else
                    {
                        return parentDoType.DaList.First((da => da.Name == sdiParent.Name));
                    }
                }
            }

            return null;
        }



        private IEnumType CheckIfExist(IEnumType enumtype, IDataTypeTemplates dataTypeTemplates)
        {
            foreach (IEnumType enumtypeitem in dataTypeTemplates.EnumTypes)
            {
                if (IsEqual(enumtypeitem, enumtype))
                {
                    return enumtypeitem;
                }
            }
            return null;
        }

        private bool IsEqual(IEnumType obj1, IEnumType obj2)
        {
            try
            {
                if ((obj1.Id != obj2.Id) && !obj1.Id.Contains(obj2.Id) && !obj2.Id.Contains(obj1.Id))
                {
                    return false;
                }

                if (obj1.EnumValList.Count != obj2.EnumValList.Count)
                {
                    return false;
                }

                int i = 0;
                foreach (IEnumVal enumvalitem in obj1.EnumValList)
                {
                    if ((enumvalitem.Ord != obj2.EnumValList[i].Ord) || (enumvalitem.Value != obj2.EnumValList[i].Value))
                    {
                        return false;
                    }

                    i++;
                }
                return true;

            }
            catch (Exception e)
            {


            }
            return true;
        }

        public void UpdateTemplatesUnderIdeName(ISclModel sclModel, string oldIdeName, string newIdeName)
        {
            var templatesWithId = GetDataTypeTemplates(sclModel).GetAllIds();
            var replaser = new IdeNameInStringReplacer();
            foreach (var id in templatesWithId)
            {
                id.Id = replaser.ReplaseIdeNameInStringWithoutExeption(id.Id, oldIdeName, newIdeName);
                id.GetAllITypes().ForEach(el => el.Type = replaser.ReplaseIdeNameInStringWithoutExeption(el.Type, oldIdeName, newIdeName));
            }
        }

        public IEnumType GetEnumTypeForDa(IDa da)
        {
            IDataTypeTemplates dataTypeTemplates = da.GetFirstParentOfType<IDataTypeTemplates>();
           return dataTypeTemplates.EnumTypes.FirstOrDefault((type => type.Id == da.Type));
        }
    }

   
}
