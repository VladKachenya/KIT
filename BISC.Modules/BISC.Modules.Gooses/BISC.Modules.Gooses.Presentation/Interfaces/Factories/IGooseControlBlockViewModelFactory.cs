using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix;

namespace BISC.Modules.Gooses.Presentation.Interfaces.Factories
{
    public interface IGooseControlBlockViewModelFactory
    {
       Task<OperationResult<List<GooseControlBlockViewModel>>> BuildGooseControlBlockViewModels(ISclModel sclModel, IDevice device);
    }
}