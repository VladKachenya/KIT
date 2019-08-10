using BISC.Infrastructure.Global.Common;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Constants;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Presentation.BaseItems.Common;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Infrastructure.UiFromModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BISC.Presentation.Infrastructure.HelperEntities;

namespace BISC.Presentation.Services
{
    public class ProjectManagementService : IProjectManagementService
    {
        #region private filds
        private readonly IProjectService _projectService;
        private readonly ILoggingService _loggingService;
        private readonly IUiFromModelElementRegistryService _uiFromModelElementRegistryService;
        private readonly IDeviceModelService _deviceModelService;
        private readonly IBiscProject _biscProject;
        private readonly ITreeManagementService _treeManagementService;
        private readonly IGoosesModelService _goosesModelService;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly ITabManagementService _tabManagementService;
        private readonly IDeviceWarningsService _deviceWarningsService;
        private readonly IUserInteractionService _userInteractionService;
        private readonly IGlobalSavingService _globalSavingService;
        #endregion

        #region ctor
        public ProjectManagementService(IProjectService projectService, ISaveCheckingService saveCheckingService, ILoggingService loggingService,
            IUiFromModelElementRegistryService uiFromModelElementRegistryService, IBiscProject biscProject, IDeviceModelService deviceModelService,
            ITreeManagementService treeManagementService, IGoosesModelService goosesModelService, IConnectionPoolService connectionPoolService,
            ITabManagementService tabManagementService, IDeviceWarningsService deviceWarningsService, IUserInteractionService userInteractionService,
            IGlobalSavingService globalSavingService)
        {
            _projectService = projectService;
            _loggingService = loggingService;
            _uiFromModelElementRegistryService = uiFromModelElementRegistryService;
            _biscProject = biscProject;
            _deviceModelService = deviceModelService;
            _treeManagementService = treeManagementService;
            _goosesModelService = goosesModelService;
            _connectionPoolService = connectionPoolService;
            _tabManagementService = tabManagementService;
            _deviceWarningsService = deviceWarningsService;
            _userInteractionService = userInteractionService;
            _globalSavingService = globalSavingService;
        }
        #endregion

        #region Implementation of IProjectManagementService
        public async void SaveProject()
        {
            await SaveProjectAsync();
        }

        public void DeleteDeviceFromProject(Guid deviceGuid)
        {
            var device = _deviceModelService.GetDeviceByGuid(_biscProject.MainSclModel.Value, deviceGuid);
            var deviceTreeItemIdentifier = _treeManagementService.GetDeviceTreeItem(deviceGuid);
            if (device == null)
            {
                throw new ArgumentException("Device by guid not found");
            }
            if (deviceTreeItemIdentifier == null)
            {
                throw new ArgumentException("Device tree item by device guid not found");
            }

            var result = _deviceModelService.DeleteDeviceFromModel(_biscProject.MainSclModel.Value, device.DeviceGuid);

            if (result.IsSucceed)
            {
                _goosesModelService.DeleteAllDeviceReferencesInGooseControlsInModel(_biscProject,
                    device.Name);
                _connectionPoolService.GetConnection(device.Ip).StopConnection();
            }
            _treeManagementService.DeleteTreeItem(deviceTreeItemIdentifier);
            _tabManagementService.CloseTabWithChildren(deviceTreeItemIdentifier.ItemId.ToString());
            _deviceWarningsService.ClearDeviceWarningsOfDevice(device.DeviceGuid);
        }

        public async void SaveProjectAsAsync()
        {
            await SaveProjectAs();
        }

        public void ClearCurrentProjectAsync()
        {
            var devices = _deviceModelService.GetDevicesFromModel(_biscProject.MainSclModel.Value);
            _treeManagementService.ClearMainTree();
            foreach (var device in devices)
            {
                var result = _deviceModelService.DeleteDeviceFromModel(_biscProject.MainSclModel.Value, device.DeviceGuid);
                if (result.IsSucceed)
                {
                    _goosesModelService.DeleteAllDeviceReferencesInGooseControlsInModel(_biscProject, device.Name);
                    _connectionPoolService.GetConnection(device.Ip).StopConnection();
                    _deviceWarningsService.ClearDeviceWarningsOfDevice(device.DeviceGuid);
                }
            }
        }



