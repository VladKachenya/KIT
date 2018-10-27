using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Modules.Connection.Infrastructure.Connection;
using BISC.Modules.Connection.Infrastructure.Connection.Dto;
using BISC.Modules.Connection.MMS.MMS_ASN1_Model;
using BISC.Modules.Connection.MMS.org.bn.types;
using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Model.Model;
using Microsoft.Practices.ObjectBuilder2;

namespace BISC.Modules.Connection.MMS.MmsClientServices
{
    public class MmsConnectionFacade : IMmsConnectionFacade
    {
        private string _ip;
        private readonly Iec61850State _state;
        private Dictionary<string, List<string>> _cachedLDeviceDefinitions = new Dictionary<string, List<string>>();
        private Dictionary<string, MmsTypeDescription> _cachedTypeDescription = new Dictionary<string, MmsTypeDescription>();

        public MmsConnectionFacade()
        {
            _state = new Iec61850State();
        }

        public Task<bool> TryOpenConnection(string ip)
        {
            _ip = ip;
            return new InitService(_state).TryOpenConnection(_ip);
        }

        public bool CheckConnection()
        {
            if (_ip == null) return false;
            return TcpRw.CheckConnection(_state);
        }

        public void StopConnection()
        {
            TcpRw.StopClient(_state);
        }

        public async Task<OperationResult<List<string>>> IdentifyAsync()
        {
            var identResult = await (new InitService(_state)).SendIdentifyAsync();
            var listIdent = new List<string>();
            if (identResult == null)
            {
                return new OperationResult<List<string>>("");
            }
            if (!identResult.isRejectPDUSelected())
            {
                if (identResult?.Confirmed_ResponsePDU.Service.Identify != null)
                {
                    listIdent = new List<string>()
                    {
                        identResult.Confirmed_ResponsePDU.Service.Identify.VendorName.Value,
                        identResult.Confirmed_ResponsePDU.Service.Identify.ModelName.Value,
                        identResult.Confirmed_ResponsePDU.Service.Identify.Revision.Value,

                    };
                }
                else
                {
                    return new OperationResult<List<string>>("");
                }
            }

            OperationResult<List<string>> operationResult = new OperationResult<List<string>>(listIdent);
            return operationResult;
        }

        public async Task<OperationResult<List<string>>> GetLdListAsync()
        {
            var receivedPdu = await new InfoModelClientService(_state).SendGetNameListDomainAsync();

            return new OperationResult<List<string>>(receivedPdu.Confirmed_ResponsePDU.Service.GetNameList
                .ListOfIdentifier
                .Select((identifier => identifier.Value.ToString())).ToList());
        }

        public async Task<OperationResult<List<string>>> GetListValiablesAsync(string ldInst, bool acceptCache)
        {
            MMSpdu receivedPdu;
            List<string> ldIdentifiersList = new List<string>();
            if (!acceptCache || !_cachedLDeviceDefinitions.ContainsKey(ldInst))
            {
                do
                {
                    receivedPdu = await new InfoModelClientService(_state).SendGetNameListVariablesAsync(ldInst,
                        ldIdentifiersList.LastOrDefault());
                    if (receivedPdu == null)
                    {
                        await Task.Delay(2000);
                        receivedPdu = await new InfoModelClientService(_state).SendGetNameListVariablesAsync(ldInst,
                            ldIdentifiersList.LastOrDefault());
                        if (receivedPdu == null)
                        {

                        }
                    }

                    if (receivedPdu?.Confirmed_ResponsePDU?.Service?.GetNameList != null)
                    {
                        ldIdentifiersList.AddRange(
                            receivedPdu.Confirmed_ResponsePDU.Service.GetNameList.ListOfIdentifier.Select((identifier =>
                                identifier.Value)));
                    }
                    else
                    {
                        break;
                    }


                } while (receivedPdu.Confirmed_ResponsePDU.Service.GetNameList.MoreFollows);

                _cachedLDeviceDefinitions[ldInst] = ldIdentifiersList;
            }
            else
            {
                ldIdentifiersList = _cachedLDeviceDefinitions[ldInst];
            }

            return new OperationResult<List<string>>(ldIdentifiersList);
        }

