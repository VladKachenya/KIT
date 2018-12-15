using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Connection.Presentation.Interfaces.Factorys.ChangeIpNetworkCard;
using BISC.Modules.Connection.Presentation.Interfaces.ViewModel.ChangeIpNetworkCard;
using BISC.Presentation.BaseItems.Commands;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;

namespace BISC.Modules.Connection.Presentation.ViewModels.ChangeIpNetworkCard
{
    public class ChangeIpNetworkCardViewModel : ComplexViewModelBase, IChangeIpNetworkCardViewModel
    {
        private readonly ICustomIpSettingsViewModelFactory _customIpSettingsViewModelFactory;
        private ObservableCollection<ICurrentCardConfigurationViewModel> _currentCardConfigurationViewModels;
        private ICustomIpSettingsViewModel _sellectedCustomIpSettingsViewModel;

        #region Ctor
        public ChangeIpNetworkCardViewModel(ICommandFactory commandFactory, ICustomIpSettingsViewModelFactory customIpSettingsViewModelFactory)
        {
            _customIpSettingsViewModelFactory = customIpSettingsViewModelFactory;

            CustomIpSettingsViewModels = new ObservableCollection<ICustomIpSettingsViewModel>();
            AddNewCustomIpSettingsCommand =
                commandFactory.CreatePresentationCommand(OnAddNewNetworkCardSettingsViewModel);
            RemoveCustomIpSettingsCommand =
                commandFactory.CreatePresentationCommand(OnRemoveNetworkCardSettingsViewModel);
            CloseCommand = commandFactory.CreatePresentationCommand((() =>
            {
                DialogCommands.CloseDialogCommand.Execute(null, null);
                Dispose();
            }));
        }
        #endregion

        #region private methods

        private void OnAddNewNetworkCardSettingsViewModel()
        {
            var newSettings =
                _customIpSettingsViewModelFactory.CreateCustomIpSettingsViewModel(CustomIpSettingsViewModels.Select(element => element.SettingsNamе).ToList());
            CustomIpSettingsViewModels.Add(newSettings);
            SellectedCustomIpSettingsViewModel = newSettings;
        }

        private void OnRemoveNetworkCardSettingsViewModel()
        {
            CustomIpSettingsViewModels.Remove(_sellectedCustomIpSettingsViewModel);
        }

        #endregion

        #region  Implementation of IChangeIpNetworkCardViewModel

        public ObservableCollection<ICurrentCardConfigurationViewModel> CurrentCardConfigurationViewModels
        {
            get => _currentCardConfigurationViewModels;
            set
            {
                // Тут потом поправить
                if (Equals(value, _currentCardConfigurationViewModels)) return;
                _currentCardConfigurationViewModels = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ICustomIpSettingsViewModel> CustomIpSettingsViewModels { get; }

        public ICustomIpSettingsViewModel SellectedCustomIpSettingsViewModel
        {
            get => _sellectedCustomIpSettingsViewModel;
            set => SetProperty(ref _sellectedCustomIpSettingsViewModel, value);
        }

        public ICommand CloseCommand { get; }
        public ICommand AddNewCustomIpSettingsCommand { get; }
        public ICommand RemoveCustomIpSettingsCommand { get; }

        #endregion
    }
}