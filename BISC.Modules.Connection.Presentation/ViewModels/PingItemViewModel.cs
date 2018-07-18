using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Connection.Presentation.Interfaces.Ping;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BISC.Modules.Connection.Presentation.ViewModels
{
    public class PingItemViewModel : ViewModelBase, IPingItemViewModel
    {
        #region private filds
        private string _ip;
        private IPingService _pingService;
        private bool _isPing;
        private ICommandFactory _commandFactory;

        #endregion

        #region Citor
        public PingItemViewModel(IPingService pingService, ICommandFactory commandFactory)
        {
            _pingService = pingService;
            _commandFactory = commandFactory;

            PingCommand = _commandFactory.CreatePresentationCommand(OnPingCommand);
            DeleteItemComand = _commandFactory.CreatePresentationCommand(OnDeleteItemComand);
            ItemClickCommand = _commandFactory.CreatePresentationCommand(OnItemClickCommand);
        }
        #endregion

        #region private metods
        private void OnItemClickCommand()
        {
            try
            {
                SetAsSelectedIP.Invoke(IP);
            }
            catch
            {
                throw new Exception("Не задан делегат SetAsSelectedIP!");
            }
        }

        private async void OnPingCommand()
        {
            IsPing = await _pingService.GetPing(IP);
        }

        private void OnDeleteItemComand()
        {
            // Разберись как работает Dispose по приложению.
            this.Dispose();
        }

        #endregion

        #region Implementation of IPingItemViewModel
        public string IP
        {
            get { return _ip; }
            set
            {
                _ip = value;
                OnPropertyChanged();
            }
        }

        public bool IsPing
        {
            get { return _isPing; }
            set
            {
                _isPing = value;
                OnPropertyChanged();
            }
        }

        public Action<string> SetAsSelectedIP { get; set; }

        public ICommand ItemClickCommand { get; }
        public ICommand DeleteItemComand { get; }
        public ICommand PingCommand { get; }
        #endregion
    }
}
