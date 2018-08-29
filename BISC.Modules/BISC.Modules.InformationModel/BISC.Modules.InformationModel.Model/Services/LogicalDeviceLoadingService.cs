using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Iec61850Ed2;
using BISC.Model.Iec61850Ed2.DataTypeTemplates;
using BISC.Model.Iec61850Ed2.SclModelTemplates;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services;
using BISC.Modules.Connection.Infrastructure.Connection;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Modules.InformationModel.Model.Elements;
using BISC.Modules.InformationModel.Model.LoadingFromConnection;

namespace BISC.Modules.InformationModel.Model.Services
{
    public class LogicalDeviceLoadingService : ILogicalDeviceLoadingService
    {
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IModelTypesResolvingService _modelTypesResolvingService;
        private readonly IDataTypeTemplatesModelService _dataTypeTemplatesModelService;
        private readonly IBiscProject _biscProject;
        private Dictionary<string, List<string>> ldDictionary = new Dictionary<string, List<string>>();
        private string _deviceName;
        private string _ip;
        private IDeviceConnection _connection;

        public LogicalDeviceLoadingService(IConnectionPoolService connectionPoolService,
            IModelTypesResolvingService modelTypesResolvingService,
            IDataTypeTemplatesModelService dataTypeTemplatesModelService, IBiscProject biscProject)
        {
            _connectionPoolService = connectionPoolService;
            _modelTypesResolvingService = modelTypesResolvingService;
            _dataTypeTemplatesModelService = dataTypeTemplatesModelService;
            _biscProject = biscProject;
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
                ldDictionary.Add(ld, (await _connection.MmsConnection.GetListValiablesAsync(ld)).Item);
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

        public async Task<List<ILDevice>> GetLDeviceFromConnection(IProgress<LogicalNodeLoadingEvent> progress)
        {
            List<ILDevice> lDevicesResult = new List<ILDevice>();
            var logicalNodeDtos = new Dictionary<string, List<LogicalNodeDTO>>();
            foreach (var ldName in ldDictionary.Keys)
            {
                ILDevice newLDevice = new LDevice();
                logicalNodeDtos.Add(ldName, new List<LogicalNodeDTO>());
                var lnNames = ldDictionary[ldName].Where((s => !s.Contains("$"))).ToList();

                foreach (var lnName in lnNames)
                {
                    LogicalNodeDTO logicalNodeDto = new LogicalNodeDTO();
                    //logicalNodeDto.AccessAttributes =
                    //    await ReadLNAttributes(ied.name + ldName, lnName, connection, cancellationToken);
                    logicalNodeDto.IedName = _deviceName;
                    logicalNodeDto.LDName = ldName;
                    logicalNodeDto.Path = _deviceName + ldName + "." + lnName;
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
                    if (logicalNode is ILogicalNodeZero)
                    {
                        newLDevice.LogicalNodeZero = logicalNode as ILogicalNodeZero;
                    }
                    else
                    {
                        newLDevice.LogicalNodes.Add(logicalNode);
                    }
                }

                lDevicesResult.Add(newLDevice);
            }

            return lDevicesResult;


        }

        private async Task<ILogicalNode> CreateLogicalNode(LogicalNodeDTO logicalNodeDto)
        {

            tAnyLN resAnyLn = null;
            CommonLogicalNode commonLogicalNode = null;
            logicalNodeDto.DoiTypeDescription =
                (await _connection.MmsConnection.GetMmsTypeDescription(logicalNodeDto.LDName, logicalNodeDto.ShortName))
                .Item;
            if (logicalNodeDto.ShortName == "LLN0")
            {
                resAnyLn = new tLN0(logicalNodeDto.ShortName);
                commonLogicalNode = new LNTypesEd2.LLN0();
                commonLogicalNode.id = logicalNodeDto.Path;
            }
            else
            {
                try
                {


                    resAnyLn = new tLN(logicalNodeDto.ShortName);

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
                                ((tLN) resAnyLn).lnClass, 2) as Type;
                    }

                    if (typeOfLNode != null)
                    {
                        commonLogicalNode =
                            (CommonLogicalNode) Activator.CreateInstance(typeOfLNode);
                        commonLogicalNode.lnClass = ((tLN) resAnyLn).lnClass;
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

            logicalNodeDto.RelatedCommonLogicalNode = commonLogicalNode;
            commonLogicalNode.id = logicalNodeDto.Path;




        //    List<DoiDto> doiDtos = CreateDoiDtos(logicalNodeDto.LnDefinitions, logicalNodeDto.DoiTypeDescription);


            //foreach (var doiDto in doiDtos)
            //{
            //    string doName = doiDto.Name;
            //    string path = commonLogicalNode.id + "." + doName;
            //    DOData dataObj = (DOData)SclObjectFactory.CreateDoType(commonLogicalNode, doName,
            //        doiDto.MembersList.Select((data => data.Name)).ToList());
            //    tDOI doi = new tDOI { name = doName };
            //    tDO tdo = new tDO(); //tdo нужен для того, чтобы добавить его в шаблон LN как объект
            //    //смысл всех этих операций в том, чтобы определить тип DO по его имени
            //    //для этого используется сборка IEC61850SCL с определениями этих типов
            //    tdo.name = doName;
            //    tdo.type = path;
            //    doiDto.FullPath = path;
            //    if (dataObj == null) continue;
            //    dataObj.id = path;
            //    AddDataObjectBySpec(dataObj, doi, doiDto);


            //    string id = scl.DataTypeTemplates.AddDOType(dataObj).id;

            //    tdo.type = id;
            //    commonLogicalNode.AddDO(tdo);
            //    //полученный объект добавляется в набор объектов шаблона LN                                    
            //    commonLogicalNode.SetAttributeByName(doName, dataObj);
            //    //полученный шаблон добавляется в набор шаблонов шаблона LN   
            //    resAnyLn.AddDOI(doi); //полученный объект (инстанс) добавляется в набор объектов объекта LN

            //}
            //resAnyLn.lnType = scl.DataTypeTemplates.AddLNodeType(commonLogicalNode).id;



            return new LogicalNode();
        }





        //public List<DoiDto> CreateDoiDtos(List<string> allDoiDefinitions, MmsTypeDescription typeDescription)
        //{
        //    List<DoiDto> doiDtos = new List<DoiDto>();
        //    foreach (var doiDefinition in allDoiDefinitions)
        //    {

        //        var doiDefinitionParts = doiDefinition.Split('$');
        //        if (doiDefinitionParts.Length == 2)
        //        {
        //            string fc = doiDefinitionParts[1];
        //            var typeDescriptionForFc = typeDescription.Components
        //                .First((type => type.ComponentName.Value == fc)).ComponentType.TypeDescription;

        //            if (fc == "RP" || fc == "BR")
        //            {
        //                continue;
        //            }

        //            if (fc == "GO")
        //            {
        //                continue;
        //            }

        //            if (fc == "SP")
        //            {
        //                continue;
        //            }

        //            foreach (var component in typeDescriptionForFc.Structure.Components)
        //            {
        //                DoiDto doiDto = new DoiDto();
        //                doiDto.Name = component.ComponentName.Value;
        //                doiDto.TypeDescription = component.ComponentType.TypeDescription;
        //                var currentDoiDefinitions = allDoiDefinitions.Where((s =>
        //                {
        //                    var parts = s.Split('$');
        //                    if (parts.Length < 3) return false;
        //                    return parts[2] == doiDto.Name;
        //                })).ToList();
        //                doiDto.MembersList = AddInnerDoDataToDoiDto(currentDoiDefinitions, doiDto.Name, 0,
        //                    doiDto.TypeDescription, fc);
        //                AddDoiDtoToList(doiDtos, doiDto);
        //            }
        //        }
        //    }

        //    return doiDtos;
        //}

        //public List<InnerDoData> AddInnerDoDataToDoiDto(List<string> doiDefinitions, string parentName, int level,
        //    TypeDescription parentTypeDescription, string fc)
        //{
        //    //if (parentName == "phsA")
        //    //{

        //    //}
        //    List<InnerDoData> innerDoDatas = new List<InnerDoData>();
        //    if (parentTypeDescription.isStructureSelected())
        //    {
        //        foreach (var component in parentTypeDescription.Structure.Components)
        //        {
        //            foreach (var doiDefinition in doiDefinitions)
        //            {
        //                if (doiDefinition == "IDPDIF1$MX$RstA$q")
        //                {

        //                }

        //                var parts = doiDefinition.Split('$');
        //                if ((parts.Length == 4 + level) && (parts[2 + level] == parentName))
        //                {

        //                    if (parts[3 + level] == component.ComponentName.Value && parts[1] == fc)
        //                    {

        //                        InnerDoData innerDoData = new InnerDoData();
        //                        innerDoData.Name = parts[3 + level];
        //                        innerDoData.Fc = parts[1];
        //                        innerDoData.MembersList.AddRange(AddInnerDoDataToDoiDto(doiDefinitions.Where((s =>
        //                            {
        //                                var partsT = s.Split('$');
        //                                if (partsT.Contains(innerDoData.Name) && partsT.Contains(innerDoData.Fc) &&
        //                                    partsT.Contains(parentName)) return true;

        //                                return false;
        //                            })).ToList(), innerDoData.Name, level + 1, component.ComponentType.TypeDescription,
        //                            fc));
        //                        if (innerDoData.MembersList.Count > 0)
        //                        {
        //                            innerDoData.IsStructure = true;
        //                        }

        //                        innerDoDatas.Add(innerDoData);
        //                    }
        //                }


        //            }
        //        }
        //    }




        //    return innerDoDatas;
        //}



        //    private void AddDataObjectBySpec(DOData dataObj, tDOI doi, DoiDto doiDto)
        //    {
        //        foreach (var innerDoItem in doiDto.MembersList)
        //        {
        //            //  try
        //            //  {
        //            GetDataObjectContentBySpec(dataObj, doi, null, innerDoItem, true);
        //            //}
        //            //catch (Exception ex)
        //            //{
        //            //    throw;
        //            //}
        //        }
        //    }


        //    private void GetDataObjectContentBySpec(DOData dataObj, tDOI doi, tSDI sdi, InnerDoData innerDoData, bool isdoi)
        //    {

        //        string path = dataObj.id + "." + innerDoData.Name;
        //        tFCEnum fc = StringOperations.GetFunctionalConstraintByString(innerDoData.Fc);
        //        // спецификация может быть структурой с набором объектов или одним простым объектом
        //        if ((innerDoData != null) && (innerDoData.IsStructure))
        //        //в случае объекта типа структуры следует создать SDI и шаблон к нему
        //        {


        //            tSDI newsdi = new tSDI { name = innerDoData.Name }; //тут создается SDI, а дальше создается шаблон
        //            if (dataObj != null)
        //            {
        //                object structObject = SclObjectFactory.CreateStructureSclObject(dataObj, innerDoData.Name,
        //                    innerDoData.MembersList.Select((data => data.Name)).ToList());

        //                if (structObject != null)
        //                {
        //                    object buff = null;
        //                    buff = dataObj.GetAttributeValByName(innerDoData.Name);
        //                    if (buff != null)
        //                    {
        //                        structObject = buff;

        //                    }
        //                    //объект может быть типом SDIDADataTypeBDA (наследником)
        //                    if (structObject.GetType().IsSubclassOf(typeof(SDIDADataTypeBDA)))
        //                    {
        //                        AddSDIDADataType(newsdi, innerDoData, path, fc, dataObj, (SDIDADataTypeBDA)structObject,
        //                            null);

        //                    }
        //                    //или DOData
        //                    if (structObject.GetType().IsSubclassOf(typeof(DOData)))
        //                    {
        //                        AddSDO(doi, newsdi, fc, (DOData)structObject, innerDoData, dataObj);
        //                    }
        //                }
        //            }


        //            if (isdoi) doi.AddSDI(newsdi); //добавление инст к SDI, т.к. структура
        //            else sdi.SDI.Add(newsdi);
        //        }
        //        else
        //        {

        //            var tBasicType = GetBasicTypeByPath(path.Split('.').ToArray(), innerDoData.Fc);

        //            AddSimpleDataTemplate(dataObj, innerDoData.Name, fc, tBasicType);
        //            tDAI dai = new tDAI { name = innerDoData.Name };
        //            //   path =StringOperations.PathToDeviceFormat(path); //приведение строки к виду, читаемому устройством
        //            //   dai.Val.Add(new tVal { Value = Connection.ReadValue(path, fc).ToString() });
        //            dai.FC = fc.ToString();
        //            if (isdoi) doi.DAI.Add(dai); //добавление инст к DAI, т.к. структура
        //            else sdi.DAI.Add(dai);
        //        }
        //    }
        //    private void AddSimpleDataTemplate(DOData dataObj, string dataObjectName, tFCEnum fc, tBasicTypeEnum tBasicType)
        //    //создание шаблона под простой (неструктурный) тип
        //    {


        //        Type type = null;
        //        tDA dataAtrobj = null;
        //        if (dataObj != null)
        //            type = dataObj.GetAttributeByName(dataObjectName);

        //        if (type != null)
        //        {
        //            try
        //            {


        //                dataAtrobj = (tDA)Activator.CreateInstance(type);
        //            }
        //            catch (Exception e)
        //            {
        //                Console.WriteLine(e);

        //            }
        //            object o = (tDA)dataObj.GetAttributeValByName(dataObjectName);
        //            if (o != null)
        //                dataAtrobj = (tDA)o;
        //            dataAtrobj.name = dataObjectName;
        //            dataAtrobj.fc = (tFCEnum)fc;
        //            tBasicTypeEnum btype;
        //            if (Enum.TryParse(type.Name, out btype))
        //                dataAtrobj.bType = btype;
        //        }

        //        if ((dataObj == null) || (type == null)) return;
        //        if (dataAtrobj.bType == tBasicTypeEnum.Enum) //неструктурный тип может быть перечислением
        //        {
        //            if (dataObjectName == "stVal")
        //                dataObjectName = dataObj.name;
        //            if (dataAtrobj.type == null)
        //                dataAtrobj.type = dataObjectName;

        //            AddEnums(dataObjectName, dataAtrobj);
        //        }
        //        if (dataAtrobj.bType == tBasicTypeEnum.Dbpos)
        //        {

        //        }
        //        if ((tBasicType != tBasicTypeEnum.Unset) && (dataAtrobj.bType != tBasicTypeEnum.Enum) && (dataAtrobj.bType != tBasicTypeEnum.Dbpos))
        //        {
        //            dataAtrobj.bType = tBasicType;
        //        }
        //        dataObj.AddDA(dataAtrobj);
        //    }



        //    private void AddSimpleBDA(tDAType datype, string shortstr, tDA da, DOData dataObj)
        //    {
        //        tAbstractDataAttribute bdaAbs = new tAbstractDataAttribute();

        //        Type type = null;
        //        if (da != null)
        //            type = da.GetAttributeByName(shortstr);
        //        if (type != null)
        //            bdaAbs = (tAbstractDataAttribute)Activator.CreateInstance(type);
        //        object o = new object();
        //        try
        //        {
        //            o = da.GetAttributeValByName(shortstr);
        //            if (o != null)
        //                bdaAbs = (tAbstractDataAttribute)o;
        //        }
        //        catch (Exception f)
        //        {

        //            throw;
        //        }


        //        if ((da == null) || (type == null)) return;
        //        bdaAbs.name = shortstr;

        //        if (bdaAbs.bType == tBasicTypeEnum.Enum)
        //        {
        //            if (shortstr == "ctlVal")
        //            {
        //                if (dataObj != null)
        //                    shortstr = dataObj.name;
        //            }
        //            bdaAbs.type = shortstr;
        //            AddEnums(shortstr, (tDA)bdaAbs);
        //        }
        //        datype.AddAbstrAtrAsBDA(bdaAbs);
        //        da.SetAttributeByName(shortstr, bdaAbs);
        //    }









        //    private void AddSDO(tDOI doi, tSDI newsdi, tFCEnum fc, DOData dotype,
        //        InnerDoData innerDoData, DOData dataObj)
        //    {
        //        string attrname = innerDoData.Name;
        //        tSDO sdo = new tSDO();
        //        sdo.name = attrname;
        //        dotype.id = dataObj.id + "." + attrname;
        //        if (dotype != null)
        //        {
        //            foreach (InnerDoData item in innerDoData.MembersList)
        //            {
        //                GetDataObjectContentBySpec(dotype, doi, newsdi, item, false);
        //            }
        //        }

        //        sdo.type = _scl.DataTypeTemplates.AddDOType(dotype).id;
        //        dataObj.AddSDOtoDOType(sdo);
        //        dataObj.SetAttributeByName(attrname, dotype);
        //    }
        //    private SDIDADataTypeBDA AddSDIDADataType(tSDI newsdi, InnerDoData innerDoData, string str, tFCEnum fc,
        //        DOData dataObj, SDIDADataTypeBDA sdiDAData, tDA da)
        //    {
        //        string attrname = str.Substring(str.LastIndexOf('.') + 1);
        //        if (dataObj != null)
        //            sdiDAData = CreateSDIDADataTypeBDA(sdiDAData, attrname, dataObj);
        //        if (dataObj == null)
        //            sdiDAData = CreateSDIDADataTypeBDA(sdiDAData, attrname, da);
        //        sdiDAData.name = attrname;
        //        sdiDAData.fc = (tFCEnum)fc;

        //        tDAType datype = new tDAType { id = str };
        //        foreach (InnerDoData item in innerDoData.MembersList)
        //        {
        //            string shortstr = item.Name;
        //            if (item.IsStructure)
        //            {
        //                AddDataAtr(newsdi, item, datype, (tDA)sdiDAData, str, fc);
        //            }
        //            else
        //            {
        //                AddSimpleBDA(datype, shortstr, sdiDAData, dataObj);

        //                tDAI dai = new tDAI { name = shortstr };
        //                //   str =StringOperations.PathToDeviceFormat(str + "." + shortstr); //приведение строки к виду, читаемому устройством
        //                dai.FC = fc.ToString();
        //                //   dai.Val.Add(new tVal { Value = Connection.ReadValue(str, fc).ToString() });
        //                newsdi.DAI.Add(dai);
        //            }
        //        }
        //        sdiDAData.type = _scl.DataTypeTemplates.AddDAType(datype).id;
        //        tBasicTypeEnum basicType = GetBasicTypeByPath(str.Split('.').ToArray(), innerDoData.Fc);
        //        if (basicType != tBasicTypeEnum.Unset)
        //        {
        //            sdiDAData.bType = basicType;
        //        }
        //        dataObj?.AddDA((tDA)sdiDAData);
        //        da?.SetAttributeByName(attrname, sdiDAData);
        //        return sdiDAData;
        //    }




        //    private void AddDataAtr(tSDI sdi, InnerDoData innerDoData, tDAType datype, tDA sdiDAData, string str,
        //        tFCEnum fc)
        //    {
        //        tSDI newsdi = new tSDI();
        //        string attrname = innerDoData.Name;
        //        newsdi.name = attrname;
        //        Type type = null;

        //        if (sdiDAData.name == StringOperations.RANGEC_STR)
        //        {
        //            type = ((rangeC)sdiDAData).RangeConfig.GetAttributeByName(attrname);
        //        }
        //        else
        //            type = sdiDAData.GetAttributeByName(attrname);

        //        object o = null;
        //        if (type != null)
        //            o = Activator.CreateInstance(type);
        //        if (o != null)
        //            if (o.GetType().IsSubclassOf(typeof(SDIDADataTypeBDA)))
        //            {
        //                SDIDADataTypeBDA sdidaDataTypeBda = (SDIDADataTypeBDA)o;
        //                sdidaDataTypeBda = AddSDIDADataType(newsdi, innerDoData, str + "." + attrname, fc, null, sdidaDataTypeBda, sdiDAData);
        //                sdidaDataTypeBda.name = attrname;
        //                AddStructBDA(datype, attrname, (tDA)sdidaDataTypeBda);
        //            }

        //        sdi.SDI.Add(newsdi);
        //    }

        //    private void AddStructBDA(tDAType dataAtr, string attrname, tDA o)
        //    {
        //        dataAtr.AddAbstrAtrAsBDA((tAbstractDataAttribute)o);
        //    }

        //    private SDIDADataTypeBDA CreateSDIDADataTypeBDA(SDIDADataTypeBDA sdiDAData, string attrname,
        //        tBaseElement dataObj)
        //    {
        //        Type type = null;
        //        if (sdiDAData != null)
        //            type = dataObj.GetAttributeByName(attrname);
        //        if (type == null)
        //        {

        //        }

        //        if (type != null)
        //            sdiDAData = (SDIDADataTypeBDA)Activator.CreateInstance(type);
        //        object o = new object();
        //        o = dataObj.GetAttributeValByName(attrname);
        //        if (o != null)
        //            sdiDAData = (SDIDADataTypeBDA)o;
        //        return sdiDAData;
        //    }


        //    private void AddEnums(string shortstr, tDA da)
        //    {
        //        tEnumType enumtype = new tEnumType { id = da.type };
        //        if (da is ENUMERATED)
        //        {
        //            switch (shortstr)
        //            {
        //                case "Mod":
        //                    (da as ENUMERATED).FullEnumList(typeof(modEnum));
        //                    break;
        //                case "Beh":
        //                    (da as ENUMERATED).FullEnumList(typeof(behEnum));
        //                    break;
        //                case "Health":
        //                    (da as ENUMERATED).FullEnumList(typeof(healthEnum));
        //                    break;
        //                case "PhyHealth":
        //                    (da as ENUMERATED).FullEnumList(typeof(healthEnum));
        //                    break;
        //                case "AutoRecSt":
        //                    (da as ENUMERATED).FullEnumList(typeof(autoRecStEnum));
        //                    break;
        //                case "CBOpCap":
        //                    (da as ENUMERATED).FullEnumList(typeof(CBOpCapEnum));
        //                    break;
        //                default:

        //                    break;

        //            }
        //        }
        //        else
        //        {

        //        }

        //        if (da.GetType().IsSubclassOf(typeof(SDIDADataTypeBDA)))
        //            enumtype.EnumVal = ((SDIDADataTypeBDA)da).EnumVal;

        //        if (da.GetType().IsSubclassOf(typeof(DADataType)))
        //            enumtype.EnumVal = ((DADataType)da).EnumVal;
        //        if (enumtype.EnumVal.Count == 0)
        //        {

        //        }
        //        da.type = _scl.DataTypeTemplates.AddEnumType(enumtype).id;
        //    }

        //    private tBasicTypeEnum GetBasicTypeByPath(string[] pathStrings, string fc)
        //    {
        //        TypeDescription typeDescription = _logicalNodeDto.AccessAttributes.TypeDescription.Structure.Components
        //            .FirstOrDefault((type => type.ComponentName.Value == fc))?.ComponentType
        //            .TypeDescription; ;

        //        foreach (var pathString in pathStrings)
        //        {
        //            if (_logicalNodeDto.IedName + _logicalNodeDto.LDName == pathString) continue;
        //            if (_logicalNodeDto.ShortName == pathString) continue;
        //            if (typeDescription.Structure != null)
        //            {
        //                typeDescription = typeDescription.Structure.Components
        //                    .FirstOrDefault((type => type.ComponentName.Value == pathString))?.ComponentType
        //                    .TypeDescription;
        //            }
        //            else
        //            {

        //            }
        //        }

        //        if (typeDescription?.Structure == null)
        //        {
        //            if (typeDescription.isBit_stringSelected())
        //            {
        //                if (pathStrings.Last() == "q")
        //                {
        //                    return tBasicTypeEnum.Quality;
        //                }
        //            }
        //            else if (typeDescription.isArraySelected())
        //            {
        //                return tBasicTypeEnum.Struct;
        //            }
        //            else if (typeDescription.Integer != null)
        //            {
        //                switch (typeDescription.Integer.Value)
        //                {
        //                    case 8:
        //                        return tBasicTypeEnum.INT8;
        //                    case 16:
        //                        return tBasicTypeEnum.INT16;
        //                    case 24:
        //                        return tBasicTypeEnum.INT24;
        //                    case 32:
        //                        return tBasicTypeEnum.INT32;
        //                }
        //            }
        //            else if (typeDescription.isBcdSelected())
        //            {
        //            }
        //            else if (typeDescription.isBooleanSelected())
        //            {
        //                return tBasicTypeEnum.BOOLEAN;
        //            }
        //            else if (typeDescription.isFloating_pointSelected())
        //            {
        //                switch (typeDescription.Floating_point.Format_width.Value)
        //                {
        //                    case 32:
        //                        return tBasicTypeEnum.FLOAT32;
        //                    case 64:
        //                        return tBasicTypeEnum.FLOAT64;
        //                }
        //            }
        //            else if (typeDescription.isGeneralized_timeSelected() || typeDescription.isUtc_timeSelected() || typeDescription.isBinary_timeSelected())
        //            {
        //                return tBasicTypeEnum.Timestamp;
        //            }
        //            else if (typeDescription.isOctet_stringSelected())
        //            {
        //                return tBasicTypeEnum.Octet64;
        //            }
        //            else if (typeDescription.isVisible_stringSelected())
        //            {
        //                switch (typeDescription.Visible_string.Value)
        //                {
        //                    case 255:
        //                    case -255:
        //                        return tBasicTypeEnum.VisString255;
        //                    case 129:
        //                    case -129:
        //                        return tBasicTypeEnum.VisString129;
        //                    case 64:
        //                    case -64:
        //                        return tBasicTypeEnum.VisString64;
        //                        break;
        //                    case 32:
        //                    case -32:
        //                        return tBasicTypeEnum.VisString32;
        //                        break;
        //                    case 65:
        //                    case -65:
        //                        return tBasicTypeEnum.VisString65;
        //                        break;
        //                }
        //            }

        //        }

        //        return tBasicTypeEnum.Unset;
        //    }


        public void AddDoiDtoToList(List<DoiDto> doiDtos, DoiDto doiDto)
        {
            var existingDto = doiDtos.FirstOrDefault((dto => dto.Name == doiDto.Name));
            if (existingDto != null)
            {
                existingDto.MembersList.AddRange(doiDto.MembersList);
            }
            else
            {
                doiDtos.Add(doiDto);
            }
        }


    }

























    public static class SclObjectFactory
    {



        public static tDOType CreateDoType(CommonLogicalNode ln, string doName, List<string> doList)
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

                    if (isEquivalentType) typeOfDo = propertyType;
                }
            }

            if (typeOfDo == null) return null;
            DOData dataObj = (DOData) Activator.CreateInstance(typeOfDo);
            object o = (DOData) ln.GetAttributeValByName(doName);
            //если внутри типа LN предопределен тип DO, то он подтягивается и используется
            if (o != null)
                dataObj = (DOData) o;
            dataObj.name = doName;
            tCDCEnumEd2 cdc;
            string n = typeOfDo.ToString().Substring(typeOfDo.ToString().LastIndexOf('.') + 1);
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

