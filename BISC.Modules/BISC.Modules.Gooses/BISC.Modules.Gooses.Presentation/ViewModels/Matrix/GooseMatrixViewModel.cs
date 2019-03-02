using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Serializing;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.FTP.Infrastructure.Serviсes;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Modules.Gooses.Model.Model.Matrix;
using BISC.Modules.Gooses.Model.Services;
using BISC.Modules.Gooses.Presentation.Commands;
using BISC.Modules.Gooses.Presentation.Events;
using BISC.Modules.Gooses.Presentation.FileParsers;
using BISC.Modules.Gooses.Presentation.Interfaces;
using BISC.Modules.Gooses.Presentation.Interfaces.Factories;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Matrix
{
    public class GooseMatrixViewModel : NavigationViewModelBase
    {
        private readonly IGoosesModelService _goosesModelService;
        private readonly IBiscProject _biscProject;

        private readonly IGlobalEventsService _globalEventsService;
        private readonly ILoggingService _loggingService;
        private readonly IUserNotificationService _userNotificationService;
        private readonly IDeviceFileWritingServices _deviceFileWritingServices;
        private readonly IUserInterfaceComposingService _userInterfaceComposingService;
        private readonly ResultFileParser _resultFileParser;
        private readonly IModelElementsRegistryService _modelElementsRegistryService;
        private readonly ISclCommunicationModelService _sclCommunicationModelService;
        private readonly GooseMatrixLoadingService _gooseMatrixLoadingService;
        private readonly IGooseControlBlockViewModelFactory _gooseControlBlockViewModelFactory;
        private readonly GooseMatrixSavingCommand _gooseMatrixSavingCommand;
        private readonly IConnectionPoolService _connectionPoolService;
        private IDevice _device;
                public ICommand CheckConformityCommand { get; }

        public ICommand SaveCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand SaveToDeviceCommand { get; }

        public ObservableCollection<GooseControlBlockViewModel> GooseControlBlockViewModels { get; }

        public GooseMatrixViewModel(IGoosesModelService goosesModelService, IBiscProject biscProject,
            IGlobalEventsService globalEventsService, ILoggingService loggingService, ICommandFactory commandFactory,
            IDeviceFileWritingServices deviceFileWritingServices,
            IUserInterfaceComposingService userInterfaceComposingService
            , ResultFileParser resultFileParser, IModelElementsRegistryService modelElementsRegistryService,
            ISclCommunicationModelService sclCommunicationModelService, GooseMatrixLoadingService gooseMatrixLoadingService,
            IGooseControlBlockViewModelFactory gooseControlBlockViewModelFactory, GooseMatrixSavingCommand gooseMatrixSavingCommand,IConnectionPoolService connectionPoolService
        )
        {
            _goosesModelService = goosesModelService;
            _biscProject = biscProject;
            _globalEventsService = globalEventsService;
            _loggingService = loggingService;
            _deviceFileWritingServices = deviceFileWritingServices;
            _resultFileParser = resultFileParser;
            _userInterfaceComposingService = userInterfaceComposingService;
            _modelElementsRegistryService = modelElementsRegistryService;
            _sclCommunicationModelService = sclCommunicationModelService;
            _gooseMatrixLoadingService = gooseMatrixLoadingService;
            _gooseControlBlockViewModelFactory = gooseControlBlockViewModelFactory;
            _gooseMatrixSavingCommand = gooseMatrixSavingCommand;
            _connectionPoolService = connectionPoolService;
            GooseControlBlockViewModels = new ObservableCollection<GooseControlBlockViewModel>();
            MessagesList = new ObservableCollection<string>();
            CheckConformityCommand = commandFactory.CreatePresentationCommand(OnCheckConformityCommand);
            SaveCommand = commandFactory.CreatePresentationCommand(OnSave);
            UpdateCommand = commandFactory.CreatePresentationCommand(OnUpdateExecute);
            SaveToDeviceCommand = commandFactory.CreatePresentationCommand(OnSaveToDevice);
        }

        private async void OnCheckConformityCommand()
        {
            if (_connectionPoolService.GetConnection(_device.Ip).IsConnected)
            {
                string fileInDevice =
                    await _deviceFileWritingServices.ReadFileStringFromDevice(_device.Ip, "1:/CFG", "GOOSERE.CFG");
                var str = _resultFileParser.GetFileStringFromGooseModel(_device).ToString();
                IsSynchronizedWithDevice = fileInDevice.Equals(str);
            }
        }

        private async void OnUpdateExecute()
        {
            await UpdateGooseMatrix(true);
            OnCheckConformityCommand();
        }


        private async void OnSaveToDevice()
        {
            var proj = _modelElementsRegistryService.SerializeModelElement(_biscProject, SerializingType.Extended)
                .ToString();
            var str = _resultFileParser.GetFileStringFromGooseModel(_device).ToString();
            if (_device.Ip == null)
            {
                _device.Ip = _sclCommunicationModelService.GetIpOfDevice(_device.Name, _biscProject.MainSclModel.Value);
            }

            if (await _deviceFileWritingServices.WriteFileStringInDevice(_device.Ip, new List<string>() { str, proj },
                new List<string>() { "GOOSERE.CFG", "PROJECT.ZIP" }))
            {
                _loggingService.LogMessage("Goose матрица успешно записана в устройство", SeverityEnum.Info);
            }
            else
            {
                _loggingService.LogMessage("Запись Goose матрицы прошла с ошибками", SeverityEnum.Info);
            }
        }

        public bool IsSynchronizedWithDevice
        {
            get { return _isSynchronizedWithDevice; }
            set { SetProperty(ref _isSynchronizedWithDevice, value); }
        }

        private async void OnSave()
        {
            _gooseMatrixSavingCommand.Initialize(_device,GooseControlBlockViewModels);
            await _gooseMatrixSavingCommand.SaveAsync();
            OnCheckConformityCommand();
        }

        #region Overrides of NavigationViewModelBase

        protected override async void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>("IED");
            await UpdateGooseMatrix(false);
            base.OnNavigatedTo(navigationContext);
        }

        private async Task UpdateGooseMatrix(bool isUpdatingFromDevice)
        {
            BlockViewModelBehavior.SetBlock("Загрузка", true);
            try
            {
                if (isUpdatingFromDevice)
                {
                    await _gooseMatrixLoadingService.Load(_device, null, _biscProject.MainSclModel.Value,
                        new CancellationToken());
                }

                _isInitialized = false;
                GooseControlBlockViewModels.Clear();
                var blocks =
                    _gooseControlBlockViewModelFactory.BuildGooseControlBlockViewModels(_biscProject.MainSclModel.Value,
                        _device);
                blocks.Item.ForEach((model => GooseControlBlockViewModels.Add(model)));
                InitDictionary();
                _isInitialized = true;
                Validate();
                IsActive = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                BlockViewModelBehavior.Unlock();
            }
        }


        public ObservableCollection<string> MessagesList { get; }


        private bool _isInitialized = false;

        private void SelectableBoxSelected(SelectableBoxEventArgs obj)
        {
            if (!_isInitialized) return;
            if (obj.IsFocused) return;
            Validate(obj.SelectableValueViewModel);
            OnSave();
        }

        private void InitDictionary()
        {
            _columnSelectableValueViewModelsDictionary = new Dictionary<int, List<ISelectableValueViewModel>>();
            for (int i = 0; i < 64; i++)
            {
                _columnSelectableValueViewModelsDictionary.Add(i, new List<ISelectableValueViewModel>(64));
                foreach (var gooseControlBlock in GooseControlBlockViewModels)
                {
                    foreach (var gooseRowViewModel in gooseControlBlock.GooseRowViewModels)
                    {
                        _columnSelectableValueViewModelsDictionary[i]
                            .Add(gooseRowViewModel.SelectableValueViewModels[i]);
                    }
                }
            }
        }


        private Dictionary<int, List<ISelectableValueViewModel>> _columnSelectableValueViewModelsDictionary =
            new Dictionary<int, List<ISelectableValueViewModel>>();

        private bool _isSynchronizedWithDevice;
        private bool _isActive1;

        public void Validate(ISelectableValueViewModel initiatorSelectableValueViewModel = null)
        {
            if (initiatorSelectableValueViewModel == null)
            {
                ValidateStates();
                ValidateQualities();
            }
            else
            {
                if (initiatorSelectableValueViewModel.Parent.GooseRowType == "State")
                {
                    ValidateStates();
                }

                if (initiatorSelectableValueViewModel.Parent.GooseRowType == "Quality")
                {
                    ValidateQualities();
                }
            }

            foreach (var gooseControlBlock in GooseControlBlockViewModels)
            {
                foreach (var gooseRowViewModel in gooseControlBlock.GooseRowViewModels)
                {
                    if (gooseRowViewModel.GooseRowType == "Validity")
                    {
                        gooseRowViewModel.SelectableValueViewModels.ForEach((model => model.IsSelectingEnabled = true));
                    }
                }
            }
        }


        private void ValidateStates()
        {
            foreach (var gooseControlBlock in GooseControlBlockViewModels)
            {
                foreach (var gooseRowViewModel in gooseControlBlock.GooseRowViewModels)
                {
                    if (gooseRowViewModel.GooseRowType == "State")
                    {
                        gooseRowViewModel.SelectableValueViewModels.ForEach(
                            (model => //енаблит все
                            {
                                model.IsSelectingEnabled = true;
                            }));

                        var selectedBox =
                            gooseRowViewModel.SelectableValueViewModels.FirstOrDefault((model => model.SelectedValue));
                        if (selectedBox != null)
                        {
                            gooseRowViewModel.SelectableValueViewModels.ForEach(
                                (model => //дизаблит всю строку кроме выбранного элемента
                                {
                                    if (model != selectedBox)
                                    {
                                        model.IsSelectingEnabled = false;
                                    }
                                }));
                        }
                    }
                }

                for (int i = 0; i < 64; i++)
                {
                    var selectedValueInColumn =
                        _columnSelectableValueViewModelsDictionary[i].FirstOrDefault((model =>
                        {
                            if ((model.Parent.GooseRowType != "State")) return false;
                            return model.SelectedValue;
                        }));

                    if (selectedValueInColumn != null)
                    {
                        _columnSelectableValueViewModelsDictionary[i].ForEach((model =>
                        {
                            if ((model != selectedValueInColumn) && ((model.Parent.GooseRowType == "State")))
                            {
                                model.IsSelectingEnabled = false;
                            }
                        }));
                    }
                }
            }
        }


        private void ValidateQualities()
        {
            foreach (var gooseControlBlock in GooseControlBlockViewModels)
            {
                foreach (var gooseRowViewModel in gooseControlBlock.GooseRowViewModels)
                {
                    if (gooseRowViewModel.GooseRowType == "Quality")
                    {
                        gooseRowViewModel.SelectableValueViewModels.ForEach(
                            (model => //енаблит все
                            {
                                model.IsSelectingEnabled = true;
                            }));

                        var selectedBox =
                            gooseRowViewModel.SelectableValueViewModels.FirstOrDefault((model => model.SelectedValue));
                        if (selectedBox != null)
                        {
                            gooseRowViewModel.SelectableValueViewModels.ForEach(
                                (model => //дизаблит всю строку кроме выбранного элемента
                                {
                                    if (model != selectedBox)
                                    {
                                        model.IsSelectingEnabled = false;
                                    }
                                }));
                        }
                    }
                }

                for (int i = 0; i < 64; i++)
                {
                    var selectedValueInColumn =
                        _columnSelectableValueViewModelsDictionary[i].FirstOrDefault((model =>
                        {
                            if ((model.Parent.GooseRowType != "Quality")) return false;
                            return model.SelectedValue;
                        }));

                    if (selectedValueInColumn != null)
                    {
                        _columnSelectableValueViewModelsDictionary[i].ForEach((model =>
                        {
                            if ((model != selectedValueInColumn) && ((model.Parent.GooseRowType == "Quality")))
                            {
                                model.IsSelectingEnabled = false;
                            }
                        }));
                    }
                }
            }
        }


        #region Overrides of ViewModelBase

        public override void OnActivate()
        {
            IsActive = true;
            _globalEventsService.Subscribe<SelectableBoxEventArgs>(SelectableBoxSelected);
            _userInterfaceComposingService.AddGlobalCommand(UpdateCommand, $"Обновить GOOSE-матрицу устройства {_device.Name}", IconsKeys.UpdateIconKey, false, true);
            base.OnActivate();
        }

        public override void OnDeactivate()
        {
            IsActive = false;
            _globalEventsService.Unsubscribe<SelectableBoxEventArgs>(SelectableBoxSelected);
            _userInterfaceComposingService.DeleteGlobalCommand(UpdateCommand);
            base.OnDeactivate();
        }

        protected override void OnDisposing()
        {
            foreach (GooseControlBlockViewModel gooseControlBlockViewModel in GooseControlBlockViewModels)
            {
                gooseControlBlockViewModel.Dispose();
            }
            base.OnDisposing();
        }

        #endregion

        #endregion
    }
}