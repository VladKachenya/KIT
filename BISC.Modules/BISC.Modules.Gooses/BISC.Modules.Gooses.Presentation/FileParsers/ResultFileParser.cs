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


        public ResultFileParser(ISclCommunicationModelService sclCommunicationModelService, IBiscProject biscProject,IGoosesModelService goosesModelService)
        {
            _sclCommunicationModelService = sclCommunicationModelService;
            _biscProject = biscProject;
            _goosesModelService = goosesModelService;
        }

        public string GetFileStringFromGooseModel(ObservableCollection<GooseControlBlockViewModel> gooseControlBlockViewModels,IDevice device)
        {
            StringBuilder sb = new StringBuilder();
            TextWriter streamWriter = new StringWriter(sb);
            Write(gooseControlBlockViewModels, streamWriter,device);
            return sb.ToString();
        }


        private void Write(ObservableCollection<GooseControlBlockViewModel> gooseControlBlockViewModels, TextWriter streamWriter,IDevice device)
        {
            var goosesForDevice = _goosesModelService.GetGooseControlsSubscribed(device,_biscProject.MainSclModel);

            using (streamWriter)
            {
                //streamWriter.WriteLine("# MAC адреса гусов для приёма и фильтрации если нужно макс 8шт.");
                streamWriter.WriteLine("MAC{");
                foreach (var gooseControlBlockViewModel in gooseControlBlockViewModels)
                {
                    var deviceForGoose =
                        goosesForDevice.First((tuple => tuple.Item2.AppId == gooseControlBlockViewModel.AppId)).Item1;
                    var gses = _sclCommunicationModelService.GetGsesForDevice(deviceForGoose.Name, _biscProject.MainSclModel);
                 
                    var mac = gses.FirstOrDefault((gse => gse.CbName == gooseControlBlockViewModel.Name))?.MacAddress;
                    if (mac != null)
                        streamWriter.WriteLine(mac);

                }
                streamWriter.WriteLine("}");
                //  streamWriter.WriteLine("# gocbref{[номер]: LD/LN$FC$goID,AppID} {1: MR771N127LD0/LLN0$GO$gcbIn}");
                streamWriter.WriteLine("gocbRef{");
                foreach (var gooseControlBlockViewModel in gooseControlBlockViewModels)
                {

                    streamWriter.WriteLine($"{gooseControlBlockViewModels.IndexOf(gooseControlBlockViewModel) + 1}:{gooseControlBlockViewModel.GoCbReference}");

                }
                streamWriter.WriteLine("}");

                // streamWriter.WriteLine("# config{ [номер гуса], [номер записи в датасете гуса], [номер бита в базе прибора], [подмешивать валидность]}");

                bool isConfigHasAnyRows = false;
                streamWriter.WriteLine("config{");

                foreach (var gooseControlBlock in gooseControlBlockViewModels)
                {
                    foreach (var gooseRow in gooseControlBlock.GooseRowViewModels)
                    {
                        for (int i = 0; i < gooseRow.SelectableValueViewModels.Count; i++)
                        {
                            if (gooseRow.SelectableValueViewModels[i].SelectedValue)
                            {
                                if (gooseRow.Model.GooseRowType=="State")
                                {
                                    isConfigHasAnyRows = true;
                                    streamWriter.WriteLine(
                                        $"{gooseControlBlockViewModels.IndexOf(gooseControlBlock) + 1},{(gooseRow.Model).NumberOfFcdaInDataSetOfGoose + 1},{i + 1},{0}");

                                }
                                if (gooseRow.Model.GooseRowType=="Quality")
                                {
                                    isConfigHasAnyRows = true;
                                    int validityInt = gooseControlBlock.GooseRowViewModels
                                        .First((row => row.Model.GooseRowType=="Validity")).SelectableValueViewModels[i].SelectedValue
                                        ? 1
                                        : 0;
                                    streamWriter.WriteLine(
                                        $"{gooseControlBlockViewModels.IndexOf(gooseControlBlock) + 1},{(gooseRow.Model).NumberOfFcdaInDataSetOfGoose + 1},{i + 64 + 1},{validityInt}");

                                }
                            }
                        }

                    }



                }


                streamWriter.WriteLine("}");
                if (!isConfigHasAnyRows)
                {
                    streamWriter.Flush();
                }

            }








        }
    }
}
