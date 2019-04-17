using BISC.Infrastructure.Global.Common;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix.Entities;
using BISC.Modules.Gooses.Presentation.ViewModels.Subscriptions;
using BISC.Presentation.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BISC.Modules.Gooses.Presentation.Commands
{
    public class GooseSubscriptionSavingCommand : ISavingCommand
    {
        private readonly IDeviceModelService _deviceModelService;
        private readonly IBiscProject _biscProject;
        private readonly IGooseSavingService _gooseSavingService;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly ILoggingService _loggingService;
        private readonly IFtpGooseModelService _ftpGooseModelService;
        private readonly IDeviceWarningsService _deviceWarningsService;
        private DataTable _gooseSubscriptionTable;
        private List<GooseDescriptionEntity> _gooseDescriptionEntities;

        public GooseSubscriptionSavingCommand(IDeviceModelService deviceModelService, IBiscProject biscProject, IGooseSavingService gooseSavingService,
            IConnectionPoolService connectionPoolService, ILoggingService loggingService,
            IFtpGooseModelService ftpGooseModelService, IDeviceWarningsService deviceWarningsService)
        {
            _deviceModelService = deviceModelService;
            _biscProject = biscProject;
            _gooseSavingService = gooseSavingService;
            _connectionPoolService = connectionPoolService;
            _loggingService = loggingService;
            _ftpGooseModelService = ftpGooseModelService;
            _deviceWarningsService = deviceWarningsService;
        }

        public void Initialize(DataTable gooseSubscriptionTable)
        {
            _gooseSubscriptionTable = gooseSubscriptionTable;
        }

        #region Implementation of ISavingCommand


        public async Task<OperationResult<SavingCommandResultEnum>> SaveAsync()
        {
            var table = _gooseSubscriptionTable.DefaultView.ToTable();
            for (int colimnIndex = 1; colimnIndex < table.Columns.Count; colimnIndex++)
            {
                try
                {

                    var deviceId = table.Columns[colimnIndex].Caption;
                    var device = _deviceModelService.GetDeviceByGuid(_biscProject.MainSclModel.Value, Guid.Parse(deviceId));
                    List<IGooseInputModelInfo> takenGooses = new List<IGooseInputModelInfo>();
                    for (int rowIndex = 0; rowIndex < table.Rows.Count; rowIndex++)
                    {
                        var rowValuew = table.Rows[rowIndex].ItemArray[colimnIndex] as SubscriptionValue;

                        if (rowValuew.IsValueEditable && rowValuew.IsSelected.HasValue && (bool)rowValuew.IsSelected)
                        {
                            var helperEntity = table.Rows[rowIndex][0] as GooseDescriptionEntity;
                            var typle = helperEntity.GooseInputModelInfo;
                            takenGooses.Add(typle);
                        }
                    }

                    await _gooseSavingService.SaveSubscriptionGooces(device, takenGooses);
                    //Тут проверяется были ли изменения подписок. 
                    if (_connectionPoolService.GetConnection(device.Ip).IsConnected)
                    {
                        if (await GetIsСhanged(device, takenGooses))
                        {
                            _loggingService.LogMessage(
                                $"Сохранение в проект подписок {device.Name} на Goose произошло успешно",
                                SeverityEnum.Info);
                            _deviceWarningsService.SetWarningOfDevice(device.DeviceGuid,
                                GooseKeys.GooseWarningKeys.GooseSubscriptionUnsavedWarningTagKey,
                                "GooseSubscriptions не соответствуют устройству");
                        }
                    }
                }
                catch (Exception e)
                {
                    _loggingService.LogException(e);
                }
            }

            RefreshViewModel?.Invoke();
            return new OperationResult<SavingCommandResultEnum>(SavingCommandResultEnum.SavedOk);
        }

        public Task<OperationResult> ValidateBeforeSave()
        {
            return Task.FromResult(OperationResult.SucceedResult);
        }

        public Action RefreshViewModel { get; set; }

        #endregion

        #region private methods

        private async Task<bool> GetIsСhanged(IDevice device, List<IGooseInputModelInfo> gooseInputModelInfosInProgect)
        {
            var writingResult =
               await _ftpGooseModelService.GetGooseDeviceInputFromDevice(device.Ip, device.Name);
            if (!writingResult.IsSucceed)
            {
                _loggingService.LogMessage($"Ошибка вычитывания подписко на Goose устройства {device.Name}", SeverityEnum.Critical);
                return true;
            }
            var gooseInputModelInfosInDevice = writingResult.Item;
            if (gooseInputModelInfosInDevice.Count != gooseInputModelInfosInProgect.Count)
            {
                return true;
            }
            foreach (var gooseInputModelInfoInProgect in gooseInputModelInfosInProgect)
            {
                if (!gooseInputModelInfosInDevice.Any(el => el.ModelElementCompareTo(gooseInputModelInfoInProgect)))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
