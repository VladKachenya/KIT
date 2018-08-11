using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Presentation.Commands;
using BISC.Presentation.Infrastructure.Commands;
using BISC.Presentation.Infrastructure.Factories;
using Prism.Commands;

namespace BISC.Presentation.Factories
{
   public class CommandFactory:ICommandFactory
    {
        public CommandFactory()
        {
            
        }


        #region Implementation of ICommandFactory

        

        #endregion

        #region Implementation of ICommandFactory

        public IPresentationCommand<T> CreatePresentationCommand<T>(Action<T> execute, Func<T, bool> canExecute=null)
        {
            return CreateDelegateCommand(execute, canExecute).GetPresentationCommand();
        }

        public IPresentationCommand CreatePresentationCommand(Action execute, Func<bool> canExecute = null)
        {
            return CreateDelegateCommand(execute, canExecute).GetPresentationCommand();
        }
        
        public ITrackableCommand<T> CreateTrackableCommand<T>(Action<T> execute, Action<T> undo, Func<T, bool> canExecute = null)
        {
           return new TrackableCommand<T>(CreateDelegateCommand<T>(execute,canExecute),undo);
        }

        public ITrackableCommand CreateTrackableCommand(Action execute, Action undo, Func<bool> canExecute = null)
        {
            return new TrackableCommand(CreateDelegateCommand(execute, canExecute), undo);
        }

        //public IAsyncCommand<T> CreateAsyncCommand<T>(Action<T> execute, Func<T, bool> canExecute = null)
        //{
        //    return CreateDelegateCommand(execute, canExecute).GetAsyncCommand();
        //}

        //public IAsyncCommand CreateAsyncCommand(Action execute, Func<bool> canExecute = null)
        //{
        //    return CreateDelegateCommand(execute, canExecute).GetAsyncCommand();
        //}

        private DelegateCommand CreateDelegateCommand(Action execute, Func<bool> canExecute = null)
        {
            if (canExecute == null)
            {
                return new DelegateCommand(execute);
            }
            else
            {
                return new DelegateCommand(execute, canExecute);
            }

        }
        private DelegateCommand<T> CreateDelegateCommand<T>(Action<T> execute, Func<T, bool> canExecute = null)
        {
            if (canExecute == null)
            {
                return new DelegateCommand<T>(execute);
            }
            else
            {
                return new DelegateCommand<T>(execute, canExecute);
            }
        }

        #endregion
    }

    public static class CommandsExtensions
    {
        public static IPresentationCommand<T> GetPresentationCommand<T>(this DelegateCommand<T> delegateCommand)
        {
            return new PresentationCommand<T>(delegateCommand);
        }
        public static IPresentationCommand GetPresentationCommand(this DelegateCommand delegateCommand)
        {
            return new PresentationCommand(delegateCommand);
        }
        //public static IAsyncCommand GetAsyncCommand(this DelegateCommand delegateCommand)
        //{
        //    return new AsyncCommand(delegateCommand);
        //}
        //public static IAsyncCommand<T> GetAsyncCommand<T>(this DelegateCommand<T> delegateCommand)
        //{
        //    return new AsyncCommand<T>(delegateCommand);
        //}
    }

   
}
