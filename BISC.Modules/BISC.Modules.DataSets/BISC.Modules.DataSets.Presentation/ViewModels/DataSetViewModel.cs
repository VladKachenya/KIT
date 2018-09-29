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
using System.Windows.Media;

namespace BISC.Modules.DataSets.Presentation.ViewModels
{
    public class DataSetViewModel : ViewModelBase, IDataSetViewModel
    {
        #region private filds
        private IDataSet _model;
        private IFcdaViewModelFactory _fcdaViewModelFactory;
        private bool _isExpanded = false;

        #endregion

        #region C-tor
        public DataSetViewModel(IDataSet model, IFcdaViewModelFactory fcdaViewModelFactory)
        {
            _model = model ?? throw new Exception("Null referens exeption!");
            _fcdaViewModelFactory = fcdaViewModelFactory;
            FcdaViewModels = _fcdaViewModelFactory.GetFcdaViewModelCollection(_model);
        }
        #endregion

        #region Implamentation of DataSetElementBaseViewModel
        public string Name {
            get => _model.Name;
            set
            {
                _model.Name = value;
                OnPropertyChanged();
            }
        }

        public string ElementName => _model.ElementName;
        public string FullName => _model.ParentModelElement.ElementName + "." + _model.ElementName + "." + _model.Name;

        public Brush TypeColorBrush => new SolidColorBrush(Color.FromRgb(126, 141, 240));

        public IDataSet GetModel()
        {
            return _model;
        }

        public void SetModel(IDataSet model)
        {
            _model = model;
        }

        public bool IsExpanded
        {
            get => _isExpanded;
            set => SetProperty(ref _isExpanded, value);
        }
        #endregion

        #region Implamentation of IDataSetViewModel
        public ObservableCollection<IFcdaViewModel> FcdaViewModels { get; }

        
        #endregion
    }
}
