using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Modules.Device.Presentation.Interfaces;

namespace BISC.Modules.Device.Presentation.ViewModels
{
  public  class FileViewModel: IFileViewModel
    {
        public FileViewModel()
        {
            
        }


        #region Implementation of IFileViewModel

        public string FullPath { get; set; }
        public string ShortPath { get; set; }
        public ICommand OpenFile { get; }
        public bool IsFileExists { get; set; }

        #endregion
    }
}
