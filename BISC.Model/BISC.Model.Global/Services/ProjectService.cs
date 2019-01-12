using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Global.Constants;
using BISC.Model.Global.Model;
using BISC.Model.Global.Project;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Constants;
using BISC.Model.Infrastructure.Keys;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Serializing;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Infrastructure.ViewModel;

namespace BISC.Model.Global.Services
{
    public class ProjectService : IProjectService
    {

        private readonly IConfigurationService _configurationService;
        private readonly IBiscProject _biscProject;
        private readonly IModelElementsRegistryService _modelElementsRegistryService;
        private readonly IInjectionContainer _injectionContainer;
        private IApplicationTitle _applicationTitle;


        private string _currentProjectPath;


        #region ctor
        public ProjectService(IConfigurationService configurationService, IBiscProject biscProject,
            IModelElementsRegistryService modelElementsRegistryService, IInjectionContainer injectionContainer)
        {
            _configurationService = configurationService;
            _biscProject = biscProject;
            _modelElementsRegistryService = modelElementsRegistryService;
            _injectionContainer = injectionContainer;
        }

        #region Lazy Initialization

        private IApplicationTitle GetApplicationTitle()
        {
            _applicationTitle =
                _applicationTitle ?? _injectionContainer.ResolveType<IApplicationTitle>();
            return _applicationTitle;
        }

        #endregion

        #endregion

        #region private methods

        private void SetApplicationTitle()
        {
            GetApplicationTitle().ApplicationTitle = $"Конфигуратор интеллектуальных терминалов (Текущий проект: {GetCurrentProjectPath(true)})";
        }




        private string CurrentProjectPath
        {
            get
            {
                if (string.IsNullOrEmpty(_currentProjectPath))
                    _currentProjectPath = _configurationService.LastProjectPath;
                return _currentProjectPath;
            }
            set
            {
                _currentProjectPath = value;
                if (_configurationService.LastProjectPath != value)
                    _configurationService.LastProjectPath = value;
                SetApplicationTitle();
            }
        }

        public void СreateNewProject()
        {
            IBiscProject biscProject;

            biscProject = new BiscProject();
            biscProject.MainSclModel.Value = new SclModel();
            biscProject.CustomElements.Value = new ModelElement() { ElementName = "CustomElements" };
            _biscProject.MainSclModel.Value = biscProject.MainSclModel.Value;
            _biscProject.CustomElements.Value = biscProject.CustomElements.Value;
        }

        #endregion

        public void OpenDefaultProject()
        {
            FileInfo lastProject;
            try
            {
                lastProject = new FileInfo(CurrentProjectPath);
            }
            catch
            {
                lastProject = null;
            }

            if (lastProject == null || !lastProject.Exists)
                CurrentProjectPath = ModelConstants.DefaultProjectPath;

            IBiscProject biscProject;

            FileInfo fileInfo = new FileInfo(CurrentProjectPath);
            if (!fileInfo.Exists)
            {
                biscProject = new BiscProject();
                biscProject.MainSclModel.Value = new SclModel();
                biscProject.CustomElements.Value = new ModelElement() { ElementName = "CustomElements" };
                var stream = File.Create(CurrentProjectPath);
                stream.Close();
                SaveCurrentProject();
            }
            else
            {
                try
                {
                    biscProject =
                        _modelElementsRegistryService.DeserializeModelElement<IBiscProject>(
                            XElement.Load(CurrentProjectPath));
                }
                catch (Exception e)
                {
                    biscProject = new BiscProject();
                    biscProject.MainSclModel.Value = new SclModel();
                    biscProject.CustomElements.Value = new ModelElement() { ElementName = "CustomElements" };
                }
            }

            SetApplicationTitle();
            _biscProject.MainSclModel.Value = biscProject.MainSclModel.Value;
            _biscProject.CustomElements.Value = biscProject.CustomElements.Value;
        }

        public void OpenProjectAs(string fileName)
        {
            IBiscProject biscProject;
            CurrentProjectPath = fileName;
            try
            {
                biscProject =
                    _modelElementsRegistryService.DeserializeModelElement<IBiscProject>(
                        XElement.Load(_currentProjectPath));
                _configurationService.LastProjectPath = _currentProjectPath;
            }
            catch(Exception e)
            {
                SetDefaultProjectPath();
                СreateNewProject();
                throw new Exception("Ошибка закрузки файла!");
            }

            _biscProject.MainSclModel.Value = biscProject.MainSclModel.Value;
            _biscProject.CustomElements.Value = biscProject.CustomElements.Value;
        }

        

        public void SaveCurrentProject()
        {
            var xProjectElement = _modelElementsRegistryService.SerializeModelElement(_biscProject, SerializingType.Extended);
            xProjectElement.Save(CurrentProjectPath);
            SetApplicationTitle();
        }



        public void SaveProjectAs(string fileName)
        {
            var xProjectElement = _modelElementsRegistryService.SerializeModelElement(_biscProject, SerializingType.Extended);
            xProjectElement.Save(fileName);
            CurrentProjectPath = fileName;
        }

        public void SetDefaultProjectPath()
        {
            CurrentProjectPath = ModelConstants.DefaultProjectPath;
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
