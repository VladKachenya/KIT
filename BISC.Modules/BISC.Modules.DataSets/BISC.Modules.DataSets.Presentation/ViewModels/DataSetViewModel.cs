using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.ViewModels;
using BISC.Modules.DataSets.Infrastructure.ViewModels.Factorys;
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
        private IDataSet _model;
        private IFcdaViewModelFactory _fcdaViewModelFactory;

        #endregion

        #region C-tor
        public DataSetViewModel(IDataSet model, IFcdaViewModelFactory fcdaViewModelFactory)
        {
            _model = model ?? throw new Exception("Null referens exeption!");
            _fcdaViewModelFactory = fcdaViewModelFactory;
            FcdaViewModels = _fcdaViewModelFactory.GetFcdaViewModelCollection(_model);
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

        public string ElementName
        {
            get => _model.ElementName;
        }

        public ObservableCollection<IFcdaViewModel> FcdaViewModels { get; }
    }
}
