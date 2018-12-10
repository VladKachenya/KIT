using System.Collections.ObjectModel;
using System.Windows.Input;
using BISC.Infrastructure.Global.IoC;
using BISC.Modules.Connection.Presentation.Interfaces.ViewModel.ChangeIpNetworkCard;
using BISC.Presentation.BaseItems.Commands;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;

namespace BISC.Modules.Connection.Presentation.ViewModels.ChangeIpNetworkCard
{
    public class ChangeIpNetworkCardViewModel : ComplexViewModelBase, IChangeIpNetworkCardViewModel
    {
        private IInjectionContainer _injectionContainer;
        private ObservableCollection<ICurrentCardConfigurationViewModel> _currentCardConfigurationViewModels;
        private ICurrentCardConfigurationViewModel _sellectedCardConfigurationViewModel;
        private INetworkCardSettingsViewModel _sellectedNetworkCardSettingsViewModel;

        #region Ctor
        public ChangeIpNetworkCardViewModel(ICommandFactory commandFactory, IInjectionContainer injectionContainer)
        {

            NetworkCardSettingsViewModels = new ObservableCollection<INetworkCardSettingsViewModel>();
            _injectionContainer = injectionContainer;
            AddNewNetworkCardSettingsViewModelCommand =
                commandFactory.CreatePresentationCommand(OnAddNewNetworkCardSettingsViewModel);
            RemoveNetworkCardSettingsViewModelCommand =
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
            var newSettings = _injectionContainer.ResolveType<INetworkCardSettingsViewModel>();
            NetworkCardSettingsViewModels.Add(newSettings);
            _sellectedNetworkCardSettingsViewModel = newSettings;
        }

        private void OnRemoveNetworkCardSettingsViewModel()
        {
            NetworkCardSettingsViewModels.Remove(_sellectedNetworkCardSettingsViewModel);
        }

        #endregion

        #region  Implementation of IChangeIpNetworkCardViewModel

        public ObservableCollection<ICurrentCardConfigurationViewModel> CurrentCardConfigurationViewModels
        {
            get => _currentCardConfigurationViewModels;
            set
            {
                if (Equals(value, _currentCardConfigurationViewModels)) return;
                _currentCardConfigurationViewModels = value;
                OnPropertyChanged();
            }
        }

        public ICurrentCardConfigurationViewModel SellectedCardConfigurationViewModel
        {
            get => _sellectedCardConfigurationViewModel;
            set
            {
                if (Equals(value, _sellectedCardConfigurationViewModel)) return;
                _sellectedCardConfigurationViewModel = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<INetworkCardSettingsViewModel> NetworkCardSettingsViewModels { get; }

        public INetworkCardSettingsViewModel SellectedNetworkCardSettingsViewModel
        {
            get => _sellectedNetworkCardSettingsViewModel;
            set
            {
                if (Equals(value, _sellectedNetworkCardSettingsViewModel)) return;
                _sellectedNetworkCardSettingsViewModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand CloseCommand { get; }
        public ICommand AddNewNetworkCardSettingsViewModelCommand { get; }
        public ICommand RemoveNetworkCardSettingsViewModelCommand { get; }

        #endregion
    }
}