using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Iec61850Ed2;
using BISC.Model.Iec61850Ed2.DataTypeTemplates;
using BISC.Model.Iec61850Ed2.DataTypeTemplates.Base;
using BISC.Model.Iec61850Ed2.SclModelTemplates;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services;
using BISC.Modules.Connection.Infrastructure.Connection;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Modules.InformationModel.Model.Elements;
using BISC.Modules.InformationModel.Model.Extensions;
using BISC.Modules.InformationModel.Model.LoadingFromConnection;

namespace BISC.Modules.InformationModel.Model.Services
{
    public class LogicalDeviceLoadingService : ILogicalDeviceLoadingService
    {
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IModelTypesResolvingService _modelTypesResolvingService;
        private readonly IDataTypeTemplatesModelService _dataTypeTemplatesModelService;
        private Dictionary<string, List<string>> ldDictionary = new Dictionary<string, List<string>>();
        private string _deviceName;
        private string _ip;
        private IDeviceConnection _connection;

        public LogicalDeviceLoadingService(IConnectionPoolService connectionPoolService,
            IModelTypesResolvingService modelTypesResolvingService,
            IDataTypeTemplatesModelService dataTypeTemplatesModelService)
        {
            _connectionPoolService = connectionPoolService;
            _modelTypesResolvingService = modelTypesResolvingService;
            _dataTypeTemplatesModelService = dataTypeTemplatesModelService;
        }


        private static string
            FindSubstring(List<string> dirList) //формируется имя IED из набора строк, путем поиска совпадающих символов
        {
            string retValue = "";
            string possibleName = dirList[0];
            foreach (string str in dirList)
            {
                if (str.Length < possibleName.Length) possibleName = str;
            }

            int i = 0;
            foreach (char symbol in possibleName)
            {
                bool toAdd = true;
                foreach (string entry in dirList)
                {
                    if (entry[i] != symbol) toAdd = false;
                }

                if (toAdd) retValue += symbol;
                i++;
            }

            return retValue;
        }

        public async Task PrepareProgressData(string ip)
        {
            _ip = ip;
            _connection = _connectionPoolService.GetConnection(ip);
            var ldList = await _connection.MmsConnection.GetLdListAsync();
            _deviceName = FindSubstring(ldList.Item);
            foreach (var ld in ldList.Item)
            {
                ldDictionary.Add(ld, (await _connection.MmsConnection.GetListValiablesAsync(ld,false)).Item);
            }
        }

        public int GetLogicalNodeCount()
        {
            int lnsTotal = 0;
            foreach (var ldName in ldDictionary.Keys)
            {
                var lnNames = ldDictionary[ldName].Where((s => !s.Contains("$"))).ToList();
                lnsTotal += lnNames.Count();
            }

            return lnsTotal;
        }

        public string GetDeviceName()
        {
            return _deviceName;
        }

        public async Task<List<ILDevice>> GetLDeviceFromConnection(IProgress<LogicalNodeLoadingEvent> progress, ISclModel sclModel,string deviceName)
        {
            _sclModel = sclModel;
            List<ILDevice> lDevicesResult = new List<ILDevice>();
            var logicalNodeDtos = new Dictionary<string, List<LogicalNodeDTO>>();
            foreach (var ldName in ldDictionary.Keys)
            {
                ILDevice newLDevice = new LDevice();
                newLDevice.Inst = ldName.Replace(deviceName,String.Empty);
                logicalNodeDtos.Add(ldName, new List<LogicalNodeDTO>());
                var lnNames = ldDictionary[ldName].Where((s => !s.Contains("$"))).ToList();

                foreach (var lnName in lnNames)
                {
                    LogicalNodeDTO logicalNodeDto = new LogicalNodeDTO();
                    //logicalNodeDto.AccessAttributes =
                    //    await ReadLNAttributes(ied.name + ldName, lnName, connection, cancellationToken);
                    logicalNodeDto.IedName = _deviceName;
                    logicalNodeDto.LDName = ldName;
                    logicalNodeDto.Path = ldName + "." + lnName;
                    logicalNodeDto.LnDefinitions = ldDictionary[ldName]
                        .Where((s =>
                        {
                            var parts = s.Split('$');
                            return parts.Length > 1 && parts.Contains(lnName);
                        })).ToList();
                    logicalNodeDto.ShortName = lnName;
                    logicalNodeDtos[ldName].Add(logicalNodeDto);



                    progress?.Report(new LogicalNodeLoadingEvent());
                    ILogicalNode logicalNode = await CreateLogicalNode(logicalNodeDto);
                    if (logicalNode == null) continue;
                    if (logicalNode is ILogicalNodeZero)
                    {
                        newLDevice.LogicalNodeZero.Value = logicalNode as ILogicalNodeZero;
                        newLDevice.LogicalNodeZero.Value.ParentModelElement = newLDevice;
                    }
                    else
                    {
                        newLDevice.LogicalNodes.Add(logicalNode);
                        logicalNode.ParentModelElement = newLDevice;
                    }
                }

                lDevicesResult.Add(newLDevice);
            }

            return lDevicesResult;


        }


