using System;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.HelpClasses;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Reports.Infrastructure.Keys;
using BISC.Modules.Reports.Infrastructure.Presentation.Factorys;
using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels;
using BISC.Modules.Reports.Infrastructure.Services;
using BISC.Modules.Reports.Presentation.Commands;
using BISC.Modules.Reports.Presentation.ViewModels.Helpers;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BISC.Modules.Reports.Presentation.Services
{
    public class ReportsConflictResolver : IElementConflictResolver
    {
        private readonly IReportsModelService _reportsModelService;
        private readonly IDeviceModelService _deviceModelService;
        private readonly INavigationService _navigationService;
        private readonly IReportControlFactoryViewModel _reportControlFactoryViewModel;
        private readonly ReportsSavingCommand _reportsSavingCommand;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly ReportsSavingService _reportsSavingService;
        public ConflictType ConflictType => ConflictType.ManualResolveNeeded;
        public ReportsConflictResolver(IReportsModelService reportsModelService, IDeviceModelService deviceModelService, INavigationService navigationService,
            IReportControlFactoryViewModel reportControlFactoryViewModel, ReportsSavingCommand reportsSavingCommand, IConnectionPoolService connectionPoolService, ReportsSavingService reportsSavingService)
        {
            _reportsModelService = reportsModelService;
            _deviceModelService = deviceModelService;
            _navigationService = navigationService;
            _reportControlFactoryViewModel = reportControlFactoryViewModel;
            _reportsSavingCommand = reportsSavingCommand;
            _connectionPoolService = connectionPoolService;
            _reportsSavingService = reportsSavingService;
        }

        #region Implementation of IElementConflictResolver

        public string ConflictName => "Report Controls";
        public bool GetIfConflictsExists(Guid deviceGuid, ISclModel sclModelInDevice, ISclModel sclModelInProject)
        {
            var deviceInsclModelInDevice = _deviceModelService.GetDeviceByGuid(sclModelInDevice, deviceGuid);
            var devicesclModelInProject = _deviceModelService.GetDeviceByGuid(sclModelInProject, deviceGuid);

            var reportsInDevice = _reportsModelService.GetAllReportControlsOfDevice(deviceInsclModelInDevice);
            var reportsInProject = _reportsModelService.GetAllReportControlsOfDevice(devicesclModelInProject);
            if (reportsInProject.Count != reportsInDevice.Count)
            {
                return true;
            }

            foreach (var reportInDevice in reportsInDevice)
            {
                var reportInProject = reportsInProject.FirstOrDefault((control => control.Name == reportInDevice.Name));
                if (reportInProject == null)
                {
                    return true;
                }

                if (!reportInProject.ModelElementCompareTo(reportInDevice))
                {
                    return true;
                }
            }
            return false;

        }

        public async Task<ResolvingResult> ResolveConflict(bool isFromDevice, Guid deviceGuid, ISclModel sclModelInDevice, ISclModel sclModelInProject)
        {
            var deviceInsclModelInDevice = _deviceModelService.GetDeviceByGuid(sclModelInDevice, deviceGuid);
            var devicesclModelInProject = _deviceModelService.GetDeviceByGuid(sclModelInProject, deviceGuid);

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


            if (isFromDevice)
            {
                _reportsModelService.DeleteAllReportsOfDevice(devicesclModelInProject);
                _reportsModelService.AddReportsToDevice(devicesclModelInProject, reportsInDevice);
                //deviceOnlyReportViewModels.ForEach((model => model.ChangeTracker.SetModified()));
                //_reportsSavingCommand.Initialize(ref reportViewmodelsInDevice, devicesclModelInProject);
                //await _reportsSavingCommand.SaveAsync();

            }
            else
            {
                projectOnlyReportViewModels.ForEach((model => model.ChangeTracker.SetModified()));
                _reportsSavingCommand.Initialize(ref reportViewmodelsInProject, deviceInsclModelInDevice);
                await _reportsSavingCommand.SaveAsync();
                await _reportsSavingService.Save(devicesclModelInProject);
            }


            if (!isFromDevice)
            {
                return new ResolvingResult() { IsRestartNeeded = true };
            }
            return ResolvingResult.SucceedResult;
        }

        public void ShowConflicts(Guid deviceGuid, ISclModel sclModelInDevice, ISclModel sclModelInProject)
        {
            var deviceInsclModelInDevice = _deviceModelService.GetDeviceByGuid(sclModelInDevice, deviceGuid);
            var devicesclModelInProject = _deviceModelService.GetDeviceByGuid(sclModelInProject, deviceGuid);
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

            _navigationService.OpenInWindow(ReportsKeys.ReportsPresentationKeys.ReportsConflictsWindow, $"Конфликты в блоках управления отчетами в устройстве {devicesclModelInProject.Name}",
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
