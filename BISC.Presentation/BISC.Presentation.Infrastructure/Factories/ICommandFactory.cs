using System;
using System.Windows.Input;

namespace BISC.Presentation.Infrastructure.Factories
{
    public interface ICommandFactory
    {
        ICommand CreateDelegateCommand(Action<object> execute, Func<object, bool> canExecute);
    }
}