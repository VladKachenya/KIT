namespace BISC.Presentation.Infrastructure.ViewModel
{
    public interface IEditableViewModel
    {
        bool IsEditable { get; set; }
        void SetIsEditable(bool isEditable);

    }
}