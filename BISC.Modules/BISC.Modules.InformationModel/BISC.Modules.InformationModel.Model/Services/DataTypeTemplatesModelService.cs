using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;
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
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.InformationModel.Model.Elements;

namespace BISC.Modules.InformationModel.Model.Services
{
    public class DataTypeTemplatesModelService : IDataTypeTemplatesModelService
    {
        private readonly ILoggingService _loggingService;

        #region Ctor
        public DataTypeTemplatesModelService(
            ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        #endregion



        #region Implementation of IDataTypeTemplatesModelService

        public void MergeDataTypeTemplatesOfDevice(ISclModel sclModelTo, ISclModel sclModelFrom, IDevice device)
        {
            var dttFrom = GetDataTypeTemplatesOfDevice(sclModelFrom, device);
            MergeDtt(sclModelTo, sclModelFrom, dttFrom);
        }

        public void MergeDataTypeTemplates(ISclModel sclModelTo, ISclModel sclModelFrom)
        {
            var dttFrom = GetDataTypeTemplates(sclModelFrom);
            MergeDtt(sclModelTo, sclModelFrom, dttFrom);
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

        public string AddLnodeType(ILNodeType lNodeType, ISclModel sclModel)
        {
            IDataTypeTemplates dataTypeTemplates = GetDataTypeTemplates(sclModel);
            if (lNodeType == null)
            {
                return null;
            }
            return AddLnTypeToDataTypeTemplates(lNodeType, dataTypeTemplates);
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

        #endregion

        #region Private members

        private string AddLnTypeToDataTypeTemplates(ILNodeType lNodeType, IDataTypeTemplates dataTypeTemplates)
        {
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


        private IDataTypeTemplates GetDataTypeTemplates(ISclModel sclModel)
        {
            if (sclModel.TryGetFirstChildOfType(out IDataTypeTemplates dataTypeTemplates))
            {
                return dataTypeTemplates;
            }

            IDataTypeTemplates newDataTypeTemplates = new DataTypeTemplates.DataTypeTemplates();
            sclModel.ChildModelElements.Add(newDataTypeTemplates);
            return newDataTypeTemplates;
        }

        private IDataTypeTemplates GetDataTypeTemplatesOfDevice(ISclModel sclModel, IDevice device)
        {
            IDataTypeTemplates deviceDataTypeTemplates = new DataTypeTemplates.DataTypeTemplates();
            var logicalDevices = (device.ChildModelElements
                .First((element => element is DeviceAccessPoint)) as DeviceAccessPoint)?
                .DeviceServer?
                .Value?
                .LDevicesCollection;
            if (sclModel.TryGetFirstChildOfType(out IDataTypeTemplates dataTypeTemplates))
            {
                if (logicalDevices != null)
                {

                    foreach (var logicalDevice in logicalDevices)
                    {
                        foreach (var logicalNode in logicalDevice.AlLogicalNodes)
                        {
                            var lnType =
                                dataTypeTemplates.LNodeTypes.FirstOrDefault(lnt => lnt.Id == logicalNode.LnType);
                            if (lnType == null)
                            {
                                _loggingService.LogMessage($"LN data template of {logicalNode.LnType} not found",
                                    SeverityEnum.Warning);
                                continue;
                            }

                            if (deviceDataTypeTemplates.LNodeTypes.Contains(lnType))
                            {
                                continue;
                            }
                            deviceDataTypeTemplates.LNodeTypes.Add(lnType);

                            foreach (var doElement in lnType.DoList)
                            {
                                var doType = dataTypeTemplates.DoTypes.FirstOrDefault(dot => dot.Id == doElement.Type);
                                if (doType == null)
                                {
                                    _loggingService.LogMessage($"Do data template of {doElement.Type} not found",
                                        SeverityEnum.Warning);
                                    continue;
                                }

                                if (deviceDataTypeTemplates.DoTypes.Contains(doType))
                                {
                                    continue;
                                }
                                deviceDataTypeTemplates.DoTypes.Add(doType);

                                var daElements = ParseSdoToDataTemplate(dataTypeTemplates, deviceDataTypeTemplates, doType);
                                foreach (var enumElement in daElements.Where(da => da.BType == "Enum"))
                                {
                                    var enumType =
                                        dataTypeTemplates.EnumTypes.FirstOrDefault(et => et.Id == enumElement.Type);
                                    if (enumType == null)
                                    {
                                        _loggingService.LogMessage($"Do data template of {enumElement.Type} not found",
                                            SeverityEnum.Warning);
                                        continue;
                                    }

                                    if (deviceDataTypeTemplates.EnumTypes.Contains(enumType))
                                    {
                                        continue;
                                    }
                                    deviceDataTypeTemplates.EnumTypes.Add(enumType);
                                }

                                foreach (var daElement in daElements.Where(da => da.BType == "Struct"))
                                {
                                    var daType = dataTypeTemplates.DaTypes.FirstOrDefault(dat => dat.Id == daElement.Type);
                                    if (daType == null)
                                    {
                                        _loggingService.LogMessage($"Da data template of {daElement.Type} not found",
                                            SeverityEnum.Warning);
                                        continue;
                                    }

                                    if (deviceDataTypeTemplates.DaTypes.Contains(daType))
                                    {
                                        continue;
                                    }

                                    deviceDataTypeTemplates.DaTypes.Add(daType);

                                    ParseBdaToDataTemplate(dataTypeTemplates, deviceDataTypeTemplates, daType);
                                }

                            }
                        }
                    }
                }
            }
            return deviceDataTypeTemplates;
        }

        private void ParseBdaToDataTemplate(IDataTypeTemplates dataTypeTemplates,
            IDataTypeTemplates deviceDataTypeTemplates, IDaType daType)
        {
            Queue<IBda> bdaQueue = new Queue<IBda>(daType.Bdas.Where(bda => bda.BType == "Struct"));
            List<IBda> bdaEnums = new List<IBda>(daType.Bdas.Where(da => da.BType == "Enum"));

            while (bdaQueue.Count != 0)
            {
                var bdaElement = bdaQueue.Dequeue();
                var bdaDaType = dataTypeTemplates.DaTypes.First(dat => dat.Id == bdaElement.Type);
                if (bdaDaType == null)
                {
                    _loggingService.LogMessage($"Do data template of {bdaElement.Type} not found",
                        SeverityEnum.Warning);
                    continue;
                }

                if (deviceDataTypeTemplates.DaTypes.Contains(bdaDaType))
                {
                    continue;
                }
                deviceDataTypeTemplates.DaTypes.Add(bdaDaType);

                foreach (var bda in bdaDaType.Bdas.Where(bdaEl => bdaEl.BType == "Struct"))
                {
                    bdaQueue.Enqueue(bda);
                }

                bdaEnums.AddRange(bdaDaType.Bdas.Where(bdaEl => bdaEl.BType == "Enum"));
            }

            foreach (var enumElement in bdaEnums)
            {
                var enumType =
                    dataTypeTemplates.EnumTypes.FirstOrDefault(et => et.Id == enumElement.Type);
                if (enumType == null)
                {
                    _loggingService.LogMessage($"Do data template of {enumElement.Type} not found",
                        SeverityEnum.Warning);
                    continue;
                }

                if (deviceDataTypeTemplates.EnumTypes.Contains(enumType))
                {
                    continue;
                }
                deviceDataTypeTemplates.EnumTypes.Add(enumType);
            }
        }

        private List<IDa> ParseSdoToDataTemplate(IDataTypeTemplates dataTypeTemplates, IDataTypeTemplates deviceDataTypeTemplates, IDoType doType)
        {
            Queue<ISdo> sdoQueue = new Queue<ISdo>(doType.SdoList);
            List<IDa> result = new List<IDa>(doType.DaList.Where(da => da.BType == "Enum" || da.BType == "Struct"));

            while (sdoQueue.Count != 0)
            {
                var sdoElement = sdoQueue.Dequeue();
                var sdoDoType = dataTypeTemplates.DoTypes.FirstOrDefault(dot => dot.Id == sdoElement.Type);
                if (sdoDoType == null)
                {
                    _loggingService.LogMessage($"Do data template of {sdoElement.Type} not found",
                        SeverityEnum.Warning);
                    continue;
                }

                if (deviceDataTypeTemplates.DoTypes.Contains(sdoDoType))
                {
                    continue;
                }
                deviceDataTypeTemplates.DoTypes.Add(sdoDoType);


                foreach (var sdo in doType.SdoList)
                {
                    sdoQueue.Enqueue(sdo);
                }

                result.AddRange(doType.DaList.Where(da => da.BType == "Enum" || da.BType == "Struct"));
            }

            return result;
        }

        private void MergeDtt(ISclModel sclModelTo, ISclModel sclModelFrom, IDataTypeTemplates dttFrom)
        {
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
                        node.Type = resId;
                    }
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
                }
            }

            foreach (var enumType in dttFrom.EnumTypes)
            {
                AddEnumType(enumType, sclModelTo);
            }
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

        private ILNodeType GetExisting(ILNodeType nodetype, IDataTypeTemplates dataTypeTemplates)
        {
            foreach (var nodetypetypeitem in dataTypeTemplates.LNodeTypes)
            {
                //проверка только по количеству     
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
            return null;
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
        #endregion
    }
}