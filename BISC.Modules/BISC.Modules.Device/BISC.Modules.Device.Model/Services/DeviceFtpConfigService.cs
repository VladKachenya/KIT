using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Modules.Device.Infrastructure.Model.Config;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Device.Model.Model.Config;
using BISC.Modules.FTP.Infrastructure.Serviсes;

namespace BISC.Modules.Device.Model.Services
{
   public class DeviceFtpConfigService: IDeviceFtpConfigService
    {
        private readonly IDeviceFileWritingServices _deviceFileWritingServices;

        public DeviceFtpConfigService(IDeviceFileWritingServices deviceFileWritingServices)
        {
            _deviceFileWritingServices = deviceFileWritingServices;
        }


        #region Implementation of IDeviceFtpConfigService

        public async Task<OperationResult<IDeviceFtpConfig>> ReadDeviceFtpConfig(string ip,string customPath=null)
        {
            try
            {
              var configAsStringRes=await  _deviceFileWritingServices.ReadFileStringFromDevice(ip, "1:/CFG", "CONFIG.CFG");
	            if (configAsStringRes.IsSucceed)
	            {
		            var configAsString = configAsStringRes.Item;

					Regex regex = new Regex(@"MAC=(.*)");
		            Match match = regex.Match(configAsString);


		            var macAddressString = Regex.Match(configAsString, @"MAC=(.*)").Groups[1].Value;
		            var techKeyString = Regex.Match(configAsString, @"KEY=(.*)").Groups[1].Value;
		            var switchModeString = Regex.Match(configAsString, @"ETHMODE=(.*)").Groups[1].Value;
		            var versionString = Regex.Match(configAsString, @"HVER=(.*)").Groups[1].Value;
		            var macsForFilterA = new List<string>();
		            var macsForFilterB = new List<string>();

		            if (configAsString.Contains("FILTERA") && configAsString.Contains("FILTERB"))
		            {
			            var strToSplit = configAsString.Substring(configAsString.IndexOf("FILTERA"));
			            var splitted = strToSplit.Split('\n');

			            var isAFilter = true;
			            foreach (var splittedPart in splitted)
			            {
				            var splittedPartPrepared = splittedPart.Replace("\r", "");
				            if (splittedPartPrepared.Contains("FILTERB"))
				            {
					            isAFilter = false;
					            continue;
				            }

				            if (splittedPartPrepared.Contains("FILTERA") || splittedPartPrepared.Length < 4)
				            {
					            continue;
				            }

				            if (isAFilter)
				            {
					            macsForFilterA.Add(ParseMacStringIntoReadableForm(splittedPartPrepared));
				            }
				            else
				            {
					            macsForFilterB.Add(ParseMacStringIntoReadableForm(splittedPartPrepared));
				            }
			            }
		            }

		            return new OperationResult<IDeviceFtpConfig>(new DeviceFtpConfig()
		            {
			            MacAddress = macAddressString.Replace("\r", ""),
			            FilterAMacList = macsForFilterA,
			            FilterBMacList = macsForFilterB,
			            SwitchMode = switchModeString.Replace("\r", ""),
			            TechKey = techKeyString.Replace("\r", ""),
			            Version = versionString.Replace("\r", "")
		            });
	            }
	            else
	            {
		            return new OperationResult<IDeviceFtpConfig>(configAsStringRes.GetFirstError());
	            }
            }
            catch (Exception e)
            {
               return new OperationResult<IDeviceFtpConfig>(e.Message+e.StackTrace);
            }
        }

	    public Task<OperationResult> SaveDeviceFtpConfig(string ip, IDeviceFtpConfig deviceFtpConfigToSave)
	    {
		    throw new NotImplementedException();
	    }

	    #endregion

        private string ParseMacStringIntoReadableForm(string macFromDevice)
        {
            var splittedParts = macFromDevice.Split('-');
            string result=string.Join("-", splittedParts.Select((s => s.Replace("0x", String.Empty))));
            return result;
        }
    }
}
