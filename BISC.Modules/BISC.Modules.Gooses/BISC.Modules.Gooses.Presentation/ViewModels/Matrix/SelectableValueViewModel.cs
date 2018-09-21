using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Gooses.Presentation.Events;
using BISC.Presentation.BaseItems.ViewModels;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Matrix
{
    //public class SelectableValueViewModel : NavigationViewModelBase
    //{
    //    private readonly IGlobalEventsService _globalEventsService;
    //    private readonly ISclConfiguratorSettingsService _sclConfiguratorSettingsService;
    //    private bool _selectedValue;
    //    private bool _isSelectingEnabled;
    //    private string _toolTip;

    //    public SelectableValueViewModel(IGlobalEventsService globalEventsService,
    //        ISclConfiguratorSettingsService sclConfiguratorSettingsService)
    //    {
    //        _globalEventsService = globalEventsService;
    //        _sclConfiguratorSettingsService = sclConfiguratorSettingsService;
    //        OnMouseEnterCommand = new DelegateCommand(OnOnMouseEnterExecute);
    //        _eventAggregator.GetEvent<SelectableBoxSelectedEvent>().Subscribe((OnSelectableBoxSelected));
    //    }

    //    private void OnSelectableBoxSelected(SelectableBoxEventArgs selectableBoxEventArgs)
    //    {
    //        if (_sclConfiguratorSettingsService.IsAutoEnabledValidityInGooseReceiving)
    //        {
    //            if ((this.Parent is ValidityGooseRowViewModel) &&
    //                (selectableBoxEventArgs.SelectableValueViewModel.Parent is StateGooseRowViewModel) &&
    //                this.ColumnNumber == selectableBoxEventArgs.SelectableValueViewModel.ColumnNumber &&
    //                this.Parent.Parent == selectableBoxEventArgs.SelectableValueViewModel.Parent.Parent)
    //            {

    //                SelectedValue = selectableBoxEventArgs.SelectableValueViewModel.SelectedValue;
    //            }
    //        }
    //        if (_sclConfiguratorSettingsService.IsAutoEnabledQualityInGooseReceiving)
    //        {
    //            if ((this.Parent is QualityGooseRowViewModel) &&
    //                (selectableBoxEventArgs.SelectableValueViewModel.Parent is StateGooseRowViewModel) &&
    //                this.ColumnNumber == selectableBoxEventArgs.SelectableValueViewModel.ColumnNumber)
    //            {
    //                if (this.Parent.RowName.Remove(this.Parent.RowName.Length - 6) ==
    //                    selectableBoxEventArgs.SelectableValueViewModel.Parent.RowName.Remove(
    //                        selectableBoxEventArgs.SelectableValueViewModel.Parent.RowName.Length - 10)
    //                ) //потому что stval - 5 букв, а q - 1 буква
    //                    SelectedValue = selectableBoxEventArgs.SelectableValueViewModel.SelectedValue;
    //            }
    //        }
    //    }


    //    private void OnOnMouseEnterExecute()
    //    {
    //        _eventAggregator.GetEvent<SelectableBoxFocusedEvent>().Publish((new SelectableBoxEventArgs(this)));

    //    }

    //    #region Implementation of ISelectableValueViewModel

    //    public int ColumnNumber { get; set; }
    //    public IGooseRowViewModel Parent { get; set; }

    //    public bool SelectedValue
    //    {
    //        get { return _selectedValue; }
    //        set
    //        {
    //            _selectedValue = value;
    //            RaisePropertyChanged();
    //            _eventAggregator.GetEvent<SelectableBoxSelectedEvent>().Publish(new SelectableBoxEventArgs(this));
    //        }
    //    }



    //    public ICommand OnMouseEnterCommand { get; }

    //    public bool IsSelectingEnabled
    //    {
    //        get { return _isSelectingEnabled; }
    //        set
    //        {
    //            _isSelectingEnabled = value;
    //            RaisePropertyChanged();
    //        }
    //    }

    //    public string ToolTip
    //    {
    //        get { return _toolTip; }
    //        set
    //        {
    //            _toolTip = value;
    //            RaisePropertyChanged();
    //        }
    //    }

    //    #endregion


    //    #region Overrides of DisposableBindableBase

    //    protected override void OnDisposing()
    //    {

    //        base.OnDisposing();
    //    }

    //    #endregion
    //}
}