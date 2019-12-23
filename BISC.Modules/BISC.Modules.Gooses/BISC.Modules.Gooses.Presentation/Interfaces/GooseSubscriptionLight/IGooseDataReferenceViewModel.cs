namespace BISC.Modules.Gooses.Presentation.Interfaces.GooseSubscriptionLight
{
    public interface IGooseDataReferenceViewModel
    {
        string DeviceName { get; set; }
        string GooseName { get; set; }
        string DoiDataReference { get; set; }
        string DataSetReferenceState { get; set; }
        string DataSetReferenceQuality { get; set; }
        bool IsUsing { get; set; }
    }
}