using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Presentation.Commands;
using BISC.Modules.Gooses.Presentation.Interfaces.Factories;
using BISC.Presentation.Infrastructure.Factories;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Matrix.Entities
{
    public class GooseFtpOnlyAssignmentItem: GooseControlBlockAssignmentItem
    {
        public IGoCbFtpEntity Model { get; }

        public GooseFtpOnlyAssignmentItem(ICommandFactory commandFactory, IGoCbFtpEntity model)
        {
            Model = model;
            RemoveFcdaElement = commandFactory.CreatePresentationCommand<object>(OnRemoveFcdaElement);
        }

        private void OnRemoveFcdaElement(object obj)
        {
            if (obj is FcdaAssignmentItem fcdaAssignmentItem)
            {
                FcdaAssignmentItems.Remove(fcdaAssignmentItem);
            }
        }

        public ICommand RemoveFcdaElement { get; }
    }
}
