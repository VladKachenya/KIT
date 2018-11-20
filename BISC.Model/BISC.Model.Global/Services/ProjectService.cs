using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Global.Model;
using BISC.Model.Global.Project;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Keys;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Serializing;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Model.Global.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IConfigurationService _configurationService;
        private readonly IBiscProject _biscProject;
        private readonly IModelElementsRegistryService _modelElementsRegistryService;
        private readonly IInjectionContainer _injectionContainer;
        private string _currentProjectPath;


        #region Lazy Initialization

        private IDeviceModelService _deviceModelService;
        private ITreeManagementService _treeManagementService;
        private IGoosesModelService _goosesModelService;
        private IConnectionPoolService _connectionPoolService;
        private ITabManagementService _tabManagementService;
        private IDeviceWarningsService _deviceWarningsService;


        private IDeviceModelService GetDeviceModelService()
        {
            _deviceModelService = 
                _deviceModelService ?? _injectionContainer.ResolveType<IDeviceModelService>();
            return _deviceModelService;
        }

        private ITreeManagementService GetTreeManagementService()
        {
            _treeManagementService =
                _treeManagementService ?? _injectionContainer.ResolveType<ITreeManagementService>();
            return _treeManagementService;
        }

        private IGoosesModelService GetGoosesModelService()
        {
            _goosesModelService =
                _goosesModelService ?? _injectionContainer.ResolveType<IGoosesModelService>();
            return _goosesModelService;
        }

        private IConnectionPoolService GetConnectionPoolService()
        {
            _connectionPoolService =
                _connectionPoolService ?? _injectionContainer.ResolveType<IConnectionPoolService>();
            return _connectionPoolService;
        }

        private ITabManagementService GetTabManagementService()
        {
            _tabManagementService =
                _tabManagementService ?? _injectionContainer.ResolveType<ITabManagementService>();
            return _tabManagementService;
        }

        private IDeviceWarningsService GetDeviceWarningsService()
        {
            _deviceWarningsService =
                _deviceWarningsService ?? _injectionContainer.ResolveType<IDeviceWarningsService>();
            return _deviceWarningsService;
        }

        #endregion

        #region ctor
        public ProjectService(IConfigurationService configurationService, IBiscProject biscProject,
            IModelElementsRegistryService modelElementsRegistryService, IInjectionContainer injectionContainer)
        {
            _configurationService = configurationService;
            _biscProject = biscProject;
            _modelElementsRegistryService = modelElementsRegistryService;
            _injectionContainer = injectionContainer;
        }
        #endregion



        public void OpenDefaultProject()
        {
            var path = "TempProject";
            FileInfo lastProject;
            try
            {
                lastProject = new FileInfo(_configurationService.LastProjectPath);
            }
            catch
            {
                lastProject = null;
            }

            if (lastProject != null && lastProject.Exists)
            {
                path = _configurationService.LastProjectPath;
            }
            else
            {
                _currentProjectPath = path;
                _configurationService.LastProjectPath = path;
            }

            IBiscProject biscProject;

            _currentProjectPath = path;
            FileInfo fileInfo = new FileInfo(_currentProjectPath);
            if (!fileInfo.Exists)
            {
                biscProject = new BiscProject();
                biscProject.MainSclModel.Value = new SclModel();
                biscProject.CustomElements.Value = new ModelElement() { ElementName = "CustomElements" };
                var stream = File.Create(path);
                stream.Close();
                _currentProjectPath = path;
                SaveCurrentProject();
            }
            else
            {
                try
                {
                    biscProject =
                        _modelElementsRegistryService.DeserializeModelElement<IBiscProject>(
                            XElement.Load(_currentProjectPath));
                }
                catch (Exception e)
                {
                    biscProject = new BiscProject();
                    biscProject.MainSclModel.Value = new SclModel();
                    biscProject.CustomElements.Value = new ModelElement() { ElementName = "CustomElements" };
                }
            }

            _biscProject.MainSclModel.Value = biscProject.MainSclModel.Value;
            _biscProject.CustomElements.Value = biscProject.CustomElements.Value;
        }

        public void SaveCurrentProject()
        {
            var xProjectElement = _modelElementsRegistryService.SerializeModelElement(_biscProject, SerializingType.Extended);
            var path = "TempProject";
            FileInfo lastProject;
            try
            {
                lastProject = new FileInfo(_configurationService.LastProjectPath);
            }
            catch
            {
                lastProject = null;
            }

            if (lastProject != null && lastProject.Exists)
            {
                path = _configurationService.LastProjectPath;
            }
            else
            {
                _currentProjectPath = path;
                _configurationService.LastProjectPath = path;
            }
            xProjectElement.Save(_currentProjectPath);
        }

        public void OpenProjectAs(string fileName)
        {
            IBiscProject biscProject;
            ClearCurrentProject();
            FileInfo fileInfo;
            try
            {
                fileInfo = new FileInfo(fileName);
            }
            catch
            {
                fileInfo = null;
            }
            if(fileInfo == null)
                return;
            _currentProjectPath = fileName;
            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException();
            }
            else
            {
                try
                {
                    biscProject =
                        _modelElementsRegistryService.DeserializeModelElement<IBiscProject>(
                            XElement.Load(_currentProjectPath));
                    _configurationService.LastProjectPath = _currentProjectPath;
                }
                catch (Exception e)
                {
                    biscProject = new BiscProject();
                    biscProject.MainSclModel.Value = new SclModel();
                    biscProject.CustomElements.Value = new ModelElement() { ElementName = "CustomElements" };
                }
            }

            _biscProject.MainSclModel.Value = biscProject.MainSclModel.Value;
            _biscProject.CustomElements.Value = biscProject.CustomElements.Value;
        }

        public void SaveProjectAs(string fileName)
        {
            var xProjectElement = _modelElementsRegistryService.SerializeModelElement(_biscProject, SerializingType.Extended);
            xProjectElement.Save(fileName);
            _currentProjectPath = fileName;
            _configurationService.LastProjectPath = _currentProjectPath;
        }

        public void ClearCurrentProject()
        {
            var devices = GetDeviceModelService().GetDevicesFromModel(_biscProject.MainSclModel.Value);
            GetTreeManagementService().ClearMainTree();
            foreach (var device in devices)
            {
                var result = GetDeviceModelService().DeleteDeviceFromModel(_biscProject.MainSclModel.Value, device.Name);
                if (result.IsSucceed)
                {
                    GetGoosesModelService().DeleteAllDeviceReferencesInGooseControlsInModel(_biscProject.MainSclModel.Value, device.Name);
                    GetConnectionPoolService().GetConnection(device.Ip).StopConnection();
                    GetDeviceWarningsService().ClearDeviceWarningsOfDevice(device.Name);
                }
            }
            _currentProjectPath = "TempProject";
            _configurationService.LastProjectPath = _currentProjectPath;
        }


        public string GetCurrentProjectPath(bool isFull)
        {
            if (_currentProjectPath == null) return null;
            if (isFull)
            {
                FileInfo fileInfo = new FileInfo(_currentProjectPath);
                return fileInfo.FullName;

            }
            else
            {
                FileInfo fileInfo = new FileInfo(_currentProjectPath);
                return fileInfo.Name;
            }
        }
    }
}
