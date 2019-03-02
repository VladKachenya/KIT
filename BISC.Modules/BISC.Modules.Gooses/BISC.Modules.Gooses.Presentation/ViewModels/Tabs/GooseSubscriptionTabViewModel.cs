using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Presentation.ViewModels.Subscriptions;
using BISC.Modules.InformationModel.Infrastucture.Elements;
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
        private readonly IProjectService _projectService;
        private List<IDevice> _devices;
        private DataTable _gooseSubscriptionTable = new DataTable();


        public GooseSubscriptionTabViewModel(IDeviceModelService deviceModelService, IBiscProject biscProject, IGoosesModelService goosesModelService, ICommandFactory commandFactory, IProjectService projectService)
        {
            _deviceModelService = deviceModelService;
            _biscProject = biscProject;
            _goosesModelService = goosesModelService;
            _commandFactory = commandFactory;
            _projectService = projectService;
            SaveChangesCommand = _commandFactory.CreatePresentationCommand(OnSaveChanges);
        }

        private void OnSaveChanges()
        {
            var allDevices = _deviceModelService.GetDevicesFromModel(_biscProject.MainSclModel.Value);
            //  _gooseSubscriptionTable.AcceptChanges();
            var table = GooseSubscriptionTable.ToTable();
            for (int rowIndex = 0; rowIndex < table.Rows.Count; rowIndex++)
            {
                for (int colimnIndex = 1; colimnIndex < table.Columns.Count; colimnIndex++)
                {
                    var deviceName = table.Columns[colimnIndex].Caption;
                    // Тут необходимо делать поиск по Guid
                    var device = allDevices.First((device1 => device1.Name == deviceName));
                    var rowValuew = table.Rows[rowIndex].ItemArray[colimnIndex];

                    if (rowValuew is SubscriptionValue subscriptionValue && subscriptionValue.IsValueEditable && subscriptionValue.IsSelected.HasValue)
                    {
                        var rowHeader = table.Rows[rowIndex][0].ToString();
                        var deviceNameForRow = rowHeader.Split('.')[0];
                        // Тут должен быть Guid
                        var deviceForRow = _devices.FirstOrDefault((d => d.Name == deviceNameForRow));
                        var gooseControlName = rowHeader.Split('.')[1];
                        var gooseControlsForRow = _goosesModelService.GetGooseControlsOfDevice(deviceForRow);
                        var gooseControl = gooseControlsForRow.First((control => control.Name == gooseControlName));
                        _goosesModelService.SetGooseControlSubscriber(subscriptionValue.IsSelected.Value, gooseControl, device);
                    }
                }
            }

            Task.Run((() => _projectService.SaveCurrentProject()));
        }

        public ICommand SaveChangesCommand { get; }

        protected override void OnDisposing()
        {
            base.OnDisposing();
        }

        public DataView GooseSubscriptionTable => _gooseSubscriptionTable.DefaultView;
        #region Overrides of NavigationViewModelBase

        protected override async void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _devices = _deviceModelService.GetDevicesFromModel(_biscProject.MainSclModel.Value);
            await LoadSubscriptions();
            base.OnNavigatedTo(navigationContext);
        }


        private async Task LoadSubscriptions()
        {
            _gooseSubscriptionTable = new DataTable();


            var devicesInProject = _deviceModelService.GetDevicesFromModel(_biscProject.MainSclModel.Value);
            _gooseSubscriptionTable.Columns.Add(new DataColumn("GooseControl", typeof(string)));
            _gooseSubscriptionTable.Columns[0].ReadOnly = true;
            foreach (var deviceInProject in devicesInProject)
            {
                _gooseSubscriptionTable.Columns.Add(new DataColumn(deviceInProject.Name, typeof(SubscriptionValue)));
            }

            var isSavingNeeded = false;
            foreach (var deviceInProject in devicesInProject)
            {
                var gooseControls = _goosesModelService.GetGooseControlsOfDevice(deviceInProject);
                foreach (var gooseControl in gooseControls)
                {

                    var goooseMatrixFtpForDevice = _goosesModelService.GetGooseMatrixFtpForDevice(deviceInProject);
                    var rowValues = new List<object>();
                    rowValues.Add(deviceInProject.Name + "." + gooseControl.Name);
                    foreach (var deviceInnerIteration in devicesInProject)
                    {
                        if (deviceInnerIteration == deviceInProject)
                        {
                            rowValues.Add(new SubscriptionValue(null, false)); // самого на себя не подписывать
                            continue;
                        }

                        var isSubscribed = false;
                        if (gooseControl.SubscriberDevice.Any((subscriberDevice =>
                            subscriberDevice.DeviceName == deviceInnerIteration.Name)))
                        {
                            isSubscribed = true;
                        }
                        else
                        {
                            if (goooseMatrixFtpForDevice != null)
                            {
                                var ldOfGooseControl = gooseControl.GetFirstParentOfType<ILDevice>().Inst;
                                isSubscribed = goooseMatrixFtpForDevice.GoCbFtpEntities.Any((gocbRef =>
                                      GoReferenceConformityCheck(gocbRef.GoCbReference, deviceInnerIteration.Name,
                                          gooseControl.Name, ldOfGooseControl)));
                                if (isSubscribed)
                                    isSavingNeeded = true;

                            }
                        }

                        rowValues.Add(new SubscriptionValue(isSubscribed));
                    }
                    _gooseSubscriptionTable.Rows.Add(rowValues.ToArray());
                }
            }

            if (isSavingNeeded)
            {
                SaveChangesCommand?.Execute(null);
            }
        }

        private bool GoReferenceConformityCheck(string referenceValue, string deviceName, string gooseName, string ldNameOfGoose)
        {
            var referenceValueParts = referenceValue.Split('/', '$');
            if (referenceValueParts.Last() != gooseName) return false;
            return referenceValueParts.First() == deviceName + ldNameOfGoose;
        }

        protected override void OnNavigatedFrom(BiscNavigationContext navigationContext)
        {
            base.OnNavigatedFrom(navigationContext);
        }

        #endregion
    }
}