        public async Task<OperationResult<MmsTypeDescription>> GetMmsTypeDescription(string ldPathName, string lnName,
            bool acceoptCache)
        {
            if (acceoptCache && _cachedTypeDescription.ContainsKey(ldPathName + lnName))
            {
                return new OperationResult<MmsTypeDescription>(_cachedTypeDescription[ldPathName + lnName]);
            }
            else
            {
                var typeDescription =
                    await new InfoModelClientService(_state).SendGetVariableAccessAttributesAsync(ldPathName, lnName);

                var response = typeDescription.Confirmed_ResponsePDU.Service.GetVariableAccessAttributes;
                MmsTypeDescription mmsTypeDescription = GetMmsTypeDescription(response.TypeDescription, "");
                if (_cachedTypeDescription.ContainsKey(ldPathName + lnName))
                {
                    _cachedTypeDescription[ldPathName + lnName] = mmsTypeDescription;
                }
                else
                {
                    _cachedTypeDescription.Add(ldPathName + lnName, mmsTypeDescription);
                }

                return new OperationResult<MmsTypeDescription>(mmsTypeDescription);
            }
        }

        public async Task<OperationResult<List<string>>> GetListDataSetsAsync(string ldInst, bool acceptCache)
        {
            MMSpdu receivedPdu;

            receivedPdu = await new DataSetClientService(_state).SendGetNameListNamedVariableListAsync(ldInst);
            if (receivedPdu.Confirmed_ResponsePDU.Service.GetNameList != null)
            {
                var datasets = receivedPdu.Confirmed_ResponsePDU.Service.GetNameList.ListOfIdentifier.Select(
                    (identifier =>
                        identifier.Value)).ToList();
                return new OperationResult<List<string>>(datasets);
            }

            return new OperationResult<List<string>>("");
        }

        public async Task<OperationResult<DataSetDto>> GetListDataSetInfoAsync(string ldInst, string lnName,
            string datasetName, bool acceptCache)
        {
            DataSetDto datasetDto = new DataSetDto();

            var receivedPdu = await (new DataSetClientService(_state)).GetDatasetInformationAsync(ldInst,
            lnName, datasetName);


            if (receivedPdu.Confirmed_ResponsePDU != null &&
                receivedPdu.Confirmed_ResponsePDU.Service.GetNamedVariableListAttributes != null)
            {
                var fcdas =
                    receivedPdu.Confirmed_ResponsePDU.Service.GetNamedVariableListAttributes
                        .ListOfVariable.Select((type =>
                            type.VariableSpecification.Name.Domain_specific.DomainID.Value + "$" +
                            type.VariableSpecification.Name.Domain_specific.ItemID.Value)).ToList();
                datasetDto.IsDynamic = receivedPdu.Confirmed_ResponsePDU.Service.GetNamedVariableListAttributes
                    .MmsDeletable;
                datasetDto.FcdaList = fcdas;
                return new OperationResult<DataSetDto>(datasetDto);
            }
            return new OperationResult<DataSetDto>("");
        }

