using BISC.Infrastructure.Global.Services;
using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Events;
using BISC.Presentation.Infrastructure.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace BISC.Modules.Reports.Presentation.ViewModels.ReportElementsViewModels
{
    public class ReportControlViewModel : ViewModelBase, IReportControlViewModel
    {
        private IReportControl _model;
        private string _name;
        private bool _isChenged;
        private string _reportID;
        private bool _isBuffered;
        private int _bufferTime;
        private string _dataSetName;
        private int _integrutyPeriod;
        private List<String> _availableDatasets;
        private IReportEnabledViewModel _reportEnabledViewModel;
        private ITriggerOptionsViewModel _triggerOptionsViewModel;
        private IOprionalFildsViewModel _oprionalFildsViewModel;
        private IGlobalEventsService _globalEventsService;



        #region ctor
        public ReportControlViewModel(IReportEnabledViewModel reportEnabledViewModel, ITriggerOptionsViewModel triggerOptionsViewModel,
            IOprionalFildsViewModel oprionalFildsViewModel, IGlobalEventsService globalEventsService, ICommandFactory commandFactory)
        {
            ReportEnabledViewModel = reportEnabledViewModel;
            TriggerOptionsViewModel = triggerOptionsViewModel;
            OprionalFildsViewModel = oprionalFildsViewModel;
            _globalEventsService = globalEventsService;
            UndoChengestCommand = commandFactory.CreatePresentationCommand(UpdateViewModel);
            _globalEventsService.Subscribe<SaveCheckEvent>(OnSaveCheck);

        }

        private void OnSaveCheck(SaveCheckEvent saveCheckEvent)
        {
            IsChenged = ChangeTracker.GetIsModifiedRecursive();
        }
        #endregion

        #region private methods

        private void SetRoportID()
        {
            if (IsDynamic)
            { 
                string buf = _isBuffered ? "BR" : "RP";
                _reportID = $"LLN0${buf}${_name}";
                OnPropertyChanged(nameof(ReportID));
            }
        }

        #endregion

        #region Implementation of IReportControlViewModel
        public string ElementName => "Report";
        public Brush TypeColorBrush => new SolidColorBrush(Color.FromRgb(240, 126, 184));


        public List<string> AvailableDatasets
        {
            get => _availableDatasets;
            set { SetProperty(ref _availableDatasets, value); }
        }
        public string Name
        {
            get => _name;
            set
            {
                if (value.Length > 20) return;
                SetProperty(ref _name, value);
                SetRoportID();
            }
        }
        public bool IsChenged
        {
            get => _isChenged;
            set
            {
                _isChenged = value;
                OnPropertyChanged();
            }
        }

        public string ReportID
        {
            get => _reportID;
        }
        public bool IsBuffered
        {
            get => _isBuffered;
            set
            {
                SetProperty(ref _isBuffered, value);
                SetRoportID();
            }
        }
        public int BufferTime
        {
            get => _bufferTime;
            set => SetProperty(ref _bufferTime, value);
        }
        public string SelectidDataSetName
        {
            get => _dataSetName;
            set
            {
                var val = AvailableDatasets.Find( item => item == value);
                SetProperty(ref _dataSetName, value);
            }
        }
        public int IntegrutyPeriod
        {
            get => _integrutyPeriod;
            set
            {
                SetProperty(ref _integrutyPeriod, value);
            }
        }

        public bool IsDynamic => _model.IsDynamic;
        public ICommand UndoChengestCommand { get; }

        public IReportEnabledViewModel ReportEnabledViewModel
        {
            get => _reportEnabledViewModel;
            protected set => SetProperty(ref _reportEnabledViewModel, value);
        }
        public ITriggerOptionsViewModel TriggerOptionsViewModel
        {
            get => _triggerOptionsViewModel;
            protected set => SetProperty(ref _triggerOptionsViewModel, value);
        }
        public IOprionalFildsViewModel OprionalFildsViewModel
        {
            get => _oprionalFildsViewModel;
            protected set => SetProperty(ref _oprionalFildsViewModel, value);
        }
        public IReportControl Model
        {
            get => _model;
            set
            {
                _model = value;
                ReportEnabledViewModel.Model = _model.RptEnabled.Value;
                TriggerOptionsViewModel.Model = _model.TrgOps.Value;
                OprionalFildsViewModel.Model = _model.OptFields.Value;
                UpdateViewModel();
            }
        }

        public void ActivateElement()
        {
            ChangeTracker.SetTrackingEnabled(true);
            ReportEnabledViewModel.ActivateElement();
            TriggerOptionsViewModel.ActivateElement();
            OprionalFildsViewModel.ActivateElement();
        }

        public void UpdateModel()
        {
            _model.Name = Name;
            _model.RptID = ReportID;
            _model.Buffered = IsBuffered;
            _model.BufTime = BufferTime;
            _model.DataSet = SelectidDataSetName;
            _model.IntgPd = IntegrutyPeriod;
            ReportEnabledViewModel.UpdateModel();
            TriggerOptionsViewModel.UpdateModel();
            OprionalFildsViewModel.UpdateModel();
        }

        public void UpdateViewModel()
        {
            this._reportID = _model.RptID;
            this.Name = _model.Name;
            this.IsBuffered = _model.Buffered;
            this.BufferTime = _model.BufTime;
            this.SelectidDataSetName = _model.DataSet;
            this.IntegrutyPeriod = _model.IntgPd;
            ReportEnabledViewModel.UpdateViewModel();
            TriggerOptionsViewModel.UpdateViewModel();
            OprionalFildsViewModel.UpdateViewModel();
            ChangeTracker.AcceptChanges();
        }
        #endregion

        protected override void OnDisposing()
        {
            _globalEventsService.Unsubscribe<SaveCheckEvent>(OnSaveCheck);
            base.OnDisposing();
        }

    }
}
