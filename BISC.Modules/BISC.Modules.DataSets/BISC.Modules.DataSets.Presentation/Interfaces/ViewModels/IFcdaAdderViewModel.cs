using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BISC.Modules.DataSets.Infrastructure.ViewModels
{
    public interface IFcdaAdderViewModel
    {
        ICommand CloseCommand { get; }
    }
}
