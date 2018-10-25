using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels;
using BISC.Presentation.BaseItems.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Reports.Presentation.ViewModels.ReportElementsViewModels
{
    public class ReportEnabledViewModel :ViewModelBase, IReportEnabledViewModel
    {
        private int _max;
        private IRptEnabled _model;
        public ReportEnabledViewModel()
        {
          

        }

        public void ActivateElement()
        {
            ChangeTracker.SetTrackingEnabled(true);
        }
        public int Max
        {
            get => _max;
            set => SetProperty(ref _max, value);
        }
        public IRptEnabled Model
        {
            get => _model;
            set
            {
                _model = value;
                UpdateViewModel();
            }
        }

        public void UpdateModel()
        {
            _model.Max = Max;
        }

        public void UpdateViewModel()
        {
            Max = _model.Max;
        }
    }
}
