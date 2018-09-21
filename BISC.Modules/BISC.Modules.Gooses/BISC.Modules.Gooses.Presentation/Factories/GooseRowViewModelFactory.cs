using BISC.Infrastructure.Global.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Gooses.Presentation.Factories
{
    //public class GooseRowViewModelFactory 
    //{
    //    private readonly IInjectionContainer _container;

    //    public GooseRowViewModelFactory(IInjectionContainer container)
    //    {
    //        _container = container;
    //    }


    //    #region Implementation of IGooseRowViewModelFactory

    //    public IGooseRowViewModel CreateGooseRowViewModel(IGooseRow gooseRow, IGooseControlBlockViewModel gooseControlBlockViewModel)
    //    {
    //        IGooseRowViewModel gooseRowViewModel = _container.Resolve<IGooseRowViewModel>(gooseRow.StrongName +
    //                                               ApplicationGlobalNames.CommonInjectionStrings.VIEW_MODEL);
    //        gooseRowViewModel.Model = gooseRow;
    //        gooseRowViewModel.Parent = gooseControlBlockViewModel;
    //        return gooseRowViewModel;

    //    }

    //    #endregion
    //}
}
