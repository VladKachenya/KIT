using BISC.Modules.DataSets.Infrastructure.ViewModels;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Presentation.BaseItems.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.DataSets.Presentation.ViewModels
{
    public class DataSetViewModel : ViewModelBase, IDataSetViewModel
    {
        #region private filds
        private IDevice _model;

        #endregion

        #region C-tor
        public DataSetViewModel(IDevice model)
        {
            if (model == null) throw new Exception("Null referens exeption!");
            _model = model;
        }
        #endregion

        public string Name
        {
            get => _model.Name;
            set
            {
                _model.Name = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<IFcdaViewModel> FcdaViewModels { get; }
    }
}