        public async Task<OperationResult<List<GooseDto>>> GetListGoosesAsync(string fullLdPath, string lnName,
            string deviceName)
        {
            List<GooseDto> gooseDtos = new List<GooseDto>();
            MMSpdu recievedMmSpdu =
                (await new ReadingValuesClientService(_state).SendReadAsync(fullLdPath, lnName, "GO"));

            if (recievedMmSpdu.Confirmed_ResponsePDU.Service.Read == null)
                return new OperationResult<List<GooseDto>>("");
            AccessResult accessResult = recievedMmSpdu.Confirmed_ResponsePDU.Service.Read.ListOfAccessResult.First();
            if (accessResult.Success == null && !accessResult.Success.isStructureSelected())
                return new OperationResult<List<GooseDto>>("");

            var typeDescriptionForFc =
                (await GetMmsTypeDescription(fullLdPath, lnName, true)).Item.Components.First(
                    (description => description.Name == "GO"));


            for (int i = 0; i < typeDescriptionForFc.Components.Count; i++)
            {
                GooseDto gooseDto = new GooseDto();
                gooseDto.Name = typeDescriptionForFc.Components[i].Name;

                var typeDescriptionForGse = typeDescriptionForFc.Components[i];
                var dataForGse = accessResult.Success.Structure.ToArray()[i];

                int index = Array.FindIndex(typeDescriptionForGse.Components.ToArray(),
                    (type =>
                        type.Name == "DatSet"));
                gooseDto.DatSet = dataForGse.Structure.ToArray()[index].Visible_string.Split('$').Last();


                index = Array.FindIndex(typeDescriptionForGse.Components.ToArray(),
                    (type =>
                        type.Name == "GoID"));
                gooseDto.GoId = dataForGse.Structure.ToArray()[index].Visible_string;

                index = Array.FindIndex(typeDescriptionForGse.Components.ToArray(),
                (type =>
                    type.Name == "ConfRev"));
            gooseDto.ConfRev = (int) dataForGse.Structure.ToArray()[index].Unsigned;
            index = Array.FindIndex(typeDescriptionForGse.Components.ToArray(),
                                        (type =>
                                            type.Name == "FixedOffs"));
                if (index > 0)
                {
                    gooseDto.FixedOffs = dataForGse.Structure.ToArray()[index].Boolean;
                }

                index = Array.FindIndex(typeDescriptionForGse.Components.ToArray(),
                                         (type =>
                                             type.Name == "DstAddress"));
            var dstAddressTypeDescription = typeDescriptionForGse.Components.First((type =>
                      type.Name == "DstAddress"));
            var dstAddressData = dataForGse.Structure.ToArray()[index];



            index = Array.FindIndex(dstAddressTypeDescription.Components.ToArray(),
                (type =>
                    type.Name == "Addr"));

            var macAddressString = string.Empty;
            dstAddressData.Structure.ToArray()[index].Octet_string.ForEach((b =>
            {
                if (macAddressString == String.Empty)
                {
                    macAddressString += b.ToString("X2");
                }
                else
                {
                    macAddressString += "-" + b.ToString("X2");
                }
            }));

                index = Array.FindIndex(dstAddressTypeDescription.Components.ToArray(),
                    (type =>
                        type.Name == "PRIORITY"));

                var prioroty = (uint)dstAddressData.Structure.ToArray()[index].Unsigned;
                index = Array.FindIndex(dstAddressTypeDescription.Components.ToArray(),
                    (type =>
                        type.Name == "VID"));

                var vlanId = (uint)dstAddressData.Structure.ToArray()[index].Unsigned;


                index = Array.FindIndex(dstAddressTypeDescription.Components.ToArray(),
                    (type =>
                        type.Name == "APPID"));

                var appId = (uint)dstAddressData.Structure.ToArray()[index].Unsigned;



                gooseDto.LdInst = fullLdPath.Replace(deviceName, "");
                gooseDto.CbName = gooseDto.Name;
                gooseDto.MAC_Address = macAddressString;
                gooseDto.APPID = appId;
                gooseDto.VLAN_ID =vlanId;
                gooseDto.VLAN_PRIORITY = prioroty;

                index = Array.FindIndex(typeDescriptionForGse.Components.ToArray(),
                   (type =>
                       type.Name == "MinTime"));
                try
                {
                    var minTime = dataForGse.Structure.ToArray()[index].Unsigned;

                    index = Array.FindIndex(typeDescriptionForGse.Components.ToArray(),
                    (type =>
                        type.Name == "MaxTime"));
                var maxTime = dataForGse.Structure.ToArray()[index].Unsigned;

                gooseDto.MaxTime = maxTime ;
                gooseDto.MinTime = minTime ;

                }
                catch(Exception e)
                {

                }
              
                gooseDtos.Add(gooseDto);

            }







            return new OperationResult<List<GooseDto>>(gooseDtos);
        }

