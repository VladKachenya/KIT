using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture.Elements;
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
using BISC.Model.Global.Common;
using BISC.Model.Infrastructure.Common;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Reports.Model.Model;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Reports.Presentation.ViewModels.ReportElementsViewModels
{
    public class ReportControlViewModel : ComplexViewModelBase, IReportControlViewModel
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
        private readonly IDatasetModelService _datasetModelService;
        private ILDevice _lDevice;
        private bool _giBool;
        private int _configurationRevision;

        

        #region ctor
        public ReportControlViewModel(IReportEnabledViewModel reportEnabledViewModel, ITriggerOptionsViewModel triggerOptionsViewModel,
            IOprionalFildsViewModel oprionalFildsViewModel, IGlobalEventsService globalEventsService, ICommandFactory commandFactory,IDatasetModelService datasetModelService)
        {
            ReportEnabledViewModel = reportEnabledViewModel;
            TriggerOptionsViewModel = triggerOptionsViewModel;
            OprionalFildsViewModel = oprionalFildsViewModel;
            _globalEventsService = globalEventsService;
            _datasetModelService = datasetModelService;
            UndoChengestCommand = commandFactory.CreatePresentationCommand(UpdateViewModel);
            _globalEventsService.Subscribe<SaveCheckEvent>(OnSaveCheck);
            UpdateAvailableDatasetsCommand = commandFactory.CreatePresentationCommand(OnUpdateAvailableDatasets);
        }

        private void OnUpdateAvailableDatasets()
        {
            var datasets = _datasetModelService.GetAllDataSetOfDevice(_lDevice.GetFirstParentOfType<IDevice>());
            var selectedDataset = SelectidDataSetName;
            AvailableDatasets = datasets.Select((ds => ds.Name)).ToList();
            SelectidDataSetName = AvailableDatasets.FirstOrDefault((s => s == selectedDataset));
        }

        private void OnSaveCheck(SaveCheckEvent saveCheckEvent)
        {
            IsChenged = ChangeTracker.GetIsModifiedRecursive();
        }
        #endregion

        #region private methods

        private void SetRoportID()
        {
            string buf = _isBuffered ? "BR" : "RP";
            _reportID = $"{ParentLnName}${buf}${_name}";
            OnPropertyChanged(nameof(ReportID));
        }

        #endregion

        #region Implementation of IReportControlViewModel
        public string ElementName => "Report";
        public Brush TypeColorBrush => new SolidColorBrush(Color.FromRgb(240, 126, 184));

        public int ConfigurationRevision
        {
            get => _configurationRevision;
            set
            {
                value = value <= 0 ? 1 : value;
                SetProperty(ref _configurationRevision, value);
            }
        }
        public List<string> AvailableDatasets
        {
            get => _availableDatasets;
            set => SetProperty(ref _availableDatasets, value,true);
        }
        public string Name
        {
            get => _name;
            set
            {
                if (value.Length > 20) return;
                if (!StaticStringValidationService.NameValidation(value)) return;
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
            set
            {
                if(value < 0 || value > 3600000) return;
                SetProperty(ref _bufferTime, value);
            }
        }
        public string SelectidDataSetName
        {
            get => _dataSetName;
            set
            {
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

        public string ParentLdName { get; protected set; }
        public string ParentLnName { get; protected set; }
        public bool IsDynamic => _model.IsDynamic;
        public ICommand UndoChengestCommand { get; }
        public ICommand UpdateAvailableDatasetsCommand { get; }

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

        public bool GiBool
        {
            get => _giBool;
            set =>SetProperty(ref _giBool , value);
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

        public IReportControl GetUpdatedModel()
        {
          IReportControl reportControl=new ReportControl();
            reportControl.Name = Name;
            reportControl.RptID = ReportID;
            reportControl.Buffered = IsBuffered;
            reportControl.BufTime = BufferTime;
            reportControl.DataSet = SelectidDataSetName;
            reportControl.IntgPd = IntegrutyPeriod;
            reportControl.GiBool = GiBool;
            reportControl.IsDynamic = IsDynamic;
            reportControl.ConfRev = ConfigurationRevision;
            reportControl.OptFields.Value= OprionalFildsViewModel.GetUpdatedModel();
            reportControl.RptEnabled.Value = ReportEnabledViewModel.GetUpdatedModel();
            reportControl.TrgOps.Value = TriggerOptionsViewModel.GetUpdatedModel();
            return reportControl;
        }

      

        public void UpdateViewModel()
        {
            this.Name = _model.Name;
            this.IsBuffered = _model.Buffered;
            this.BufferTime = _model.BufTime;
            this.SelectidDataSetName = _model.DataSet;
            this.IntegrutyPeriod = _model.IntgPd;
            this.ConfigurationRevision = Model.ConfRev;
            this.GiBool = _model.GiBool;
            ReportEnabledViewModel.UpdateViewModel();
            TriggerOptionsViewModel.UpdateViewModel();
            OprionalFildsViewModel.UpdateViewModel();
            if(Model.ParentModelElement != null)
                ChangeTracker.AcceptChanges();
        }
        #endregion

        #region override of IDisposable
        protected override void OnDisposing()
        {
            _globalEventsService.Unsubscribe<SaveCheckEvent>(OnSaveCheck);
            base.OnDisposing();
        }

        public void SetParentLDevice(ILDevice lDevice)
        {
            _lDevice = lDevice;
            ParentLdName = lDevice.Inst;
            OnUpdateAvailableDatasets();
            ParentLnName = lDevice.LogicalNodeZero.Value.Name;
            SetRoportID();
        }
        #endregion

    }
}
