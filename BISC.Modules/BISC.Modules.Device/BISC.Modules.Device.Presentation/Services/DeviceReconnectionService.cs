using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Loading;
using BISC.Modules.Device.Infrastructure.Loading.Events;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Device.Presentation.Interfaces.Services;
using BISC.Modules.Device.Presentation.Services.Helpers;
using BISC.Modules.FTP.Infrastructure.Serviсes;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
		private readonly INavigationService _navigationService;
		private List<IDeviceElementLoadingService> _elementLoadingServices;
		private List<IElementConflictResolver> _elementConflictResolvers;

		private Func<ISclModel> _sclModelCreator;
		private readonly IBiscProject _biscProject;
		private readonly IDeviceAddingService _deviceAddingService;
		private readonly IDeviceWarningsService _deviceWarningsService;
		private readonly ISclCommunicationModelService _sclCommunicationModelService;
		private readonly IPingService _pingService;
		private readonly ISaveCheckingService _saveCheckingService;
	    private readonly IUserInteractionService _userInteractionService;

	    public DeviceReconnectionService(IDeviceFileWritingServices deviceFileWritingServices,
			IDeviceConnectionService deviceConnectionService, IDeviceModelService deviceModelService,
			ITreeManagementService treeManagementService, IConnectionPoolService connectionPoolService,
			ITabManagementService tabManagementService, ILoggingService loggingService,
			IGlobalEventsService globalEventsService,
			INavigationService navigationService, IInjectionContainer injectionContainer,
			Func<ISclModel> sclModelCreator,
			IBiscProject biscProject, IDeviceAddingService deviceAddingService,
			IDeviceWarningsService deviceWarningsService,
			ISclCommunicationModelService sclCommunicationModelService, IPingService pingService,
			ISaveCheckingService saveCheckingService,IUserInteractionService userInteractionService)
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
			_biscProject = biscProject;
			_deviceAddingService = deviceAddingService;
			_deviceWarningsService = deviceWarningsService;
			_sclCommunicationModelService = sclCommunicationModelService;
			_pingService = pingService;
			_saveCheckingService = saveCheckingService;
		    _userInteractionService = userInteractionService;
		    _elementLoadingServices = injectionContainer.ResolveAll(typeof(IDeviceElementLoadingService))
				.Cast<IDeviceElementLoadingService>().ToList();
			_elementConflictResolvers = injectionContainer.ResolveAll(typeof(IElementConflictResolver))
				.Cast<IElementConflictResolver>().ToList();
		}


		#region Implementation of IDeviceRestartService

		public async Task<bool> ReconnectDevice(IDevice existingDevice, TreeItemIdentifier treeItemIdToRemove)
		{
			if (!await _pingService.GetPing(
				_sclCommunicationModelService.GetIpOfDevice(existingDevice.Name, _biscProject.MainSclModel.Value)))
			{
				_loggingService.LogMessage($"Устройство {existingDevice.Name} не отвечает", SeverityEnum.Critical);
				return false;
			}

			await Reconnect(existingDevice, treeItemIdToRemove, false);
			return true;
		}

		private async Task Reconnect(IDevice existingDevice, TreeItemIdentifier treeItemIdToRemove, bool isRestarting)
		{
			_treeManagementService.DeleteTreeItem(treeItemIdToRemove);

			_tabManagementService.CloseTabWithChildren(treeItemIdToRemove.ItemId.ToString());

			var sortedElements = _elementLoadingServices.OrderBy((service => service.Priority));

			CancellationTokenSource cts = new CancellationTokenSource();
			BiscNavigationParameters biscNavigationParameters = new BiscNavigationParameters();
			RestartDeviceContext restartDeviceContext =
				new RestartDeviceContext(existingDevice, cts);
			biscNavigationParameters.AddParameterByName(DeviceKeys.RestartDeviceContextKey, restartDeviceContext);


			var treeItemId = _treeManagementService.AddTreeItem(biscNavigationParameters,
				DeviceKeys.DeviceRestartViewKey,
				null);
			restartDeviceContext.TreeItemIdentifier = treeItemId;
			if (isRestarting)
			{
				await Task.Delay(3000, cts.Token);
			}

			existingDevice.Ip =
				_sclCommunicationModelService.GetIpOfDevice(existingDevice.Name, _biscProject.MainSclModel.Value);
			var deviceConnectResult = await _deviceConnectionService.ConnectDevice(existingDevice.Ip, 4);
			if (!deviceConnectResult.IsSucceed)
			{
				_loggingService.LogMessage($"IED Устройство по адресу {existingDevice.Ip} не обнаружено",
					SeverityEnum.Warning);
			}

			var device = deviceConnectResult.Item;
			var sclModel = _sclModelCreator();
			var itemsCount = 0;
			try
			{
				foreach (var sortedElement in sortedElements)
				{
					itemsCount += await sortedElement.EstimateProgress(device);
				}

				_deviceModelService.AddDeviceInModel(sclModel, device);

				int currentElementsCount = 0;


				foreach (var sortedElement in sortedElements)
				{
					await sortedElement.Load(device,
						new Progress<object>(deviceLoadingEvent =>
						{
							_globalEventsService.SendMessage(new DeviceLoadingEvent(device.Ip, device.Name,
								itemsCount, ++currentElementsCount));
						}), sclModel, cts.Token);
				}
			}
			catch (Exception e)
			{
				if (cts.IsCancellationRequested)
				{
					_connectionPoolService.GetConnection(device.Ip).StopConnection();
					_loggingService.LogUserAction($"Загрузка устройства отменена пользователем {device.Name}");
					//  return new OperationResult($"Загрузка устройства отменена пользователем {device.Name}");
				}
				else
				{
					_loggingService.LogMessage(
						$"Ошибка загрузки устройства {e.Message + Environment.NewLine + e.StackTrace}",
						SeverityEnum.Critical);
					// return new OperationResult($"Ошибка загрузка устройства {device.Name}");
				}
			}

			var hasConflics = false;
			_elementConflictResolvers.ForEach((resolver =>
			{
				if (resolver.GetIfConflictsExists(device.Name, sclModel, _biscProject.MainSclModel.Value))
				{
					hasConflics = true;

					return;
				}
			}));
			restartDeviceContext.HaveConflicts = hasConflics;
			_globalEventsService.SendMessage(new DeviceLoadingEvent(device.Ip) { IsFinished = true });
			if (!hasConflics)
			{
				_treeManagementService.DeleteTreeItem(treeItemId);
				_deviceAddingService.AddDeviceToTree(existingDevice);
				_deviceWarningsService.ClearDeviceWarningsOfDevice(existingDevice.Name);
			}
			else
			{
				restartDeviceContext.DeviceConflictContext = new DeviceConflictContext(_biscProject.MainSclModel.Value,
					sclModel, existingDevice.Name);
			}
		}

		public async Task RestartDevice(IDevice existingDevice, TreeItemIdentifier treeItemIdToRemove)
		{

			await _deviceFileWritingServices.ResetDevice(existingDevice.Ip);
			_connectionPoolService.GetConnection(existingDevice.Ip).StopConnection();
			await Reconnect(existingDevice, treeItemIdToRemove, true);
		}

		public async Task ExecuteBeforeRestart(Func<Task> taskToExecute, IDevice existingDevice)
		{
	
			var unsavedEntitiesInfo = (await _saveCheckingService.GetIsDeviceEntitiesSaved(existingDevice.Name));
			if (!unsavedEntitiesInfo.IsEntitiesSaved)
			{

			    if (unsavedEntitiesInfo.UnsavedCheckingEntities.Count > 1)
			    {
			        var warnings =
			            unsavedEntitiesInfo.UnsavedCheckingEntities.Select(
			                change => ("Изменения " + change.EntityFriendlyName + " будут сохранены.")).ToList();
			        var res = await _userInteractionService.ShowOptionToUser("Требуется перезагрузка", 
			            "Сохранение потребует перезагрузки устройства. \nЖелаете продолжить?", 
                        new List<string>() { "Да", "Нет" }, warnings);
			        if (res == 1)
			        {
			            return;
			        }
                    var savingRes = await _saveCheckingService.SaveDeviceUnsavedEntities(existingDevice.Name, false);
				    if (savingRes.IsValidationFailed) return;
			    }
			    else
			    {
			        var res = await _userInteractionService.ShowOptionToUser("Требуется перезагрузка", "Сохранение потребует перезагрузки устройства",
			            new List<string>() { "ОК", "Отмена" });
			        if (res == 1)
			        {
			            return;
			        }
                    var savingRes = await _saveCheckingService.SaveDeviceUnsavedEntities(existingDevice.Name, false);
					if(savingRes.IsValidationFailed)return;
				    

			    }
            }

			//await taskToExecute();
			await _deviceFileWritingServices.ResetDevice(existingDevice.Ip);
			_connectionPoolService.GetConnection(existingDevice.Ip).StopConnection();
			await Reconnect(existingDevice, _treeManagementService.GetDeviceTreeItem(existingDevice.Name), true);

		}

		#endregion
	}
}