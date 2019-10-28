using BISC.Infrastructure.Global.Common;
using BISC.Model.Infrastructure.Serializing;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.FTP.Infrastructure.Serviсes;
using BISC.Modules.Gooses.Infrastructure.Factorys;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Model.Model;
using BISC.Modules.Gooses.Model.Model.Matrix;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;

namespace BISC.Modules.Gooses.Model.Services
{
    public class FtpGooseModelService : IFtpGooseModelService
    {
        private readonly IDeviceFileWritingServices _deviceFileWritingServices;
        private readonly IModelElementsRegistryService _elementsRegistryService;
        private readonly IPingService _pingService;
        private readonly IGooseMatrixParsersFactory _gooseMatrixParsersFactory;
        private readonly ILoggingService _loggingService;

        public FtpGooseModelService(IDeviceFileWritingServices deviceFileWritingServices, IModelElementsRegistryService elementsRegistryService,
            IPingService pingService, IGooseMatrixParsersFactory gooseMatrixParsersFactory, ILoggingService loggingService)
        {
            _deviceFileWritingServices = deviceFileWritingServices;
            _elementsRegistryService = elementsRegistryService;
            _pingService = pingService;
            _gooseMatrixParsersFactory = gooseMatrixParsersFactory;
            _loggingService = loggingService;
        }


        #region Implementation of IFtpGooseModelService

        public async Task<OperationResult<List<GooseFtpDto>>> GetGooseDtosFromDevice(string ip)
        {
            var fileInDeviceRes =
                await _deviceFileWritingServices.ReadFileStringFromDevice(ip, "1:/CFG", "GOOSETR.CFG");
            if (!fileInDeviceRes.IsSucceed)
            {
                return new OperationResult<List<GooseFtpDto>>(fileInDeviceRes.GetFirstError());
            }
            try
            {
                var fileInDevice = fileInDeviceRes.Item;
                var gooseStrings = GetGooseNamesListFromFile(fileInDevice);


                List<GooseFtpDto> gooseFtpDtos = new List<GooseFtpDto>();
                foreach (var gooseString in gooseStrings)
                {
                    gooseFtpDtos.Add(new GooseFtpDto() { Name = gooseString });
                }

                return new OperationResult<List<GooseFtpDto>>(gooseFtpDtos);
            }
            catch (Exception e)
            {
                return new OperationResult<List<GooseFtpDto>>(new List<GooseFtpDto>(), false, "Goose write error");
            }
        }

        public async Task<OperationResult> WriteGooseDtosToDevice(IDevice device, List<GooseFtpDto> gooseDtos)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                TextWriter streamWriter = new StringWriter(sb);
                Write(device, gooseDtos, streamWriter);
                string fileString = sb.ToString();

