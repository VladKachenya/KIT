using System;
using System.Windows.Input;

namespace BISC.Presentation.Infrastructure.Commands
{
    public interface IPresentationCommand<T>:ICommand
    {
        Action<T> ExecuteAction { get;  }
        Func<T,bool> CanExecuteAction { get;  }
        void RaiseCanExecute();
    }
    public interface IPresentationCommand:ICommand
    {
        Action ExecuteAction { get;  }
        Func<bool> CanExecuteAction { get; }
        void RaiseCanExecute();
    }
}