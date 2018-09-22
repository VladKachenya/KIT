using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix;

namespace BISC.Modules.Gooses.Presentation.FileParsers
{
    //public class ResultFileParser
    //{
    //    private readonly ISclCommunicationModelService _sclCommunicationModelService;
    //    private readonly IBiscProject _biscProject;


    //    public ResultFileParser(ISclCommunicationModelService sclCommunicationModelService, IBiscProject biscProject)
    //    {
    //        _sclCommunicationModelService = sclCommunicationModelService;
    //        _biscProject = biscProject;
    //    }

    //    public string GetFileStringFromGooseModel(ObservableCollection<GooseControlBlockViewModel> gooseControlBlockViewModels)
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        TextWriter streamWriter = new StringWriter(sb);
    //        Write(gooseControlBlockViewModels, streamWriter);
    //        return sb.ToString();
    //    }


    //    private void Write(ObservableCollection<GooseControlBlockViewModel> gooseControlBlockViewModels, TextWriter streamWriter, string deviceName)
    //    {
    //        var gses = _sclCommunicationModelService.GetGsesForDevice(deviceName, _biscProject.MainSclModel);


    //        using (streamWriter)
    //        {
    //            //streamWriter.WriteLine("# MAC адреса гусов для приёма и фильтрации если нужно макс 8шт.");
    //            streamWriter.WriteLine("MAC{");
    //            foreach (var gooseControlBlockViewModel in gooseControlBlockViewModels)
    //            {

    //                var mac = gses.FirstOrDefault((gse => gse.CbName == gooseControlBlockViewModel.Name))?.MacAddress;
    //                if(mac!=null)
    //                    streamWriter.WriteLine(mac);

    //            }
    //            streamWriter.WriteLine("}");
    //            //  streamWriter.WriteLine("# gocbref{[номер]: LD/LN$FC$goID,AppID} {1: MR771N127LD0/LLN0$GO$gcbIn}");
    //            streamWriter.WriteLine("gocbRef{");
    //            foreach (var gooseControlBlockViewModel in gooseControlBlockViewModels)
    //            {
                   
    //                    streamWriter.WriteLine($"{gooseControlBlockViewModels.IndexOf(gooseControlBlockViewModel) + 1}:{gooseControlBlockViewModel.GoCbReference}");
                    
    //            }
    //            streamWriter.WriteLine("}");

    //            // streamWriter.WriteLine("# config{ [номер гуса], [номер записи в датасете гуса], [номер бита в базе прибора], [подмешивать валидность]}");

    //            bool isConfigHasAnyRows = false;
    //            streamWriter.WriteLine("config{");

    //            foreach (var gooseControlBlock in gooseMatrixModel.GooseControlBlocks)
    //            {
    //                foreach (var gooseRow in gooseControlBlock.GooseRows)
    //                {
    //                    for (int i = 0; i < gooseRow.ValueList.Count; i++)
    //                    {
    //                        if (gooseRow.ValueList[i])
    //                        {
    //                            if (gooseRow is StateGooseRow)
    //                            {
    //                                isConfigHasAnyRows = true;
    //                                streamWriter.WriteLine(
    //                                    $"{gooseMatrixModel.GooseControlBlocks.IndexOf(gooseControlBlock) + 1},{(gooseRow as StateGooseRow).NumberOfFcdaInDataSetOfGoose + 1},{i + 1},{0}");

    //                            }
    //                            if (gooseRow is QualityGooseRow)
    //                            {
    //                                isConfigHasAnyRows = true;
    //                                int validityInt = gooseControlBlock.GooseRows
    //                                    .First((row => row is ValidityGooseRow)).ValueList[i]
    //                                    ? 1
    //                                    : 0;
    //                                streamWriter.WriteLine(
    //                                    $"{gooseMatrixModel.GooseControlBlocks.IndexOf(gooseControlBlock) + 1},{(gooseRow as QualityGooseRow).NumberOfFcdaInDataSetOfGoose + 1},{i + gooseMatrixModel.GoInCount + 1},{validityInt}");

    //                            }
    //                        }
    //                    }

    //                }



    //            }


    //            streamWriter.WriteLine("}");
    //            if (!isConfigHasAnyRows)
    //            {
    //                streamWriter.Flush();
    //            }

    //        }








    //    }
    //}
}
