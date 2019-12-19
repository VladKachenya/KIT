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
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Modules.InformationModel.Presentation.ViewModels.InfoModelTree;
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
using BISC.Modules.DataSets.Presentation.Interfaces.ViewModels;
using TreeItemViewModelBase = BISC.Modules.InformationModel.Presentation.ViewModels.Base.TreeItemViewModelBase;

namespace BISC.Modules.DataSets.Presentation.ViewModels
{
    public class DataSetViewModel : ViewModelBase, IDataSetViewModel, GongSolutions.Wpf.DragDrop.IDropTarget
    {
        #region private filds

        private IDataSet _model;
        private IFcdaViewModelFactory _fcdaViewModelFactory;
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
        private bool _isEditable;
        private IModelElement _device;
        private bool _isChanged;
        private bool _isInitialized = true;
        private int _weight = 0;
        private bool _isSelect = false;

        #endregion

        #region C-tor

        public DataSetViewModel(
            IFcdaViewModelFactory fcdaViewModelFactory, 
            ICommandFactory commandFactory,
            IFcdaAdderViewModelService fcdaAdderViewModelService,
            IDataTypeTemplatesModelService dataTypeTemplatesModelService,
            IBiscProject biscProject, 
            IFcdaFactory fcdaFactory, 
            ISaveCheckingService saveCheckingService,
            IInfoModelService infoModelService,
            ILoggingService loggingService, 
            IFcdaInfoService fcdaInfoService)
        {
            _fcdaFactory = fcdaFactory;
            _saveCheckingService = saveCheckingService;
            _infoModelService = infoModelService;
            _loggingService = loggingService;
            _fcdaInfoService = fcdaInfoService;
            _biscProject = biscProject;
            _dataTypeTemplatesModelService = dataTypeTemplatesModelService;
            _fcdaViewModelFactory = fcdaViewModelFactory;
            _fcdaAdderViewModelService = fcdaAdderViewModelService;

            DeleteFcdaCommand = commandFactory.CreatePresentationCommand<object>(OnDeleteFcda);
            AddFcdaToDataset = commandFactory.CreatePresentationCommand(OnAddFcdaTpDataset, () => IsEditable);
            FcdaViewModels = new ObservableCollection<IFcdaViewModel>();
        }

        #endregion

        #region private methods

        private void OnDeleteFcda(object obj)
        {
            var fcdaObj = obj as IFcdaViewModel;
            if (fcdaObj == null) return;
            FcdaViewModels.Remove(obj as IFcdaViewModel);
            _loggingService.LogUserAction(
                $"Удаление FCDA {(obj as IFcdaViewModel).FullName} из датасета {Name}, устройствa {(_device as IDevice)?.Name}");
            Weigh();
        }

        private void OnAddFcdaTpDataset()
        {
            _fcdaAdderViewModelService.OpenFcdaAdderView();
        }

        #endregion

        #region Implamentation of DataSetElementBaseViewModel

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }


        public string ElementName => "DataSet";

        public Brush TypeColorBrush => new SolidColorBrush(Color.FromRgb(126, 141, 240));


