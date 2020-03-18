using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Events;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Loading;
using BISC.Modules.Device.Infrastructure.Loading.Events;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Device.Presentation.Interfaces.Services;
using BISC.Modules.Device.Presentation.Services.Helpers;
using BISC.Modules.FTP.Infrastructure.Serviсes;
using BISC.Presentation.Infrastructure.Events;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Common;
using BISC.Modules.Gooses.Infrastructure.Services;

namespace BISC.Modules.Device.Presentation.Services
{
    public class DeviceReconnectionService : IDeviceReconnectionService
    {
        private readonly IDeviceFileWritingServices _deviceFileWritingServices;
        private readonly IDeviceConnectionService _deviceConnectionService;
        private readonly IDeviceModelService _deviceModelService;
        private readonly ITreeManagementService _treeManagementService;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly ITabManagementService _tabManagementService;
        private readonly ILoggingService _loggingService;
        private readonly IGlobalEventsService _globalEventsService;
        private List<IDeviceElementLoadingService> _elementLoadingServices;
        private List<IElementConflictResolver> _elementConflictResolvers;

        private Func<ISclModel> _sclModelCreator;
        private readonly IBiscProject _biscProject;
        private readonly IDeviceAddingService _deviceAddingService;
        private readonly IDeviceWarningsService _deviceWarningsService;
        private readonly ISclCommunicationModelService _sclCommunicationModelService;
        private readonly IPingService _pingService;
        private readonly IUserInteractionService _userInteractionService;
        private readonly IGooseModelServicesFacade _gooseModelServicesFacade;


        public DeviceReconnectionService(
            IDeviceFileWritingServices deviceFileWritingServices,
            IDeviceConnectionService deviceConnectionService,
            IDeviceModelService deviceModelService,
            ITreeManagementService treeManagementService,
            IConnectionPoolService connectionPoolService,
            ITabManagementService tabManagementService,
            ILoggingService loggingService,
            IGlobalEventsService globalEventsService,
            IInjectionContainer injectionContainer,
            Func<ISclModel> sclModelCreator,
            IBiscProject biscProject,
            IDeviceAddingService deviceAddingService,
            IDeviceWarningsService deviceWarningsService,
            ISclCommunicationModelService sclCommunicationModelService,
            IPingService pingService,
            IUserInteractionService userInteractionService,
            IGooseModelServicesFacade gooseModelServicesFacade)
        {
            _deviceFileWritingServices = deviceFileWritingServices;
            _deviceConnectionService = deviceConnectionService;
            _deviceModelService = deviceModelService;
            _treeManagementService = treeManagementService;
            _connectionPoolService = connectionPoolService;
            _tabManagementService = tabManagementService;
            _loggingService = loggingService;
            _globalEventsService = globalEventsService;
            _sclModelCreator = sclModelCreator;
            _biscProject = biscProject;
            _deviceAddingService = deviceAddingService;
            _deviceWarningsService = deviceWarningsService;
            _sclCommunicationModelService = sclCommunicationModelService;
            _pingService = pingService;
            _userInteractionService = userInteractionService;
            _gooseModelServicesFacade = gooseModelServicesFacade;

            _elementLoadingServices = injectionContainer.ResolveAll(typeof(IDeviceElementLoadingService))
                .Cast<IDeviceElementLoadingService>().ToList();
            _elementConflictResolvers = injectionContainer.ResolveAll(typeof(IElementConflictResolver))
                .Cast<IElementConflictResolver>().ToList();
        }

        #region Implementation of IDeviceRestartService
        public async Task<bool> ReconnectDevice(IDevice existingDevice, UiEntityIdentifier uiEntityIdToRemove)
        {
            if (!await _pingService.GetPing(
                _sclCommunicationModelService.GetIpOfDevice(existingDevice.Name, _biscProject.MainSclModel.Value)))
            {
                _loggingService.LogMessage($"Устройство {existingDevice.Name} с IP {existingDevice.Ip} не отвечает", SeverityEnum.Critical);
                return false;
            }

            await Reconnect(existingDevice, uiEntityIdToRemove, false);
            return true;
        }

        public async Task RebootOnly(IDevice existingDevice)
        {
            await _deviceFileWritingServices.ResetDevice(existingDevice.Ip);
            _connectionPoolService.GetConnection(existingDevice.Ip).StopConnection();
            _loggingService.LogMessage($"Перезагрузка устройства {existingDevice.Name}", SeverityEnum.Critical);
        }

        public async Task RestartDevice(IDevice existingDevice, UiEntityIdentifier uiEntityIdToRemove = null)
        {
            await RebootOnly(existingDevice);
            if (uiEntityIdToRemove == null)
            {
                var treeItem = _treeManagementService.GetDeviceTreeItem(existingDevice.DeviceGuid);
                if (treeItem == null)
                {
                    _loggingService.LogMessage($"Ошибка при подключении к устройству: {existingDevice.Name}", SeverityEnum.Critical);
                    return;
                }
                await Reconnect(existingDevice, treeItem, true);

            }
            else
            {
                await Reconnect(existingDevice, uiEntityIdToRemove, true);
            }
        }
        #endregion

