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
    public class OprionalFildsViewModel : ViewModelBase, IOprionalFildsViewModel
    {
        private bool _sequenceNumber;
        private bool _reportTimeStamp;
        private bool _reasonForInclusion;
        private bool _dataSetName;
        private bool _dataReference;
        private bool _bufferOverflow;
        private bool _entruID;
        private bool _configRevision;
        private IOptFields _model;
        private bool _segmentation;

        #region ctor
        public OprionalFildsViewModel()
        {
           
        }
        #endregion
        public void ActivateElement()
        {
            ChangeTracker.SetTrackingEnabled(true);
        }
        public bool SequenceNumber
        {
            get => _sequenceNumber;
            set => SetProperty(ref _sequenceNumber, value);
        }
        public bool ReportTimeStamp
        {
            get => _reportTimeStamp;
            set => SetProperty(ref _reportTimeStamp, value);
        }
        public bool ReasonForInclusion
        {
            get => _reasonForInclusion;
            set => SetProperty(ref _reasonForInclusion, value);
        }
        public bool DataSetName
        {
            get => _dataSetName;
            set => SetProperty(ref _dataSetName, value);
        }
        public bool DataReference
        {
            get => _dataReference;
            set => SetProperty(ref _dataReference, value);
        }
        public bool BufferOverflow
        {
            get => _bufferOverflow;
            set => SetProperty(ref _bufferOverflow, value);
        }
        public bool EntruID
        {
            get => _entruID;
            set => SetProperty(ref _entruID, value);
        }
        public bool ConfigRevision
        {
            get => _configRevision;
            set => SetProperty(ref _configRevision, value);
        }

        public bool Segmentation
        {
            get => _segmentation;
            set => SetProperty(ref _segmentation , value);
        }


        public IOptFields Model
        {
            get => _model;
            set
            {
                _model = value;
                UpdateViewModel();
            }
        }

        public IOptFields GetUpdatedModel()
        {
            IOptFields optFields=new OptFields();
            optFields.SeqNum = SequenceNumber;
            optFields.TimeStamp = ReportTimeStamp;
            optFields.ReasonCode = ReasonForInclusion;
            optFields.DataSet = DataSetName;
            optFields.DataRef = DataReference;
            optFields.BufOvfl = BufferOverflow;
            optFields.EntryID = EntruID;
            optFields.ConfigRef = ConfigRevision;
            optFields.Segmentation = Segmentation;
            return optFields;
        }

        public void UpdateViewModel()
        {
            SequenceNumber = _model.SeqNum;
            ReportTimeStamp = _model.TimeStamp;
            ReasonForInclusion = _model.ReasonCode;
            DataSetName = _model.DataSet;
            DataReference = _model.DataRef;
            BufferOverflow = _model.BufOvfl;
            EntruID = _model.EntryID;
            ConfigRevision = _model.ConfigRef;
            Segmentation = _model.Segmentation;

        }
    }
}
