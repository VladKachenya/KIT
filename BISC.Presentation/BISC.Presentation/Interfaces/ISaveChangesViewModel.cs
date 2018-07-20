using System.Collections.Generic;
using System.Windows.Input;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Presentation.Interfaces
{
    public interface ISaveChangesViewModel
    {
        object InputParameter { get; set; }
        List<SaveCheckingEntity> UnsavedEntities { get; set; }
        ICommand SaveCommand { get; }
        ICommand DontSaveCommand { get; }
        ICommand CancelCommand { get; }
    }
}