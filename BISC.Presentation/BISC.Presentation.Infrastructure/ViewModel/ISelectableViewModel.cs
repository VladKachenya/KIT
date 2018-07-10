namespace BISC.Presentation.Infrastructure.ViewModel
{
    public interface ISelectableViewModel
    {
        bool IsSelected { get; set; }
        void SetIsSelected(bool isSelected);
    }
}