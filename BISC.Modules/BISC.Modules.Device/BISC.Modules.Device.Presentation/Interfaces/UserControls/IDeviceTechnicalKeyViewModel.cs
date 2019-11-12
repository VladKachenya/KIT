namespace BISC.Modules.Device.Presentation.Interfaces.UserControls
{
    public interface IDeviceTechnicalKeyViewModel
    {
        string TechKey { get; set; }

        bool IsValid { get;}

        void SetTechKey(string techKey);
    }
}