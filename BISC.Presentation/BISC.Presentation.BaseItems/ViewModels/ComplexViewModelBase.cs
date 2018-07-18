using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.Infrastructure.ViewModel;

namespace BISC.Presentation.BaseItems.ViewModels
{
    public class ComplexViewModelBase:ViewModelBase,IEditableViewModel,ISelectableViewModel
    {
        private bool _isEditable;
        private bool _isSelected;

        public bool IsEditable
        {
            get => _isEditable;
            set =>SetProperty(ref _isEditable,value);
        }

        public virtual void SetIsEditable(bool isEditable)
        {
            IsEditable = isEditable;
        }

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

       public virtual void SetIsSelected(bool isSelected)
       {
           IsSelected = isSelected;
       }
    }
}
