using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.Infrastructure.Commands;
using Prism.Commands;

namespace BISC.Presentation.Commands
{
    //public class AsyncCommand : IAsyncCommand
    //{
    //    private readonly DelegateCommand _delegateCommand;

    //    public AsyncCommand(DelegateCommand delegateCommand) 
    //    {
    //        _delegateCommand = delegateCommand;
    //    }

    //    #region Implementation of IAsyncCommand

    //    public Task ExecuteAsync()
    //    {
    //        return Task.Run(()=>_delegateCommand.Execute());
    //    }

    //    #endregion

    //    #region Implementation of ICommand

    //    public bool CanExecute(object parameter)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Execute(object parameter)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public event EventHandler CanExecuteChanged;

    //    #endregion
    //}

    //public class AsyncCommand<T> : IAsyncCommand<T>
    //{
    //    private readonly DelegateCommand<T> _delegateCommand;

    //    public AsyncCommand(DelegateCommand<T> delegateCommand)
    //    {
    //        _delegateCommand = delegateCommand;
    //    }

    //    #region Implementation of IAsyncCommand<T>

    //    public Task ExecuteAsync(T parameter)

    //    {
    //        return Task.Run(() => _delegateCommand.Execute(parameter));
    //    }


    //    #endregion
    //}
}
