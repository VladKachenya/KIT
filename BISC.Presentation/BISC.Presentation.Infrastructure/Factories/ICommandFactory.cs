using System;
using System.Windows.Input;
using BISC.Presentation.Infrastructure.Commands;

namespace BISC.Presentation.Infrastructure.Factories
{
    public interface ICommandFactory
    {
        IPresentationCommand<T> CreatePresentationCommand<T>(Action<T> execute, Func<T, bool> canExecute=null);
        IPresentationCommand CreatePresentationCommand(Action execute, Func<bool> canExecute = null);
        ITrackableCommand<T> CreateTrackableCommand<T>(Action<T> execute,Action<T> undo, Func<T, bool> canExecute = null);
        ITrackableCommand CreateTrackableCommand(Action execute,Action undo, Func<bool> canExecute = null);
    }

}