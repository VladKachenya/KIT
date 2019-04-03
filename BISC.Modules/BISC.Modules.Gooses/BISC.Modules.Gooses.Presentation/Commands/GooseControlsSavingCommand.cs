using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Global.Model.Communication;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Project.Communication;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Model.Model;
using BISC.Modules.Gooses.Presentation.ViewModels.GooseControls;
using BISC.Presentation.Infrastructure.Commands;

namespace BISC.Modules.Gooses.Presentation.Commands
{
	public class GooseControlsSavingCommand:ISavingCommand
	{

		private readonly IFtpGooseModelService _ftpGooseModelService;
		private readonly IGoosesModelService _goosesModelService;
		private readonly IProjectService _projectService;
		private readonly ISclCommunicationModelService _sclCommunicationModelService;
		private readonly IBiscProject _biscProject;
		private readonly ILoggingService _loggingService;
		private readonly IConnectionPoolService _connectionPoolService;

	    private Func<bool> _isConnected;


		public GooseControlsSavingCommand(IFtpGooseModelService ftpGooseModelService, IGoosesModelService goosesModelService,
			IProjectService projectService, ISclCommunicationModelService sclCommunicationModelService, IBiscProject biscProject,
			ILoggingService loggingService,IConnectionPoolService connectionPoolService)
		{
			_ftpGooseModelService = ftpGooseModelService;
			_goosesModelService = goosesModelService;
			_projectService = projectService;
			_sclCommunicationModelService = sclCommunicationModelService;
			_biscProject = biscProject;
			_loggingService = loggingService;
			_connectionPoolService = connectionPoolService;
		}

		private ObservableCollection<GooseControlViewModel> _gooseControlViewModelsToSave;
		private IDevice _device;

		internal void Initialize(ObservableCollection<GooseControlViewModel> gooseControlViewModelsToSave, IDevice device, Func<bool> isConnected)
		{
			_gooseControlViewModelsToSave = gooseControlViewModelsToSave;
			_device = device;
		    _isConnected = isConnected;

		}
	
		public async Task<bool> IsSavingByFtpNeeded()
		{
			var isSavingNeeded = _gooseControlViewModelsToSave.Any((model =>
				model.ChangeTracker.GetIsModifiedRecursive() && model.IsDynamic)) && _isConnected();
			return isSavingNeeded;
		}

		public async Task<OperationResult> ValidateBeforeSave()
		{
			return OperationResult.SucceedResult;
		}

     	public async Task<OperationResult<SavingCommandResultEnum>> SaveAsync()
		{
			var goosesExisting = _goosesModelService.GetGooseControlsOfDevice(_device);
		
			if (_connectionPoolService.GetConnection(_device.Ip).IsConnected)
			{
				List<GooseFtpDto> gooseFtpDtos = _gooseControlViewModelsToSave.Where((model => model.IsDynamic)).Select((GetGooseFtpDtosFromViewModel)).ToList();

				var res = await _ftpGooseModelService.WriteGooseDtosToDevice(_device, gooseFtpDtos);

				if (!res.IsSucceed)
				{
					_loggingService.LogMessage($"Сохранение блоков управления GOOSE в устройство {_device.Name} по FTP {_device.Ip} произошло с ошибкой", SeverityEnum.Critical);
					return new OperationResult<SavingCommandResultEnum>(SavingCommandResultEnum.SavedWithErrors,false,res.GetFirstError());
				}
				else
				{
					_loggingService.LogMessage($"Сохранение блоков управления GOOSE в устройство {_device.Name} по FTP {_device.Ip} произошло успешно", SeverityEnum.Info);
					
				}

			}
			foreach (var gooseControlViewModel in _gooseControlViewModelsToSave)
			{
				if (!gooseControlViewModel.IsDynamic) continue;
				if (gooseControlViewModel.ChangeTracker.GetIsModifiedRecursive())
				{
					var existingGooseControl = goosesExisting.FirstOrDefault(control => control.Name == gooseControlViewModel.Name);
					MapGooseControlFromViewModel(existingGooseControl, gooseControlViewModel, existingGooseControl == null, _device);
				}
			}

			foreach (var gooseControlExisting in goosesExisting)
			{
				if (!_gooseControlViewModelsToSave.Any((model => model.Name == gooseControlExisting.Name)))
				{
					_goosesModelService.DeleteGooseCbAndGseByName(gooseControlExisting.Name, _device);
				}
			}
			_projectService.SaveCurrentProject();
			_loggingService.LogMessage($"Сохранение блоков управления GOOSE в устройство {_device.Name} произошло успешно", SeverityEnum.Info);
			return new OperationResult<SavingCommandResultEnum>(SavingCommandResultEnum.SavedOk);
		}