        public async Task<OperationResult<List<IReportControl>>> GetListReportsAsync(string fullLdPath, string lnName, string deviceName, string reportType)
        {
            List<IReportControl> reportDtos = new List<IReportControl>();
            MMSpdu recievedMmSpdu =
                (await new ReadingValuesClientService(_state).SendReadAsync(fullLdPath, lnName, reportType));
            if (recievedMmSpdu.Confirmed_ResponsePDU.Service.Read == null) return new OperationResult<List<IReportControl>>("");
            AccessResult accessResult = recievedMmSpdu.Confirmed_ResponsePDU.Service.Read.ListOfAccessResult.First();
            if (accessResult.Success == null && !accessResult.Success.isStructureSelected()) return new OperationResult<List<IReportControl>>("");
            var r = (await GetMmsTypeDescription(fullLdPath, lnName, true)).Item.Components;
            var typeDescriptionForFc =
                (await GetMmsTypeDescription(fullLdPath, lnName, true)).Item.Components.First(
                    (description => description.Name == reportType));
            for (int i = 0; i < typeDescriptionForFc.Components.Count; i++)
            {
                IReportControl reportDto = new ReportControl();
                reportDto.Name = typeDescriptionForFc.Components.ToArray()[i].Name;

                var typeDescriptionForReport = typeDescriptionForFc.Components.ToArray()[i];
                var dataForReport = accessResult.Success.Structure.ToArray()[i];

                int index = Array.FindIndex(typeDescriptionForReport.Components.ToArray(),
                    (type =>
                        type.Name == "RptID"));
                reportDto.RptID = dataForReport.Structure.ToArray()[index].Visible_string;

                index = Array.FindIndex(typeDescriptionForReport.Components.ToArray(), (type =>
                    type.Name == "RptEna"));
                reportDto.RptEnabledBool = dataForReport.Structure.ToArray()[index].Boolean;

                //index = Array.FindIndex(typeDescriptionForReport.Structure.Components.ToArray(), (type =>
                //    type.ComponentName.Value == "Resv"));
                //reportControl.reasonCode = dataForReport.Structure.ToArray()[index].Boolean;


                index = Array.FindIndex(typeDescriptionForReport.Components.ToArray(), (type =>
                    type.Name == "DatSet"));
                reportDto.DataSet = dataForReport.Structure.ToArray()[index].Visible_string.Split('$').Last();


                index = Array.FindIndex(typeDescriptionForReport.Components.ToArray(), (type =>
                    type.Name == "ConfRev"));
                reportDto.ConfRev = dataForReport.Structure.ToArray()[index].Integer.ToString();

                index = Array.FindIndex(typeDescriptionForReport.Components.ToArray(), (type =>
                    type.Name == "OptFlds"));
                reportDto.OptFields.Value =dataForReport.Structure.ToArray()[index].Bit_string.Value.ReportOptionsFromBytes(new OptFields());

                index = Array.FindIndex(typeDescriptionForReport.Components.ToArray(), (type =>
                    type.Name == "BufTm"));
                reportDto.BufTime = (int)dataForReport.Structure.ToArray()[index].Unsigned;

                //index = Array.FindIndex(typeDescriptionForReport.Structure.Components.ToArray(), (type =>
                //    type.ComponentName.Value == "SqNum"));
                //reportControl. = (uint)dataForReport.Structure.ToArray()[index].Integer;

                index = Array.FindIndex(typeDescriptionForReport.Components.ToArray(), (type =>
                    type.Name == "TrgOps"));

                reportDto.TrgOps.Value =dataForReport.Structure.ToArray()[index].Bit_string.Value.TriggerOptionsFromBytes(new TrgOps());

                index = Array.FindIndex(typeDescriptionForReport.Components.ToArray(), (type =>
                    type.Name == "IntgPd"));
                reportDto.IntgPd = (int)dataForReport.Structure.ToArray()[index].Unsigned;


                index = Array.FindIndex(typeDescriptionForReport.Components.ToArray(), (type =>
                    type.Name == "GI"));
                reportDto.GiBool = dataForReport.Structure.ToArray()[index].Boolean;
                if (reportType == "RP")
                {
                    reportDto.Buffered = false;
                }
                else
                {
                    reportDto.Buffered = true;
                }
                reportDto.RptEnabled.Value=new RptEnabled();
                reportDto.RptEnabled.Value.Max = 1;
                reportDtos.Add(reportDto);
            }
            return new OperationResult<List<IReportControl>>(reportDtos);
        }

