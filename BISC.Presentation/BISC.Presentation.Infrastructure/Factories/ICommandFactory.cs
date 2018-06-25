using System;
using System.Windows.Input;

namespace BISC.Presentation.Infrastructure.Factories
{
    public interface ICommandFactory
    {
        ICommand CreateDelegateCommand(Action<CommandArguments> execute, Func<CommandArguments, bool> canExecute);
    }

    public class CommandArguments
    {

    }
}