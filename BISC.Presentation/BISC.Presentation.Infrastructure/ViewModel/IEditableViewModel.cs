namespace BISC.Presentation.Infrastructure.ViewModel
{
    public interface IEditableViewModel
    {
        bool IsEditable { get; set; }
        void SetIsEditable(bool isEditable);

        bool IsReadOnly { get; }

        void SetIsReadOnly(bool isReadOnly);
    }
}