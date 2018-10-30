using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Global.Model;
using BISC.Model.Global.Project;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Keys;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Serializing;

namespace BISC.Model.Global.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IConfigurationService _configurationService;
        private readonly IBiscProject _biscProject;
        private readonly IModelElementsRegistryService _modelElementsRegistryService;
        private string _currentProjectPath;
        public ProjectService(IConfigurationService configurationService, IBiscProject biscProject,
            IModelElementsRegistryService modelElementsRegistryService)
        {
            _configurationService = configurationService;
            _biscProject = biscProject;
            _modelElementsRegistryService = modelElementsRegistryService;
        }
        public void OpenDefaultProject()
        {
            var path = "TempProject";
            if (String.IsNullOrEmpty(_configurationService.LastProjectPath))
            {
                _currentProjectPath = path;
                _configurationService.LastProjectPath = path;

            }
            else
            {

                path = _configurationService.LastProjectPath;
            }
            IBiscProject biscProject;

            _currentProjectPath = path;
            FileInfo fileInfo = new FileInfo(_currentProjectPath);
            if (!fileInfo.Exists)
            {
                biscProject = new BiscProject();
                biscProject.MainSclModel.Value = new SclModel();
                biscProject.CustomElements.Value = new ModelElement(){ElementName = "CustomElements"};
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
            var xProjectElement = _modelElementsRegistryService.SerializeModelElement(_biscProject,SerializingType.Extended);
            xProjectElement.Save(_currentProjectPath);
        }

        public void OpenProjectAs(string fileName)
        {
            throw new NotImplementedException();
        }

        public void SaveProjectAs(string fileName)
        {
            throw new NotImplementedException();
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
