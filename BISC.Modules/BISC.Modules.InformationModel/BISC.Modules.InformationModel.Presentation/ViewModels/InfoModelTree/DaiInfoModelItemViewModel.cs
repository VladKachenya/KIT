using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Modules.InformationModel.Presentation.Interfaces.Helpers;
using BISC.Modules.InformationModel.Presentation.Interfaces.InfoModelDetails;
using BISC.Modules.InformationModel.Presentation.ViewModels.Base;

namespace BISC.Modules.InformationModel.Presentation.ViewModels.InfoModelTree
{
    public class DaiInfoModelItemViewModel : TreeItemViewModelBase
    {

        private readonly ITreeItemDetailsBuilder _treeItemDetailsBuilder;

        private readonly IInfoModelService _infoModelService;
        private readonly IDataTypeTemplatesModelService _dataTypeTemplatesModelService;

        private List<IInfoModelDetail> _treeItemDetails;


        private string _valueLocal;
        private bool _isByteTransferred;
        private string _value;
        private bool _canEditingValue;
        private bool _isValueChanged;

        public DaiInfoModelItemViewModel(
            ITreeItemDetailsBuilder treeItemDetailsBuilder, 
            IInfoModelService infoModelService, 
            IDataTypeTemplatesModelService dataTypeTemplatesModelService)
        {
            _treeItemDetailsBuilder = treeItemDetailsBuilder;
            _infoModelService = infoModelService;
            _dataTypeTemplatesModelService = dataTypeTemplatesModelService;
        }

        public void UpdateValue()
        {
            if (_model is IDai dai)
            {
                if (dai.Value?.Value?.Value != null)
                {
                    try
                    {

                        var da = _dataTypeTemplatesModelService.GetDaOfDai(dai, dai.GetFirstParentOfType<ISclModel>());
                        if (da.BType == "Enum")
                        {
                            var enumType = _dataTypeTemplatesModelService.GetEnumTypeForDa(da);
                            Value = enumType?.EnumValList.FirstOrDefault((val =>val.Ord.ToString()== ((IDai)_model).Value.Value.Value))?.Value;
                        }
                        else
                        {
                            Value = ((IDai) _model).Value.Value.Value;

                        }
                    }
                    catch (Exception e)
                    {
                        Value = ((IDai) _model).Value.Value.Value;
                    }

                }
            }
        }

        #region Overrides of TreeItemViewModelBase

        public override Brush TypeColorBrush => new SolidColorBrush(Color.FromArgb(0x5F, 0x55, 0x00, 0x50));
        public override string TypeName => "DAI";

        public string Value
        {
            get => _value;
            set
            {
                if (ValueValidationFunction != null && !ValueValidationFunction(value))
                {
                    return;
                }
                SetProperty(ref _value, value);
            }
        }

        public string ValueToolTip { get; set; }

        public bool CanEditingValue
        {
            get => _canEditingValue;
            set
            {
                if (!value)
                {
                    CheckValue();
                }
                SetProperty(ref _canEditingValue, value);
            }
        }

        public bool IsValueChanged
        {
            get => _isValueChanged;
            set
            {
                _isValueChanged = value;
                OnPropertyChanged();
            }
        }

        public override List<IInfoModelDetail> TreeItemDetails
        {
            get { return _treeItemDetails; }
        }

        public Func<string, bool> ValueValidationFunction { get; set;}

        #endregion

        #region private fields

        public void CheckValue()
        {
            IDai dai = (Model as IDai);
            if (dai?.Value?.Value?.Value != Value)
            {
                IsValueChanged = true;
                return;
            }

            IsValueChanged = false;
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
            if (dai.Value?.Value?.Value != null)
            {
                Value = dai.Value.Value.Value;
            }
            base.SetModel(value);

        }

        #endregion

    }
}