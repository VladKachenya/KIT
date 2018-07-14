using System;

namespace BISC.Presentation.Infrastructure.Commands
{
    public interface ITrackableCommand<T>:IPresentationCommand<T>
    {
        Action<T> UnDoAction { get;  }
    }
    public interface ITrackableCommand : IPresentationCommand
    {
        Action UnDoAction { get;  }
    }
}