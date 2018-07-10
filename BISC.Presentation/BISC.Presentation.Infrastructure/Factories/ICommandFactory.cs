using System;
using System.Windows.Input;

namespace BISC.Presentation.Infrastructure.Factories
{
    public interface ICommandFactory
    {
        ICommand CreateDelegateCommand<T>(Action<T> execute, Func<T, bool> canExecute=null);
        ICommand CreateDelegateCommand(Action execute, Func<bool> canExecute = null);

    }

}