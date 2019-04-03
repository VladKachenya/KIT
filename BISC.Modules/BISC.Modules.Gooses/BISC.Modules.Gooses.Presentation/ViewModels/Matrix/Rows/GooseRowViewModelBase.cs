using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Presentation.Interfaces;
using BISC.Presentation.BaseItems.ViewModels;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Matrix.Rows
{
    public class GooseRowViewModel : ViewModelBase, IGooseRowViewModel
    {
   
        private IGooseRow _model;
        private List<ISelectableValueViewModel> _selectableValueViewModels;

        public GooseRowViewModel()
        {
            SelectableValueViewModels = new List<ISelectableValueViewModel>();
        }
        #region Implementation of IGooseRowViewModel

        //public IGooseRow Model
        //{
        //    get
        //    {
        //        _model.ValueList.Clear();
        //        foreach (var selectableValueViewModel in SelectableValueViewModels)
        //        {
        //            _model.ValueList.Add(selectableValueViewModel.SelectedValue);
        //        }
        //        return _model;
        //    }
        //    set
        //    {
        //        _model = value;
        //        RowName = _model.Signature;
        //        SelectableValueViewModels.Clear();

        //        for (int i = 0; i < _model.ValueList.Count; i++)
        //        {
        //            ISelectableValueViewModel selectableValueViewModel = StaticContainer.CurrentContainer.ResolveType<ISelectableValueViewModel>();
        //            selectableValueViewModel.SelectedValue = _model.ValueList[i];
        //            selectableValueViewModel.ColumnNumber = i;
        //            selectableValueViewModel.Parent = this;
        //            selectableValueViewModel.ToolTip = RowName + "     " + (i + 1);
        //            SelectableValueViewModels.Add(selectableValueViewModel);

        //        }
        //    }
        //}

        public List<ISelectableValueViewModel> SelectableValueViewModels
        {
            get => _selectableValueViewModels;
            protected set => SetProperty(ref _selectableValueViewModels, value);
        }

        public string RowName { get; set; }
        public GooseControlBlockViewModel Parent { get; set; }
        public string GooseRowType { get; set; }
        public int NumberOfFcdaInDataSet { get; set; }
        public IDataSet RelatedDataSet { get; set; }

        #endregion


        #region Overrides of DisposableBindableBase

        protected override void OnDisposing()
        {
            foreach (var model in SelectableValueViewModels)
            {
                model.Dispose();
            }
            base.OnDisposing();
        }

        #endregion
    }
}
