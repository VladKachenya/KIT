using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Presentation.Interfaces;
using BISC.Modules.InformationModel.Presentation.Interfaces.InfoModelDetails;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.BaseItems.ViewModels.Behaviors;

namespace BISC.Modules.InformationModel.Presentation.ViewModels.Base
{
    public abstract class TreeItemViewModelBase : ComplexViewModelBase, IInfoModelItemWithDetails
    {
        protected IModelElement _model;
        private string _header;
        private int _level;
        private bool _isChecked;
        private string _description;
        private bool _isCheckable;
        private IInfoModelItemViewModel _parent;
        private bool _isSelected;


        protected TreeItemViewModelBase()
        {
            ChildInfoModelItemViewModels = new ObservableCollection<IInfoModelItemViewModel>();
        }
        
        #region Implementation of IViewModel

        public IModelElement Model
        {
            get => GetModel();
            set => SetModel(value);
        }

        protected virtual void SetModel(object value)
        {
            _model = value as IModelElement;
        }
        protected virtual IModelElement GetModel()
        {
            return _model;
        }

        #endregion

        #region Implementation of IConfigurationItemViewModel

        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                OnPropertyChanged();
            }
        }

        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
                OnPropertyChanged();
            }
        }

        public Action<bool?> Checked { get; set; }

        public abstract string TypeName { get; }

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public bool IsCheckable
        {
            get { return _isCheckable; }
            set
            {
                if (_isCheckable == value) return;
                _isCheckable = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<IInfoModelItemViewModel> ChildInfoModelItemViewModels { get; set; }

        public IInfoModelItemViewModel Parent
        {
            get { return _parent; }
            set
            {
                _parent = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Implementation of ITreeItemWithDetails

        public abstract List<IInfoModelDetail> TreeItemDetails { get; }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public virtual Brush TypeColorBrush =>Brushes.Transparent;

        public virtual bool IsChildItemsShowing => true;

        #region Overrides of DisposableBindableBase

        protected override void OnDisposing()
        {

            foreach (var childStructItemViewModel in ChildInfoModelItemViewModels)
            {
                (childStructItemViewModel as IDisposable)?.Dispose();
            }
            base.OnDisposing();
        }

        #endregion

        #endregion
    }
}
