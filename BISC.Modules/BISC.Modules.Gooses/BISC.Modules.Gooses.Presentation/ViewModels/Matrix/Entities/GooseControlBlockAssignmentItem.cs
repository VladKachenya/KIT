using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Matrix.Entities
{
   public abstract class GooseControlBlockAssignmentItem:ViewModelBase
    {
        private string _signature;

        public GooseControlBlockAssignmentItem()
        {
            FcdaAssignmentItems = new ObservableCollection<FcdaAssignmentItem>();
        }

    
        public string Signature
        {
            get => _signature;
            set { SetProperty(ref _signature , value); }
        }
        public ObservableCollection<FcdaAssignmentItem> FcdaAssignmentItems { get; }
    }
}
