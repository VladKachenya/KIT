using BISC.Infrastructure.Global.Services;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Connection.Presentation.Interfaces.Factorys;
using BISC.Modules.Connection.Presentation.Interfaces.Ping;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Commands;
using BISC.Presentation.Infrastructure.Factories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Modules.Connection.Presentation.Events;
using BISC.Modules.Connection.Presentation.Interfaces.ViewModel;
using BISC.Presentation.BaseItems.Commands;
using BISC.Presentation.Infrastructure.Navigation;

namespace BISC.Modules.Connection.Presentation.ViewModels
{
    public class PingViewModel : ComplexViewModelBase, IPingViewModel
    {
        #region private filds
        private ICommandFactory _commandFactory;
        private IIpAddressViewModelFactory _ipAddressViewModelFactory;
        private IIpValidationService _ipValidationService;
        private bool _pingAllCanExecute = true;
        #endregion

        #region C-tor
        public PingViewModel(ICommandFactory commandFactory, IIpAddressViewModelFactory ipAddressViewModelFactory, IIpValidationService ipValidationService,
            LastIpAddressesViewModel lastIpAddressesViewModel)
        {

            _ipValidationService = ipValidationService;
            _ipAddressViewModelFactory = ipAddressViewModelFactory;
            this.LastIpAddressesViewModel = lastIpAddressesViewModel;
            CurrentAddressViewModel = _ipAddressViewModelFactory.GetPingItemViewModel("", false);
            this.LastIpAddressesViewModel.CurrentAddressViewModel = CurrentAddressViewModel; // Необходимо задать обязательно
            _commandFactory = commandFactory;
            PingAllCommand = _commandFactory.CreatePresentationCommand(OnPingAllCommand, () => _pingAllCanExecute); 
            CloseCommand = commandFactory.CreatePresentationCommand((() =>
            {
                DialogCommands.CloseDialogCommand.Execute(null, null);
                Dispose();
            }));
            
        }
        #endregion

        #region private methods
        private async void OnPingAllCommand()
        {
            try
            {
                _pingAllCanExecute = false;
                (PingAllCommand as IPresentationCommand).RaiseCanExecute();
                List<IIpAddressViewModel> items = new List<IIpAddressViewModel>( this.LastIpAddressesViewModel.LastIpAddresses);

                foreach (var connection in items)
                {
                    await connection.PingAsync();
                }
                items.Clear();
            }
            finally
            {
                _pingAllCanExecute = true;
                (PingAllCommand as IPresentationCommand).RaiseCanExecute();
            }
            //Task[] tasks = new Task[LastConnections.Count];
            //for (int i = 0; i < LastConnections.Count; i++)
            //    tasks[i] = LastConnections[i].OnPing();
            //await Task.WhenAll(tasks);
        }
        #endregion

        #region Implementation of IPingViewModel


        public IIpAddressViewModel CurrentAddressViewModel { get; }

        public ICommand PingAllCommand { get; }
        public ICommand CloseCommand { get; }

        public ILastIpAddressesViewModel LastIpAddressesViewModel { get; }


        #endregion

        #region Overrides of ViewModelBase

        protected override void OnDisposing()
        {
            (LastIpAddressesViewModel as ComplexViewModelBase).Dispose();
            base.OnDisposing();
        }

        #endregion
    }
}
