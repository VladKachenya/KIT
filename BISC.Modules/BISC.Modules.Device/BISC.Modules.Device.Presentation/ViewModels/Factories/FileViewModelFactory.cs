using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Device.Presentation.Interfaces;
using BISC.Modules.Device.Presentation.Interfaces.Factories;

namespace BISC.Modules.Device.Presentation.ViewModels.Factories
{
    public class FileViewModelFactory : IFileViewModelFactory
    {
        private readonly Func<IFileViewModel> _fileViewModelCreator;

        public FileViewModelFactory(Func<IFileViewModel> fileViewModelCreator)
        {
            _fileViewModelCreator = fileViewModelCreator;
        }

        #region Implementation of IFileViewModelFactory

        public IFileViewModel CreateFileViewModel(string fullFilePath)
        {
            IFileViewModel fileViewModel = _fileViewModelCreator();
            FileInfo fileInfo = new FileInfo(fullFilePath);
            fileViewModel.FullPath = fullFilePath;
            fileViewModel.ShortPath = fileInfo.Name;
            fileViewModel.IsFileExists = fileInfo.Exists;
            return fileViewModel;
        }

        #endregion
    }

}
