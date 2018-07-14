using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.Infrastructure.Commands;
using Prism.Commands;

namespace BISC.Presentation.Commands
{
    public class TrackableCommand:PresentationCommand,ITrackableCommand
    {
        public TrackableCommand(DelegateCommand delegateCommand,Action undoAction) : base(delegateCommand)
        {
            UnDoAction = undoAction;
        }

        #region Implementation of ITrackableCommand

        public Action UnDoAction { get; set; }

        #endregion
    }
    public class TrackableCommand<T> : PresentationCommand<T>, ITrackableCommand<T>
    {
        public TrackableCommand(DelegateCommand<T> delegateCommand, Action<T> undoAction) : base(delegateCommand)
        {
            UnDoAction = undoAction;
        }

        #region Implementation of ITrackableCommand

        public Action<T> UnDoAction { get; set; }

        #endregion
    }
}
