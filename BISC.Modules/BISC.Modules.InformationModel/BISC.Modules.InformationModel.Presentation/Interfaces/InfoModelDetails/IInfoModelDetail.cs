namespace BISC.Modules.InformationModel.Presentation.Interfaces.InfoModelDetails
{
    public interface IInfoModelDetail
    {
        string DetailDescription { get; set; }
        object DetailValue { get; set; }
        string ToolTip { get; set; }
        bool IsGrouped { get; set; }
    }
}