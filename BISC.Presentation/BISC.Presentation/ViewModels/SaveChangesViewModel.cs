using BISC.Presentation.BaseItems.Commands;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Interfaces;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using BISC.Presentation.Infrastructure.HelperEntities;

namespace BISC.Presentation.ViewModels
{
    public class SaveChangesViewModel : NavigationViewModelBase, ISaveChangesViewModel
    {
        private List<SaveCheckingEntity> _unsavedEntities;
        private object _inputParameter;
        private SaveResult _saveResult;
        public SaveChangesViewModel(ICommandFactory commandFactory)
            : base(null)
        {
            CancelCommand = commandFactory.CreatePresentationCommand(OnCancelExecute);
            SaveCommand = commandFactory.CreatePresentationCommand(OnSaveExecute);
            DontSaveCommand = commandFactory.CreatePresentationCommand(OnDontSaveExecute);

        }

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            UnsavedEntities =
                navigationContext.BiscNavigationParameters.GetParameterByName<List<SaveCheckingEntity>>(
                    SaveCheckingEntity.NavigationKey);
            _saveResult = navigationContext.BiscNavigationParameters.GetParameterByName<SaveResult>(
                nameof(SaveResult));
            base.OnNavigatedTo(navigationContext);
        }

        public object InputParameter
        {
            get => _inputParameter;
            set
            {
                _inputParameter = value;
                OnPropertyChanged();
            }
        }

        public List<SaveCheckingEntity> UnsavedEntities
        {
            get => _unsavedEntities;
            set
            {
                _unsavedEntities = value;
                OnPropertyChanged();
            }
        }

        private void OnSaveExecute()
        {
            _saveResult.IsSaved = true;

            CloseView();
        }

        private void OnDontSaveExecute()
        {
            _saveResult.IsDeclined = true;

            CloseView();
        }

        private void OnCancelExecute()
        {
            _saveResult.IsCancelled = true;
            CloseView();
        }

        private void CloseView()
        {
            DialogCommands.CloseDialogCommand.Execute(null, InputParameter as IInputElement);
        }


        public ICommand SaveCommand { get; }
        public ICommand DontSaveCommand { get; }
        public ICommand CancelCommand { get; }
    }
}
