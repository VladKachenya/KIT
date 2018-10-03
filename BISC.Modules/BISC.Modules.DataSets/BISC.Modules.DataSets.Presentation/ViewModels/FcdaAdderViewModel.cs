using BISC.Modules.DataSets.Infrastructure.ViewModels;
using BISC.Presentation.BaseItems.Commands;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BISC.Modules.DataSets.Presentation.ViewModels
{
    public class FcdaAdderViewModel : ViewModelBase, IFcdaAdderViewModel
    {
        #region private filds

        #endregion

        #region C-tor
        public FcdaAdderViewModel(ICommandFactory commandFactory)
        {
            CloseCommand = commandFactory.CreatePresentationCommand((() =>
            {
                DialogCommands.CloseDialogCommand.Execute(null, null);
            }));
        }
        #endregion
        #region private methods

        #endregion


        #region Implementation of IFcdaAdderViewModel
        public string TestProp => "Hellow my friend!";
        public ICommand CloseCommand { get; }
        #endregion

    }
}
