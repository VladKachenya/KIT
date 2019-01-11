using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Presentation.Factories;
using BISC.Modules.Gooses.Presentation.Interfaces;
using BISC.Presentation.BaseItems.ViewModels;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Matrix
{
    public class GooseControlBlockViewModel : ViewModelBase 
    {
        private readonly GooseRowViewModelFactory _gooseRowViewModelFactory;
        //private readonly IEventAggregator _eventAggregator;
        //private IGooseControlBlock _model;
	    private string _macAddressString;
        private bool _isReferenceEnabled;
        private string _gocbReferenceString;
        private string _dataSetName;
        private string _name;
        private string _appId;



        public GooseControlBlockViewModel(GooseRowViewModelFactory gooseRowViewModelFactory)
        {
            _gooseRowViewModelFactory = gooseRowViewModelFactory;
			GooseRowViewModels=new List<IGooseRowViewModel>();
        }


        public void SetRows(List<IGooseRow> rowsForBlock)
        {
            var rowVms=new List<IGooseRowViewModel>();
            foreach (var row in rowsForBlock)
            {
              var rowVm=  _gooseRowViewModelFactory.CreateGooseRowViewModelOld(row, this);
                rowVms.Add(rowVm);
            }
			GooseRowViewModels.Clear();
            GooseRowViewModels.AddRange(rowVms);
        }


        //#region Implementation of IGooseControlBlockViewModel

        //public IGooseControlBlock Model
        //{
        //    get
        //    {
        //        _model.GooseRows.Clear();
        //        foreach (var gooseRowViewModel in GooseRowViewModels)
        //        {
        //            _model.GooseRows.Add(gooseRowViewModel.Model);
        //        }
        //        _model.MacAddressString = MacAddressString;
        //        _model.IsReferenceEnabled = IsReferenceEnabled;
        //        _model.GocbReferenceString = GocbReferenceString;
        //        _model.DataSetName = DataSetName;
        //        _model.Name = Name;
        //        _model.AppId = AppId;
        //        return _model;

        //    }
        //    set
        //    {
        //        _model = value;
        //        GooseRowViewModels.Clear();
        //        foreach (var gooseRow in _model.GooseRows)
        //        {
        //            GooseRowViewModels.Add(_gooseRowViewModelFactory.CreateGooseRowViewModelOld(gooseRow, this));
        //        }
        //        MacAddressString = _model.MacAddressString;
        //        _isReferenceEnabled = _model.IsReferenceEnabled;
        //        GocbReferenceString = _model.GocbReferenceString;
        //        DataSetName = _model.DataSetName;
        //        Name = _model.Name;
        //        AppId = _model.AppId;

        //    }
        //}

        public List<IGooseRowViewModel> GooseRowViewModels { get; }

	    //public string MacAddressString
        //{
        //    get { return _macAddressString; }
        //    set
        //    {
        //        _macAddressString = value;
        //        RaisePropertyChanged();
        //    }
        //}

        //public bool IsReferenceEnabled
        //{
        //    get { return _isReferenceEnabled; }
        //    set
        //    {
        //        _model.IsReferenceEnabled = value;
        //        _isReferenceEnabled = value;
        //        RaisePropertyChanged();
        //        GooseRowViewModels.ForEach((model => model.SelectableValueViewModels.ForEach((viewModel => viewModel.SelectedValue = false))));
        //        _eventAggregator.GetEvent<GooseControlBlockEnableChangedEvent>()
        //            .Publish(new GooseControlBlockEnableChangedEventArgs(this));
        //    }
        //}

        //public string GocbReferenceString
        //{
        //    get { return _gocbReferenceString; }
        //    set
        //    {
        //        _gocbReferenceString = value;
        //        RaisePropertyChanged();

        //    }
        //}

        public string DataSetName
        {
            get { return _dataSetName; }
            set
            {
                SetProperty(ref _dataSetName, value);
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value);
            }
        }

        public string AppId
        {
            get { return _appId; }
            set
            {
                SetProperty(ref _appId , value);
            }
        }

        public string GoCbReference { get; set; }


        //#region Overrides of DisposableBindableBase

        protected override void OnDisposing()
        {

            foreach (var gooseRowViewModel in GooseRowViewModels)
            {
                gooseRowViewModel.Dispose();
            }
            base.OnDisposing();
        }

        //#endregion

        //#endregion
    }
}