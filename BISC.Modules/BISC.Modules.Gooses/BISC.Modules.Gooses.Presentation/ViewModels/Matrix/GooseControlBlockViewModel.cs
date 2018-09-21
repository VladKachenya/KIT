using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Matrix
{
    //public class GooseControlBlockViewModel : DisposableBindableBase, IGooseControlBlockViewModel
    //{
    //    private readonly IGooseRowViewModelFactory _gooseRowViewModelFactory;
    //    private readonly IEventAggregator _eventAggregator;
    //    private IGooseControlBlock _model;
    //    private List<IGooseRowViewModel> _gooseRowViewModels;
    //    private string _macAddressString;
    //    private bool _isReferenceEnabled;
    //    private string _gocbReferenceString;
    //    private string _dataSetName;
    //    private string _name;
    //    private int _appId;



    //    public GooseControlBlockViewModel(IGooseRowViewModelFactory gooseRowViewModelFactory,
    //        IEventAggregator eventAggregator)
    //    {
    //        _gooseRowViewModelFactory = gooseRowViewModelFactory;
    //        _eventAggregator = eventAggregator;
    //        GooseRowViewModels = new List<IGooseRowViewModel>();

    //    }



    //    #region Implementation of IGooseControlBlockViewModel

    //    public IGooseControlBlock Model
    //    {
    //        get
    //        {
    //            _model.GooseRows.Clear();
    //            foreach (var gooseRowViewModel in GooseRowViewModels)
    //            {
    //                _model.GooseRows.Add(gooseRowViewModel.Model);
    //            }
    //            _model.MacAddressString = MacAddressString;
    //            _model.IsReferenceEnabled = IsReferenceEnabled;
    //            _model.GocbReferenceString = GocbReferenceString;
    //            _model.DataSetName = DataSetName;
    //            _model.Name = Name;
    //            _model.AppId = AppId;
    //            return _model;

    //        }
    //        set
    //        {
    //            _model = value;
    //            GooseRowViewModels.Clear();
    //            foreach (var gooseRow in _model.GooseRows)
    //            {
    //                GooseRowViewModels.Add(_gooseRowViewModelFactory.CreateGooseRowViewModel(gooseRow, this));
    //            }
    //            MacAddressString = _model.MacAddressString;
    //            _isReferenceEnabled = _model.IsReferenceEnabled;
    //            GocbReferenceString = _model.GocbReferenceString;
    //            DataSetName = _model.DataSetName;
    //            Name = _model.Name;
    //            AppId = _model.AppId;

    //        }
    //    }

    //    public List<IGooseRowViewModel> GooseRowViewModels
    //    {
    //        get { return _gooseRowViewModels; }
    //        set
    //        {
    //            _gooseRowViewModels = value;
    //            RaisePropertyChanged();
    //        }
    //    }

    //    public string MacAddressString
    //    {
    //        get { return _macAddressString; }
    //        set
    //        {
    //            _macAddressString = value;
    //            RaisePropertyChanged();
    //        }
    //    }

    //    public bool IsReferenceEnabled
    //    {
    //        get { return _isReferenceEnabled; }
    //        set
    //        {
    //            _model.IsReferenceEnabled = value;
    //            _isReferenceEnabled = value;
    //            RaisePropertyChanged();
    //            GooseRowViewModels.ForEach((model => model.SelectableValueViewModels.ForEach((viewModel => viewModel.SelectedValue = false))));
    //            _eventAggregator.GetEvent<GooseControlBlockEnableChangedEvent>()
    //                .Publish(new GooseControlBlockEnableChangedEventArgs(this));
    //        }
    //    }

    //    public string GocbReferenceString
    //    {
    //        get { return _gocbReferenceString; }
    //        set
    //        {
    //            _gocbReferenceString = value;
    //            RaisePropertyChanged();

    //        }
    //    }

    //    public string DataSetName
    //    {
    //        get { return _dataSetName; }
    //        set
    //        {
    //            _dataSetName = value;
    //            RaisePropertyChanged();

    //        }
    //    }

    //    public string Name
    //    {
    //        get { return _name; }
    //        set
    //        {
    //            _name = value;
    //            RaisePropertyChanged();

    //        }
    //    }

    //    public int AppId
    //    {
    //        get { return _appId; }
    //        set
    //        {
    //            _appId = value;
    //            RaisePropertyChanged();

    //        }
    //    }




    //    #region Overrides of DisposableBindableBase

    //    protected override void OnDisposing()
    //    {

    //        foreach (var gooseRowViewModel in GooseRowViewModels)
    //        {
    //            gooseRowViewModel.Dispose();
    //        }
    //        base.OnDisposing();
    //    }

    //    #endregion

    //    #endregion
    //}
}