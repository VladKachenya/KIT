using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Presentation.Interfaces.Helpers;
using BISC.Modules.InformationModel.Presentation.Interfaces.InfoModelDetails;
using BISC.Modules.InformationModel.Presentation.ViewModels.Base;

namespace BISC.Modules.InformationModel.Presentation.ViewModels.InfoModelTree
{
   public class DaiInfoModelItemViewModel : TreeItemViewModelBase
    {

        private readonly ITreeItemDetailsBuilder _treeItemDetailsBuilder;
        //private readonly IProjectSclModel _projectSclModel;
        //private readonly IValuesEditingController _valuesEditingController;
        private List<IInfoModelDetail> _treeItemDetails;
    

        private string _valueLocal;
        private bool _isByteTransferred;
        private Guid _daiTreeItemViewModelGuid;
        private string _value;

        public DaiInfoModelItemViewModel(ITreeItemDetailsBuilder treeItemDetailsBuilder)
        {
            _treeItemDetailsBuilder = treeItemDetailsBuilder;        
            _daiTreeItemViewModelGuid = Guid.NewGuid();
        
        }

        public void UpdateValue()
        {
            if ((_model as IDai)?.Value?.Value?.Value != null)
            {
                Value = ((IDai) _model).Value.Value.Value;
            }
        }

        //private void OnDeviceConnectonChanged()
        //{
        //    if (_projectSclModel.Scl.IED.Count > 0)
        //    {
        //        var isLocalDataTransferred = _projectSclModel.Scl.IED[0]?.IsLocalDataTransferred;
        //        if (isLocalDataTransferred != null)
        //            _isByteTransferred = (bool)isLocalDataTransferred;
        //    }
        //    (EditDeviceValueCommand as DelegateCommand)?.RaiseCanExecuteChanged();
        //    (EditLocalValueCommand as DelegateCommand)?.RaiseCanExecuteChanged();
        //    (TransferValueFromLocalToDeviceCommand as DelegateCommand)?.RaiseCanExecuteChanged();
        //}

        //private bool CanExecuteTransferValueFromLocalToDevice()
        //{
        //    tDAI dai = _model as tDAI;
        //    if (!_projectSclModel.IsDeviceConnected) return false;
        //    return (dai.ValFromDevice != null) && (dai.Val != null);
        //}

        //private bool CanExecuteEditLocalValue()
        //{
        //    if (_projectSclModel.Scl.IsLoadedFromFile)
        //    {
        //        return true;
        //    }
        //    else if (_isByteTransferred)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //private bool CanExecuteEditDeviceValue()
        //{
        //    return _projectSclModel.IsDeviceConnected;
        //}

        //private async void OnTransferValueFromLocalToDeviceExecute()
        //{
        //    await _valuesEditingController.TransferValueFromLocalToDevice(_model);
        //    Update();
        //}

        //private void OnEEditLocalValueExecute()
        //{
        //    _valuesEditingController.EditValueLocal(_model);
        //    Update();
        //}

        //private void OnEditDeviceValueExecute()
        //{
        //    _valuesEditingController.EditValueInDevice(_model);
        //    Update();
        //}


        #region Overrides of TreeItemViewModelBase

        public override Brush TypeColorBrush =>new SolidColorBrush(Color.FromArgb(0x5F,0x55,0x00,0x50));
        public override string TypeName => "DAI";

        public string Value
        {
            get => _value;
            set => SetProperty(ref _value , value);
        }
        //public ICommand EditDeviceValueCommand { get; }
        //public ICommand EditLocalValueCommand { get; }
        //public ICommand TransferValueFromLocalToDeviceCommand { get; }


        public override List<IInfoModelDetail> TreeItemDetails
        {
            get { return _treeItemDetails; }
        }

        #endregion


        #region Overrides of TreeItemViewModelBase

        protected override void SetModel(object value)
        {
            IDai dai = (value as IDai);
            Header = dai.Name;
            _treeItemDetailsBuilder.Reset();
            _treeItemDetailsBuilder.AddStringDetail("Имя", dai.Name);
            _treeItemDetails = _treeItemDetailsBuilder.Build();
            if (dai.Value?.Value?.Value!= null)
            {
                Value = dai.Value.Value.Value;
            }
            //if (_projectSclModel.Scl.IsLoadedFromFile && dai.Val != null)
            //{
            //    ValueLocal = dai.Val;
            //}
            base.SetModel(value);

        }

        #endregion

        //#region Implementation of IValueContainingViewModel

        //public void Update()
        //{
        //    tDAI dai = (_model as tDAI);
        //    if (dai.ValueFromDevice != null)
        //    {
        //        Value = dai.ValueFromDevice;
        //    }
        //    if (dai.Val != null)
        //    {
        //        ValueLocal = dai.Val;
        //    }
        //}

        //public string Value
        //{
        //    get { return _value; }
        //    set
        //    {
        //        _value = value;
        //        RaisePropertyChanged();
        //    }
        //}


        //public string ValueLocal
        //{
        //    get { return _valueLocal; }
        //    set
        //    {
        //        _valueLocal = value;
        //        RaisePropertyChanged();
        //    }
        //}


        //#region Overrides of TreeItemViewModelBase

        //protected override void OnDisposing()
        //{
        //    _projectSclModel.ConnectionChangedEventSubscription.TryDeleteActionByKey(
        //        "daiTreeItemNum" + _daiTreeItemViewModelGuid.ToString());
        //    base.OnDisposing();
        //}

        //#endregion

        //#endregion
    }
}