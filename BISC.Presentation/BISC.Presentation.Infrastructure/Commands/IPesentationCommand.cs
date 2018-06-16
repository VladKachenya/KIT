using System;

namespace BISC.Presentation.Infrastructure.Commands
{
    public interface IPesentationCommand<T>
    {
        Action<T> ExecuteAction { get; set; }
        Func<T,bool> CanExecuteAction { get; set; }
        void RaiseCanExecute();
    }
}