        public async Task<OperationResult> WriteReportDataAsync(string ldFullPath, string rptId, string itemValueName,
            object valueToSave)
        {
            try
            {
                MMSpdu res=null;
                if (itemValueName == "TrgOps")
                {
                     res = await (new ReportClientService(_state)).WriteReportValueAsync(tBasicTypeEnum.bit_string, ldFullPath,
                        rptId, itemValueName,
                        new BitString((byte[]) valueToSave, 2));
                }
                else if (itemValueName == "OptFlds")
                {
                     res = await (new ReportClientService(_state)).WriteReportValueAsync(tBasicTypeEnum.bit_string, ldFullPath,
                        rptId, itemValueName,
                        new BitString((byte[]) valueToSave, 6));
                }
                else if (itemValueName == "DatSet")
                {
                     res = await (new ReportClientService(_state)).WriteReportValueAsync(tBasicTypeEnum.VisString255,
                        ldFullPath, rptId, itemValueName,
                        valueToSave);
                }
                else if (itemValueName == "GI")
                {
                     res = await (new ReportClientService(_state)).WriteReportValueAsync(tBasicTypeEnum.BOOLEAN, ldFullPath,
                        rptId, itemValueName,
                        valueToSave);
                }
                else if (itemValueName == "RptID")
                {
                    res= await (new ReportClientService(_state)).WriteReportValueAsync(tBasicTypeEnum.VisString255,
                        ldFullPath, rptId, itemValueName,
                        valueToSave);
                    
                }
                else if ((itemValueName == "BufTm")|| (itemValueName == "IntgPd"))
                {
                     res = await (new ReportClientService(_state)).WriteReportValueAsync(tBasicTypeEnum.INT32U,
                        ldFullPath, rptId, itemValueName,
                        valueToSave);
                }
                else
                {
                    return new OperationResult("Неизвестные данные для сохранения в отчет");

                }
                if (res != null&&res.Confirmed_ResponsePDU.Service.Write.Value.First().Success!=null)
                {
                   return OperationResult.SucceedResult;
                }
                else
                {
                    return new OperationResult($"Ответ на запись {itemValueName} в устройство: ошибка");
                }


            }
            catch (Exception e)
            {
                return new OperationResult("Не удалось записать данные по MMS");
            }

        }

        public async Task<OperationResult> DeleteDataSet(string ln, string ld, string ied, string name)
        {
            try
            {
               await new DataSetClientService(_state).SendDeleteNVLAsync(new Dto.DataSetDto()
                {
                    Ied = ied,
                    Ld = ld,
                    Ln = ln,
                    Name = name
                });
                return OperationResult.SucceedResult;
            }
            catch (Exception e)
            {
               return new OperationResult(e.Message);
            }
        }

