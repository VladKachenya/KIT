using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.ViewModels;
using BISC.Modules.DataSets.Infrastructure.ViewModels.Factorys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Presentation.BaseItems.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Modules.DataSets.Infrastructure.ViewModels.Services;
using BISC.Modules.InformationModel.Presentation.ViewModels.Base;
using GongSolutions.Wpf.DragDrop;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;
using BISC.Modules.DataSets.Infrastructure.Factorys;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.DataSets.Presentation.ViewModels
{
    public class DataSetViewModel : ViewModelBase, IDataSetViewModel, GongSolutions.Wpf.DragDrop.IDropTarget
    {
        #region private filds
        private IDataSet _model;
        private IFcdaViewModelFactory _fcdaViewModelFactory;
        private bool _isExpanded = false;
        private IFcdaAdderViewModelService _fcdaAdderViewModelService;
        private readonly IDataTypeTemplatesModelService _dataTypeTemplatesModelService;
        private readonly IBiscProject _biscProject;
        private IFcdaFactory _fcdaFactory;
        private readonly ISaveCheckingService _saveCheckingService;
        private ObservableCollection<IFcdaViewModel> _fcdaViewModels;

        #endregion

        #region C-tor
        public DataSetViewModel(IFcdaViewModelFactory fcdaViewModelFactory,ICommandFactory commandFactory, 
            IFcdaAdderViewModelService fcdaAdderViewModelService, IDataTypeTemplatesModelService dataTypeTemplatesModelService,
            IBiscProject biscProject, IFcdaFactory fcdaFactory,ISaveCheckingService saveCheckingService)
        {
            _fcdaFactory = fcdaFactory;
            _saveCheckingService = saveCheckingService;
            _biscProject = biscProject;
            _dataTypeTemplatesModelService = dataTypeTemplatesModelService;
            _fcdaViewModelFactory = fcdaViewModelFactory;
            DeleteFcdaCommand = commandFactory.CreatePresentationCommand<object>(OnDeleteFcda);
            AddFcdaToDataset = commandFactory.CreatePresentationCommand(OnAddFcdaTpDataset, () => IsEditeble);
            _fcdaAdderViewModelService = fcdaAdderViewModelService;
        }
        #endregion

        #region private methods
        private void OnDeleteFcda(object obj)
        {
            FcdaViewModels.Remove(obj as IFcdaViewModel);
        }

        private void OnAddFcdaTpDataset()
        {
            _fcdaAdderViewModelService.OpenFcdaAdderView();
        }
        #endregion

        #region Implamentation of DataSetElementBaseViewModel
        public string Name => _model.Name;

        public string ElementName => _model.ElementName;
        public string FullName => _model.ParentModelElement.ElementName + "." + _model.ElementName + "." + _model.Name;

        public Brush TypeColorBrush => new SolidColorBrush(Color.FromRgb(126, 141, 240));

        public IDataSet GetModel()
        {
            _model.FcdaList.Clear();
            foreach (var fcdaViewModel in FcdaViewModels)
            {
                _model.FcdaList.Add(fcdaViewModel.GetModel());
            }
            return _model;
        }

        public void SetModel(IDataSet model)
        {
            _model = model ?? throw new NullReferenceException();
            FcdaViewModels = _fcdaViewModelFactory.GetFcdaViewModelCollection(_model);
        }

        public bool IsExpanded
        {
            get => _isExpanded;
            set => SetProperty(ref _isExpanded, value,true);
        }

        public bool IsEditeble => _model.IsDynamic;
        #endregion

        #region Implamentation of IDataSetViewModel

        public ObservableCollection<IFcdaViewModel> FcdaViewModels
        {
            get => _fcdaViewModels;
            protected set { SetProperty(ref _fcdaViewModels , value); }
        }

        public ICommand DeleteFcdaCommand { get; }
        public ICommand AddFcdaToDataset { get; }


        #endregion

        #region override of NavigationViewModelBase


        


        public void DragOver(IDropInfo dropInfo)
        {
            TreeItemViewModelBase sourceItem = dropInfo.Data as TreeItemViewModelBase;
            TreeItemViewModelBase targetItem = dropInfo.TargetItem as TreeItemViewModelBase;

            if (sourceItem != null && sourceItem.TypeName == InfoModelKeys.ModelKeys.DaiKey)
            {
                IDa Da = _dataTypeTemplatesModelService.GetDaOfDai(sourceItem.Model as IDai, _biscProject.MainSclModel.Value);
                if(Da.Fc == "ST" || Da.Fc == "MX")
                {
                    dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                    dropInfo.Effects = System.Windows.DragDropEffects.Move;
                }
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            TreeItemViewModelBase sourceItem = dropInfo.Data as TreeItemViewModelBase;
            //TreeItemViewModelBase targetItem = dropInfo.TargetItem as TreeItemViewModelBase;
            var elementModel = sourceItem.Model;
            IFcda fcdaModel = null;
            if (elementModel is IDai)
                fcdaModel = _fcdaFactory.GetFcda((elementModel as IDai));

            if (fcdaModel != null)
                FcdaViewModels.Add(_fcdaViewModelFactory.GetFcdaViewModelElement(fcdaModel));
        }

        private bool CheckFc(object model)
        {
            return false;
        }

        #endregion
    }
}
