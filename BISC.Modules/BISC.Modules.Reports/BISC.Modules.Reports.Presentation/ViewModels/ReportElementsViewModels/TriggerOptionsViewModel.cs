using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Presentation.BaseItems.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Reports.Model.Model;
using BISC.Modules.Reports.Presentation.Interfaces.ViewModels;

namespace BISC.Modules.Reports.Presentation.ViewModels.ReportElementsViewModels
{
    public class TriggerOptionsViewModel : ViewModelBase, ITriggerOptionsViewModel
    {
        private bool _dataChange;
        private bool _qualityChange;
        private bool _dataUpdate;
        private bool _integrity;
        private bool _genetralInterrogation;
        private ITrgOps _model;

        #region Ctor
        public TriggerOptionsViewModel()
        {}
        #endregion



        #region Implementation of ITriggerOptionsViewModel
        public bool DataChange
        {
            get => _dataChange;
            set => SetProperty(ref _dataChange, value);
        }
        public bool QualityChange
        {
            get => _qualityChange;
            set => SetProperty(ref _qualityChange, value);
        }
        public bool DataUpdate
        {
            get => _dataUpdate;
            set => SetProperty(ref _dataUpdate, value);
        }
        public bool Integrity
        {
            get => _integrity;
            set => SetProperty(ref _integrity, value);
        }
        public bool GenetralInterrogation
        {
            get => _genetralInterrogation;
            set => SetProperty(ref _genetralInterrogation, value);
        }
        public ITrgOps Model
        {
            get => _model;
            set
            {
                _model = value;
                UpdateViewModel();
            }
        }

        public void ActivateElement()
        {
            ChangeTracker.SetTrackingEnabled(true);
        }

        public ITrgOps GetUpdatedModel()
        {
            ITrgOps trgOps=new TrgOps();
            trgOps.Dchg = DataChange;
            trgOps.Qchg = QualityChange;
            trgOps.Dupd = DataUpdate;
            trgOps.Period = Integrity;
            trgOps.Gi = GenetralInterrogation;
            return trgOps;
        }

        public void UpdateViewModel()
        {
            DataChange = _model.Dchg;
            QualityChange = _model.Qchg;
            DataUpdate = _model.Dupd;
            Integrity = _model.Period;
            GenetralInterrogation = _model.Gi;

        }
        #endregion
    }
}
