using System;
using System.Collections.Generic;
using System.IO;
using BISC.Infrastructure.Global.Common;
using BISC.Infrastructure.Global.IoC;
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

namespace BISC.Presentation.Services
{
    public class ProjectManagementService : IProjectManagementService
    {
        private readonly IProjectService _projectService;
        private readonly ISaveCheckingService _saveCheckingService;
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



        public ProjectManagementService(IProjectService projectService, ISaveCheckingService saveCheckingService, ILoggingService loggingService,
            IUiFromModelElementRegistryService uiFromModelElementRegistryService, IBiscProject biscProject, IDeviceModelService deviceModelService,
            ITreeManagementService treeManagementService, IGoosesModelService goosesModelService, IConnectionPoolService connectionPoolService,
            ITabManagementService tabManagementService, IDeviceWarningsService deviceWarningsService, IUserInteractionService userInteractionService)
        {
            _projectService = projectService;
            _saveCheckingService = saveCheckingService;
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
        }

        public async void SaveProject()
        {
            FileInfo curentProjectPath = new FileInfo( _projectService.GetCurrentProjectPath(true));
            FileInfo defaultProjectPath = new FileInfo(ModelConstants.DefaultProjectPath);
            if (curentProjectPath.FullName == defaultProjectPath.FullName)
            {
                SaveProjectAs();
            }
            else
            {
                try
                {
                    _projectService.SaveCurrentProject();
                    await _saveCheckingService.SaveAllUnsavedEntities(false);
                    _loggingService.LogMessage($"Проект сохранен {_projectService.GetCurrentProjectPath(true)}", SeverityEnum.Info);
                }
                catch (Exception e)
                {
                    _loggingService.LogMessage(e.Message, SeverityEnum.Info);
                    SaveProjectAs();
                }
            }
            
        }

        public async void SaveProjectAs()
        {
            Maybe<string> listOfPaths = FileHelper.SelectFilePathToSave("Сохранение файла", ".bisc", "BISC Files (*.bisc)|*.bisc|" +
                                                                                                     "All Files (*.*)|*.*", "New project");
            if (!listOfPaths.Any()) return;
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
                _projectService.SaveProjectAs(defPath);
                await _saveCheckingService.SaveAllUnsavedEntities(false);
                _loggingService.LogMessage($"Проект сохранен {_projectService.GetCurrentProjectPath(true)}", SeverityEnum.Info);
                return;
            }
            _projectService.SaveProjectAs(listOfPaths.GetFirstValue());
            await _saveCheckingService.SaveAllUnsavedEntities(false);
            _loggingService.LogMessage($"Проект сохранен {_projectService.GetCurrentProjectPath(true)}", SeverityEnum.Info);
        }



        public void ClearCurrentProject()
        {
            var devices = _deviceModelService.GetDevicesFromModel(_biscProject.MainSclModel.Value);
            _treeManagementService.ClearMainTree();
            foreach (var device in devices)
            {
                var result = _deviceModelService.DeleteDeviceFromModel(_biscProject.MainSclModel.Value, device.Name);
                if (result.IsSucceed)
                {
                    _goosesModelService.DeleteAllDeviceReferencesInGooseControlsInModel(_biscProject.MainSclModel.Value, device.Name);
                    _connectionPoolService.GetConnection(device.Ip).StopConnection();
                    _deviceWarningsService.ClearDeviceWarningsOfDevice(device.Name);
                }
            }
        }



        public async void OpenProjectAs()
        {
            var fileMaybe = FileHelper.SelectFileToOpen("Открыть файл с устройствами", "BISC Files (*.bisc)|*.bisc|" +
                                                                                       "All Files (*.*)|*.*");
            if (!fileMaybe.Any()) return;

            var res = await _userInteractionService.ShowOptionToUser("Сохранение проекта",
                $"При открытии проекта изменения текущего проекта будут утеряны!  \n" +
                $"Не желаете сохранить текущий проект?", new List<string> { "Сохранить", "НЕТ" });
            if (res == 0)
            {
                SaveProject();
            }

            FileInfo projectToOpen = fileMaybe.GetFirstValue();
            if (!projectToOpen.Exists)
            {
                await _userInteractionService.ShowOptionToUser("Ошибка открытия файла ",
                    $"Фаил {projectToOpen.FullName} не найден", new List<string> { "ОК" });
                _loggingService.LogMessage($"Фаил {projectToOpen.FullName} не найден", SeverityEnum.Warning);
                return;
            }

            ClearCurrentProject();
            try
            {
                _projectService.OpenProjectAs(fileMaybe.GetFirstValue().FullName);
            }
            catch (Exception e)
            {
                _loggingService.LogMessage(e.Message, SeverityEnum.Warning);
            }
            _uiFromModelElementRegistryService.TryHandleModelElementInUiByKey(_biscProject.MainSclModel.Value, null, "SCL");
            await _saveCheckingService.SaveAllUnsavedEntities(false);
            _loggingService.LogMessage($"Проект открыт {_projectService.GetCurrentProjectPath(true)}", SeverityEnum.Info);
        }

        public async void СreateNewProject()
        {
            ClearCurrentProject();
            _projectService.СreateNewProject();
            _projectService.SetDefaultProjectPath();
            await _saveCheckingService.SaveAllUnsavedEntities(false);
            _loggingService.LogMessage($"Проект {_projectService.GetCurrentProjectPath(true)} очистен", SeverityEnum.Info);
        }





        public void OpenDefaultProject()
        {
            _projectService.OpenDefaultProject();
        }

    }
}