using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels;
using BISC.Presentation.BaseItems.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BISC.Modules.Reports.Presentation.ViewModels.ReportElementsViewModels
{
    public class ReportControlViewModel : ViewModelBase, IReportControlViewModel
    {
        private IReportControl _model;
        private string _name;
        private string _reportID;
        private bool _isBuffered;
        private int _bufferTime;
        private string _dataSetName;
        private int _integrutyPeriod;


        #region ctor
        public ReportControlViewModel(IReportEnabledViewModel reportEnabledViewModel, ITriggerOptionsViewModel triggerOptionsViewModel,
            IOprionalFildsViewModel oprionalFildsViewModel)
        {
            ReportEnabledViewModel = reportEnabledViewModel;
            TriggerOptionsViewModel = triggerOptionsViewModel;
            OprionalFildsViewModel = oprionalFildsViewModel;
        }
        #endregion

        #region private methods


        #endregion

        #region Implementation of IReportControlViewModel
        public string ElementName => "Report";
        public Brush TypeColorBrush => new SolidColorBrush(Color.FromRgb(240, 126, 184));
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public string ReportID
        {
            get => _reportID;
            set => SetProperty(ref _reportID, value);
        }
        public bool IsBuffered
        {
            get => _isBuffered;
            set => SetProperty(ref _isBuffered, value);
        }
        public int BufferTime
        {
            get => _bufferTime;
            set => SetProperty(ref _bufferTime, value);
        }
        public string DataSetName
        {
            get => _dataSetName;
            set => SetProperty(ref _dataSetName, value);
        }
        public int IntegrutyPeriod
        {
            get => _integrutyPeriod;
            set => SetProperty(ref _integrutyPeriod, value);
        }

        public IReportEnabledViewModel ReportEnabledViewModel { get; }
        public ITriggerOptionsViewModel TriggerOptionsViewModel { get; }
        public IOprionalFildsViewModel OprionalFildsViewModel { get; }
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

        //public string PrefixName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void UpdateModel()
        {
            _model.Name = Name;
            _model.RptID = ReportID;
            _model.Buffered = IsBuffered;
            _model.BufTime = BufferTime;
            _model.DataSet = DataSetName;
            _model.IntgPd = IntegrutyPeriod;
        }

        public void UpdateViewModel()
        {
            this.Name = _model.Name;
            this.ReportID = _model.RptID;
            this.IsBuffered = _model.Buffered;
            this.BufferTime = _model.BufTime;
            this.DataSetName = _model.DataSet;
            this.IntegrutyPeriod = _model.IntgPd;
        }
        #endregion

    }
}
