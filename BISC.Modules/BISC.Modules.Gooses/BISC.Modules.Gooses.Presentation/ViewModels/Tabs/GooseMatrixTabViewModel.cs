using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Model.Services.LoadingServices;
using BISC.Modules.Gooses.Presentation.Commands;
using BISC.Modules.Gooses.Presentation.Events;
using BISC.Modules.Gooses.Presentation.Interfaces;
using BISC.Modules.Gooses.Presentation.Interfaces.Factories;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Commands;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using BISC.Modules.Gooses.Infrastructure.Keys;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Tabs
{
    public class GooseMatrixTabViewModel : NavigationViewModelBase
    {
        private readonly IBiscProject _biscProject;
        private readonly ISaveCheckingService _saveCheckingService;
        private readonly IGlobalEventsService _globalEventsService;
        private readonly ILoggingService _loggingService;
        private readonly IUserInterfaceComposingService _userInterfaceComposingService;
        private readonly GooseMatrixLoadingService _gooseMatrixLoadingService;
        private readonly IGooseControlBlockViewModelFactory _gooseControlBlockViewModelFactory;
        private readonly GooseSubscriptionMatrixSavingCommand _gooseSubscriptionMatrixSavingCommand;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly GooseInputModelInfosLoadingService _gooseInputModelInfosLoadingService;
        private readonly IGlobalSavingService _globalSavingService;
        private readonly IConfigurationService _configurationService;

        private Dictionary<int, List<ISelectableValueViewModel>> _columnSelectableValueViewModelsDictionary =
            new Dictionary<int, List<ISelectableValueViewModel>>();
        private ObservableCollection<GooseControlBlockViewModel> _gooseControlBlockViewModels;

        private IDevice _device;
        private string _regionName;
        private bool _isCommandEnabled = true;
        private DataTable _gooseMatrix;

        #region ctor

        public GooseMatrixTabViewModel(
            IBiscProject biscProject,
            ISaveCheckingService saveCheckingService,
            IGlobalEventsService globalEventsService,
            ILoggingService loggingService,
            ICommandFactory commandFactory,
            IUserInterfaceComposingService userInterfaceComposingService,
            GooseMatrixLoadingService gooseMatrixLoadingService,
            IGooseControlBlockViewModelFactory gooseControlBlockViewModelFactory,
            GooseSubscriptionMatrixSavingCommand gooseSubscriptionMatrixSavingCommand,
            IConnectionPoolService connectionPoolService,
            GooseInputModelInfosLoadingService gooseInputModelInfosLoadingService,
            IGlobalSavingService globalSavingService,
            IConfigurationService configurationService)
            : base(globalEventsService)
        {
            _biscProject = biscProject;
            _saveCheckingService = saveCheckingService;
            _globalEventsService = globalEventsService;
            _loggingService = loggingService;
            _userInterfaceComposingService = userInterfaceComposingService;
            _gooseMatrixLoadingService = gooseMatrixLoadingService;
            _gooseControlBlockViewModelFactory = gooseControlBlockViewModelFactory;
            _gooseSubscriptionMatrixSavingCommand = gooseSubscriptionMatrixSavingCommand;
            _connectionPoolService = connectionPoolService;
            _gooseInputModelInfosLoadingService = gooseInputModelInfosLoadingService;
            _globalSavingService = globalSavingService;
            _configurationService = configurationService;
            MessagesList = new ObservableCollection<string>();
            SaveCommand = commandFactory.CreatePresentationCommand(OnSave, () => _isCommandEnabled);
            UpdateCommand = commandFactory.CreatePresentationCommand(OnUpdateExecute, () => _isCommandEnabled);
            GooseControlBlockViewModels = new ObservableCollection<GooseControlBlockViewModel>();
        }
        #endregion

        #region public methods
        public ICommand SaveCommand { get; }
        public ICommand UpdateCommand { get; }

        public DataView GooseMatrixDataView => _gooseMatrix?.DefaultView; 

        public ObservableCollection<GooseControlBlockViewModel> GooseControlBlockViewModels
        {
            get => _gooseControlBlockViewModels;
            set
            {
                var count = value.FirstOrDefault()?.ColumnsName.Count;
                if (count != null)
                {
                    ColumnCount = (int)count;
                    OnPropertyChanged(nameof(ColumnCount));
                }

                SetProperty(ref _gooseControlBlockViewModels, value);
                _gooseSubscriptionMatrixSavingCommand.Initialize(_device, GooseControlBlockViewModels.ToList());
                _gooseSubscriptionMatrixSavingCommand.RefreshViewModel = async () => await UpdateGooseMatrix(false);

            }
        }

        public int ColumnCount { get; private set; }


        public ObservableCollection<string> MessagesList { get; }

        public void Validate(ISelectableValueViewModel initiatorSelectableValueViewModel = null)
        {
            if (initiatorSelectableValueViewModel == null)
            {
                ValidateStates();
                ValidateQualities();
            }
            else
            {
                if (initiatorSelectableValueViewModel.Parent.GooseRowType == GooseKeys.GooseSubscriptionPresentationKeys.State)
                {
                    ValidateStates();
                    if (_configurationService.IsAutoEnabledQualityInGooseReceiving)
                    {
                        SetQuality(
                            initiatorSelectableValueViewModel.Parent.Parent?.GoCbReference.GoCbReference,
                            initiatorSelectableValueViewModel.Parent.DoiDataRef,
                            initiatorSelectableValueViewModel.ColumnNumber,
                            _configurationService.IsAutoEnabledValidityInGooseReceiving,
                            initiatorSelectableValueViewModel.SelectedValue);
                    }
                }

                if (initiatorSelectableValueViewModel.Parent.GooseRowType == GooseKeys.GooseSubscriptionPresentationKeys.Quality)
                {
                    ValidateQualities();
                }
            }

            foreach (var gooseControlBlock in GooseControlBlockViewModels)
            {
                foreach (var gooseRowViewModel in gooseControlBlock.GooseRowViewModels)
                {
                    if (gooseRowViewModel.GooseRowType == GooseKeys.GooseSubscriptionPresentationKeys.Validity)
                    {
                        gooseRowViewModel.SelectableValueViewModels.ToList().ForEach((model => model.IsSelectingEnabled = true));
                    }
                }
            }
        }
        #endregion

        private void SetEnableCommands(bool isEnable)
        {
            _isCommandEnabled = isEnable;
            (SaveCommand as IPresentationCommand)?.RaiseCanExecute();
            (UpdateCommand as IPresentationCommand)?.RaiseCanExecute();
        }

        private async void OnUpdateExecute()
        {
            SetEnableCommands(false);
            try
            {
                await UpdateGooseMatrix(false);
            }
            catch (Exception e)
            {
                _loggingService.LogException(e);
            }
            finally
            {
                SetEnableCommands(true);
            }
        }


        private async void OnSave()
        {
            try
            {
                SetEnableCommands(false);
                await SaveGooseMatrix();
                await UpdateGooseMatrix(false);
            }
            catch (Exception e)
            {
                _loggingService.LogException(e);
            }
            finally
            {
                SetEnableCommands(true);
            }
        }

        private void SetQuality(string goRef, string doiDataRef, int columnNumber, bool isValidityNeed, bool value)
        {
            if(goRef == null) return;
            var goseViewModel =
                this.GooseControlBlockViewModels.FirstOrDefault(g => g.GoCbReference.GoCbReference == goRef);
            var goseQualityRow = goseViewModel?.GooseRowViewModels
                .FirstOrDefault(qr => qr.DoiDataRef == doiDataRef && qr.GooseRowType == GooseKeys.GooseSubscriptionPresentationKeys.Quality);
            if (goseQualityRow != null)
            {
                goseQualityRow.SelectableValueViewModels[columnNumber].SetValue(value);
                if (isValidityNeed)
                {
                    var goseValidityRow = goseViewModel.GooseRowViewModels
                        .FirstOrDefault(gr => gr.GooseRowType == GooseKeys.GooseSubscriptionPresentationKeys.Validity);
                    goseValidityRow?.SelectableValueViewModels[columnNumber].SetValue(value);
                }
            }
        }
        #region Overrides of NavigationViewModelBase

        protected override async void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            // Так как метод асинхронный то поток синхронно выполняется до первого await. После чего управление переходит к следующему оператору.
            //Необходимо вызвать OnNavigatedTo до первого await.
            base.OnNavigatedTo(navigationContext);
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>("IED");
            _regionName =
                navigationContext.BiscNavigationParameters.GetParameterByName<UiEntityIdentifier>(
                    UiEntityIdentifier.Key).ItemId.ToString();
            await UpdateGooseMatrix(false);
        }

        private async Task UpdateGooseMatrix(bool isFromDevice)
        {
            BlockViewModelBehavior.SetBlock("Обновление данных...", false);
            try
            {
                if (isFromDevice && _connectionPoolService.GetConnection(_device.Ip).IsConnected)
                {
                    await _gooseMatrixLoadingService.Load(_device, null, _biscProject.MainSclModel.Value,
                        new CancellationToken());
                    await _gooseInputModelInfosLoadingService.Load(_device, null, _biscProject.MainSclModel.Value,
                        new CancellationToken());
                }

                _isInitialized = false;
                GooseControlBlockViewModels?.Clear();
                var blocks = await _gooseControlBlockViewModelFactory.BuildGooseControlBlockViewModels(_biscProject.MainSclModel.Value, _device);
                var table = _gooseControlBlockViewModelFactory.BuildGooseMatrixDataTable(_biscProject.MainSclModel.Value, _device);
                OnPropertyChanged(nameof(GooseMatrixDataView));
                GooseControlBlockViewModels = new ObservableCollection<GooseControlBlockViewModel>(blocks.Item);
                InitDictionary();
                _isInitialized = true;
                Validate();
                _saveCheckingService.RemoveSaveCheckingEntityByOwner(_regionName);
                _saveCheckingService.AddSaveCheckingEntity(
                    new SaveCheckingEntity(ChangeTracker, $"Матрица GOOSE устройства {_device.Name}",
                        _gooseSubscriptionMatrixSavingCommand, _device.DeviceGuid, _regionName));
                ChangeTracker.AcceptChanges();
                ChangeTracker.SetTrackingEnabled(true);
            }
            catch (Exception e)
            {
                _loggingService.LogException(e);
                throw;
            }
            finally
            {
                BlockViewModelBehavior.Unlock();
            }
        }

        public ListCollectionView GroupedGoosesViewModels
        {
            get => _groupedGoosesViewModels;
            set => SetProperty(ref _groupedGoosesViewModels, value);
        }

        private async Task SaveGooseMatrix()
        {
            BlockViewModelBehavior.SetBlock("Сохранение Goose матрицы...", true);
            try
            {
                await _globalSavingService.SaveСhangesToRegion(_regionName, false);
            }
            catch (Exception e)
            {
                _loggingService.LogException(e);
            }
            finally
            {
                BlockViewModelBehavior.Unlock();
            }
        }

        private bool _isInitialized = false;
        private ListCollectionView _groupedGoosesViewModels;

        private void SelectableBoxSelected(SelectableBoxEventArgs obj)
        {
            if (!_isInitialized)
            {
                return;
            }

            if (obj.IsFocused)
            {
                return;
            }

            Validate(obj.SelectableValueViewModel);
        }

        private void InitDictionary()
        {
            _columnSelectableValueViewModelsDictionary = new Dictionary<int, List<ISelectableValueViewModel>>();
            for (int i = 0; i < ColumnCount; i++)
            {
                _columnSelectableValueViewModelsDictionary.Add(i, new List<ISelectableValueViewModel>(ColumnCount));
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

        private void ValidateStates()
        {
            foreach (var gooseControlBlock in GooseControlBlockViewModels)
            {
                foreach (var gooseRowViewModel in gooseControlBlock.GooseRowViewModels)
                {
                    if (gooseRowViewModel.GooseRowType == "State")
                    {
                        gooseRowViewModel.SelectableValueViewModels.ToList().ForEach(
                            (model => //енаблит все
                            {
                                model.IsSelectingEnabled = true;
                            }));

                        var selectedBox =
                            gooseRowViewModel.SelectableValueViewModels.FirstOrDefault((model => model.SelectedValue));
                        if (selectedBox != null)
                        {
                            gooseRowViewModel.SelectableValueViewModels.ToList().ForEach(
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

                for (int i = 0; i < ColumnCount; i++)
                {
                    var selectedValueInColumn =
                        _columnSelectableValueViewModelsDictionary[i].FirstOrDefault((model =>
                        {
                            if ((model.Parent.GooseRowType != "State"))
                            {
                                return false;
                            }

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
                        gooseRowViewModel.SelectableValueViewModels.ToList().ForEach(
                            (model => //енаблит все
                            {
                                model.IsSelectingEnabled = true;
                            }));

                        var selectedBox =
                            gooseRowViewModel.SelectableValueViewModels.FirstOrDefault((model => model.SelectedValue));
                        if (selectedBox != null)
                        {
                            gooseRowViewModel.SelectableValueViewModels.ToList().ForEach(
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

                for (int i = 0; i < ColumnCount; i++)
                {
                    var selectedValueInColumn =
                        _columnSelectableValueViewModelsDictionary[i].FirstOrDefault((model =>
                        {
                            if ((model.Parent.GooseRowType != "Quality"))
                            {
                                return false;
                            }

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
            _globalEventsService.Subscribe<SelectableBoxEventArgs>(SelectableBoxSelected);
            _userInterfaceComposingService.SetCurrentSaveCommand(SaveCommand, $"Сохранить матрицу GOOSE устройства {_device.Name}", false);
            _userInterfaceComposingService.AddGlobalCommand(UpdateCommand, $"Обновить GOOSE-матрицу устройства {_device.Name}", IconsKeys.UpdateIconKey, false, true);
            base.OnActivate();
        }

        public override void OnDeactivate()
        {
            _userInterfaceComposingService.ClearCurrentSaveCommand();
            _userInterfaceComposingService.DeleteGlobalCommand(UpdateCommand);
            _globalEventsService.Unsubscribe<SelectableBoxEventArgs>(SelectableBoxSelected);
            base.OnDeactivate();
        }

        protected override void OnDisposing()
        {
            Task.Run(() =>
            {
                OnDeactivate();
                foreach (GooseControlBlockViewModel gooseControlBlockViewModel in GooseControlBlockViewModels)
                {
                    gooseControlBlockViewModel.Dispose();
                }
                _saveCheckingService.RemoveSaveCheckingEntityByOwner(_regionName);
                base.OnDisposing();
            });
        }

        #endregion

        #endregion
    }
}