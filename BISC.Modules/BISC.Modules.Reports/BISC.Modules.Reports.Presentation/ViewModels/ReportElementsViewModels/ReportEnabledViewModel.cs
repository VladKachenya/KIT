using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels;
using BISC.Presentation.BaseItems.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Reports.Model.Model;

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
            set
            {
                if (value < 1)
                {
                    value = 1;
                }
                if (value > 3)
                {
                    value = 3;
                }
                SetProperty(ref _max, value);
            }
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

        public IRptEnabled GetUpdatedModel()
        {
            IRptEnabled rptEnabled=new RptEnabled();
            rptEnabled.Max = Max;
            return rptEnabled;
        }

        public void UpdateViewModel()
        {
            Max = _model.Max;
        }
    }
}
