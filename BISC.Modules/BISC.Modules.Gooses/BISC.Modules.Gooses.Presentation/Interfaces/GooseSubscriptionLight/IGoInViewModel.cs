namespace BISC.Modules.Gooses.Presentation.Interfaces.GooseSubscriptionLight
{
    public interface IGoInViewModel
    {
        string Name { get; set; }
        int Number { get; set; }
        bool EnableState { get; set; }
        bool IsSetSateEnable { get; set; }
        bool EnableQuality { get; set; }
        bool IsSetQualityEnable { get; set; }
        bool EnableGooseMonitoring { get; set; }
        bool IsSetGooseMonitoringEnable { get; set; }

        IGooseDataReferenceViewModel GooseDataReferenceViewModel { get; set; }
    }
}