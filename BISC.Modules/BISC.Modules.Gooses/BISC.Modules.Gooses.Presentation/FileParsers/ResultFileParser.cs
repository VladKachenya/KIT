using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix;

namespace BISC.Modules.Gooses.Presentation.FileParsers
{
    public class ResultFileParser
    {
        private readonly ISclCommunicationModelService _sclCommunicationModelService;
        private readonly IBiscProject _biscProject;
        private readonly IGoosesModelService _goosesModelService;
        private readonly IGooseMatrixFtpService _gooseMatrixFtpService;


        public ResultFileParser(ISclCommunicationModelService sclCommunicationModelService, IBiscProject biscProject,
            IGoosesModelService goosesModelService, IGooseMatrixFtpService gooseMatrixFtpService)
        {
            _sclCommunicationModelService = sclCommunicationModelService;
            _biscProject = biscProject;
            _goosesModelService = goosesModelService;
            _gooseMatrixFtpService = gooseMatrixFtpService;
        }

        public string GetFileStringFromGooseModel(IDevice device)
        {
            StringBuilder sb = new StringBuilder();
            TextWriter streamWriter = new StringWriter(sb);
            Write(streamWriter,device);
            return sb.ToString();
        }


        private void Write(TextWriter streamWriter,IDevice device)
        {
            var gooseMatrix = _gooseMatrixFtpService.GetGooseMatrixFtpForDevice(device);

            using (streamWriter)
            {
                //streamWriter.WriteLine("# MAC адреса гусов для приёма и фильтрации если нужно макс 8шт.");
                streamWriter.WriteLine("MAC{");
                foreach (var macAddressEntity in gooseMatrix.MacAddressList)
                {
                    if (macAddressEntity.MacAddress != null)
                        streamWriter.WriteLine(macAddressEntity.MacAddress);
                }
                streamWriter.WriteLine("}");
                //  streamWriter.WriteLine("# gocbref{[номер]: LD/LN$FC$goID,AppID} {1: MR771N127LD0/LLN0$GO$gcbIn}");
                streamWriter.WriteLine("gocbRef{");
                foreach (var goCbFtpEntity in gooseMatrix.GoCbFtpEntities)
                {
                 
                    streamWriter.WriteLine($"{goCbFtpEntity.IndexOfGoose}:{goCbFtpEntity.GoCbReference},{goCbFtpEntity.AppId}");

                }
                streamWriter.WriteLine("}");

                // streamWriter.WriteLine("# config{ [номер гуса], [номер записи в датасете гуса], [номер бита в базе прибора], [подмешивать валидность]}");

             //   bool isConfigHasAnyRows = false;
                streamWriter.WriteLine("config{");

                foreach (var gooseRowFtpEntity in gooseMatrix.GooseRowFtpEntities)
                {
                    streamWriter.WriteLine($"{gooseRowFtpEntity.IndexOfGoose},{gooseRowFtpEntity.NumberOfFcdaInDataSetOfGoose},{gooseRowFtpEntity.BitIndex},0");
                }
                foreach (var gooseRowQuality in gooseMatrix.GooseRowQualityFtpEntities)
                {
                    var validityInt = gooseRowQuality.IsValiditySelected ? 1 : 0;
                    streamWriter.WriteLine($"{gooseRowQuality.IndexOfGoose},{gooseRowQuality.NumberOfFcdaInDataSetOfGoose},{gooseRowQuality.BitIndex+64},{validityInt}");
                }

                streamWriter.WriteLine("}");
                //if (!isConfigHasAnyRows)
                //{
                //    streamWriter.Flush();
                //}

            }
        }
    }
}
