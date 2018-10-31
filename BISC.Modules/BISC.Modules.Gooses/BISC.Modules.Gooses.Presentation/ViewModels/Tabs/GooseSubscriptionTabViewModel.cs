using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Presentation.ViewModels.Subscription;
using BISC.Presentation.BaseItems.Behaviors.DataTable;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Tabs
{
    public class GooseSubscriptionTabViewModel : NavigationViewModelBase
    {
        private readonly IDeviceModelService _deviceModelService;
        private readonly IBiscProject _biscProject;
        private readonly IGoosesModelService _goosesModelService;
        private readonly ICommandFactory _commandFactory;
        private IDevice _device;
        private DataTable _gooseSubscriptionTable=new DataTable();
        private List<IGooseControl> _gooseControls;

        public GooseSubscriptionTabViewModel(IDeviceModelService deviceModelService,IBiscProject biscProject,IGoosesModelService goosesModelService,ICommandFactory commandFactory)
        {
            _deviceModelService = deviceModelService;
            _biscProject = biscProject;
            _goosesModelService = goosesModelService;
            _commandFactory = commandFactory;
            SaveChangesCommand = _commandFactory.CreatePresentationCommand(OnSaveChanges);
        }

        private void OnSaveChanges()
        {
            var allDevices=_deviceModelService.GetDevicesFromModel(_biscProject.MainSclModel.Value);
          //  _gooseSubscriptionTable.AcceptChanges();
          var table = GooseSubscriptionTable.ToTable();
            for (int rowIndex = 0; rowIndex < table.Rows.Count; rowIndex++)
            {
                for (int colimnIndex= 1; colimnIndex < table.Columns.Count; colimnIndex++)
                {
                    var deviceName = table.Columns[colimnIndex].Caption;
                    var device = allDevices.First((device1 => device1.Name == deviceName));

                    var isSubscribed = table.Rows[rowIndex].ItemArray[colimnIndex];

                    if (isSubscribed is bool)
                    {
                        var gooseControlName = table.Rows[rowIndex][0].ToString();
                        var gooseControl = _gooseControls.First((control => control.Name == gooseControlName));
                        _goosesModelService.SetGooseControlSubscriber((bool) isSubscribed,gooseControl,device);
                    }
                }
            }
        }

        public ICommand SaveChangesCommand { get; }

        protected override void OnDisposing()
        {
            base.OnDisposing();
        }

        public DataView GooseSubscriptionTable => _gooseSubscriptionTable.DefaultView;
        #region Overrides of NavigationViewModelBase

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>("IED");

            var devicesInProject = _deviceModelService.GetDevicesFromModel(_biscProject.MainSclModel.Value);
            _gooseSubscriptionTable.Columns.Add(new DataColumn("GooseControl", typeof(string)));
            _gooseSubscriptionTable.Columns[0].ReadOnly = true;
            foreach (var deviceInProject in devicesInProject)
            {
                if (deviceInProject == _device) continue;
                _gooseSubscriptionTable.Columns.Add(new DataColumn(deviceInProject.Name, typeof(bool)));
            }
            _gooseControls=  _goosesModelService.GetGooseControlsOfDevice(_device);
            foreach (var gooseControl in _gooseControls)
            {
                var rowValues=new List<object>();
                rowValues.Add(gooseControl.Name);
                foreach (var device in devicesInProject)
                {
                    if(device==_device)continue;
                    rowValues.Add(gooseControl.SubscriberDevice.Any((subscriberDevice =>subscriberDevice.DeviceName==device.Name)));
                }
                _gooseSubscriptionTable.Rows.Add(rowValues.ToArray());
            }
            base.OnNavigatedTo(navigationContext);
        }

        protected override void OnNavigatedFrom(BiscNavigationContext navigationContext)
        {
            base.OnNavigatedFrom(navigationContext);
        }

        #endregion
    }
}
