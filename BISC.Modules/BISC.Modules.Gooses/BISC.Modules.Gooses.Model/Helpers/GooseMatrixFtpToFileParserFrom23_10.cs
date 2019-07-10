using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using System.IO;
using System.Text;
using BISC.Modules.Gooses.Infrastructure.Helpers;
using System.Globalization;

namespace BISC.Modules.Gooses.Model.Helpers
{
    internal class GooseMatrixFtpToFileParser23_10 : IGooseMatrixFtpToFileParser
    {

        public string GetFileStringFromMatrixModel(IGooseMatrixFtp matrixFtp)
        {
            StringBuilder sb = new StringBuilder();
            TextWriter streamWriter = new StringWriter(sb);
            Write(streamWriter, matrixFtp);
            return sb.ToString();
        }

        private void Write(TextWriter streamWriter, IGooseMatrixFtp gooseMatrix)
        {
            using (streamWriter)
            {
                //streamWriter.WriteLine("# MAC адреса гусов для приёма и фильтрации если нужно макс 8шт.");
                streamWriter.WriteLine("MAC{");
                foreach (var macAddressEntity in gooseMatrix.MacAddressList)
                {
                    if (macAddressEntity.MacAddress != null)
                    {
                        streamWriter.WriteLine(macAddressEntity.MacAddress);
                    }
                }
                streamWriter.WriteLine("}");
                //  streamWriter.WriteLine("# gocbref{[номер]: LD/LN$FC$goID,AppID} {1: MR771N127LD0/LLN0$GO$gcbIn}");
                streamWriter.WriteLine("GocbRef{");
                foreach (var goCbFtpEntity in gooseMatrix.GoCbFtpEntities)
                {
                    var confRev = goCbFtpEntity.ConfRev ?? 0;
                    // parse appId from hex to uint
                    //string decAppId = uint.Parse(goCbFtpEntity.AppId, NumberStyles.HexNumber).ToString("D");
                    streamWriter.WriteLine($"{goCbFtpEntity.IndexOfGoose}:{goCbFtpEntity.GoCbReference},{goCbFtpEntity.AppId},{confRev}");

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
                    streamWriter.WriteLine($"{gooseRowQuality.IndexOfGoose},{gooseRowQuality.NumberOfFcdaInDataSetOfGoose},{gooseRowQuality.BitIndex + 64},{validityInt}");
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