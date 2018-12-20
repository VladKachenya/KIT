using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.DataSets.Infrastructure.Factorys;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.DataSets.Infrastructure.ViewModels;
using BISC.Modules.DataSets.Infrastructure.ViewModels.Factorys;
using BISC.Modules.DataSets.Infrastructure.ViewModels.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Modules.InformationModel.Presentation.ViewModels.Base;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Services;
using GongSolutions.Wpf.DragDrop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;

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
        private readonly IInfoModelService _infoModelService;
        private readonly ILoggingService _loggingService;
        private readonly IFcdaInfoService _fcdaInfoService;
        private ObservableCollection<IFcdaViewModel> _fcdaViewModels;
        private string _name;
        private string _editableNamePart;
        private string _selectedParentLd;
        private string _selectedParentLn;
        private List<string> _parentLdList;
        private List<string> _parentLnList;
        private bool _isEditing;
        private bool _isEditeble;
        private IModelElement _device;
        private bool _isChanged;

        #endregion

        #region C-tor
        public DataSetViewModel(IFcdaViewModelFactory fcdaViewModelFactory, ICommandFactory commandFactory,
            IFcdaAdderViewModelService fcdaAdderViewModelService, IDataTypeTemplatesModelService dataTypeTemplatesModelService,
            IBiscProject biscProject, IFcdaFactory fcdaFactory, ISaveCheckingService saveCheckingService, IInfoModelService infoModelService,
            ILoggingService loggingService, IFcdaInfoService fcdaInfoService)
        {
            _fcdaFactory = fcdaFactory;
            _saveCheckingService = saveCheckingService;
            _infoModelService = infoModelService;
            _loggingService = loggingService;
            _fcdaInfoService = fcdaInfoService;
            _biscProject = biscProject;
            _dataTypeTemplatesModelService = dataTypeTemplatesModelService;
            _fcdaViewModelFactory = fcdaViewModelFactory;
            DeleteFcdaCommand = commandFactory.CreatePresentationCommand<object>(OnDeleteFcda);
            AddFcdaToDataset = commandFactory.CreatePresentationCommand(OnAddFcdaTpDataset, () => IsEditeble);
            _fcdaAdderViewModelService = fcdaAdderViewModelService;
            FcdaViewModels = new ObservableCollection<IFcdaViewModel>();
        }
        #endregion

        #region private methods
        private void OnDeleteFcda(object obj)
        {
            FcdaViewModels.Remove(obj as IFcdaViewModel);
            if (obj != null)
            {
                _loggingService.LogUserAction(
                    $"Пользователь удалил FCDA {(obj as IFcdaViewModel).FullName} (устройство {(_device as IDevice)?.Name})");
                OnPropertyChanged(nameof(Weight));
            }
        }

        private void OnAddFcdaTpDataset()
        {
            _fcdaAdderViewModelService.OpenFcdaAdderView();
        }
        #endregion

        #region Implamentation of DataSetElementBaseViewModel

        public int MaxSizeFcdaList
        {
            get
            {
                if (_isEditeble)
                {
                    return 100;
                }
                else
                {
                    return FcdaViewModels.Count;
                }
            }
        }
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public int Weight
        {
            get
            {
                int weight = 0;
                foreach (var fcdaViewModel in FcdaViewModels)
                {
                    weight = weight + fcdaViewModel.Weight;
                }

                return weight;
            }
        }

        public string ElementName => "DataSet";

        public Brush TypeColorBrush => new SolidColorBrush(Color.FromRgb(126, 141, 240));


        public void SetModel(IDataSet model)
        {
            _model = model ?? throw new NullReferenceException();
            _device = model.GetFirstParentOfType<IDevice>();
            FcdaViewModels = _fcdaViewModelFactory.CreateFcdaViewModelCollection(_device as IDevice, _model);
            SelectedParentLn = (_model.ParentModelElement as ILogicalNode).Name;

            ParentLdList = _infoModelService.GetLDevicesFromDevices(_device)
                .Select((device => device.Inst)).ToList();

            SelectedParentLd =
                ParentLdList.FirstOrDefault((s => s == ((_model.ParentModelElement as ILogicalNode).ParentModelElement as ILDevice).Inst));
            IsEditeble = model.IsDynamic;

            EditableNamePart = _model.Name;
            Name = model.Name;
        }


        private void FillParentLnList()
        {
            var ldevice = _infoModelService.GetLDevicesFromDevices(_device)
                .FirstOrDefault((device => device.Inst == SelectedParentLd));
            if (ldevice != null)
            {
                var parentLnList = ldevice.LogicalNodes.Select((node => node.Name))
                    .ToList();
                parentLnList.Add(ldevice.LogicalNodeZero.Value.Name);
                ParentLnList = parentLnList;
                if (!ParentLnList.Contains(SelectedParentLn))
                {
                    SelectedParentLn = ParentLnList.FirstOrDefault((s => s == "LLN0"));
                }
                else
                {
                    SelectedParentLn = ParentLnList.First((s => s == SelectedParentLn));
                }
            }


        }

        public bool IsExpanded
        {
            get => _isExpanded;
            set => SetProperty(ref _isExpanded, value, true);
        }

        public bool IsEditing
        {
            get => _isEditing;
            set { SetProperty(ref _isEditing, value, true); }
        }

        public bool IsEditeble
        {
            get => _isEditeble;
            set { SetProperty(ref _isEditeble, value, true); }
        }
        public bool IsChanged
        {
            get => _isChanged;
            set { SetProperty(ref _isChanged, value, true); }
        }

        #endregion

        #region Implamentation of IDataSetViewModel

        public ObservableCollection<IFcdaViewModel> FcdaViewModels
        {
            get => _fcdaViewModels;
            protected set
            {
                SetProperty(ref _fcdaViewModels, value);
            }
        }

        public ICommand DeleteFcdaCommand { get; }
        public ICommand AddFcdaToDataset { get; }

        public string EditableNamePart
        {
            get => _editableNamePart;
            set
            {
                if (!StaticStringValidationService.NameValidation(value))
                {
                    return;
                }

                SetProperty(ref _editableNamePart, value);
            }
        }

        public string SelectedParentLd
        {
            get => _selectedParentLd;
            set
            {
                SetProperty(ref _selectedParentLd, value);
                FillParentLnList();
            }
        }

        public string SelectedParentLn
        {
            get => _selectedParentLn;
            set { SetProperty(ref _selectedParentLn, value); }
        }

        public List<string> ParentLdList
        {
            get => _parentLdList;
            set { SetProperty(ref _parentLdList, value, true); }
        }

        public List<string> ParentLnList
        {
            get => _parentLnList;
            set { SetProperty(ref _parentLnList, value, true); }
        }

        public void SetParentDevice(IModelElement device)
        {
            _device = device;
        }
        #endregion

        #region override of NavigationViewModelBase

        public void DragOver(IDropInfo dropInfo)
        {
            TreeItemViewModelBase sourceItem = dropInfo.Data as TreeItemViewModelBase;
            TreeItemViewModelBase targetItem = dropInfo.TargetItem as TreeItemViewModelBase;

            if (sourceItem != null && sourceItem.TypeName == InfoModelKeys.ModelKeys.DaiKey)
            {
                if ((sourceItem.Model as IDai).GetFirstParentOfType<IDevice>() != _device)
                {
                    return;
                }

                if (!IsEditing)
                {
                    return;
                }


                IDa Da = _dataTypeTemplatesModelService.GetDaOfDai(sourceItem.Model as IDai,
                    _biscProject.MainSclModel.Value);
                if (Da.Fc == "ST" || Da.Fc == "MX")
                {
                    if (MaxSizeFcdaList >= (Weight + _fcdaInfoService.GetModelElementWeight(_device as IDevice, sourceItem.Model, Da.Fc)))
                    {
                        dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                        dropInfo.Effects = System.Windows.DragDropEffects.Move;
                    }
                }
            }
            else if (sourceItem != null && sourceItem.TypeName == InfoModelKeys.ModelKeys.FcSetKey)
            {
                if ((sourceItem.Model as IDoi).GetFirstParentOfType<IDevice>() != _device)
                {
                    return;
                }

                if (!IsEditing)
                {
                    return;
                }

                if (sourceItem.Header == "ST" || sourceItem.Header == "MX")
                {
                    if (MaxSizeFcdaList >= (Weight + _fcdaInfoService.GetModelElementWeight(_device as IDevice, sourceItem.Model, sourceItem.Header)))
                    {
                        dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                        dropInfo.Effects = System.Windows.DragDropEffects.Move;
                    }
                }
            }
            else if (sourceItem != null && sourceItem.TypeName == InfoModelKeys.ModelKeys.DoiKey)
            {
                if ((sourceItem.Model as IDoi).GetFirstParentOfType<IDevice>() != _device)
                {
                    return;
                }

                if (!IsEditing)
                {
                    return;
                }

                if (sourceItem.ChildInfoModelItemViewModels.Count != 1 &&
                    sourceItem.ChildInfoModelItemViewModels[0].TypeName == InfoModelKeys.ModelKeys.FcSetKey)
                {
                    return;
                }

                if (sourceItem.ChildInfoModelItemViewModels[0].TypeName == InfoModelKeys.ModelKeys.DaiKey)
                {
                    List<string> fcList = new List<string>();
                    foreach (var viewModelElement in sourceItem.ChildInfoModelItemViewModels)
                    {
                        IDai daiElement = viewModelElement.Model as IDai;
                        IDa DaElement = _dataTypeTemplatesModelService.GetDaOfDai(daiElement, _biscProject.MainSclModel.Value);
                        if (!fcList.Contains(DaElement.Fc))
                        {
                            fcList.Add(DaElement.Fc);
                        }
                    }
                    if (fcList.Count > 1)
                    {
                        return;
                    }
                }
                string header = "";

                if (sourceItem.ChildInfoModelItemViewModels[0].TypeName == InfoModelKeys.ModelKeys.DaiKey)
                {
                    IDai daiElement = sourceItem.ChildInfoModelItemViewModels[0].Model as IDai;
                    IDa daElement = _dataTypeTemplatesModelService.GetDaOfDai(daiElement, _biscProject.MainSclModel.Value);
                    header = daElement.Fc;
                }
                else
                {
                    header = sourceItem.ChildInfoModelItemViewModels[0].Header;
                }

                if (header == "ST" || header == "MX")
                {
                    if (MaxSizeFcdaList >= (Weight + _fcdaInfoService.GetModelElementWeight(_device as IDevice, sourceItem.Model, header)))
                    {
                        dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                        dropInfo.Effects = System.Windows.DragDropEffects.Move;
                    }
                }
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            if (!IsEditing)
            {
                return;
            }

            if (Weight >= MaxSizeFcdaList)
            {
                return;
            }
            IFcda fcdaModel = null;
            TreeItemViewModelBase sourceItem = dropInfo.Data as TreeItemViewModelBase;
            var model = sourceItem.Model;
            //TreeItemViewModelBase targetItem = dropInfo.TargetItem as TreeItemViewModelBase;
            if (sourceItem.TypeName == InfoModelKeys.ModelKeys.DaiKey)
            {
                if (model is IDai)
                {
                    fcdaModel = _fcdaFactory.GetFcda((model as IDai));
                }

                if (fcdaModel != null)
                {
                    AddFcda(fcdaModel, dropInfo.InsertIndex);
                }
            }
            else if (sourceItem.TypeName == InfoModelKeys.ModelKeys.FcSetKey)
            {

                IDoi doiParent = model as IDoi;

                if (doiParent != null)
                {
                    fcdaModel = _fcdaFactory.GetStructFcda(doiParent, sourceItem.Header);
                }

                if (fcdaModel != null)
                {
                    AddFcda(fcdaModel, dropInfo.InsertIndex);
                }
            }
            else if (sourceItem.TypeName == InfoModelKeys.ModelKeys.DoiKey)
            {
                string header = "";

                IDoi doiParent = model as IDoi;

                if (sourceItem.ChildInfoModelItemViewModels[0].TypeName == InfoModelKeys.ModelKeys.DaiKey)
                {
                    IDai daiElement = sourceItem.ChildInfoModelItemViewModels[0].Model as IDai;
                    IDa daElement = _dataTypeTemplatesModelService.GetDaOfDai(daiElement, _biscProject.MainSclModel.Value);
                    header = daElement.Fc;
                }
                else
                {
                    header = sourceItem.ChildInfoModelItemViewModels[0].Header;
                }

                if (doiParent != null)
                {
                    fcdaModel = _fcdaFactory.GetStructFcda(doiParent, header);
                }

                if (fcdaModel != null)
                {
                    AddFcda(fcdaModel, dropInfo.InsertIndex);
                }

            }
            // var element = _fcdaInfoService.GetFcdaWeight(_device as IDevice, fcdaModel);
            //throw new NullReferenceException("Element not found!!");
        }

        private void AddFcda(IFcda fcda, int insertIdex)
        {
            var existing =
                FcdaViewModels.FirstOrDefault((model => model.GetFcda().ModelElementCompareTo(fcda)));
            if (existing != null)
            {
                _loggingService.LogMessage($"FCDA {existing.FullName} уже есть с списке", SeverityEnum.Warning);
                return;
            }

            var newFcdaViewModel = _fcdaViewModelFactory.CreateFcdaViewModelElement(_device as IDevice, fcda);
            FcdaViewModels.Insert(insertIdex, newFcdaViewModel);
            _loggingService.LogUserAction($"Добавлен FCDA {newFcdaViewModel.FullName} через DragDrop");
            OnPropertyChanged(nameof(Weight));
        }
        private bool CheckFc(object model)
        {
            return false;
        }

        #endregion
    }
}
