using BISC.Infrastructure.Global.Common;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Serializing;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.FTP.Infrastructure.Serviсes;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Model.Model;
using BISC.Modules.Gooses.Presentation.FileParsers;
using BISC.Modules.Gooses.Presentation.Interfaces;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix;
using BISC.Presentation.Infrastructure.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BISC.Modules.Gooses.Presentation.Commands
{
    public class GooseMatrixSavingCommand : ISavingCommand
    {
        private readonly IGoosesModelService _goosesModelService;
        private IDevice _device;
        private IBiscProject _biscProject;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly ResultFileParser _resultFileParser;
        private readonly IModelElementsRegistryService _modelElementsRegistryService;
        private readonly ISclCommunicationModelService _sclCommunicationModelService;
        private readonly IDeviceFileWritingServices _deviceFileWritingServices;
        private readonly ILoggingService _loggingService;
        private readonly IGooseMatrixFtpService _gooseMatrixFtpService;
        private ObservableCollection<GooseControlBlockViewModel> _gooseControlBlockViewModels;


        public GooseMatrixSavingCommand(IGoosesModelService goosesModelService, IBiscProject biscProject, IConnectionPoolService connectionPoolService,
            ResultFileParser resultFileParser, IModelElementsRegistryService modelElementsRegistryService, ISclCommunicationModelService sclCommunicationModelService,
            IDeviceFileWritingServices deviceFileWritingServices, ILoggingService loggingService, IGooseMatrixFtpService gooseMatrixFtpService)
        {
            _goosesModelService = goosesModelService;
            _biscProject = biscProject;
            _connectionPoolService = connectionPoolService;
            _resultFileParser = resultFileParser;
            _modelElementsRegistryService = modelElementsRegistryService;
            _sclCommunicationModelService = sclCommunicationModelService;
            _deviceFileWritingServices = deviceFileWritingServices;
            _loggingService = loggingService;
            _gooseMatrixFtpService = gooseMatrixFtpService;
        }
        #region Implementation of ISavingCommand

        public void Initialize(IDevice device, ObservableCollection<GooseControlBlockViewModel> gooseControlBlockViewModels)
        {
            _device = device;
            _gooseControlBlockViewModels = gooseControlBlockViewModels;
        }
        public async Task<OperationResult<SavingCommandResultEnum>> SaveAsync()
        {

            var gooseMatrixFtp = _gooseMatrixFtpService.GetGooseMatrixFtpForDevice(_device);

            var gooseControlBlocksSubscribed =
         _goosesModelService.GetGooseControlsSubscribed(_device, _biscProject.MainSclModel.Value);
            var gooseDeviceInputToSerialize = _biscProject.CustomElements.Value.ChildModelElements.FirstOrDefault(
                (element => (element is IGooseDeviceInput gooseDeviceInput) &&
                            gooseDeviceInput.DeviceOwnerName == _device.Name)) as IGooseDeviceInput;

            gooseMatrixFtp.GooseRowFtpEntities.Clear();
            gooseMatrixFtp.GooseRowQualityFtpEntities.Clear();

            var gocbList = gooseDeviceInputToSerialize.GooseInputModelInfoList.Where((info =>
                    _gooseControlBlockViewModels.Any((model =>
                            model.Name == info.EmittingGooseControl.Value.Name && model.GooseRowViewModels.Any(
                                (viewModel =>
                                    viewModel.SelectableValueViewModels.Any((valueViewModel =>
                                        valueViewModel.SelectedValue))))
                        ))))
                .Select((info => new GoCbFtpEntity()
                {
                    AppId = info.EmittingGse.Value.AppId,
                    GoCbReference =
                        info.EmittingDeviceName + "LD0/LLN0$GO$" +
                        info.EmittingGooseControl.Value.Name // MR771N125LD0/LLN0$GO$gcbIn
                })).ToList();
            gooseMatrixFtp.GoCbFtpEntities.Clear();
            gooseMatrixFtp.GoCbFtpEntities.AddRange(gocbList);
            gooseMatrixFtp.MacAddressList.AddRange(gooseDeviceInputToSerialize.GooseInputModelInfoList.Where((info => gocbList.Any((entity) => entity.AppId == info.EmittingGse.Value.AppId))).Select((info => new MacAddressEntity()
            {
                MacAddress = info.EmittingGse.Value.MacAddress
            })));

            foreach (var gooseControlBlockViewModel in _gooseControlBlockViewModels)
            {
                foreach (var gooseRowViewModel in gooseControlBlockViewModel.GooseRowViewModels)
                {
                    if (gooseRowViewModel.GooseRowType == "State")
                    {
                        foreach (var selectedValueInRow in gooseRowViewModel.SelectableValueViewModels.Where((model => model.SelectedValue)))
                        {
                            var gooseMatrixRow = new GooseRowFtpEntity
                            {
                                IndexOfGoose = gocbList.First((entity =>
                                    entity.GoCbReference.Split('$').Last() == gooseControlBlockViewModel.Name)).IndexOfGoose,
                                BitIndex = selectedValueInRow.ColumnNumber + 1,
                                NumberOfFcdaInDataSetOfGoose = gooseRowViewModel.NumberOfFcdaInDataSet + 1
                            };
                            gooseMatrixFtp.GooseRowFtpEntities.Add(gooseMatrixRow);
                        }
                    }
                    if (gooseRowViewModel.GooseRowType == "Quality")
                    {
                        foreach (var selectedValueInRow in gooseRowViewModel.SelectableValueViewModels.Where((model => model.SelectedValue)))
                        {
                            var gooseMatrixRow = new GooseRowQualityFtpEntity
                            {
                                IndexOfGoose = gocbList.First((entity =>
                                    entity.GoCbReference.Split('$').Last() == gooseControlBlockViewModel.Name)).IndexOfGoose,
                                BitIndex = selectedValueInRow.ColumnNumber + 1,
                                NumberOfFcdaInDataSetOfGoose = gooseRowViewModel.NumberOfFcdaInDataSet + 1
                            };
                            IGooseRowViewModel first = gooseControlBlockViewModel.GooseRowViewModels.Where((model => model.GooseRowType == "Validity")).FirstOrDefault();
                            if (first != null)
                            {
                                gooseMatrixRow.IsValiditySelected = first.SelectableValueViewModels
                                    .Any((model =>
                                        model.SelectedValue && model.ColumnNumber == selectedValueInRow.ColumnNumber));
                            }

                            gooseMatrixFtp.GooseRowQualityFtpEntities.Add(gooseMatrixRow);
                        }
                    }
                }
            }


            foreach (var gooseControlBlockSubscribed in gooseControlBlocksSubscribed)
            {
                GooseControlBlockViewModel gooseControlBlockViewModel =
                    _gooseControlBlockViewModels.FirstOrDefault(
                        (model => model.Name == gooseControlBlockSubscribed.Item2.AppId));
                if (gooseControlBlockViewModel == null)
                {
                    continue;
                }

                var input = _goosesModelService.GetGooseInputsOfDevice(_device).FirstOrDefault();
                if (input == null)
                {
                    continue;
                }

                input.ExternalGooseReferences.Clear();
                foreach (var gooseRowViewModel in gooseControlBlockViewModel.GooseRowViewModels)
                {
                    if (gooseRowViewModel.GooseRowType == "Validity")
                    {
                        continue;
                    }

                    IExternalGooseRef externalGooseRef = new ExternalGooseRef();
                    var fcda = gooseRowViewModel.RelatedDataSet.FcdaList[gooseRowViewModel.NumberOfFcdaInDataSet];
                    externalGooseRef.DaName = fcda.DaName;
                    externalGooseRef.DoName = fcda.DoName;
                    externalGooseRef.IedName =
                        gooseRowViewModel.RelatedDataSet.GetFirstParentOfType<IDevice>().Name;
                    externalGooseRef.LdInst = fcda.LdInst;
                    externalGooseRef.LnClass = fcda.LnClass;
                    externalGooseRef.LnInst = fcda.LnInst;
                    externalGooseRef.Prefix = fcda.Prefix;
                    input.ExternalGooseReferences.Add(externalGooseRef);
                }
            }

            _gooseMatrixFtpService.SetGooseMatrixFtpForDevice(_device, gooseMatrixFtp);


            if (_connectionPoolService.GetConnection(_device.Ip).IsConnected)
            {

                var gooseSerialized = _modelElementsRegistryService.SerializeModelElement(gooseDeviceInputToSerialize, SerializingType.Extended)
                    .ToString();
                var str = _resultFileParser.GetFileStringFromGooseModel(_device).ToString();
                if (_device.Ip == null)
                {
                    _device.Ip = _sclCommunicationModelService.GetIpOfDevice(_device.Name, _biscProject.MainSclModel.Value);
                }

                var res = await _deviceFileWritingServices.WriteFileStringInDevice(_device.Ip,
                    new List<string>() { str, gooseSerialized },
                    new List<string>() { "GOOSERE.CFG", "GOOSEINPUT.ZIP" });
                if (res.IsSucceed)
                {
                    _loggingService.LogMessage("Goose матрица успешно записана в устройство", SeverityEnum.Info);
                }
                else
                {
                    _loggingService.LogMessage($"Запись Goose матрицы прошла с ошибками: {res.GetFirstError()}", SeverityEnum.Info);
                }
            }


            return new OperationResult<SavingCommandResultEnum>("goven");
        }


        public async Task<bool> IsSavingByFtpNeeded()
        {
            return false;
        }

        public async Task<OperationResult> ValidateBeforeSave()
        {
            return OperationResult.SucceedResult;
        }

        #endregion
    }
}
