using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Gooses.Presentation.Events;
using BISC.Modules.Gooses.Presentation.Interfaces;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Matrix.Entities
{
    public class SelectableValueViewModel : ViewModelBase, ISelectableValueViewModel
    {
        private readonly IGlobalEventsService _globalEventsService;
        private readonly IConfigurationService _configurationService;
        private bool _selectedValue;
        private bool _isSelectingEnabled;
        private string _toolTip;

        public SelectableValueViewModel(IGlobalEventsService globalEventsService,
            IConfigurationService configurationService,ICommandFactory commandFactory)
        {
            _globalEventsService = globalEventsService;
            _configurationService = configurationService;

            OnMouseEnterCommand = commandFactory.CreatePresentationCommand(OnOnMouseEnterExecute);
            OnMouseDownCommand = commandFactory.CreatePresentationCommand(OnMouseDownExecute);

            _globalEventsService.Subscribe<SelectableBoxEventArgs>(OnSelectableBoxSelected);
        }

        private void OnMouseDownExecute()
        {
            if (IsSelectingEnabled)
            {
                SelectedValue = !SelectedValue;
            }
        }

        private void OnSelectableBoxSelected(SelectableBoxEventArgs selectableBoxEventArgs)
        {
            if(selectableBoxEventArgs.IsFocused)return;
            if (_configurationService.IsAutoEnabledValidityInGooseReceiving)
            {
                if ((this.Parent.Model.GooseRowType=="State") &&
                    (selectableBoxEventArgs.SelectableValueViewModel.Parent.Model.GooseRowType== "State") &&
                    this.ColumnNumber == selectableBoxEventArgs.SelectableValueViewModel.ColumnNumber &&
                    this.Parent.Parent == selectableBoxEventArgs.SelectableValueViewModel.Parent.Parent)
                {

                    SelectedValue = selectableBoxEventArgs.SelectableValueViewModel.SelectedValue;
                }
            }
            if (_configurationService.IsAutoEnabledQualityInGooseReceiving)
            {
                if ((this.Parent.Model.GooseRowType == "Quality") &&
                    (selectableBoxEventArgs.SelectableValueViewModel.Parent.Model.GooseRowType == "Quality") &&
                    this.ColumnNumber == selectableBoxEventArgs.SelectableValueViewModel.ColumnNumber)
                {
                    if (this.Parent.RowName.Remove(this.Parent.RowName.Length - 6) ==
                        selectableBoxEventArgs.SelectableValueViewModel.Parent.RowName.Remove(
                            selectableBoxEventArgs.SelectableValueViewModel.Parent.RowName.Length - 10)
                    ) //потому что stval - 5 букв, а q - 1 буква
                        SelectedValue = selectableBoxEventArgs.SelectableValueViewModel.SelectedValue;
                }
            }
        }


        private void OnOnMouseEnterExecute()
        {
            _globalEventsService.SendMessage((new SelectableBoxEventArgs(this,true)));

        }

        #region Implementation of ISelectableValueViewModel

        public int ColumnNumber { get; set; }
        public IGooseRowViewModel Parent { get; set; }

        public bool SelectedValue
        {
            get { return _selectedValue; }
            set
            {
                 SetProperty(ref _selectedValue , value);
                _globalEventsService.SendMessage(new SelectableBoxEventArgs(this,false));
            }
        }


        public ICommand OnMouseDownCommand { get; }

        public ICommand OnMouseEnterCommand { get; }

        public bool IsSelectingEnabled
        {
            get { return _isSelectingEnabled; }
            set
            {
               SetProperty(ref _isSelectingEnabled , value);
                
            }
        }

        public string ToolTip
        {
            get { return _toolTip; }
            set
            {
                SetProperty(ref _toolTip, value);
            }
        }

        #endregion


        #region Overrides of DisposableBindableBase

        protected override void OnDisposing()
        {
            _globalEventsService.Unsubscribe<SelectableBoxEventArgs>(OnSelectableBoxSelected);
            base.OnDisposing();
        }

        #endregion
    }
}