        public async Task<OperationResult> AddDataSet(string ln, string ld, string ied, string nameDataSet, List<FcdaDto> fcdaDtos)
        {
            try
            {
                await new DataSetClientService(_state).SendDefineNVLAsync(new Dto.DataSetDto()
                {
                    Ied = ied,
                    Ld = ld,
                    Ln = ln,
                    Name = nameDataSet
                },fcdaDtos);
                return OperationResult.SucceedResult;
            }
            catch (Exception e)
            {
                return new OperationResult(e.Message);
            }
        }


        private MmsTypeDescription GetMmsTypeDescription(TypeDescription responseTypeDescription, string name)
        {
            MmsTypeDescription mmsTypeDescription = new MmsTypeDescription();
            mmsTypeDescription.Name = name;
            mmsTypeDescription.IsArray = responseTypeDescription.isArraySelected();
            mmsTypeDescription.IsStructure = responseTypeDescription.isStructureSelected();
            if (responseTypeDescription.isBit_stringSelected())
            {

                mmsTypeDescription.BasicType = tBasicTypeEnum.bit_string;

            }
            else if (responseTypeDescription.isArraySelected())
            {
                mmsTypeDescription.BasicType = tBasicTypeEnum.Struct;
            }
            else if (responseTypeDescription.Integer != null)
            {
                switch (responseTypeDescription.Integer.Value)
                {
                    case 8:
                        mmsTypeDescription.BasicType = tBasicTypeEnum.INT8;
                        break;
                    case 16:
                        mmsTypeDescription.BasicType = tBasicTypeEnum.INT16;
                        break;
                    case 24:
                        mmsTypeDescription.BasicType = tBasicTypeEnum.INT24;
                        break;
                    case 32:
                        mmsTypeDescription.BasicType = tBasicTypeEnum.INT32;
                        break;
                }
            }
            else if (responseTypeDescription.isBcdSelected())
            {
            }
            else if (responseTypeDescription.isBooleanSelected())
            {
                mmsTypeDescription.BasicType = tBasicTypeEnum.BOOLEAN;
            }
            else if (responseTypeDescription.isFloating_pointSelected())
            {
                switch (responseTypeDescription.Floating_point.Format_width.Value)
                {
                    case 32:
                        mmsTypeDescription.BasicType = tBasicTypeEnum.FLOAT32;
                        break;
                    case 64:
                        mmsTypeDescription.BasicType = tBasicTypeEnum.FLOAT64;
                        break;
                }
            }
            else if (responseTypeDescription.isGeneralized_timeSelected() ||
                     responseTypeDescription.isUtc_timeSelected() || responseTypeDescription.isBinary_timeSelected())
            {
                mmsTypeDescription.BasicType = tBasicTypeEnum.Timestamp;
            }
            else if (responseTypeDescription.isOctet_stringSelected())
            {
                mmsTypeDescription.BasicType = tBasicTypeEnum.Octet64;
            }
            else if (responseTypeDescription.isVisible_stringSelected())
            {
                switch (responseTypeDescription.Visible_string.Value)
                {
                    case 255:
                    case -255:
                        mmsTypeDescription.BasicType = tBasicTypeEnum.VisString255;
                        break;
                    case 129:
                    case -129:
                        mmsTypeDescription.BasicType = tBasicTypeEnum.VisString129;
                        break;
                    case 64:
                    case -64:
                        mmsTypeDescription.BasicType = tBasicTypeEnum.VisString64;
                        break;
                    case 32:
                    case -32:
                        mmsTypeDescription.BasicType = tBasicTypeEnum.VisString32;
                        break;
                    case 65:
                    case -65:
                        mmsTypeDescription.BasicType = tBasicTypeEnum.VisString65;
                        break;
                }
            }

            if (mmsTypeDescription.IsStructure)
            {
                foreach (var component in responseTypeDescription.Structure.Components)
                {
                    mmsTypeDescription.Components.Add(GetMmsTypeDescription(component.ComponentType.TypeDescription,
                        component.ComponentName.Value));
                }
            }

            return mmsTypeDescription;
        }
    }
}