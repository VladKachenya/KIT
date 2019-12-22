using BISC.Infrastructure.Global.Common;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Model.Model;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix;
using BISC.Presentation.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BISC.Modules.Gooses.Presentation.Interfaces.GooseSubscriptionLight;

namespace BISC.Modules.Gooses.Presentation.Commands
{
    public class GooseSubscriptionMatrixSavingCommand : ISavingCommand
    {
        private readonly IGooseSavingService _gooseSavingService;
        private readonly IDeviceWarningsService _deviceWarningsService;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly ILoggingService _loggingService;
        private IDevice _device;
        private List<GooseControlBlockViewModel> _gooseControlBlockViewModels;
        private List<IGoInViewModel> _goInViewModels;

        public GooseSubscriptionMatrixSavingCommand(IGooseSavingService gooseSavingService,
            IDeviceWarningsService deviceWarningsService, IConnectionPoolService connectionPoolService,
            ILoggingService loggingService)
        {
            _gooseSavingService = gooseSavingService;
            _deviceWarningsService = deviceWarningsService;
            _connectionPoolService = connectionPoolService;
            _loggingService = loggingService;
        }


        public void Initialize(IDevice device,
            List<GooseControlBlockViewModel> gooseControlBlockViewModels, List<IGoInViewModel> goInViewModels = null)
        {
            _device = device;
            _goInViewModels = goInViewModels;
            _gooseControlBlockViewModels = gooseControlBlockViewModels;
        }

        public async Task<OperationResult<SavingCommandResultEnum>> SaveAsync()
        {
            try
            {
                if (_goInViewModels != null)
                {
                    UpdateGooseMatrix();
                }
                List<Tuple<IGoCbFtpEntity, IGooseRowFtpEntity>>
                    entitysForSaving = new List<Tuple<IGoCbFtpEntity, IGooseRowFtpEntity>>();
                // Перебираем гусы
                foreach (var gooseControlBlockViewModel in _gooseControlBlockViewModels)
                {
                    var gooseSdRef = gooseControlBlockViewModel.GoCbReference;
                    // Перебираем строчки
                    foreach (var row in gooseControlBlockViewModel.GooseRowViewModels)
                    {
                        var value = row.SelectableValueViewModels.FirstOrDefault(el => el.SelectedValue);

                        if (value != null)
                        {
                            IGooseRowFtpEntity entityForSaving;
                            if (row.GooseRowType == GooseKeys.GooseSubscriptionPresentationKeys.State)
                            {
                                // Формируем state
                                entityForSaving = new GooseRowFtpEntity()
                                {
                                    NumberOfFcdaInDataSetOfGoose = row.NumberOfFcdaInDataSet + 1,
                                    BitIndex = value.ColumnNumber + 1
                                };
                            }
                            else if (row.GooseRowType == GooseKeys.GooseSubscriptionPresentationKeys.Quality)
                            {
                                // Формируем quality
                                entityForSaving = new GooseRowQualityFtpEntity()
                                {
                                    NumberOfFcdaInDataSetOfGoose = row.NumberOfFcdaInDataSet + 1,
                                    BitIndex = value.ColumnNumber + 1,
                                    // Устанавливаем валидити
                                    IsValiditySelected = gooseControlBlockViewModel.GooseRowViewModels.First(el =>
                                            el.GooseRowType == GooseKeys.GooseSubscriptionPresentationKeys.Validity)
                                        .SelectableValueViewModels[value.ColumnNumber].SelectedValue
                                };
                            }
                            else
                            {
                                continue;
                            }

                            entitysForSaving.Add(new Tuple<IGoCbFtpEntity, IGooseRowFtpEntity>(gooseSdRef, entityForSaving));
                        }
                    }
                }

                await _gooseSavingService.SaveSubscriptionMatrix(_device, entitysForSaving);
            }
            catch (Exception e)
            {
                _loggingService.LogException(e);
                return new OperationResult<SavingCommandResultEnum>(SavingCommandResultEnum.SavedWithErrors);
            }
            if (_connectionPoolService.GetConnection(_device.Ip).IsConnected)
            {
                _deviceWarningsService.SetWarningOfDevice(_device.DeviceGuid,
                    GooseKeys.GooseWarningKeys.GooseSubscriptionUnsavedWarningTagKey,
                    "GooseSubscriptions не соответствуют устройству");
            }
            _loggingService.LogMessage(
                $"Сохранение в проект подписок {_device.Name} на Goose произошло успешно",
                SeverityEnum.Info);
            RefreshViewModel?.Invoke();
            return new OperationResult<SavingCommandResultEnum>(SavingCommandResultEnum.SavedOk);
        }

        public async Task<OperationResult> ValidateBeforeSave()
        {
            return OperationResult.SucceedResult;
        }

        public Action RefreshViewModel { get; set; }

        private void UpdateGooseMatrix()
        {
            if(_goInViewModels == null) return;
            foreach (var gooseControlBlockViewModel in _gooseControlBlockViewModels)
            {
                foreach (var gooseRowViewModel in gooseControlBlockViewModel.GooseRowViewModels)
                {
                    if (gooseRowViewModel.SelectableValueViewModels != null)
                    {
                        foreach (var selectableValueViewModel in gooseRowViewModel.SelectableValueViewModels)
                        {
                            selectableValueViewModel.SetValue(false);
                        }
                    }
                }
            }

            foreach (var goInViewModel in _goInViewModels)
            {
                if(!goInViewModel.EnableState) continue;
                var gooseControlBlockViewModel = _gooseControlBlockViewModels.First(gcb =>
                    gcb.DeviceName == goInViewModel.GooseDataReferenceViewModel.DeviceName);
                gooseControlBlockViewModel.GooseRowViewModels
                    .First(gr => gr.RowName == goInViewModel.GooseDataReferenceViewModel.DataSetReferenceState)
                    .SelectableValueViewModels[goInViewModel.Number].SetValue(true);
                if(!goInViewModel.EnableQuality) continue;
                gooseControlBlockViewModel.GooseRowViewModels
                    .First(gr => gr.RowName == goInViewModel.GooseDataReferenceViewModel.DataSetReferenceQuality)
                    .SelectableValueViewModels[goInViewModel.Number].SetValue(true);
                if (!goInViewModel.EnableGooseMonitoring) continue;
                gooseControlBlockViewModel.GooseRowViewModels
                    .First(gr => gr.GooseRowType == GooseKeys.GooseSubscriptionPresentationKeys.Validity)
                    .SelectableValueViewModels[goInViewModel.Number].SetValue(true);
            }
        }
    }
}