        public async void OpenProjectAsAsync()
        {
            var fileMaybe = FileHelper.SelectFileToOpen("Открыть файл с устройствами", "BISC Files (*.bisc)|*.bisc|" +
                                                                                       "All Files (*.*)|*.*");
            if (!fileMaybe.Any())
            {
                return;
            }

            var res = await _userInteractionService.ShowOptionToUser("Сохранение проекта",
                $"При открытии проекта изменения текущего проекта будут утеряны!  \n" +
                $"Не желаете сохранить текущий проект?", new List<string> { "Сохранить", "НЕТ" });
            if (res == 0)
            {
                //await _globalSavingService.SaveAllDevices();
                await SaveProjectAsync(false);
            }

            FileInfo projectToOpen = fileMaybe.GetFirstValue();
            if (!projectToOpen.Exists)
            {
                await _userInteractionService.ShowOptionToUser("Ошибка открытия файла ",
                    $"Фаил {projectToOpen.FullName} не найден", new List<string> { "ОК" });
                _loggingService.LogMessage($"Фаил {projectToOpen.FullName} не найден", SeverityEnum.Warning);
                return;
            }

            ClearCurrentProjectAsync();
            try
            {
                _projectService.OpenProjectAs(fileMaybe.GetFirstValue().FullName);
            }
            catch (Exception e)
            {
                _loggingService.LogMessage(e.Message, SeverityEnum.Warning);
            }
            _uiFromModelElementRegistryService.TryHandleModelElementInUiByKey(_biscProject.MainSclModel.Value, null, "SCL");
            _loggingService.LogMessage($"Проект открыт {_projectService.GetCurrentProjectPath(true)}", SeverityEnum.Info);
        }

        public async void СreateNewProjectAsync()
        {
            var res = await _userInteractionService.ShowOptionToUser("Сохранение проекта",
                $"При создании нового проекта изменения текущего проекта будут утеряны!  \n" +
                $"Не желаете сохранить текущий проект?", new List<string> { "Сохранить", "НЕТ" });
            if (res == 0)
            {
                //await _globalSavingService.SaveAllDevices();
                await SaveProjectAsync(false);
            }
            ClearCurrentProjectAsync();
            _projectService.СreateNewProject();
            _projectService.SetDefaultProjectPath();
            _loggingService.LogMessage($"Проект {_projectService.GetCurrentProjectPath(true)} очищен", SeverityEnum.Info);
        }



        public void OpenDefaultProjectAsync()
        {
            _projectService.OpenDefaultProject();
        }

        #endregion

        #region private methods

        public async Task SaveProjectAsync(bool isReconnectIfNeed = true)
        {
            FileInfo curentProjectPath = new FileInfo(_projectService.GetCurrentProjectPath(true));
            FileInfo defaultProjectPath = new FileInfo(ModelConstants.DefaultProjectPath);
            if (curentProjectPath.FullName == defaultProjectPath.FullName)
            {
                await SaveProjectAs(isReconnectIfNeed);
            }
            else
            {
                try
                {
                    await _globalSavingService.SaveAllDevices(isReconnectIfNeed);
                    _projectService.SaveCurrentProject();
                    _loggingService.LogMessage($"Проект сохранен {_projectService.GetCurrentProjectPath(true)}", SeverityEnum.Info);
                }
                catch (Exception e)
                {
                    _loggingService.LogMessage(e.Message, SeverityEnum.Info);
                    await SaveProjectAs(isReconnectIfNeed);
                }
            }
        }

        private async Task SaveProjectAs(bool isReconnectIfNeed = true)
        {
            Maybe<string> listOfPaths = FileHelper.SelectFilePathToSave("Сохранение файла", ".bisc", "BISC Files (*.bisc)|*.bisc|" +
                                                                                                     "All Files (*.*)|*.*", "New project");
            if (!listOfPaths.Any())
            {
                return;
            }

            FileInfo projectToOpen;
            try
            {
                projectToOpen = new FileInfo(listOfPaths.GetFirstValue());
            }
            catch
            {
                projectToOpen = null;
            }

            if (projectToOpen == null || !projectToOpen.Directory.Exists)
            {
                string defPath = new FileInfo(ModelConstants.DefaultProjectPath).FullName;
                _loggingService.LogMessage($"Проект {_projectService.GetCurrentProjectPath(false)} не может быть сохранён по пути " +
                                           $"{listOfPaths.GetFirstValue()}", SeverityEnum.Warning);
                await _userInteractionService.ShowOptionToUser("Сохранение проекта",
                    $"Проект {_projectService.GetCurrentProjectPath(false)} не может быть сохранён по пути " +
                    $"{listOfPaths.GetFirstValue()}. Проект будет сохранён в {defPath}", new List<string> { "ОК" });
                await _globalSavingService.SaveAllDevices(isReconnectIfNeed);
                _projectService.SaveProjectAs(defPath);
                _loggingService.LogMessage($"Проект сохранен {_projectService.GetCurrentProjectPath(true)}", SeverityEnum.Info);
                return;
            }
            await _globalSavingService.SaveAllDevices(isReconnectIfNeed);
            _projectService.SaveProjectAs(listOfPaths.GetFirstValue());
            _loggingService.LogMessage($"Проект сохранен {_projectService.GetCurrentProjectPath(true)}", SeverityEnum.Info);
        }


        #endregion


    }
}