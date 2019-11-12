using System.Text.RegularExpressions;
using BISC.Modules.Device.Presentation.Interfaces.UserControls;
using BISC.Presentation.BaseItems.ViewModels;

namespace BISC.Modules.Device.Presentation.ViewModels.UserControlsViewModels
{
    public class DeviceTechnicalKeyViewModel : ViewModelBase, IDeviceTechnicalKeyViewModel
    {
        private string _techKey;
        private readonly Regex _nameValidationRegex;
        private bool _isValid;

        public DeviceTechnicalKeyViewModel()
        {
            _nameValidationRegex = new Regex(@"^[a-zA-Z0-9_]{0,55}$");
        }

        public string TechKey
        {
            get => _techKey;
            set
            {
                if (_nameValidationRegex.IsMatch(value))
                {
                    _techKey = value;
                    ValidateTechKey();
                    OnPropertyChanged();
                }
            }
        }

        public bool IsValid => _isValid;

        public void SetTechKey(string techKey)
        {
            _techKey = techKey;
            ValidateTechKey();
            OnPropertyChanged(nameof(TechKey));
        }

        private void ValidateTechKey()
        {
            _isValid = _techKey.Length >= 3 && _techKey.Length <= 55;
            OnPropertyChanged(nameof(IsValid));
        }
    }
}