                var res = await _deviceFileWritingServices.WriteFileStringInDevice(device.Ip, new List<string>() { fileString },
                    new List<string>() { "GOOSETR.CFG" });
                if (res.IsSucceed)
                {
                    return OperationResult.SucceedResult;
                }
                else
                {
                    return new OperationResult($"Не удалось обновить блоки управления GOOSE по FTP: {res.GetFirstError()}");
                }
            }
            catch (Exception e)
            {
                return new OperationResult("Не удалось обновить блоки управления GOOSE по FTP");
            }
        }

        public async Task<OperationResult<IGooseMatrixFtp>> GetGooseMatrixByFtp(string ip)
        {
            var fileInDeviceRes =
                await _deviceFileWritingServices.ReadFileStringFromDevice(ip, "1:/CFG", "GOOSERE.CFG");
            IGooseMatrixFtp gooseMatrixFtp = new GooseMatrixFtp();
            if (!fileInDeviceRes.IsSucceed)
            {
                _loggingService.LogMessage(fileInDeviceRes.GetFirstError(), SeverityEnum.Warning);
                //return new OperationResult<IGooseMatrixFtp>(null, false, fileInDeviceRes.GetFirstError());
            }

            if (fileInDeviceRes.Item == null || string.IsNullOrWhiteSpace(fileInDeviceRes.Item))
            {
                return new OperationResult<IGooseMatrixFtp>(gooseMatrixFtp);
            }
            var fileInDevice = fileInDeviceRes.Item;

            try
            {

                string macAddressesPattern = "MAC{([^.}])*}";
                string gocbrefPattern = "GocbRef{([^.}])*}";
                string configPattern = "config{([^.}])*}";
                var macAddressesPatternRegEx = new Regex(macAddressesPattern, RegexOptions.Singleline);
                var gocbrefPatternRegEx = new Regex(gocbrefPattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                var configPatternRegEx = new Regex(configPattern, RegexOptions.Singleline);

                var macMatch = macAddressesPatternRegEx.Match(fileInDevice);
                var gocbMatch = gocbrefPatternRegEx.Match(fileInDevice);
                var configMatch = configPatternRegEx.Match(fileInDevice);

                var macs = macMatch.Value.Replace("MAC{\r\n", String.Empty).Replace("\r\n}", String.Empty).Replace("}", String.Empty).Replace("\r", String.Empty).Split('\n');
                var gocbs = gocbMatch.Value.
                    Replace("GocbRef{\r\n", String.Empty). // For 23.10
                    Replace("gocbRef{\r\n", String.Empty). // For 23.9
                    Replace("gocbref{\r\n", String.Empty). // For 23.9
                    Replace("\r\n}", String.Empty).
                    Replace("}", String.Empty).
                    Replace("\r", String.Empty).
                    Split('\n');
                var configs = configMatch.Value.Replace("config{\r\n", String.Empty).Replace("\r\n}", String.Empty).Replace("}", String.Empty).Replace("\r", String.Empty).Split('\n');

                foreach (var mac in macs)
                {
                    gooseMatrixFtp.MacAddressList.Add(new MacAddressEntity() { MacAddress = mac });
                }

                foreach (var gocb in gocbs)
                {
                    if (string.IsNullOrEmpty(gocb))
                    {
                        continue;
                    }
                    IGoCbFtpEntity goCbFtpEntity = new GoCbFtpEntity();
                    var entries = gocb.Split(',', ':');
                    goCbFtpEntity.IndexOfGoose = int.Parse(entries[0]);
                    goCbFtpEntity.GoCbReference = entries[1];
                    goCbFtpEntity.AppId = entries[2];
                    if (entries.Length > 3) // For 23.10
                    {
                        goCbFtpEntity.ConfRev = int.Parse(entries[3]);
                    }
                    else
                    {
                        goCbFtpEntity.ConfRev = null;
                    }

                    gooseMatrixFtp.GoCbFtpEntities.Add(goCbFtpEntity);
                }
                foreach (var config in configs)
                {
                    if (string.IsNullOrEmpty(config))
                    {
                        continue;
                    }
                    IGooseRowFtpEntity gooseRowFtpEntity = new GooseRowFtpEntity();
                    var entries = config.Split(',');
                    int bitIndex = int.Parse(entries[2]);
                    if (bitIndex > 64)
                    {
                        gooseRowFtpEntity = new GooseRowQualityFtpEntity();
                        gooseRowFtpEntity.BitIndex = bitIndex - 64;
                        (gooseRowFtpEntity as GooseRowQualityFtpEntity).IsValiditySelected = entries[3] == "1";
                    }
                    else
                    {
                        gooseRowFtpEntity.BitIndex = bitIndex;
                    }
                    gooseRowFtpEntity.IndexOfGoose = int.Parse(entries[0]);
                    gooseRowFtpEntity.NumberOfFcdaInDataSetOfGoose = int.Parse(entries[1]);
                    if (gooseRowFtpEntity is GooseRowQualityFtpEntity qualityFtpEntity)
                    {
                        gooseMatrixFtp.GooseRowQualityFtpEntities.Add(qualityFtpEntity);
                    }
                    else
                    {
                        gooseMatrixFtp.GooseRowFtpEntities.Add(gooseRowFtpEntity);
                    }
                }
                return new OperationResult<IGooseMatrixFtp>(gooseMatrixFtp);
            }
            catch (Exception e)
            {
                return new OperationResult<IGooseMatrixFtp>(null, false, "Goose matrix read error");
            }
        }

        public async Task<OperationResult> DeletGoosesAndResetDevice(IDevice device)
        {
            await _deviceFileWritingServices.DeletFileStringFromDevice(device.Ip, "1:/CFG/GOOSERE.CFG");
            await _deviceFileWritingServices.DeletFileStringFromDevice(device.Ip, "1:/CFG/GOOSEIN.ZIP");
            await _deviceFileWritingServices.ResetDevice(device.Ip);
            // Ожидание перезагрузки устройства
            bool isPing;
            int counter = 0;
            do
            {
                if (counter >= 30)
                {
                    return new OperationResult("Устройство не отвечает!");
                }

                await Task.Delay(2000);
                isPing = await _pingService.GetPing(device.Ip);
                counter++;
            } while (!isPing);

            return OperationResult.SucceedResult;
        }

        public async Task<OperationResult> WriteGooseMatrixFtpToDevice(IDevice device, IGooseMatrixFtp gooseMatrixFtp)
        {
            //RestartProces

            try
            {
                var text = _gooseMatrixParsersFactory.GetGooseMatrixParser(device).GetFileStringFromMatrixModel(gooseMatrixFtp);

                if (text != null)
                {
                    return await _deviceFileWritingServices.WriteFileStringInDevice(device.Ip, new List<string>() { text },
                        new List<string>() { "GOOSERE.CFG" });
                }

                return OperationResult.SucceedResult;

            }
            catch (Exception e)
            {
                return new OperationResult(e.Message);
            }
        }

        public async Task<OperationResult<List<IGooseInputModelInfo>>> GetGooseDeviceInputFromDevice(string ip, string deviceName)
        {
            try
            {
                var readResult = await _deviceFileWritingServices.ReadFileStringFromDevice(ip, "1:/CFG", "GOOSEIN.ZIP");
                IGooseDeviceInput gooseDeviceInput;
                if (readResult.Item == null)
                {
                    gooseDeviceInput = new GooseDeviceInput() { DeviceOwnerName = deviceName };
                }
                else
                {
                    gooseDeviceInput =
                        _elementsRegistryService.DeserializeModelElement<IGooseDeviceInput>(
                            XElement.Parse(readResult.Item));
                }
                return new OperationResult<List<IGooseInputModelInfo>>(gooseDeviceInput.GooseInputModelInfoList.ToList());
            }
            catch (Exception e)
            {
                return new OperationResult<List<IGooseInputModelInfo>>(null, false, e.Message);
            }
        }

        public async Task<OperationResult> WriteGooseDeviceInputFromDevice(string ip,
            List<IGooseInputModelInfo> gooseInputModelInfos)
        {
            var gooseDeviceInput = new GooseDeviceInput();
            gooseInputModelInfos.ForEach(el => gooseDeviceInput.GooseInputModelInfoList.Add(el));
            try
            {
                var text = _elementsRegistryService.SerializeModelElement(gooseDeviceInput, SerializingType.Extended).ToString();
                return await _deviceFileWritingServices.WriteFileStringInDevice(ip, new List<string>() { text },
                    new List<string>() { "GOOSEIN.ZIP" });
            }
            catch (Exception e)
            {
                return new OperationResult(e.Message);
            }

        }



        private void Write(IDevice device, List<GooseFtpDto> gooseDtosToParse, TextWriter streamWriter)
        {
            using (streamWriter)
            {
                streamWriter.WriteLine($"Dev({device.Type})");
                foreach (var gooseDtoObj in gooseDtosToParse)
                {
                    string ld = gooseDtoObj.LdInst;
                    string goCBName = gooseDtoObj.Name;
                    string goId = gooseDtoObj.GoId;
                    string dataSet = gooseDtoObj.SelectedDataset;
                    uint confRev = (uint)gooseDtoObj.ConfRev;
                    string fixedOffs = gooseDtoObj.FixedOffs ? "1" : "0";
                    string minTime = gooseDtoObj.MinTime.ToString();
                    string maxTime = gooseDtoObj.MaxTime.ToString();
                    string VlanPriority = gooseDtoObj.VlanPriority.ToString();
                    string VlanId = gooseDtoObj.VlanId.ToString();
                    string AppId = gooseDtoObj.AppId.ToString();
                    string MacAddress = gooseDtoObj.MacAddress.Replace("-", String.Empty);

                    streamWriter.WriteLine(
                        $"GoCB({ld} {goCBName} {goId} {dataSet} {confRev} {fixedOffs} {minTime} {maxTime})");
                    streamWriter.WriteLine($"GoDA({VlanPriority} {VlanId} {AppId} {MacAddress})");
                    streamWriter.WriteLine();
                }
            }
        }


        private List<string> GetGooseNamesListFromFile(string fileString)
        {
            TextReader textReader = new StringReader(fileString);
            List<string> gooseNames = new List<string>();
            string gooseCbPattern = "GoCB(.*)";
            Regex gooseCbRegex = new Regex(gooseCbPattern);


            string line;
            do
            {
                line = textReader.ReadLine();
                if (!string.IsNullOrEmpty(line))
                {
                    if (gooseCbRegex.IsMatch(line))
                    {
                        var gooseCbStrings = line.Split(' ');
                        if (gooseCbStrings.Length != 8)
                        {
                            continue;
                        }

                        var gooseName = gooseCbStrings[1];
                        gooseNames.Add(gooseName);
                    }
                }
            } while (line != null);

            return gooseNames;
        }

        #endregion


    }
}