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
                reportDto.ConfRev = (int)dataForReport.Structure.ToArray()[index].Integer;

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
               var res=await new DataSetClientService(_state).SendDeleteNVLAsync(new Dto.DataSetDto()
                {
                    Ied = ied,
                    Ld = ld,
                    Ln = ln,
                    Name = name
                });
                if (res?.Confirmed_ResponsePDU?.Service?.DeleteNamedVariableList == null ||
                    res.Confirmed_ResponsePDU.Service.DeleteNamedVariableList.NumberDeleted.Value != 1)
                {
                    return new OperationResult("Датасет не был удален");
                }
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
               var res= await new DataSetClientService(_state).SendDefineNVLAsync(new Dto.DataSetDto()
                {
                    Ied = ied,
                    Ld = ld,
                    Ln = ln,
                    Name = nameDataSet
                },fcdaDtos);
                if (res?.Confirmed_ResponsePDU?.Service?.DefineNamedVariableList == null)
                {
                    return new OperationResult("Датасет не был добавлен");
                }
                return OperationResult.SucceedResult;
            }
            catch (Exception e)
            {
                return new OperationResult(e.Message);
            }
        }

        public Task<SettingsControlDto> GetSettingsControl(MmsTypeDescription lnMmsTypeDescription, string fc, string iedName, string lnName,
            string ldName)
        {
            return new InfoModelClientService(_state).ReadSettingsControls(lnMmsTypeDescription,fc,iedName,lnName,ldName);
        }

        public async Task<bool> SetSettingsControl(string fc, string iedName, string lnName,
            string ldName,string newVal)
        {
            var customParts=new List<string>();
            customParts.Add("SGCB");
            customParts.Add("ActSG");

            var res =await new InfoModelClientService(_state).SendWriteAsync(tBasicTypeEnum.INT32U,iedName+ldName, lnName, fc, newVal,customParts);
            return true;
        }

        public async Task<OperationResult<ValueDescription>> ReadValuesAsync(string fc, string iedName, string lnName, string ldName, List<string> customItemPathParts = null)
        {
            var res = await new ReadingValuesClientService(_state).SendReadAsync(iedName + ldName, lnName, fc,
                customItemPathParts);
            if (res.Confirmed_ResponsePDU?.Service?.Read?.ListOfAccessResult?.All(
                    (result => result.isSuccessSelected())) != true)
            {
                return new OperationResult<ValueDescription>($"Не удалось прочитать значения {iedName}.{ldName}.{lnName}.[{fc}] {string.Join(".",customItemPathParts)}");
            }
            else
            {
                return new OperationResult<ValueDescription>(GetValueDescription(res.Confirmed_ResponsePDU?.Service?.Read?.ListOfAccessResult));
            }
        }

        private ValueDescription GetValueDescription(ICollection<AccessResult> results)
        {
            ValueDescription valueDescription = new ValueDescription();
            valueDescription.IsStructure = true;
            valueDescription.BasicType = tBasicTypeEnum.Struct;
            foreach (var result in results)
            {
                valueDescription.IsStructure = result.Success.isStructureSelected();
                if (result.Success.Structure != null)
                {
                    valueDescription.Components = MapData(result.Success.Structure);
                }

            }

            return valueDescription;
        }

        public static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return origin.AddSeconds(timestamp);
        }


        private string GetStringValue( Data asn1Data)
        {
            if (asn1Data == null) return String.Empty;
            if (asn1Data.isIntegerSelected())
            {
                return asn1Data.Integer.ToString();
            }
            else if (asn1Data.isBcdSelected())
            {
                return asn1Data.Bcd.ToString();
            }
            else if (asn1Data.isBooleanSelected())
            {
               return asn1Data.Boolean.ToString();
            }
            else if (asn1Data.isFloating_pointSelected())
            {
                if (asn1Data.Floating_point.Value.Length == 5)
                {
                    float k = 0.0F;
                    byte[] tmp = new byte[4];
                    tmp[0] = asn1Data.Floating_point.Value[4];
                    tmp[1] = asn1Data.Floating_point.Value[3];
                    tmp[2] = asn1Data.Floating_point.Value[2];
                    tmp[3] = asn1Data.Floating_point.Value[1];
                    k = BitConverter.ToSingle(tmp, 0);
                    return k.ToString();
                }
                else if (asn1Data.Floating_point.Value.Length == 9)
                {
                    double k = 0.0;
                    byte[] tmp = new byte[8];
                    tmp[0] = asn1Data.Floating_point.Value[8];
                    tmp[1] = asn1Data.Floating_point.Value[7];
                    tmp[2] = asn1Data.Floating_point.Value[6];
                    tmp[3] = asn1Data.Floating_point.Value[5];
                    tmp[4] = asn1Data.Floating_point.Value[4];
                    tmp[5] = asn1Data.Floating_point.Value[3];
                    tmp[6] = asn1Data.Floating_point.Value[2];
                    tmp[7] = asn1Data.Floating_point.Value[1];
                    k = BitConverter.ToDouble(tmp, 0);
                    return k.ToString();
                }
            }
            else if (asn1Data.isGeneralized_timeSelected())
            {
                return asn1Data.Generalized_time.ToString();
            }
            else if (asn1Data.isMMSStringSelected())
            {
               return asn1Data.MMSString.Value.ToString();
            }
            else if (asn1Data.isObjIdSelected())
            {
                return asn1Data.ObjId.Value.ToString();
            }
            else if (asn1Data.isOctet_stringSelected())
            {
                System.Text.Decoder ascii = (new ASCIIEncoding()).GetDecoder();
                int bytesUsed = 0;
                int charsUsed = 0;
                bool completed = false;
                char[] chars = new char[asn1Data.Octet_string.Length];
                ascii.Convert(asn1Data.Octet_string, 0, asn1Data.Octet_string.Length, chars, 0,
                    asn1Data.Octet_string.Length, true, out bytesUsed, out charsUsed, out completed);
                return new String(chars);
            }
            else if (asn1Data.isUnsignedSelected())
            {
                return asn1Data.Unsigned.ToString();
            }
            else if (asn1Data.isUtc_timeSelected())
            {
                long seconds;
                long millis;

                //if (actualNode.Address == "BayControllerQ/LTRK1.ApcFTrk.T")
                //    iecs.logger.LogDebug("BayControllerQ/LTRK1.ApcFTrk.T"); ;
                if (asn1Data.Utc_time.Value != null && asn1Data.Utc_time.Value.Length == 8)
                {
                    seconds = (asn1Data.Utc_time.Value[0] << 24) +
                              (asn1Data.Utc_time.Value[1] << 16) +
                              (asn1Data.Utc_time.Value[2] << 8) +
                              (asn1Data.Utc_time.Value[3]);

                    millis = 0;
                    for (int i = 0; i < 24; i++)
                    {
                        if (((asn1Data.Utc_time.Value[(i / 8) + 4] << (i % 8)) & 0x80) > 0)
                        {
                            millis += 1000000 / (1 << (i + 1));
                        }
                    }
                    millis /= 1000;


                    DateTime dt = ConvertFromUnixTimestamp(seconds);
                    dt = dt.AddMilliseconds(millis);
                    return dt.ToLocalTime().ToString();
                }
                else
                {
                    return DateTime.Now.ToString();
                }
            }

            else if (asn1Data.isVisible_stringSelected())
            {
                return asn1Data.Visible_string;
            }
            else if (asn1Data.isBinary_timeSelected())
            {
                ulong millis;
                ulong days = 0;
                DateTime origin;

                millis = (ulong)(asn1Data.Binary_time.Value[0] << 24) +
                         (ulong)(asn1Data.Binary_time.Value[1] << 16) +
                         (ulong)(asn1Data.Binary_time.Value[2] << 8) +
                         (ulong)(asn1Data.Binary_time.Value[3]);
                if (asn1Data.Binary_time.Value.Length == 6)
                {
                    days = (ulong)(asn1Data.Binary_time.Value[4] << 8) +
                           (ulong)(asn1Data.Binary_time.Value[5]);
                    origin = new DateTime(1984, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                    //millis *= 1000;
                }
                else
                {
                    origin = DateTime.UtcNow.Date;
                }

                double dMillis = (double)(millis + days * 24 * 3600 * 1000);
                origin = origin.AddMilliseconds(dMillis);

                return origin.ToLocalTime().ToString();
            }
            else if (asn1Data.isBit_stringSelected())
            {
                string bitString = String.Empty;
                asn1Data.Bit_string.Value.ForEach((b => bitString = bitString + b.ToString()));
                if (asn1Data.Bit_string.Value.Length == 0)
                {

                }
                return bitString;
            }
         return String.Empty;
        }


        private List<ValueDescription> MapData(ICollection<Data> datas)
        {
            List<ValueDescription> valueDescriptions = new List<ValueDescription>();


            foreach (var data in datas)
            {
               ValueDescription valueDescription=new ValueDescription();
                valueDescription.IsStructure = data.isStructureSelected();
                if (data.Structure != null)
                {
                    if (valueDescription.Components == null)
                    {
                        valueDescription.Components=new List<ValueDescription>();
                    }
                    valueDescription.Components.AddRange(MapData(data.Structure));
                }
                else
                {
                    valueDescription.Value = GetStringValue(data);
                    //if (data.isBit_stringSelected())
                    //{

                    //    valueDescription.BasicType = tBasicTypeEnum.bit_string;

                    //}
                    //else if (data.isArraySelected())
                    //{
                    //    valueDescription.BasicType = tBasicTypeEnum.Struct;
                    //}
                    //else if (data.isIntegerSelected())
                    //{
                    //    valueDescription.Value = data.Integer;
                    //    valueDescription.BasicType = tBasicTypeEnum.INT32;
                    //}
                    //else if (data.isBcdSelected())
                    //{
                    //}
                    //else if (data.isBooleanSelected())
                    //{
                    //    valueDescription.BasicType = tBasicTypeEnum.BOOLEAN;
                    //    valueDescription.Value = data.Boolean;
                    //}
                    //else if (data.isFloating_pointSelected())
                    //{


                    //    valueDescription.BasicType = tBasicTypeEnum.FLOAT32;
                    //    valueDescription.Value = data.Floating_point.Value;
                    //}

                    //else if (data.isGeneralized_timeSelected() ||
                    //         data.isUtc_timeSelected() || data.isBinary_timeSelected())
                    //{
                    //    mmsTypeDescription.BasicType = tBasicTypeEnum.Timestamp;
                    //}
                    //else if (data.isOctet_stringSelected())
                    //{
                    //    mmsTypeDescription.BasicType = tBasicTypeEnum.Octet64;
                    //}
                    //else if (data.isVisible_stringSelected())
                    //{
                    //    switch (responseTypeDescription.Visible_string.Value)
                    //    {
                    //        case 255:
                    //        case -255:
                    //            mmsTypeDescription.BasicType = tBasicTypeEnum.VisString255;
                    //            break;
                    //        case 129:
                    //        case -129:
                    //            mmsTypeDescription.BasicType = tBasicTypeEnum.VisString129;
                    //            break;
                    //        case 64:
                    //        case -64:
                    //            mmsTypeDescription.BasicType = tBasicTypeEnum.VisString64;
                    //            break;
                    //        case 32:
                    //        case -32:
                    //            mmsTypeDescription.BasicType = tBasicTypeEnum.VisString32;
                    //            break;
                    //        case 65:
                    //        case -65:
                    //            mmsTypeDescription.BasicType = tBasicTypeEnum.VisString65;
                    //            break;
                    //    }
                    //}
                    
                }
                valueDescriptions.Add(valueDescription);
            }

            return valueDescriptions;
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