		private GooseFtpDto GetGooseFtpDtosFromViewModel(GooseControlViewModel gooseControlViewModel)
		{
			var gooseFtpDto = new GooseFtpDto();
			gooseFtpDto.Name = gooseControlViewModel.Name;
			gooseFtpDto.AppId = gooseControlViewModel.AppId;
			gooseFtpDto.FixedOffs = gooseControlViewModel.FixedOffs;
			gooseFtpDto.GoId = gooseControlViewModel.GoId;
			gooseFtpDto.GseType = gooseControlViewModel.GseType;
			gooseFtpDto.MacAddress = gooseControlViewModel.MacAddress;
			gooseFtpDto.MaxTime = gooseControlViewModel.MaxTime;
			gooseFtpDto.MinTime = gooseControlViewModel.MinTime;
			gooseFtpDto.SelectedDataset = gooseControlViewModel.SelectedDataset;
			gooseFtpDto.VlanId = gooseControlViewModel.VlanId;
			gooseFtpDto.VlanPriority = gooseControlViewModel.VlanPriority;
			gooseFtpDto.ConfRev = gooseControlViewModel.ConfRev;
			gooseFtpDto.LdInst = gooseControlViewModel.LdInst;

			return gooseFtpDto;
		}


		private void MapGooseControlFromViewModel(IGooseControl gooseControl, GooseControlViewModel gooseControlViewModel, bool isNew, IDevice device)
		{
			IGse relatedGse = null;
			if (isNew)
			{
				gooseControl = new GooseControl();
				relatedGse = new Gse();
				relatedGse.ChildModelElements.Add(new SclAddress());
			}
			else
			{
				if (gooseControl.DataSet != gooseControlViewModel.SelectedDataset)
				{

				}

				relatedGse = _sclCommunicationModelService.GetGsesForDevice(device.Name, _biscProject.MainSclModel.Value).FirstOrDefault((gse => gse.CbName == gooseControl.Name));

			}

			gooseControl.Name = gooseControlViewModel.Name;
			gooseControl.ConfRev = gooseControlViewModel.ConfRev;
			gooseControl.AppId = gooseControlViewModel.GoId;
			gooseControl.DataSet = gooseControlViewModel.SelectedDataset;
			gooseControl.IsDynamic = gooseControlViewModel.IsDynamic;
			gooseControl.GooseType = gooseControlViewModel.GseType;
			gooseControl.FixedOffs = gooseControlViewModel.FixedOffs;

			relatedGse.CbName = gooseControlViewModel.Name;
			relatedGse.AppIdDec = gooseControlViewModel.AppId.ToString();
			relatedGse.LdInst = gooseControlViewModel.LdInst;
			relatedGse.MacAddress = gooseControlViewModel.MacAddress;
			relatedGse.MaxTime.Value = new DurationInMilliSec("MaxTime")
			{
				Multiplier = "m",
				Value = (int)gooseControlViewModel.MaxTime,
				Unit = "s"
			};
			relatedGse.MinTime.Value = new DurationInMilliSec("MinTime")
			{
				Multiplier = "m",
				Value = (int)gooseControlViewModel.MinTime,
				Unit = "s"
			};
			relatedGse.VlanId = gooseControlViewModel.VlanId.ToString();
			relatedGse.VlanPriority = (int)gooseControlViewModel.VlanPriority;
			if (isNew)
			{
				_goosesModelService.AddGseControl("LLN0", relatedGse.LdInst, device, gooseControl);
				_sclCommunicationModelService.AddGse(relatedGse, _biscProject.MainSclModel.Value, device.Name);
			}

		}

	}
}
