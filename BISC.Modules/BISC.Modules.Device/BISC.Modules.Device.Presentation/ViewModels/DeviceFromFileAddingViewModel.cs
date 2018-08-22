using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Device.Presentation.Interfaces;
using BISC.Modules.Device.Presentation.Interfaces.Factories;
using BISC.Modules.Device.Presentation.Interfaces.Services;
using BISC.Presentation.BaseItems.Common;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;

namespace BISC.Modules.Device.Presentation.ViewModels
{
   public class DeviceFromFileAddingViewModel:NavigationViewModelBase, IDeviceFromFileAddingViewModel
    {
        private readonly ICommandFactory _commandFactory;
        private readonly IConfigurationService _configurationService;
        private readonly IFileViewModelFactory _fileViewModelFactory;
        private readonly IModelComposingService _modelComposingService;
        private readonly IDeviceModelService _deviceModelService;
        private readonly IDeviceViewModelFactory _deviceViewModelFactory;
        private readonly IDeviceAddingService _deviceAddingService;
        private ISclModel _currentAddingSclModel;
        public DeviceFromFileAddingViewModel(ICommandFactory commandFactory, IConfigurationService configurationService,
            IFileViewModelFactory fileViewModelFactory, IModelComposingService modelComposingService, IDeviceModelService deviceModelService,
            IDeviceViewModelFactory deviceViewModelFactory, IDeviceAddingService deviceAddingService)
        {
            _commandFactory = commandFactory;
            _commandFactory = commandFactory;
            _configurationService = configurationService;
            _fileViewModelFactory = fileViewModelFactory;
            _modelComposingService = modelComposingService;
            _deviceModelService = deviceModelService;
            _deviceViewModelFactory = deviceViewModelFactory;
            _deviceAddingService = deviceAddingService;
            LastOpenedFiles = new ObservableCollection<IFileViewModel>();
            OpenFileWithDevices = _commandFactory.CreatePresentationCommand(OnOpenFileWithDevicesExecute);
            DeleteFileFromView = _commandFactory.CreatePresentationCommand<IFileViewModel>(OnDeleteFileFromViewExecute);
            LoadDevicesFromFile = _commandFactory.CreatePresentationCommand<IFileViewModel>(OnLoadDevicesFromFileExecute);
            AddSelectedDevices = _commandFactory.CreatePresentationCommand(OnAddSelectedDevicesExecute);
            FillLastOpenedFilesFromConfig();
            CurrentDevicesToAdd = new ObservableCollection<IDeviceViewModel>();
        }


        private void OnAddSelectedDevicesExecute()
        {
            _deviceAddingService.AddDevicesInProject(CurrentDevicesToAdd.Where((model => model.IsSelected)).Select((model => model.Device)).ToList(),_currentAddingSclModel);
        }

        private void OnLoadDevicesFromFileExecute(IFileViewModel fileViewModel)
        {
            var model = _modelComposingService.DeserializeModelFromFile(XElement.Load(fileViewModel.FullPath));
            _currentAddingSclModel = model;
            var devices = _deviceModelService.GetDevicesFromModel(model);
            CurrentDevicesToAdd.Clear();
            foreach (var device in devices)
            {
                CurrentDevicesToAdd.Add(_deviceViewModelFactory.CreateDeviceViewModel(device));
            }

            if (CurrentDevicesToAdd.Count == 1)
            {
                CurrentDevicesToAdd[0].IsSelected = true;
            }
        }

        private void OnDeleteFileFromViewExecute(IFileViewModel fileViewModel)
        {
            if (LastOpenedFiles.Contains(fileViewModel))
            {
                LastOpenedFiles.Remove(fileViewModel);
                SaveChangesInConfig();
            }
        }

        #region Overrides of NavigationViewModelBase

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)

        {
            base.OnNavigatedTo(navigationContext);
        }

        protected override void OnNavigatedFrom(BiscNavigationContext navigationContext)
        {
            base.OnNavigatedFrom(navigationContext);
        }

        #endregion


        private void OnOpenFileWithDevicesExecute()
        {
            var fileMaybe = FileHelper.SelectFileToOpen("Открыть файл с устройствами", "SCL Files (*.cid,*.icd,*.iid)|*.cid;*.icd;*iid|" +
                                                                        "Configured IED Description Files (*.cid)|*.cid|" +
                                                                        "IED Capability Description Files (*.icd)|*.icd|" +
                                                                        "Instantiated IED description Files (*.iid)|*.iid|" +
                                                                        "All Files (*.*)|*.*");
            if (!fileMaybe.Any()) return;

            var existing =
                LastOpenedFiles.FirstOrDefault((model => model.FullPath == fileMaybe.GetFirstValue().FullName));
            if (existing != null)
            {
                LastOpenedFiles.Remove(existing);
                LastOpenedFiles.Add(existing);
                return;
            }
            LastOpenedFiles.Add(_fileViewModelFactory.CreateFileViewModel(fileMaybe.GetFirstValue().FullName));
            SaveChangesInConfig();
        }

        private void FillLastOpenedFilesFromConfig()
        {
            LastOpenedFiles.Clear();
            var lastOpenedFiles = _configurationService.LastOpenedFiles;
            foreach (var lastOpenedFile in lastOpenedFiles)
            {
                LastOpenedFiles.Add(_fileViewModelFactory.CreateFileViewModel(lastOpenedFile));
            }
        }

        private void SaveChangesInConfig()
        {
            var lastOpenedFiles = new List<string>();
            foreach (var lastOpenedFile in LastOpenedFiles)
            {
                if (lastOpenedFile.IsFileExists)
                {
                    lastOpenedFiles.Add(lastOpenedFile.FullPath);
                }
            }

            _configurationService.LastOpenedFiles = lastOpenedFiles;
        }



        #region Overrides of DisposableViewModelBase

        protected override void OnDisposing()
        {

            base.OnDisposing();
        }

        #endregion

        public ICommand OpenFileWithDevices { get; }
        public ICommand DeleteFileFromView { get; }

        public ICommand AddSelectedDevices { get; }
        public ICommand LoadDevicesFromFile { get; }

        public ObservableCollection<IFileViewModel> LastOpenedFiles { get; }
        public ObservableCollection<IDeviceViewModel> CurrentDevicesToAdd { get; }
    }
}
