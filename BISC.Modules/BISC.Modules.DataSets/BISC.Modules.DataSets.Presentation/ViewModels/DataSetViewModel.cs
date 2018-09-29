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
using System.Windows.Input;
using System.Windows.Media;
using BISC.Presentation.Infrastructure.Factories;

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
        public DataSetViewModel(IDataSet model, IFcdaViewModelFactory fcdaViewModelFactory,ICommandFactory commandFactory)
        {
            _model = model ?? throw new NullReferenceException();
            _fcdaViewModelFactory = fcdaViewModelFactory;
            FcdaViewModels = _fcdaViewModelFactory.GetFcdaViewModelCollection(_model);
            DeleteFcdaCommand = commandFactory.CreatePresentationCommand<object>(OnDeleteFcda);
        }

        private void OnDeleteFcda(object obj)
        {
            throw new NotImplementedException();
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
            _model.FcdaList.Clear();
            foreach (var fcdaViewModel in FcdaViewModels)
            {
                _model.FcdaList.Add(fcdaViewModel.GetModel());
            }
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

        public bool IsEditeble => _model.IsDynamic;
        #endregion

        #region Implamentation of IDataSetViewModel
        public ObservableCollection<IFcdaViewModel> FcdaViewModels { get; }
        public ICommand DeleteFcdaCommand { get; }

        #endregion
    }
}
