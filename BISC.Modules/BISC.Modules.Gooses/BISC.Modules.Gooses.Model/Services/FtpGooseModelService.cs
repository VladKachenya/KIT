using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.FTP.Infrastructure.Serviсes;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Model.Model;
using BISC.Modules.Gooses.Model.Model.Matrix;

namespace BISC.Modules.Gooses.Model.Services
{
    public class FtpGooseModelService : IFtpGooseModelService
    {
        private readonly IDeviceFileWritingServices _deviceFileWritingServices;

        public FtpGooseModelService(IDeviceFileWritingServices deviceFileWritingServices)
        {
            _deviceFileWritingServices = deviceFileWritingServices;
        }


        #region Implementation of IFtpGooseModelService

        public async Task<OperationResult<List<GooseFtpDto>>> GetGooseDtosFromDevice(string ip)
        {
            string fileInDevice =
                await _deviceFileWritingServices.ReadFileStringFromDevice(ip, "1:/CFG", "GOOSETR.CFG");
            try
            {
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
                Write(device,gooseDtos, streamWriter);
                string fileString = sb.ToString();

                var res = await _deviceFileWritingServices.WriteFileStringInDevice(device.Ip, new List<string>() { fileString },
                    new List<string>() { "GOOSETR.CFG" });
                if (res)
                {
                    return OperationResult.SucceedResult;
                }
                else
                {
                    return new OperationResult("Не удалось обновить блоки управления GOOSE по FTP");
                }
            }
            catch (Exception e)
            {
                return new OperationResult("Не удалось обновить блоки управления GOOSE по FTP");
            }
        }

        public async Task<OperationResult<IGooseMatrixFtp>> GetGooseMatrixByFtp(string ip)
        {
            string fileInDevice =
                await _deviceFileWritingServices.ReadFileStringFromDevice(ip, "1:/CFG", "GOOSERE.CFG");
            IGooseMatrixFtp gooseMatrixFtp = new GooseMatrixFtp();
            if (string.IsNullOrWhiteSpace(fileInDevice))
            {
                return new OperationResult<IGooseMatrixFtp>(gooseMatrixFtp);
            }
            try
            {

                string macAddressesPattern = "MAC{([^.}])*}";
                string gocbrefPattern = "gocbRef{([^.}])*}";
                string configPattern = "config{([^.}])*}";
                var macAddressesPatternRegEx = new Regex(macAddressesPattern, RegexOptions.Singleline);
                var gocbrefPatternRegEx = new Regex(gocbrefPattern, RegexOptions.Singleline);
                var configPatternRegEx = new Regex(configPattern, RegexOptions.Singleline);

                var macMatch = macAddressesPatternRegEx.Match(fileInDevice);
                var gocbMatch = gocbrefPatternRegEx.Match(fileInDevice);
                var configMatch = configPatternRegEx.Match(fileInDevice);

                var macs = macMatch.Value.Replace("MAC{\r\n", String.Empty).Replace("\r\n}", String.Empty).Replace("\r", String.Empty).Split('\n');
                var gocbs = gocbMatch.Value.Replace("gocbRef{\r\n", String.Empty).Replace("\r\n}", String.Empty).Replace("\r", String.Empty).Split('\n');
                var configs = configMatch.Value.Replace("config{\r\n", String.Empty).Replace("\r\n}", String.Empty).Replace("\r", String.Empty).Split('\n');

                foreach (var mac in macs)
                {
                    gooseMatrixFtp.MacAddressList.Add(new MacAddressEntity() { MacAddress = mac });
                }

                foreach (var gocb in gocbs)
                {
                    IGoCbFtpEntity goCbFtpEntity = new GoCbFtpEntity();
                    var entries = gocb.Split(',', ':');
                    goCbFtpEntity.IndexOfGoose = int.Parse(entries[0]);
                    goCbFtpEntity.GoCbReference = entries[1];
                    goCbFtpEntity.AppId = entries[2];

                    gooseMatrixFtp.GoCbFtpEntities.Add(goCbFtpEntity);
                }
                foreach (var config in configs)
                {
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
                return new OperationResult<IGooseMatrixFtp>(null, false, "Goose read error");
            }
        }

        public async Task<OperationResult> WriteGooseMatrixToDevice(string ip, List<GooseFtpDto> gooseDtos)
        {

            return OperationResult.SucceedResult;
        }


        private void Write(IDevice device, List<GooseFtpDto> gooseDtosToParse, TextWriter streamWriter)
        {
            using (streamWriter)
            {
                Regex reg = new Regex("MR\\d*");
                var matches = reg.Matches(device.Name);
                if (matches.Count > 0)
                {
                    streamWriter.WriteLine($"Dev{matches[0]}");
                }
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
                        if (gooseCbStrings.Length != 8) continue;
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