        public void SetModel(IDataSet model)
        {
            _model = model ?? throw new NullReferenceException();
            _device = model.GetFirstParentOfType<IDevice>();
            FcdaViewModels = _fcdaViewModelFactory.CreateFcdaViewModelCollection(_device as IDevice, _model, this);
            SelectedParentLn = (_model.ParentModelElement as ILogicalNode).Name;

            ParentLdList = _infoModelService.GetLDevicesFromDevices(_device)
                .Select((device => device.Inst)).ToList();

            SelectedParentLd =
                ParentLdList.FirstOrDefault((s =>
                    s == ((_model.ParentModelElement as ILogicalNode).ParentModelElement as ILDevice).Inst));
            IsEditable = model.IsDynamic;

            EditableNamePart = _model.Name;
            Name = model.Name;
            Weigh();
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

        public bool IsInitialized
        {
            get => _isInitialized;
            set => SetProperty(ref _isInitialized, value, true);
        }

        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                if (_isEditing && !IsInitialized)
                {
                    IsInitialized = true;
                }

                SetProperty(ref _isEditing, value, true);
            }
        }

        public bool IsEditable
        {
            get => _isEditable;
            set => SetProperty(ref _isEditable, value, true);
        }

        public bool IsChanged
        {
            get => _isChanged;
            set => SetProperty(ref _isChanged, value, true);
        }

        #endregion

        #region Implamentation of IDataSetViewModel

        public ObservableCollection<IFcdaViewModel> FcdaViewModels
        {
            get => _fcdaViewModels;
            protected set => SetProperty(ref _fcdaViewModels, value);
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
            set => SetProperty(ref _selectedParentLn, value);
        }

        public List<string> ParentLdList
        {
            get => _parentLdList;
            set => SetProperty(ref _parentLdList, value, true);
        }

        public List<string> ParentLnList
        {
            get => _parentLnList;
            set => SetProperty(ref _parentLnList, value, true);
        }

        public void SetParentDevice(IModelElement device)
        {
            _device = device;
        }

        #endregion

        #region Implementation of IWeigher

        public int Weight
        {
            get => _weight;
            protected set => SetProperty(ref _weight, value, true);
        }

        public bool CanSetWeight(int addedWeight, int removeWeight = 0)
        {
            return (Weight + addedWeight - removeWeight) <= MaxSizeFcdaList;
        }

        public int MaxSizeFcdaList
        {
            get => 100;
        }

        public bool IsSelect
        {
            get => _isSelect;
            set => SetProperty(ref _isSelect, value, true);
        }


        public void Weigh()
        {

            Weight = 0;
            foreach (var fcdaViewModel in FcdaViewModels)
            {
                Weight = Weight + fcdaViewModel.SellectedFc.FcWeight;
            }

            foreach (var fcdaElement in FcdaViewModels)
            {
                foreach (var fcElement in fcdaElement.FcCollection)
                {
                    fcElement.IsSellecteble =
                        CanSetWeight(fcElement.FcWeight, fcdaElement.SellectedFc.FcWeight);
                }
            }
        }

        #endregion

        #region override of NavigationViewModelBase

        public void DragOver(IDropInfo dropInfo)
        {
            if (!IsEditing)
            {
                return;
            }

            if (Weight >= MaxSizeFcdaList)
            {
                return;
            }

            // Запрет добавления структуры с CO
            if (dropInfo.Data is SetFcTreeItemViewModel data && data.Header == "CO")
            {
                return;
            }

            switch (dropInfo.Data)
            {
                case TreeItemViewModelBase sourceItem:
                    // Только Doi
                    if (!(sourceItem.Model is IDoi) && (sourceItem.Model.GetFirstParentOfType<IDoi>() == default(IDoi)))
                    {
                        return;
                    }

                    // fs co нельзя
                    if (sourceItem.Model is IModelElement mE)
                    {
                        var fcs = _fcdaInfoService.GetFcsOfModelElement((IDevice)_device, mE);
                        if (fcs.Count == 1 && fcs[0] == "CO")
                        {
                            return;
                        }
                    }
                    dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                    dropInfo.Effects = System.Windows.DragDropEffects.Move;
                    return;
                case IFcdaViewModel fcdaViewModel:
                    dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                    dropInfo.Effects = System.Windows.DragDropEffects.Move;
                    return;
                default: return;
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            int index = dropInfo.InsertIndex;
            switch (dropInfo.Data)
            {
                case TreeItemViewModelBase sourceItem:
                    var fc = GetSellectedFc(sourceItem);

                    var fcdas = new List<IFcda>();

                    if (sourceItem.Model is IDai daiModel)
                    {
                        fcdas.Add(_fcdaFactory.GetFcda(daiModel));
                    }
                    else if (sourceItem.Model is IModelElement modelElement)
                    {
                        if (fc == null)
                        {
                            fcdas.AddRange(_fcdaFactory.GetFcdasFromModelElement(sourceItem.Model));
                        }
                        else
                        {
                            fcdas.Add(_fcdaFactory.GetStructFcda(modelElement, fc));
                        }
                    }
                    else
                    {
                        _loggingService.LogMessage($"Невозможно добавить {sourceItem.Model.ToString()}", SeverityEnum.Critical);
                    }

                    foreach (var modelFcda in fcdas)
                    {
                        try
                        {
                            AddFcda(modelFcda, index, sourceItem.Model);
                        }
                        catch (Exception e)
                        {
                            _loggingService.LogMessage(e.Message, SeverityEnum.Critical);
                            continue;
                        }
                        index++;
                    }
                    return;
                case IFcdaViewModel fcdaViewModel:
                    AddFcdaViewModel(index, fcdaViewModel);
                    return;
                default: return;
            }
            //TreeItemViewModelBase sourceItem = dropInfo.Data as TreeItemViewModelBase;

        }

        private void AddFcda(IFcda fcda, int insertIdex, IModelElement modelElement)
        {
            var newFcdaViewModel = _fcdaViewModelFactory.CreateFcdaViewModelElement(_device as IDevice, fcda, this);
            var fcdaName = $"{newFcdaViewModel.FullName}[{newFcdaViewModel.SellectedFc.Fc}]";
            if (!CanSetWeight(newFcdaViewModel.SellectedFc.FcWeight))
            {
                throw new Exception($"FCDA {fcdaName} не может быть добавлен");
            }

            if (FcdaViewModels.Any(el => el.FullName == newFcdaViewModel.FullName && el.SellectedFc.Fc == newFcdaViewModel.SellectedFc.Fc))
            {
                throw new Exception($"FCDA {fcdaName} уже добавлен");
            }

            FcdaViewModels.Insert(insertIdex, newFcdaViewModel);
            _loggingService.LogUserAction($"Добавлен FCDA {fcdaName} через DragDrop");
            Weigh();
        }

        private void AddFcdaViewModel(int insertIndex, IFcdaViewModel fcdaViewModel)
        {
            var fcdaName = $"{fcdaViewModel.FullName}[{fcdaViewModel.SellectedFc.Fc}]";
            if (FcdaViewModels.Any(el => el.Equals(fcdaViewModel)))
            {
                var elementIndex = FcdaViewModels.IndexOf(fcdaViewModel);
                FcdaViewModels.Remove(fcdaViewModel);
                if (insertIndex > elementIndex) insertIndex--;
                FcdaViewModels.Insert(insertIndex, fcdaViewModel);
            }

            _loggingService.LogUserAction($"FCDA {fcdaName} перенесён через DragDrop");
            Weigh();
        }

        private string GetSellectedFc(TreeItemViewModelBase treeItemViewModelBase)
        {
            if (treeItemViewModelBase.TypeName == "FC")
            {
                return treeItemViewModelBase.Header;
            }

            if (treeItemViewModelBase.Parent != null && treeItemViewModelBase.Parent is TreeItemViewModelBase parent)
            {
                return GetSellectedFc(parent);
            }

            return null;
        }

        #endregion
    }
}