        private void SetLtName(string lnstr, ILogicalNode logicalNode)
        {
            uint i;
           logicalNode.Prefix = string.Empty;
            var lnNames = Enum.GetNames(typeof(tLNClassEnum));
            var allСoncurrences = new List<string>();
            foreach (string str in lnNames)
            {
                if (lnstr.Contains(str))
                {
                    allСoncurrences.Add(str);
                }

            }
            string lnClassString = allСoncurrences[0];

            if (allСoncurrences.Count > 1)
            {
                foreach (var concurrence in allСoncurrences)
                {
                    if (lnstr.IndexOf(concurrence) > lnstr.IndexOf(lnClassString))
                    {
                        lnClassString = concurrence;
                    }
                }
            }

            string[] s = new string[1];
            s[0] = lnClassString;
            logicalNode.LnClass = lnClassString;
            string[] substr = new string[2];
            substr = lnstr.Split(s, 2, StringSplitOptions.None);
            if (substr[0].Length > 0)
                logicalNode.Prefix = substr[0];
            if (substr[1].Length > 0)
            {
                logicalNode.Inst = (uint.TryParse(substr[1], out i) ? i : 1).ToString();
            }
            else logicalNode.Inst = "1";
            return;
        }




        private async Task<ILogicalNode> CreateLogicalNode(LogicalNodeDTO logicalNodeDto)
        {

            ILogicalNode resAnyLn = null;
            CommonLogicalNode commonLogicalNode = null;
            logicalNodeDto.DoiTypeDescription =
                (await _connection.MmsConnection.GetMmsTypeDescription(logicalNodeDto.LDName, logicalNodeDto.ShortName,false))
                .Item;
            if (logicalNodeDto.ShortName == "LLN0")
            {
                resAnyLn = new LogicalNodeZero();
                resAnyLn.LnClass = logicalNodeDto.ShortName;
                resAnyLn.LnType = logicalNodeDto.Path;
                commonLogicalNode = new LNTypesEd2.LLN0();
                commonLogicalNode.id = logicalNodeDto.Path;
            }
            else
            {
                try
                {


                    resAnyLn = new LogicalNode();
                    resAnyLn.LnType = logicalNodeDto.Path;
                    SetLtName(logicalNodeDto.ShortName,resAnyLn);
                    Type typeOfLNode = null;
                    if (logicalNodeDto.ShortName.Contains("PDPR")) // Мишино творение
                    {
                        typeOfLNode =
                            _modelTypesResolvingService.ResolveTypeByName(typeof(CommonLogicalNode), "PDUP", 2) as Type;
                    }
                    else
                    {
                        typeOfLNode =
                            _modelTypesResolvingService.ResolveTypeByName(typeof(CommonLogicalNode),
                                ( resAnyLn).LnClass, 2) as Type;
                    }

                    if (typeOfLNode != null)
                    {
                        commonLogicalNode =
                            (CommonLogicalNode) Activator.CreateInstance(typeOfLNode);
                        commonLogicalNode.lnClass = ( resAnyLn).LnClass;
                    }
                    else
                    {

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                }

            }

            try
            {
            logicalNodeDto.RelatedCommonLogicalNode = commonLogicalNode;
            commonLogicalNode.id = logicalNodeDto.Path;
            }
            catch (Exception e)
            {
                return null;
            }





           List<DoiDto> doiDtos = CreateDoiDtos(logicalNodeDto.LnDefinitions, logicalNodeDto.DoiTypeDescription);


            foreach (var doiDto in doiDtos)
            {
                if (doiDto.Name.Contains("GseControl"))
                {

                }
                string doName = doiDto.Name;
                string path = commonLogicalNode.id + "." + doName;
                DOData dataObj = (DOData)SclObjectFactory.CreateDoType(commonLogicalNode, doName,
                    doiDto.MembersList.Select((data => data.Name)).ToList(), _modelTypesResolvingService);
                IDoi doi = new Doi() { Name = doName };
                tDO tdo = new tDO(); //tdo нужен для того, чтобы добавить его в шаблон LN как объект
                //смысл всех этих операций в том, чтобы определить тип DO по его имени
                //для этого используется сборка IEC61850SCL с определениями этих типов
                tdo.name = doName;
                tdo.type = path;
                doiDto.FullPath = path;
                if (dataObj == null)
                {
                   continue;
                }

                if (dataObj != null)
                {
                    dataObj.id = path;
                }
                
                AddDataObjectBySpec(dataObj, doi, doiDto, logicalNodeDto);
                if (dataObj != null)
                {
                    string id = _dataTypeTemplatesModelService.AddDoType(dataObj.MapDoType(), _sclModel);

                    tdo.type = id;
                    commonLogicalNode.AddDO(tdo);
                    //полученный объект добавляется в набор объектов шаблона LN  
                    try
                    {
                        commonLogicalNode.SetAttributeByName(doName, dataObj);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }

                //полученный шаблон добавляется в набор шаблонов шаблона LN   
                doi.ParentModelElement = resAnyLn;
                resAnyLn.DoiCollection.Add(doi); //полученный объект (инстанс) добавляется в набор объектов объекта LN

            }
            resAnyLn.LnType =_dataTypeTemplatesModelService.AddLnodeType(commonLogicalNode.MapLNodeType(), _sclModel);



            return resAnyLn;
        }





        public List<DoiDto> CreateDoiDtos(List<string> allDoiDefinitions, MmsTypeDescription typeDescription)
        {
            List<DoiDto> doiDtos = new List<DoiDto>();
            foreach (var doiDefinition in allDoiDefinitions)
            {

                var doiDefinitionParts = doiDefinition.Split('$');
                if (doiDefinitionParts.Length == 2)
                {
                    string fc = doiDefinitionParts[1];
                    var typeDescriptionForFc = typeDescription.Components
                        .First((type => type.Name== fc));

                    if (fc == "RP" || fc == "BR")
                    {
                        continue;
                    }

                    if (fc == "GO")
                    {
                        continue;
                    }

                    if (fc == "SP")
                    {
                        continue;
                    }

                    foreach (var component in typeDescriptionForFc.Components)
                    {
                        DoiDto doiDto = new DoiDto();
                        doiDto.Name = component.Name;
                        doiDto.DoiTypeDescription = component;
                        var currentDoiDefinitions = allDoiDefinitions.Where((s =>
                        {

                            var parts = s.Split('$');
                            if (parts.Length < 3) return false;
                            return parts[2] == doiDto.Name;
                        })).ToList();

                        doiDto.MembersList = AddInnerDoDataToDoiDto(currentDoiDefinitions, doiDto.Name, 0,
                            doiDto.DoiTypeDescription, fc);
                        AddDoiDtoToList(doiDtos, doiDto);
                    }
                }
            }

            return doiDtos;
        }

        public List<InnerDoData> AddInnerDoDataToDoiDto(List<string> doiDefinitions, string parentName, int level,
            MmsTypeDescription parentTypeDescription, string fc)
        {
            List<InnerDoData> innerDoDatas = new List<InnerDoData>();
            if (parentTypeDescription.IsStructure)
            {
                foreach (var component in parentTypeDescription.Components)
                {
                    foreach (var doiDefinition in doiDefinitions)
                    {

                        var parts = doiDefinition.Split('$');
                        if ((parts.Length == 4 + level) && (parts[2 + level] == parentName))
                        {

                            if (parts[3 + level] == component.Name && parts[1] == fc)
                            {
                                if (doiDefinition.Contains("A$phsA"))
                                {

                                }
                                InnerDoData innerDoData = new InnerDoData();
                                innerDoData.Name = parts[3 + level];
                                innerDoData.Fc = parts[1];
                                innerDoData.MembersList.AddRange(AddInnerDoDataToDoiDto(doiDefinitions.Where((s =>
                                    {
                                        var partsT = s.Split('$');
                                        if (partsT.Contains(innerDoData.Name) && partsT.Contains(innerDoData.Fc) &&
                                            partsT.Contains(parentName)) return true;

                                        return false;
                                    })).ToList(), innerDoData.Name, level + 1, component,
                                    fc));
                                if (innerDoData.MembersList.Count > 0)
                                {
                                    innerDoData.IsStructure = true;
                                }

                                innerDoDatas.Add(innerDoData);
                            }
                        }

                    }
                }
            }
            return innerDoDatas;
        }



        private void AddDataObjectBySpec(DOData dataObj, 
            IDoi doi, DoiDto doiDto,LogicalNodeDTO logicalNodeDto)
        {
            foreach (var innerDoItem in doiDto.MembersList)
            {
                //  try
                //  {
                GetDataObjectContentBySpec(dataObj, doi, null, innerDoItem, true, logicalNodeDto);
                //}
                //catch (Exception ex)
                //{
                //    throw;
                //}
            }
        }
        public tFCEnum GetFunctionalConstraintByString(string fc)
        {
            tFCEnum functionalConstraint;
            tFCEnum.TryParse(fc, out functionalConstraint);
            return functionalConstraint;
        }

        private void GetDataObjectContentBySpec(DOData dataObj, IDoi doi, ISdi sdi, InnerDoData innerDoData, bool isdoi,LogicalNodeDTO logicalNodeDto)
        {

            string path = dataObj.id + "." + innerDoData.Name;
            tFCEnum fc = GetFunctionalConstraintByString(innerDoData.Fc);
            // спецификация может быть структурой с набором объектов или одним простым объектом
            if ((innerDoData != null) && (innerDoData.IsStructure))
                //в случае объекта типа структуры следует создать SDI и шаблон к нему
            {
                if (path.Contains("A.phsA"))
                {

                }
                ISdi newsdi = new Sdi {Name = innerDoData.Name}; //тут создается SDI, а дальше создается шаблон
                if (dataObj != null)
                {
                    object structObject = SclObjectFactory.CreateStructureSclObject(dataObj, innerDoData.Name,
                        innerDoData.MembersList.Select((data => data.Name)).ToList());

                    if (structObject != null)
                    {
                        object buff = null;
                        buff = dataObj.GetAttributeValByName(innerDoData.Name);
                        if (buff != null)
                        {
                            structObject = buff;

                        }
                        //объект может быть типом SDIDADataTypeBDA (наследником)
                        if (structObject.GetType().IsSubclassOf(typeof(SDIDADataTypeBDA)))
                        {
                            var res=AddSDIDADataType(newsdi, innerDoData, path, fc, dataObj, (SDIDADataTypeBDA) structObject,
                                null, logicalNodeDto);

                        }

                        //или DOData
                        if (structObject.GetType().IsSubclassOf(typeof(DOData)))
                        {
                            AddSDO(doi, newsdi, fc, (DOData) structObject, innerDoData, dataObj, logicalNodeDto);
                        }
                    }
                }

                if (isdoi)
                {
                    newsdi.ParentModelElement = doi;
                    doi.SdiCollection.Add(newsdi); //добавление инст к SDI, т.к. структура
                }

                else
                {
                    newsdi.ParentModelElement = sdi;
                    sdi.SdiCollection.Add(newsdi);
                }
            }
            else
            {

                var tBasicType = GetBasicTypeByPath(path.Split('.').ToArray(), innerDoData.Fc, logicalNodeDto);
                AddSimpleDataTemplate(dataObj, innerDoData.Name, fc, tBasicType);
                IDai dai = new Dai() {Name = innerDoData.Name};
                //   path =StringOperations.PathToDeviceFormat(path); //приведение строки к виду, читаемому устройством
                //   dai.Val.Add(new tVal { Value = Connection.ReadValue(path, fc).ToString() });
                // dai.FC = fc.ToString();
                if (isdoi)
                {
                    dai.ParentModelElement = doi;
                    doi.DaiCollection.Add(dai); //добавление инст к DAI, т.к. структура
                }


                else
                {
                    dai.ParentModelElement = sdi;
                    sdi.DaiCollection.Add(dai);
                }
            }
        }
        private void AddSimpleDataTemplate(DOData dataObj, string dataObjectName, tFCEnum fc, tBasicTypeEnum tBasicType)
        //создание шаблона под простой (неструктурный) тип
        {


            Type type = null;
            tDA dataAtrobj = null;
            if (dataObj != null)
                type = dataObj.GetAttributeByName(dataObjectName);// баг тут

            if (type != null)
            {
                try
                {


                    dataAtrobj = (tDA)Activator.CreateInstance(type);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                }
                object o = (tDA)dataObj.GetAttributeValByName(dataObjectName);
                if (o != null)
                    dataAtrobj = (tDA)o;
                dataAtrobj.name = dataObjectName;
                dataAtrobj.fc = (tFCEnum)fc;
                tBasicTypeEnum btype;
                if (Enum.TryParse(type.Name, out btype))
                    dataAtrobj.bType = btype;
            }

            if ((dataObj == null) || (type == null)) return;
            if (dataAtrobj.bType == tBasicTypeEnum.Enum) //неструктурный тип может быть перечислением
            {
                if (dataObjectName == "stVal")
                    dataObjectName = dataObj.name;
                if (dataAtrobj.type == null)
                    dataAtrobj.type = dataObjectName;

                AddEnums(dataObjectName, dataAtrobj);
            }
            if (dataAtrobj.bType == tBasicTypeEnum.Dbpos)
            {

            }
            if ((tBasicType != tBasicTypeEnum.Unset) && (dataAtrobj.bType != tBasicTypeEnum.Enum) && (dataAtrobj.bType != tBasicTypeEnum.Dbpos))
            {
                dataAtrobj.bType = tBasicType;
            }
            dataObj.AddDA(dataAtrobj);
        }



        private void AddSimpleBDA(tDAType datype, string shortstr, tDA da, DOData dataObj)
        {
            tAbstractDataAttribute bdaAbs = new tAbstractDataAttribute();

            Type type = null;
            if (da != null)
                type = da.GetAttributeByName(shortstr);
            if (type != null)
                bdaAbs = (tAbstractDataAttribute)Activator.CreateInstance(type);
            object o = new object();
            try
            {
                o = da.GetAttributeValByName(shortstr);
                if (o != null)
                    bdaAbs = (tAbstractDataAttribute)o;
            }
            catch (Exception f)
            {

                throw;
            }


            if ((da == null) || (type == null)) return;
            bdaAbs.name = shortstr;

            if (bdaAbs.bType == tBasicTypeEnum.Enum)
            {
                if (shortstr == "ctlVal")
                {
                    if (dataObj != null)
                        shortstr = dataObj.name;
                }
                bdaAbs.type = shortstr;
                AddEnums(shortstr, (tDA)bdaAbs);
            }
            datype.AddAbstrAtrAsBDA(bdaAbs);
            da.SetAttributeByName(shortstr, bdaAbs);
        }









        private void AddSDO(IDoi doi, ISdi newsdi, tFCEnum fc, DOData dotype,
            InnerDoData innerDoData, DOData dataObj,LogicalNodeDTO logicalNodeDto)
        {
            string attrname = innerDoData.Name;
            tSDO sdo = new tSDO();
            sdo.name = attrname;
            dotype.id = dataObj.id + "." + attrname;
            if (dotype != null)
            {
                foreach (InnerDoData item in innerDoData.MembersList)
                {
                    GetDataObjectContentBySpec(dotype, doi, newsdi, item, false,logicalNodeDto);
                }
            }

            sdo.type = _dataTypeTemplatesModelService.AddDoType(dotype.MapDoType(), _sclModel);
            dataObj.AddSDOtoDOType(sdo);
            dataObj.SetAttributeByName(attrname, dotype);
        }
        private SDIDADataTypeBDA AddSDIDADataType(ISdi newsdi, InnerDoData innerDoData, string str, tFCEnum fc,
            DOData dataObj, SDIDADataTypeBDA sdiDAData, tDA da,LogicalNodeDTO logicalNodeDto)
        {
            if (str.Contains(".MMXU1.A"))
            {

            }
            string attrname = str.Substring(str.LastIndexOf('.') + 1);
            if (dataObj != null)
                sdiDAData = CreateSDIDADataTypeBDA(sdiDAData, attrname, dataObj);
            if (dataObj == null)
                sdiDAData = CreateSDIDADataTypeBDA(sdiDAData, attrname, da);
            if (sdiDAData == null)
            {
                return null;
            }
            sdiDAData.name = attrname;
            sdiDAData.fc = (tFCEnum)fc;

            tDAType datype = new tDAType { id = str };
            foreach (InnerDoData item in innerDoData.MembersList)
            {
                string shortstr = item.Name;
                if (item.IsStructure)
                {
                    AddDataAtr(newsdi, item, datype, (tDA)sdiDAData, str, fc,logicalNodeDto);
                }
                else
                {
                    AddSimpleBDA(datype, shortstr, sdiDAData, dataObj);

                    IDai dai = new Dai() { Name = shortstr };
                    dai.ParentModelElement = newsdi;
                    //   str =StringOperations.PathToDeviceFormat(str + "." + shortstr); //приведение строки к виду, читаемому устройством
                    //   dai.Val.Add(new tVal { Value = Connection.ReadValue(str, fc).ToString() });
                    newsdi.DaiCollection.Add(dai);
                }
            }

            sdiDAData.type = _dataTypeTemplatesModelService.AddDaType(datype.MapDaType(), _sclModel);
            tBasicTypeEnum basicType = GetBasicTypeByPath(str.Split('.').ToArray(), innerDoData.Fc,logicalNodeDto);
            if (basicType != tBasicTypeEnum.Unset)
            {
                sdiDAData.bType = basicType;
            }
            dataObj?.AddDA((tDA)sdiDAData);
            da?.SetAttributeByName(attrname, sdiDAData);
            return sdiDAData;
        }



        public static string RANGEC_STR = "rangeC";
        private ISclModel _sclModel;

        private void AddDataAtr(ISdi sdi, InnerDoData innerDoData, tDAType datype, tDA sdiDAData, string str,
            tFCEnum fc,LogicalNodeDTO logicalNodeDto)
        {
            ISdi newsdi = new Sdi();
            string attrname = innerDoData.Name;
            newsdi.Name = attrname;
            Type type = null;

            if (sdiDAData.name == RANGEC_STR)
            {
                try
                {
                    type = ((rangeC)sdiDAData).RangeConfig.GetAttributeByName(attrname);

                }
                catch (Exception e)
                {
                    type = ((RangeConfig)sdiDAData).GetAttributeByName(attrname);
                }
            }
            else
                type = sdiDAData.GetAttributeByName(attrname);

            object o = null;
            if (type != null)
                o = Activator.CreateInstance(type);
            if (o != null)
                if (o.GetType().IsSubclassOf(typeof(SDIDADataTypeBDA)))
                {
                    SDIDADataTypeBDA sdidaDataTypeBda = (SDIDADataTypeBDA)o;
                    sdidaDataTypeBda = AddSDIDADataType(newsdi, innerDoData, str + "." + attrname, fc, null, sdidaDataTypeBda, sdiDAData,logicalNodeDto);
                    if (sdidaDataTypeBda == null)
                    {
                        return;
                    }
                    sdidaDataTypeBda.name = attrname;
                    AddStructBDA(datype, attrname, (tDA)sdidaDataTypeBda);
                }

            newsdi.ParentModelElement = sdi;
            sdi.SdiCollection.Add(newsdi);
        }

        private void AddStructBDA(tDAType dataAtr, string attrname, tDA o)
        {
            dataAtr.AddAbstrAtrAsBDA((tAbstractDataAttribute)o);
        }

        private SDIDADataTypeBDA CreateSDIDADataTypeBDA(SDIDADataTypeBDA sdiDAData, string attrname,
            tBaseElement dataObj)
        {
            Type type = null;
            if (sdiDAData != null)
                type = dataObj.GetAttributeByName(attrname);
            if (type == null)
            {
                return null;
            }

            if (type != null)
                sdiDAData = (SDIDADataTypeBDA)Activator.CreateInstance(type);
            object o = new object();
            o = dataObj.GetAttributeValByName(attrname);
            if (o != null)
                sdiDAData = (SDIDADataTypeBDA)o;
            return sdiDAData;
        }


        private void AddEnums(string shortstr, tDA da)
        {
            tEnumType enumtype = new tEnumType { id = da.type };
            if (da is ENUMERATED)
            {
                switch (shortstr)
                {
                    case "Mod":
                        (da as ENUMERATED).FullEnumList(typeof(modEnum));
                        break;
                    case "Beh":
                        (da as ENUMERATED).FullEnumList(typeof(behEnum));
                        break;
                    case "Health":
                        (da as ENUMERATED).FullEnumList(typeof(healthEnum));
                        break;
                    case "PhyHealth":
                        (da as ENUMERATED).FullEnumList(typeof(healthEnum));
                        break;
                    case "AutoRecSt":
                        (da as ENUMERATED).FullEnumList(typeof(autoRecStEnum));
                        break;
                    case "CBOpCap":
                        (da as ENUMERATED).FullEnumList(typeof(CBOpCapEnum));
                        break;
                    default:

                        break;

                }
            }
            else
            {

            }

            if (da.GetType().IsSubclassOf(typeof(SDIDADataTypeBDA)))
                enumtype.EnumVal = ((SDIDADataTypeBDA)da).EnumVal;

            if (da.GetType().IsSubclassOf(typeof(DADataType)))
                enumtype.EnumVal = ((DADataType)da).EnumVal;
            try
            {
       if (enumtype.EnumVal.Count == 0)
            {

            }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
     
            da.type = _dataTypeTemplatesModelService.AddEnumType(enumtype.MapEnumType(), _sclModel);
        }

        private tBasicTypeEnum GetBasicTypeByPath(string[] pathStrings, string fc,LogicalNodeDTO logicalNodeDto)
        {
            MmsTypeDescription typeDescription = logicalNodeDto.DoiTypeDescription.Components
                .FirstOrDefault((type => type.Name == fc));

            foreach (var pathString in pathStrings)
            {
                if (logicalNodeDto.LDName == pathString) continue;
                if (logicalNodeDto.ShortName == pathString) continue;
           
                if (typeDescription.IsStructure)
                {
                    var t = typeDescription;
                    typeDescription = typeDescription.Components
                        .FirstOrDefault(type => type.Name == pathString);
                    if (typeDescription == null)
                    {

                    }
                }
                else
                {

                }
            }

            if (!typeDescription.IsStructure)
            {
                if (typeDescription.BasicType.HasValue)
                {
                    if( (typeDescription.BasicType == tBasicTypeEnum.bit_string)&&(pathStrings.Last()=="q"))
                    {
                        return tBasicTypeEnum.Quality;
                    }
                    else
                    {
                        return typeDescription.BasicType.Value;
                    }
                }

            }

            return tBasicTypeEnum.Unset;
        }


        public void AddDoiDtoToList(List<DoiDto> doiDtos, DoiDto doiDto)
        {
            var existingDto = doiDtos.FirstOrDefault((dto => dto.Name == doiDto.Name));
            if (existingDto != null)
            {
                doiDto.MembersList.ForEach((data => AddInnerData(existingDto.MembersList,data)));
               // existingDto.MembersList.AddRange(doiDto.MembersList);
            }
            else
            {
                doiDtos.Add(doiDto);
            }
        }

        private void AddInnerData(List<InnerDoData> innerDoDatas,InnerDoData doDataToAdd)
        {
            var existing = innerDoDatas.FirstOrDefault((data => data.Name == doDataToAdd.Name));
            if (existing != null)
            {
                doDataToAdd.MembersList.ForEach((data => AddInnerData(existing.MembersList, data)));
            }
            else
            {
                innerDoDatas.Add(doDataToAdd);
            }
        }



    }

























    public static class SclObjectFactory
    {



        public static tDOType CreateDoType(CommonLogicalNode ln, string doName, List<string> doList,IModelTypesResolvingService modelTypesResolvingService)
        {
            Type typeOfDo = ln.GetAttributeByName(doName); //подтягивается тип соответствующий названию DO

            if (typeOfDo == null)
            {

                foreach (var propertyType in ln.GetAllPropertyTypes())
                {
                    bool isEquivalentType = true;
                    foreach (var innerDoName in doList)
                    {
                        if (!propertyType.GetProperties().Any((info => info.Name == innerDoName)))
                            isEquivalentType = false;
                    }

                    if (isEquivalentType)
                    {
                        typeOfDo = propertyType;
                        break;
                    }
                }
            }

            if (typeOfDo == null)
            {
                var allLnTypes = modelTypesResolvingService.GetAllRegisteredTypes(2, typeof(CommonLogicalNode));
                foreach (var lnType in allLnTypes)
                {
                    foreach (var propertyType in lnType.GetProperties())
                    {
                        bool isEquivalentType = true;
                        foreach (var innerDoName in doList)
                        {
                            if (!propertyType.PropertyType.GetProperties().Any((info => info.Name == innerDoName)))
                                isEquivalentType = false;
                        }

                        if (isEquivalentType)
                        {
                            typeOfDo = propertyType.PropertyType;
                            break;
                        }
                    }
                    if(typeOfDo!=null)break;
                }
                
            }
            if (typeOfDo == null)
            {
              
                    foreach (var propertyType in ln.GetAllPropertyTypes())
                    {
                        bool isEquivalentType = propertyType.Name == doName;


                        if (isEquivalentType)
                        {
                            typeOfDo = propertyType;
                            break;
                        }
                    }
            }
            if (typeOfDo == null)
            {
                var allLnTypes = modelTypesResolvingService.GetAllRegisteredTypes(2, typeof(CommonLogicalNode));
                foreach (var lnType in allLnTypes)
                {
                    foreach (var propertyType in lnType.GetProperties())
                    {
                        bool isEquivalentType = propertyType.Name == doName;


                        if (isEquivalentType)
                        {
                            typeOfDo = propertyType.PropertyType;
                            break;
                        }
                    }
                    if (typeOfDo != null) break;
                }
            }
            DOData dataObj = null;
            if (typeOfDo == null)
            {
                return null;
            }
            else
            {
                 dataObj = (DOData)Activator.CreateInstance(typeOfDo);

            }
            object o = (DOData) ln.GetAttributeValByName(doName);
            //если внутри типа LN предопределен тип DO, то он подтягивается и используется
            if (o != null)
                dataObj = (DOData) o;
            dataObj.name = doName;
            tCDCEnumEd2 cdc;
            string n = typeOfDo?.ToString().Substring(typeOfDo.ToString().LastIndexOf('.') + 1);
            if (Enum.TryParse(n, out cdc)) dataObj.cdc = cdc;
            return dataObj;
        }



        public static object CreateStructureSclObject(DOData parent, string structureName, List<string> membersList)
        {
            var type = parent.GetAttributeByName(structureName);
            object o = null;
            if (type == null)
            {

                foreach (var propertyType in parent.GetAllPropertyTypes())
                {
                    bool isEquivalentType = true;
                    foreach (var innerDoName in membersList)
                    {
                        if (!propertyType.GetProperties().Any((info => info.Name == innerDoName)))
                            isEquivalentType = false;
                    }

                    if (isEquivalentType) type = propertyType;
                }


            }

            if (type == null)
            {
                var cdcTypes = typeof(DOData).Assembly.GetTypes()
                    .Where((type1 => type1.IsSubclassOf(typeof(DOData))));
                foreach (var cdcType in cdcTypes)
                {

                    bool isEquivalentType = true;
                    foreach (var innerDoName in membersList)
                    {
                        if (!cdcType.GetProperties().Any((info => info.Name == innerDoName)))
                        {
                            isEquivalentType = false;
                        }
                    }

                    if (isEquivalentType)
                    {
                        type = cdcType;
                    }



                }
            }

            if (type != null)
                o = Activator.CreateInstance(type); //создается объект из подтянутого типа
            return o;
        }
    }
}

