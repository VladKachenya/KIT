using System;
using System.Collections.Generic;
using BISC.Modules.Gooses.Presentation.Interfaces.GooseSubscriptionLight;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix;

namespace BISC.Modules.Gooses.Presentation.Interfaces.Factories
{
    public interface IGooseMatrixLightViewModelFactory
    {
        Tuple<List<IGoInViewModel>, List<IGooseDataReferenceViewModel>> CreateGooseMatrixLightViewModel(List<GooseControlBlockViewModel> gooseMatrix);
    }
}