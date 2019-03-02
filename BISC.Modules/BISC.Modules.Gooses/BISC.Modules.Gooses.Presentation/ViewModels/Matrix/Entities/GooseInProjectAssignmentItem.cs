using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Presentation.Infrastructure.Factories;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Matrix.Entities
{
    public class GooseInProjectAssignmentItem : GooseControlBlockAssignmentItem
    {
        public IGooseControl Model { get; }

        public GooseInProjectAssignmentItem(ICommandFactory commandFactory, IGooseControl model)
        {
            Model = model;
            SelectAllCommand = commandFactory.CreatePresentationCommand(OnSelectAll);
            UnSelectAllCommand = commandFactory.CreatePresentationCommand(OnUnSelectAll);

        }
        private void OnUnSelectAll()
        {
            foreach (var fcdaAssignmentItem in FcdaAssignmentItems)
            {
                fcdaAssignmentItem.IsSubscribed = false;

            }
        }

        private void OnSelectAll()
        {
            foreach (var fcdaAssignmentItem in FcdaAssignmentItems)
            {
                fcdaAssignmentItem.IsSubscribed = true;
            }
        }


        public ICommand SelectAllCommand { get; }
        public ICommand UnSelectAllCommand { get; }
    }
}
