using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BISC.Infrastructure.Global.Common;
using BISC.Presentation.BaseItems.Commands;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;

namespace BISC.Presentation.ViewModels
{
    public class UserInteractionOptionsViewModel : NavigationViewModelBase
    {
        private readonly ICommandFactory _commandFactory;
        private OperationResult<int> _result;
        private ObservableCollection<string> _options;
        private ObservableCollection<string> _warnings;
        private string _message;
        private string _title;

        public UserInteractionOptionsViewModel(ICommandFactory commandFactory)
            : base(null)
        {
            _commandFactory = commandFactory;
            OptionCollection = new ObservableCollection<OptionSelectionCommand>();
        }

        #region Overrides of NavigationViewModelBase

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _result = navigationContext.BiscNavigationParameters.GetParameterByName<OperationResult<int>>("result");
            _options =
                new ObservableCollection<string>(
                    navigationContext.BiscNavigationParameters.GetParameterByName<List<string>>("options"));
            Message = navigationContext.BiscNavigationParameters.GetParameterByName<string>("message");
            Title = navigationContext.BiscNavigationParameters.GetParameterByName<string>("title");
            if (navigationContext.BiscNavigationParameters?.GetParameterByName<List<string>>("warnings") != null)
            {
                Warnings =
                    new ObservableCollection<string>(
                        navigationContext.BiscNavigationParameters?.GetParameterByName<List<string>>("warnings"));
            }
            else
            {
                Warnings = new ObservableCollection<string>();
            }

            OptionCollection.Clear();
            foreach (var option in _options)
            {
                OptionCollection.Add(new OptionSelectionCommand(
                    _commandFactory.CreatePresentationCommand((() => { OnOptionSelected(option); })),
                    option));
            }
            base.OnNavigatedTo(navigationContext);
        }

        #endregion

        private void OnOptionSelected(string option)
        {
            _result.Item = _options.IndexOf(option);
            CloseView();
        }

        private void CloseView()
        {
            DialogCommands.CloseDialogCommand.Execute(null, null);
        }
        public ObservableCollection<OptionSelectionCommand> OptionCollection { get; }

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public ObservableCollection<string> Warnings
        {
            get => _warnings;
            protected set => SetProperty(ref _warnings, value);
        }
    }

    public class OptionSelectionCommand
    {
        public OptionSelectionCommand(ICommand selectionCommand, string optionSignature)
        {
            OptionSignature = optionSignature;
            SelectionCommand = selectionCommand;
        }
        public string OptionSignature { get; }
        public ICommand SelectionCommand { get; }
    }
}
