using System.Collections.Generic;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix.Entities;

namespace BISC.Modules.Gooses.Presentation.Interfaces.Factories
{
    public interface IGooseControlBlockAssignmentItemFactory
    {
        IEnumerable<GooseControlBlockAssignmentItem> CreateGooseControlBlockAssignmentItems(IDevice deviceOfTheItems);
    }
}