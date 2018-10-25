using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Modules.FTP.Infrastructure.Serviсes;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Infrastructure.Services;

namespace BISC.Modules.Gooses.Model.Services
{
   public class FtpGooseModelService: IFtpGooseModelService
    {
        private readonly IDeviceFileWritingServices _deviceFileWritingServices;

        public FtpGooseModelService(IDeviceFileWritingServices deviceFileWritingServices )
        {
            _deviceFileWritingServices = deviceFileWritingServices;
        }


        #region Implementation of IFtpGooseModelService

        public async Task<List<GooseFtpDto>> GetGooseDtosFromDevice(string ip)
        {
            string fileInDevice =await _deviceFileWritingServices.ReadFileStringFromDevice(ip, "1:/CFG", "GOOSETR.CFG");
            try
            {

         
            var gooseStrings = GetGooseNamesListFromFile(fileInDevice);


            List<GooseFtpDto> gooseFtpDtos=new List<GooseFtpDto>();
            foreach (var gooseString in gooseStrings)
            {
                gooseFtpDtos.Add(new GooseFtpDto(){Name = gooseString});
            }

            return gooseFtpDtos;
            }
            catch (Exception e)
            {
                return new List<GooseFtpDto>();
            }
        }

        public async Task<OperationResult> WriteGooseDtosToDevice(string ip, List<GooseFtpDto> gooseDtos)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                TextWriter streamWriter = new StringWriter(sb);
                Write(gooseDtos, streamWriter);
                string fileString = sb.ToString();

                var res= await _deviceFileWritingServices.WriteFileStringInDevice(ip, new List<string>() {fileString},
                    new List<string>() {"GOOSETR.CFG"});
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


        private void Write(List<GooseFtpDto> gooseDtosToParse, TextWriter streamWriter)
        {
            using (streamWriter)
            {


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
                    string MacAddress = gooseDtoObj.MacAddress.Replace("-",String.Empty);

                    streamWriter.WriteLine($"GoCB({ld} {goCBName} {goId} {dataSet} {confRev} {fixedOffs} {minTime} {maxTime})");
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
