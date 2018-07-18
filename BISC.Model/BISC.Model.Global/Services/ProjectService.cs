using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Keys;
using BISC.Model.Infrastructure.Project;

namespace BISC.Model.Global.Services
{
  public  class ProjectService:IProjectService
    {
        private readonly IConfigurationService _configurationService;
        private readonly IBiscProject _biscProject;
        private readonly IModelElementsRegistryService _modelElementsRegistryService;
        private string _currentProjectPath;
        public ProjectService(IConfigurationService configurationService,IBiscProject biscProject,
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
              var stream=  File.Create(path);
                stream.Close();
                _currentProjectPath = path;
                SaveCurrentProject();
                _configurationService.LastProjectPath = path;
                
            }
            else
            {
                path = _configurationService.LastProjectPath;
            }

            _currentProjectPath = path;
           var project= _modelElementsRegistryService.GetModelElementSerializatorByKey(ModelKeys.BiscProjectKey)
                .DeserializeModelElement(XElement.Load(_currentProjectPath)) as IBiscProject;
            _biscProject.MainSclModel = project.MainSclModel;
        }

        public void SaveCurrentProject()
        {
          var xProjectElement=  _modelElementsRegistryService.GetModelElementSerializatorByKey(ModelKeys.BiscProjectKey)
                .SerializeModelElement(_biscProject);
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
                return _currentProjectPath;

            }
            else
            {
                FileInfo fileInfo=new FileInfo(_currentProjectPath);
                return fileInfo.Name;
            }
        }
    }
}
