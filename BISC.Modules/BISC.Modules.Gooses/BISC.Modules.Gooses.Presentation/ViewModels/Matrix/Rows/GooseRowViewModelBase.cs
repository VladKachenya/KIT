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
        public string DoiDataRef { get; set; }
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
