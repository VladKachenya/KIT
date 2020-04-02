using BISC.Presentation.BaseItems.ViewModels.Behaviors;
using BISC.Presentation.Infrastructure.ViewModel;

namespace BISC.Presentation.BaseItems.ViewModels
{
    public class ComplexViewModelBase : ViewModelBase, IEditableViewModel, ISelectableViewModel
    {
        private bool _isEditable;
        private bool _isReadOnly;
        private bool _isSelected;
        public BlockViewModelBehavior BlockViewModelBehavior { get; set; }=new BlockViewModelBehavior();
        public bool IsEditable
        {
            get => _isEditable;
            set => SetProperty(ref _isEditable, value, true);
        }

        public virtual void SetIsEditable(bool isEditable)
        {
            IsEditable = isEditable;
        }

        public bool IsReadOnly
        {
            get => _isReadOnly;
            protected set => SetProperty(ref _isReadOnly, value, true);
        }

        public virtual void SetIsReadOnly(bool isReadOnly)
        {
            IsReadOnly = isReadOnly;
        }

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value, true);
        }

        public virtual void SetIsSelected(bool isSelected)
        {
            IsSelected = isSelected;
        }
    }
}