        private async Task Reconnect(IDevice existingDevice, UiEntityIdentifier uiEntityIdToRemove, bool isRestarting)
        {
            _globalEventsService.SendMessage(new ShellBlockEvent() { IsBlocked = true, Message = $"Подготовка к перезапуску{existingDevice.Name}" });

            var index = _treeManagementService.GetTreeItemIndex(uiEntityIdToRemove);
            try
            {
                _treeManagementService.DeleteTreeItem(uiEntityIdToRemove);
                _tabManagementService.CloseTabWithChildren(uiEntityIdToRemove.ItemId.ToString());
            }
            finally
            {
                _globalEventsService.SendMessage(new ShellBlockEvent() { IsBlocked = false });
            }

            var sortedElements = _elementLoadingServices.OrderBy((service => service.Priority));
            CancellationTokenSource cts = new CancellationTokenSource();
            BiscNavigationParameters biscNavigationParameters = new BiscNavigationParameters();
            RestartDeviceContext restartDeviceContext =
                new RestartDeviceContext(existingDevice, cts);
            biscNavigationParameters.AddParameterByName(DeviceKeys.RestartDeviceContextKey, restartDeviceContext);


            var treeItemId = _treeManagementService.AddTreeItem(biscNavigationParameters,
                DeviceKeys.DeviceRestartViewKey,
                null, null, index);
            restartDeviceContext.UiEntityIdentifier = treeItemId;
            if (isRestarting)
            {
                await Task.Delay(4000, cts.Token);
            }

            IDevice device = null;
            var sclModel = _sclModelCreator();
            int itemsCount = 0;
            try
            {
                existingDevice.Ip =
                    _sclCommunicationModelService.GetIpOfDevice(existingDevice.Name, _biscProject.MainSclModel.Value);
                var deviceConnectResult = await _deviceConnectionService.ConnectDevice(existingDevice.Ip, 2);
                if (!deviceConnectResult.IsSucceed)
                {
                    _loggingService.LogMessage($"IED Устройство по адресу {existingDevice.Ip} не обнаружено",
                        SeverityEnum.Warning);
                    await CancellationLoading(treeItemId, existingDevice);
                }
                device = deviceConnectResult.Item;
                device.SetGuid(existingDevice.DeviceGuid);

                foreach (var sortedElement in sortedElements)
                {
                    itemsCount += await sortedElement.EstimateProgress(device);
                }

                if (existingDevice.Name != device.Name)
                {
                    await ShowMissing($"Несоответствие имён {existingDevice.Name} и {device.Name}");
                    await CancellationLoading(treeItemId, existingDevice);
                    return;
                }

                if (existingDevice.RevisionDetails.CompareVersionTo(device.RevisionDetails) > 0)
                {
                    await ShowMissing("Несоответствие версий:\n" +
                                      $"Версия устройства в проекте: {existingDevice.Revision}\n" +
                                      $"Версия устройства в сети: {device.Revision}");
                    await CancellationLoading(treeItemId, existingDevice);
                    return;
                }

                _deviceModelService.AddDeviceInModel(sclModel, device);

                int currentElementsCount = 0;


                foreach (var sortedElement in sortedElements)
                {
                    await sortedElement.Load(device,
                        new Progress<object>(deviceLoadingEvent =>
                        {
                            _globalEventsService.SendMessage(new DeviceLoadingEvent(device.DeviceGuid, device.Name,
                                itemsCount, ++currentElementsCount));
                        }), sclModel, cts.Token);
                }
                _loggingService.LogMessage($"Устройство {device.Name} c IP {device.Ip} переподключено!", SeverityEnum.Info);
            }
            catch (Exception e)
            {
                if (device?.Ip != null)
                {
                    _connectionPoolService.GetConnection(device.Ip).StopConnection();
                }
                if (cts.IsCancellationRequested)
                {
                    _loggingService.LogUserAction($"Загрузка устройства отменена пользователем {device.Name}");
                }
                else
                {
                    _loggingService.LogMessage(
                        $"Ошибка загрузки устройства {e.Message + Environment.NewLine + e.StackTrace}",
                        SeverityEnum.Critical);
                    _globalEventsService.SendMessage(new LoadErrorEvent(existingDevice.Ip, existingDevice.DeviceGuid));
                }
                return;
            }

            var hasConflics = false;
            _elementConflictResolvers.ForEach((resolver =>
            {
                if (resolver.GetIfConflictsExists(device.DeviceGuid, sclModel, _biscProject.MainSclModel.Value))
                {
                    hasConflics = true;

                    return;
                }
            }));
            restartDeviceContext.HaveConflicts = hasConflics;
            _globalEventsService.SendMessage(new DeviceLoadingEvent(device.DeviceGuid) { IsFinished = true });
            if (!hasConflics)
            {
                var index2 = _treeManagementService.GetTreeItemIndex(treeItemId);
                _treeManagementService.DeleteTreeItem(treeItemId);
                _deviceModelService.DeleteDeviceFromModel(existingDevice.GetFirstParentOfType<ISclModel>(),
                    existingDevice.DeviceGuid);
                _deviceAddingService.AddDevicesInProject(new List<IDevice>() { device }, device.GetFirstParentOfType<ISclModel>(), treeItemIndex: index2);
                //Adding Goose subscription to device
                var biscProject = device.GetFirstParentOfType<IBiscProject>();
                if (biscProject != null)
                {
                    _gooseModelServicesFacade.SetGooseReceiving(device, _biscProject, biscProject);
                }
                _deviceWarningsService.ClearDeviceWarningsOfDevice(device.DeviceGuid);
            }
            else
            {
                restartDeviceContext.DeviceConflictContext = new DeviceConflictContext(_biscProject.MainSclModel.Value,
                    sclModel, existingDevice.DeviceGuid);
            }
        }


        private async Task ShowMissing(string miissingMessage)
        {
            await _userInteractionService.ShowOptionToUser("Не соответствие модели устройства", miissingMessage,
                new List<string>() { "Ок" });
        }

        private async Task CancellationLoading(UiEntityIdentifier treeItemIdForRmove, IDevice existingDevice)
        {
            _treeManagementService.DeleteTreeItem(treeItemIdForRmove);
            await _deviceConnectionService.DisconnectDevice(existingDevice.Ip);
            _deviceAddingService.AddDeviceToTree(existingDevice);
        }

    }
}