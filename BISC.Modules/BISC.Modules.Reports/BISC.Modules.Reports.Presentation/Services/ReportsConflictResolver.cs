using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.DataSets.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.HelpClasses;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Reports.Infrastructure.Keys;
using BISC.Modules.Reports.Infrastructure.Presentation.Factorys;
using BISC.Modules.Reports.Infrastructure.Presentation.Services;
using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels;
using BISC.Modules.Reports.Infrastructure.Services;
using BISC.Modules.Reports.Presentation.ViewModels.Helpers;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Reports.Presentation.Services
{
   public class ReportsConflictResolver: IElementConflictResolver
    {
        private readonly IReportsModelService _reportsModelService;
        private readonly IDeviceModelService _deviceModelService;
        private readonly INavigationService _navigationService;
        private readonly IReportControlFactoryViewModel _reportControlFactoryViewModel;
        private readonly IReportsSavingService _reportsSavingService;
        private readonly IConnectionPoolService _connectionPoolService;
        public ConflictType ConflictType => ConflictType.ManualResolveNeeded;
        public ReportsConflictResolver(IReportsModelService reportsModelService,IDeviceModelService deviceModelService,INavigationService navigationService,
            IReportControlFactoryViewModel reportControlFactoryViewModel,IReportsSavingService reportsSavingService,IConnectionPoolService connectionPoolService)
        {
            _reportsModelService = reportsModelService;
            _deviceModelService = deviceModelService;
            _navigationService = navigationService;
            _reportControlFactoryViewModel = reportControlFactoryViewModel;
            _reportsSavingService = reportsSavingService;
            _connectionPoolService = connectionPoolService;
        }






        #region Implementation of IElementConflictResolver

        public string ConflictName => "Report Controls";
        public bool GetIfConflictsExists(string deviceName, ISclModel sclModelInDevice, ISclModel sclModelInProject)
        {
            var deviceInsclModelInDevice = _deviceModelService.GetDeviceByName(sclModelInDevice, deviceName);
            var devicesclModelInProject = _deviceModelService.GetDeviceByName(sclModelInProject, deviceName);

            var reportsInDevice = _reportsModelService.GetAllReportControlsOfDevice(deviceInsclModelInDevice);
            var reportsInProject = _reportsModelService.GetAllReportControlsOfDevice(devicesclModelInProject);
            if (reportsInProject.Count != reportsInDevice.Count)
            {
                return true;
            }

            foreach (var reportInDevice in reportsInDevice)
            {
                var reportInProject = reportsInProject.FirstOrDefault((control =>control.Name == reportInDevice.Name));
                if (reportInProject == null) return true;
                if (!reportInProject.ModelElementCompareTo(reportInDevice))
                {
                    return true;
                }
            }




            return false;

        }

        public async Task<ResolvingResult> ResolveConflict(bool isFromDevice, string deviceName, ISclModel sclModelInDevice, ISclModel sclModelInProject)
        {
            var deviceInsclModelInDevice = _deviceModelService.GetDeviceByName(sclModelInDevice, deviceName);
            var devicesclModelInProject = _deviceModelService.GetDeviceByName(sclModelInProject, deviceName);
            var reportsInDevice = _reportsModelService.GetAllReportControlsOfDevice(deviceInsclModelInDevice);
            var reportsInProject = _reportsModelService.GetAllReportControlsOfDevice(devicesclModelInProject);

            var reportViewmodelsInDevice = _reportControlFactoryViewModel.GetReportControlsViewModel(reportsInDevice, deviceInsclModelInDevice);
            var reportViewmodelsInProject = _reportControlFactoryViewModel.GetReportControlsViewModel(reportsInProject, devicesclModelInProject);
            foreach (var reportControlViewModel in reportViewmodelsInProject)
            {
               reportControlViewModel.ActivateElement(); 
            }
            foreach (var reportControlViewModel in reportViewmodelsInDevice)
            {
                reportControlViewModel.ActivateElement();
            }
            var deviceOnlyReportViewModels = GetDeviceOnlyReportViewModels(reportViewmodelsInProject,reportViewmodelsInDevice);
            var projectOnlyReportViewModels = GetProjectOnlyReportViewModels(reportViewmodelsInProject, reportViewmodelsInDevice);

            deviceOnlyReportViewModels.ForEach((report => reportViewmodelsInDevice.FirstOrDefault((model => model.Name == report.Name))?.ChangeTracker.SetModified()));
            projectOnlyReportViewModels.ForEach((report => reportViewmodelsInProject.FirstOrDefault((model => model.Name == report.Name))?.ChangeTracker.SetModified()));


            SavingResultEnum savingResult;
            if (isFromDevice)
            {
                deviceOnlyReportViewModels.ForEach((model =>model.ChangeTracker.SetModified() ));
               savingResult = await _reportsSavingService.SaveReportsAsync(reportViewmodelsInDevice.ToList(), devicesclModelInProject, false);
               
            }
            else
            {
                projectOnlyReportViewModels.ForEach((model => model.ChangeTracker.SetModified()));

                savingResult = await _reportsSavingService.SaveReportsAsync(reportViewmodelsInProject.ToList(), deviceInsclModelInDevice, _connectionPoolService.GetConnection(devicesclModelInProject.Ip).IsConnected);

            }

            if (savingResult == SavingResultEnum.SavedUsingFtp)
            {
                return new ResolvingResult(){IsRestartNeeded = true};
            }
            return ResolvingResult.SucceedResult;
        }

        public void ShowConflicts(string deviceName, ISclModel sclModelInDevice, ISclModel sclModelInProject)
        {
            var deviceInsclModelInDevice = _deviceModelService.GetDeviceByName(sclModelInDevice, deviceName);
            var devicesclModelInProject = _deviceModelService.GetDeviceByName(sclModelInProject, deviceName);
            var reportsInDevice = _reportsModelService.GetAllReportControlsOfDevice(deviceInsclModelInDevice);
            var reportsInProject = _reportsModelService.GetAllReportControlsOfDevice(devicesclModelInProject);

            var reportViewmodelsInDevice = _reportControlFactoryViewModel.GetReportControlsViewModel(reportsInDevice, deviceInsclModelInDevice);
            var reportViewmodelsInProject = _reportControlFactoryViewModel.GetReportControlsViewModel(reportsInProject, devicesclModelInProject);
            foreach (var reportControlViewModel in reportViewmodelsInProject)
            {
                reportControlViewModel.ActivateElement();
            }
            foreach (var reportControlViewModel in reportViewmodelsInDevice)
            {
                reportControlViewModel.ActivateElement();
            }
            var deviceOnlyReportViewModels = GetDeviceOnlyReportViewModels(reportViewmodelsInProject, reportViewmodelsInDevice);
            var projectOnlyReportViewModels = GetProjectOnlyReportViewModels(reportViewmodelsInProject, reportViewmodelsInDevice);


            deviceOnlyReportViewModels.ForEach((report => reportViewmodelsInDevice.FirstOrDefault((model => model.Name == report.Name))?.ChangeTracker.SetModified()));
            projectOnlyReportViewModels.ForEach((report => reportViewmodelsInProject.FirstOrDefault((model => model.Name == report.Name))?.ChangeTracker.SetModified()));

            _navigationService.OpenInWindow(ReportsKeys.ReportsPresentationKeys.ReportsConflictsWindow, $"Конфликты в блоках управления отчетами в устройстве {deviceName}",
                new BiscNavigationParameters().AddParameterByName(ReportsKeys.ReportsPresentationKeys.ReportsConflictsContext,
                    new ReportsConflictsContext(reportViewmodelsInDevice, reportViewmodelsInProject)));

        }

        private List<IReportControlViewModel> GetDeviceOnlyReportViewModels(ObservableCollection<IReportControlViewModel> reportViewmodelsInProject, ObservableCollection<IReportControlViewModel> reportViewmodelsInDevice)
        {
            List<IReportControlViewModel> deviceOnlyReportControlViewModels = new List<IReportControlViewModel>();

            foreach (var reportControlViewModelInDevice in reportViewmodelsInDevice)
            {
                if (!reportViewmodelsInProject.Any((model =>
                    model.Model.ModelElementCompareTo(reportControlViewModelInDevice.Model))))
                {
                    deviceOnlyReportControlViewModels.Add(reportControlViewModelInDevice);
                }
            }
            return deviceOnlyReportControlViewModels;
        }
        private List<IReportControlViewModel> GetProjectOnlyReportViewModels(ObservableCollection<IReportControlViewModel> reportViewmodelsInProject, ObservableCollection<IReportControlViewModel> reportViewmodelsInDevice)
        {
            List<IReportControlViewModel> projectOnlyReportControlViewModels = new List<IReportControlViewModel>();

            foreach (var reportControlViewModelInProject in reportViewmodelsInProject)
            {
                if (!reportViewmodelsInDevice.Any((model =>
                    model.Model.ModelElementCompareTo(reportControlViewModelInProject.Model))))
                {
                    projectOnlyReportControlViewModels.Add(reportControlViewModelInProject);
                }
            }
            return projectOnlyReportControlViewModels;
        }
        #endregion
    }
}
