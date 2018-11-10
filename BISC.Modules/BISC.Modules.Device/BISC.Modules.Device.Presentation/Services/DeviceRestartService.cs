using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Loading;
using BISC.Modules.Device.Infrastructure.Loading.Events;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Device.Presentation.Services.Helpers;
using BISC.Modules.FTP.Infrastructure.Serviсes;
using BISC.Presentation.BaseItems.Commands;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Device.Presentation.Services
{
   public class DeviceRestartService:IDeviceRestartService
    {
        private readonly IDeviceFileWritingServices _deviceFileWritingServices;
        private readonly IDeviceConnectionService _deviceConnectionService;
        private readonly IDeviceModelService _deviceModelService;
        private readonly ITreeManagementService _treeManagementService;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly ITabManagementService _tabManagementService;
        private readonly ILoggingService _loggingService;
        private readonly IGlobalEventsService _globalEventsService;
        private readonly INavigationService _navigationService;
        private List<IDeviceElementLoadingService> _elementLoadingServices;
        private List<IElementConflictResolver> _elementConflictResolvers;

        private Func<ISclModel> _sclModelCreator;

        public DeviceRestartService(IDeviceFileWritingServices deviceFileWritingServices,
            IDeviceConnectionService deviceConnectionService,IDeviceModelService deviceModelService,
            ITreeManagementService treeManagementService, IConnectionPoolService connectionPoolService,
            ITabManagementService tabManagementService, ILoggingService loggingService,IGlobalEventsService globalEventsService,
            INavigationService navigationService, IInjectionContainer injectionContainer, Func<ISclModel> sclModelCreator)
        {
            _deviceFileWritingServices = deviceFileWritingServices;
            _deviceConnectionService = deviceConnectionService;
            _deviceModelService = deviceModelService;
            _treeManagementService = treeManagementService;
            _connectionPoolService = connectionPoolService;
            _tabManagementService = tabManagementService;
            _loggingService = loggingService;
            _globalEventsService = globalEventsService;
            _navigationService = navigationService;
            _sclModelCreator = sclModelCreator;
            _elementLoadingServices = injectionContainer.ResolveAll(typeof(IDeviceElementLoadingService))
                .Cast<IDeviceElementLoadingService>().ToList();
            _elementConflictResolvers = injectionContainer.ResolveAll(typeof(IElementConflictResolver))
                .Cast<IElementConflictResolver>().ToList();
        }


        #region Implementation of IDeviceRestartService

        public async Task RestartDevice(IDevice existingDevice, TreeItemIdentifier treeItemIdOfExistingDevice)
        {
            await _deviceFileWritingServices.ResetDevice(existingDevice.Ip);
          //  _treeManagementService.DeleteTreeItem(treeItemIdOfExistingDevice);
          //  _connectionPoolService.GetConnection(existingDevice.Ip).StopConnection();
          //  _tabManagementService.CloseTabWithChildren(treeItemIdOfExistingDevice.ItemId.ToString());



          //  var sortedElements = _elementLoadingServices.OrderBy((service => service.Priority));

          //  CancellationTokenSource cts = new CancellationTokenSource();
          //  BiscNavigationParameters biscNavigationParameters = new BiscNavigationParameters();
          //  RestartDeviceEntity restartDeviceEntity =
          //      new RestartDeviceEntity(existingDevice.Name, cts, existingDevice.Ip, false);
          //  biscNavigationParameters.AddParameterByName(DeviceKeys.RestartDeviceEntityKey,restartDeviceEntity);

          //  DialogCommands.CloseDialogCommand.Execute(null, null);
          //  var treeItemId = _treeManagementService.AddTreeItem(biscNavigationParameters,
          //      DeviceKeys.DeviceRestartViewKey,
          //      null);
          //  await Task.Delay(3000, cts.Token);
          //  var deviceConnectResult = await _deviceConnectionService.ConnectDevice(existingDevice.Ip);
          //  var device = deviceConnectResult.Item;
          //  var sclModel = _sclModelCreator();
          //  var itemsCount = 0;
          //  try
          //  {
          //      foreach (var sortedElement in sortedElements)
          //      {
          //          itemsCount += await sortedElement.EstimateProgress(device);
          //      }

          //      _deviceModelService.AddDeviceInModel(sclModel, device);

          //      int currentElementsCount = 0;


          //      foreach (var sortedElement in sortedElements)
          //      {
          //          await sortedElement.Load(device,
          //              new Progress<object>(deviceLoadingEvent =>
          //              {
          //                  _globalEventsService.SendMessage(new DeviceLoadingEvent(device.Ip, device.Name,
          //                      itemsCount, ++currentElementsCount));
          //              }), sclModel, cts.Token);
          //      }
          //  }
          //  catch (Exception e)
          //  {
          //      if (cts.IsCancellationRequested)
          //      {
          //          _connectionPoolService.GetConnection(device.Ip).StopConnection();
          //          _loggingService.LogUserAction($"Загрузка устройства отменена пользователем {device.Name}");
          //          //  return new OperationResult($"Загрузка устройства отменена пользователем {device.Name}");
          //      }
          //      else
          //      {
          //          _loggingService.LogMessage($"Ошибка загрузки устройства {e.Message + Environment.NewLine + e.StackTrace}", SeverityEnum.Critical);
          //          // return new OperationResult($"Ошибка загрузка устройства {device.Name}");
          //      }
          //  }

          ////  _i
          // // restartDeviceEntity.HaveConflicts=


          //  _globalEventsService.SendMessage(new DeviceLoadingEvent(device.Ip) { IsFinished = true });
          //  //_treeManagementService.DeleteTreeItem(treeItemId);
        }


        #endregion
    }
}
