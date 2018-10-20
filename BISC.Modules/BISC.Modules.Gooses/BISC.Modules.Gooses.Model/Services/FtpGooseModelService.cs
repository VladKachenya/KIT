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

        public FtpGooseModelService( )
        {
        }


        #region Implementation of IFtpGooseModelService

        public List<GooseFtpDto> GetGooseDtosFromDevice(string ip)
        {
            string fileInDevice = string.Empty;
            var gooseStrings = GetGooseNamesListFromFile(fileInDevice);


            List<GooseFtpDto> gooseFtpDtos=new List<GooseFtpDto>();
            foreach (var gooseString in gooseStrings)
            {
                gooseFtpDtos.Add(new GooseFtpDto(){Name = gooseString});
            }

            return gooseFtpDtos;
        }

        public Task<OperationResult> WriteGooseDtosToDevice(string ip, List<GooseFtpDto> gooseDtos)
        {
            throw new NotImplementedException();
        }


        private void Write(List<GooseFtpDto> gooseDtosToParse, TextWriter streamWriter)
        {
            using (streamWriter)
            {


                foreach (var gooseDtoObj in gooseDtosToParse)
                {
                    //var gse = gooseDto.Gse;
                    //var gseControl = gooseDto.GseControl;

                    //string ld = gse.ldInst;
                    //string goCBName = gseControl.name;
                    //string goId = gseControl.appID;
                    //string dataSet = gseControl.datSet;
                    //uint confRev = gseControl.confRev;
                    //string fixedOffs = gseControl.fixedOffs ? "1" : "0";
                    //string minTime = gse.MinTime.Value.ToString();
                    //string maxTime = gse.MaxTime.Value.ToString();
                    //string VlanPriority = uint.Parse(gse.Address.P.First((p => p.type == "VLAN-PRIORITY")).Value, NumberStyles.HexNumber).ToString();
                    //string VlanId = uint.Parse(gse.Address.P.First((p => p.type == "VLAN-ID")).Value, NumberStyles.HexNumber).ToString();
                    //string AppId = uint.Parse(gse.Address.P.First((p => p.type == "APPID")).Value, NumberStyles.HexNumber).ToString();
                    //string MacAddress = gse.Address.P.First((p => p.type == "MAC-Address")).Value.Replace("-", "");


                    //streamWriter.WriteLine($"GoCB({ld} {goCBName} {goId} {dataSet} {confRev} {fixedOffs} {minTime} {maxTime})");
                    //streamWriter.WriteLine($"GoDA({VlanPriority} {VlanId} {AppId} {MacAddress})");
                    //streamWriter.WriteLine();
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
