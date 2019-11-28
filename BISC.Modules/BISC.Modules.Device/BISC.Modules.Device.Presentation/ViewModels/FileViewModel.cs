using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Modules.Device.Presentation.Interfaces;
using BISC.Presentation.BaseItems.ViewModels;

namespace BISC.Modules.Device.Presentation.ViewModels
{
    public class FileViewModel : ViewModelBase, IFileViewModel
    {
        private bool _isAddingFileProcess;
        private bool _isAddingDevicesProcess;

        public FileViewModel()
        {

        }


        #region Implementation of IFileViewModel

        public string FullPath { get; set; }
        public string ShortPath { get; set; }
        public ICommand OpenFile { get; }
        public bool IsFileExists { get; set; }

        public bool IsAddingFileProcess
        {
            get => _isAddingFileProcess;
            set => SetProperty(ref _isAddingFileProcess, value);
        }

        public bool IsAddingDevicesProcess
        {
            get => _isAddingDevicesProcess;
            set => SetProperty(ref _isAddingDevicesProcess, value);
        }

        #endregion
    }
}
