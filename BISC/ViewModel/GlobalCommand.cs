using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Infrastructure.Global.Modularity;
using BISC.Interfaces;
using BISC.Presentation.BaseItems.ViewModels;

namespace BISC.ViewModel
{
   public class GlobalCommand:ViewModelBase,IGlobalCommand
    {
        public GlobalCommand()
        {
            
        }


        private bool _isEnabled;
        public string CommandName { get; set; }
        public ICommand Command { get; set; }
        public string IconId { get; set; }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled , value,true);
        }
    }
}
