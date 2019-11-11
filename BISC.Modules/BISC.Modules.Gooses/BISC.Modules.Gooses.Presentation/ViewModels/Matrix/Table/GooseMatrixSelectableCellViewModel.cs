using BISC.Infrastructure.Global.Services;
using BISC.Modules.Gooses.Presentation.Interfaces;
using BISC.Presentation.BaseItems.ViewModels;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Matrix.Table
{
    public class GooseMatrixSelectableCellViewModel : ViewModelBase, IGooseMatrixSelectableCellViewModel
    {
        private readonly IGlobalEventsService _globalEventsService;
        private readonly IConfigurationService _configurationService;
        private bool _isSelectingEnabled;
        private string _toolTip;
        private bool _selectedValue;

        #region Ctor

        public GooseMatrixSelectableCellViewModel(
            IGlobalEventsService globalEventsService,
            IConfigurationService configurationService)
        {
            // It needs to delete if it don't use
            _globalEventsService = globalEventsService;
            _configurationService = configurationService;
        }
        #endregion

        #region Inplementation of IGooseMatrixSelectableCellViewModel

        public bool IsSelectingEnabled
        {
            get => _isSelectingEnabled;
            set => SetProperty(ref _isSelectingEnabled, value);
        }

        public string ToolTip
        {
            get => _toolTip;
            set => SetProperty(ref _toolTip, value);

        }

        public bool SelectedValue
        {
            get => _selectedValue;
            set => SetProperty(ref _selectedValue, value);

        }

        public void SetValue(bool value)
        {
            SetProperty(ref _selectedValue, value, propertyName: nameof(SelectedValue));
        }
        #endregion

        #region Overrides of DisposableBindableBase

        protected override void OnDisposing()
        {
            base.OnDisposing();
        }

        #endregion
    }
}