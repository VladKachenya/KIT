﻿using BISC.Infrastructure.Global.Common;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Loading;
using BISC.Modules.Device.Infrastructure.Loading.Events;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Device.Presentation.Interfaces.Services;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Presentation.BaseItems.Commands;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BISC.Modules.Device.Presentation.Services
{
    public class DeviceLoadingService : IDeviceLoadingService
    {
        private readonly IDeviceModelService _deviceModelService;
        private readonly ITreeManagementService _treeManagementService;
        private readonly IDeviceAddingService _deviceAddingService;
        private readonly Func<ISclModel> _sclModelCreator;
        private readonly IGlobalEventsService _globalEventsService;
        private readonly IBiscProject _biscProject;
        private readonly ILoggingService _loggingService;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IUserInteractionService _userInteractionService;
        private List<IDeviceElementLoadingService> _elementLoadingServices;

        public DeviceLoadingService(
            IInjectionContainer injectionContainer, 
            IDeviceModelService deviceModelService,
            ITreeManagementService treeManagementService, 
            IDeviceAddingService deviceAddingService,
            Func<ISclModel> sclModelCreator, 
            IGlobalEventsService globalEventsService, 
            IBiscProject biscProject,
            ILoggingService loggingService, 
            IConnectionPoolService connectionPoolService,
            IUserInteractionService userInteractionService)
        {
            _deviceModelService = deviceModelService;
            _treeManagementService = treeManagementService;
            _deviceAddingService = deviceAddingService;
            _sclModelCreator = sclModelCreator;
            _globalEventsService = globalEventsService;
            _biscProject = biscProject;
            _loggingService = loggingService;
            _connectionPoolService = connectionPoolService;
            _userInteractionService = userInteractionService;
            _elementLoadingServices = injectionContainer.ResolveAll(typeof(IDeviceElementLoadingService))
                .Cast<IDeviceElementLoadingService>().ToList();
        }

        public void Dispose()
        {
            _elementLoadingServices.ForEach((service => service.Dispose()));
        }

        public async Task<OperationResult> LoadElements(IDevice device)
        {
            var sortedElements = _elementLoadingServices.OrderBy((service => service.Priority));

            CancellationTokenSource cts = new CancellationTokenSource();
            BiscNavigationParameters biscNavigationParameters = new BiscNavigationParameters();
            biscNavigationParameters.AddParameterByName(DeviceKeys.DeviceModelKey, device);
            biscNavigationParameters.AddParameterByName("cts", cts);
            DialogCommands.CloseDialogCommand.Execute(null, null);
            var treeItemId = _treeManagementService.AddTreeItem(biscNavigationParameters,
                DeviceKeys.DeviceLoadingTreeItemViewKey,
                null);
            var sclModel = _sclModelCreator();
            var itemsCount = 0;
            var isDeviceExist = false;
            try
            {
                foreach (var sortedElement in sortedElements)
                {
                    itemsCount += await sortedElement.EstimateProgress(device);
                }

                _deviceModelService.AddDeviceInModel(sclModel, device);

                int currentElementsCount = 0;

                if (_deviceModelService.GetDevicesFromModel(_biscProject.MainSclModel.Value).Any(d => d.Name == device.Name))
                {
                    isDeviceExist = true;
                    throw new Exception();
                }

                foreach (var sortedElement in sortedElements)
                {
                    await sortedElement.Load(device,
                        new Progress<object>(deviceLoadingEvent =>
                        {
                            _globalEventsService.SendMessage(new DeviceLoadingEvent(device.DeviceGuid, device.Name,
                                itemsCount, ++currentElementsCount));
                        }), sclModel, cts.Token);
                }

                
                _treeManagementService.DeleteTreeItem(treeItemId);
                _deviceAddingService.AddDevicesInProject(new List<IDevice>() { device }, sclModel);
                return OperationResult.SucceedResult;
            }
            catch (Exception e)
            {
                _treeManagementService.DeleteTreeItem(treeItemId);
                if (device?.Ip != null)
                {
                    _connectionPoolService.GetConnection(device.Ip).StopConnection();
                }

                if (cts.IsCancellationRequested)
                {
                    _loggingService.LogUserAction($"Загрузка устройства отменена пользователем {device.Name}");
                    return new OperationResult($"Загрузка устройства отменена пользователем {device.Name}");
                }

                if (isDeviceExist)
                {
                    var mes = $"Устройство с именем {device.Name} уже существует в проекте";
                    await _userInteractionService.ShowOptionToUser("Не соответствие модели устройства", mes,
                        new List<string>() { "Ок" });
                    return new OperationResult(mes);
                }

                _loggingService.LogMessage($"Ошибка загрузки устройства {e.Message + Environment.NewLine + e.StackTrace}", SeverityEnum.Critical);
                return new OperationResult($"Ошибка загрузки устройства {device.Name}");
            }
        }

    }
}