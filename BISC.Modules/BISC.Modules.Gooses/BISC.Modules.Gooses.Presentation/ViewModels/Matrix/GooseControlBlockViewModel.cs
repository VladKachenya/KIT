using BISC.Modules.Gooses.Presentation.Interfaces;
using BISC.Presentation.BaseItems.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Matrix
{
    public class GooseRowViewModelGrouped
    {
        public string GroupName { get; set; }
        public IGooseRowViewModel GooseRowViewModel { get; set; }
    }
    public class GooseControlBlockViewModel : ViewModelBase
    {
        //private readonly IEventAggregator _eventAggregator;
        //private IGooseControlBlock _model;
        private string _macAddressString;
        private bool _isReferenceEnabled;
        private string _gocbReferenceString;
        private string _dataSetName;
        private string _name;
        private string _appId;
        private List<IGooseRowViewModel> _gooseRowViewModels;
        private List<string> _columnsName;


        public GooseControlBlockViewModel()
        {
            GooseRowViewModels = new List<IGooseRowViewModel>();
        }

        public List<string> ColumnsName
        {
            get => _columnsName;
            set => SetProperty(ref _columnsName, value, true);
        }

        public bool IsConsigerTheQuality { get; set; }

        public List<IGooseRowViewModel> GooseRowViewModels
        {
            get => _gooseRowViewModels;
            set => SetProperty(ref _gooseRowViewModels, value);
        }

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
                SetProperty(ref _appId, value);
            }
        }

        public IGoCbFtpEntity GoCbReference { get; set; }

        protected override void OnDisposing()
        {

            foreach (var gooseRowViewModel in GooseRowViewModels)
            {
                gooseRowViewModel.Dispose();
            }
            base.OnDisposing();
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


        //#region Overrides of DisposableBindableBase
        //#endregion

        //#endregion
    }
}