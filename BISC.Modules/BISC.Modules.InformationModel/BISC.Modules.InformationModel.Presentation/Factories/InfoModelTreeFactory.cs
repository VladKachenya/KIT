using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Keys;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Modules.InformationModel.Presentation.Helpers;
using BISC.Modules.InformationModel.Presentation.Interfaces;
using BISC.Modules.InformationModel.Presentation.Interfaces.Factories;
using BISC.Modules.InformationModel.Presentation.ViewModels.Base;
using BISC.Modules.InformationModel.Presentation.ViewModels.InfoModelTree;
using static System.String;

namespace BISC.Modules.InformationModel.Presentation.Factories
{
    public class InfoModelTreeFactory : IInfoModelTreeFactory
    {
        private readonly Func<LogicalNodeInfoModelItemViewModel> _lnViewModelCreator;
        private readonly Func<LogicalNodeZeroInfoModelItemViewModel> _ln0ViewModelCreator;
        private readonly Func<LDeviceInfoModelItemViewModel> _ldeviceViewModelCreator;
        private readonly Func<DoiInfoModelItemViewModel> _doiCreator;
        private readonly Func<DaiInfoModelItemViewModel> _daiCreator;
        private readonly Func<SdiInfoModelItemViewModel> _sdiCreator;
        private readonly IDataTypeTemplatesModelService _dataTypeTemplatesModelService;
        private readonly IBiscProject _biscProject;
        private readonly Func<SetFcTreeItemViewModel> _fcSetCreator;
        private readonly IInfoModelService _infoModelService;
        private readonly ValueValidatorsHelper _valueValidatorsHelper;

        public InfoModelTreeFactory(
            Func<LogicalNodeInfoModelItemViewModel> lnViewModelCreator,
            Func<LogicalNodeZeroInfoModelItemViewModel> ln0ViewModelCreator,
            Func<LDeviceInfoModelItemViewModel> ldeviceViewModelCreator,
            Func<DoiInfoModelItemViewModel> doiCreator,
            Func<DaiInfoModelItemViewModel> daiCreator,
            Func<SdiInfoModelItemViewModel> sdiCreator,
            IDataTypeTemplatesModelService dataTypeTemplatesModelService,
            IBiscProject biscProject,
            Func<SetFcTreeItemViewModel> fcSetCreator,
            IInfoModelService infoModelService,
            ValueValidatorsHelper valueValidatorsHelper)
        {
            _lnViewModelCreator = lnViewModelCreator;
            _ln0ViewModelCreator = ln0ViewModelCreator;
            _ldeviceViewModelCreator = ldeviceViewModelCreator;
            _doiCreator = doiCreator;
            _daiCreator = daiCreator;
            _sdiCreator = sdiCreator;
            _dataTypeTemplatesModelService = dataTypeTemplatesModelService;
            _biscProject = biscProject;
            _fcSetCreator = fcSetCreator;
            _infoModelService = infoModelService;
            _valueValidatorsHelper = valueValidatorsHelper;
        }



        public ObservableCollection<IInfoModelItemViewModel> CreateFullInfoModelTree(List<ILDevice> lDevices,
            bool isFcSortingEnabled, bool isDbOnly = false, ObservableCollection<IInfoModelItemViewModel> existingInfoModelItemViewModels = null)
        {
            ObservableCollection<IInfoModelItemViewModel> infoModelItemViewModels =
                existingInfoModelItemViewModels ?? new ObservableCollection<IInfoModelItemViewModel>();

            foreach (var lDevice in lDevices)
            {
                if (isDbOnly && !_infoModelService.ContainsDbRecursive(lDevice))
                {
                    continue;
                }

                LDeviceInfoModelItemViewModel lDeviceInfoModelItemViewModel =
                    infoModelItemViewModels.FirstOrDefault((model => model.Model == lDevice)) as
                        LDeviceInfoModelItemViewModel ?? _ldeviceViewModelCreator();
                lDeviceInfoModelItemViewModel.Model = lDevice;
                lDeviceInfoModelItemViewModel.ChildInfoModelItemViewModels = CreateLDeviceInfoModelTree(lDevice,
                    isFcSortingEnabled, isDbOnly, lDeviceInfoModelItemViewModel.ChildInfoModelItemViewModels);
                if (!infoModelItemViewModels.Contains(lDeviceInfoModelItemViewModel))
                {
                    infoModelItemViewModels.Add(lDeviceInfoModelItemViewModel);
                }
            }

            return infoModelItemViewModels;
        }

        public ObservableCollection<IInfoModelItemViewModel> CreateLDeviceInfoModelTree(ILDevice lDevice,
            bool isFcSortingEnabled,
            bool isDbOnly = false,
            ObservableCollection<IInfoModelItemViewModel> existingInfoModelItemViewModels = null)
        {
            ObservableCollection<IInfoModelItemViewModel> infoModelItemViewModels = existingInfoModelItemViewModels ??
                                                                                    new ObservableCollection<IInfoModelItemViewModel>();

            LogicalNodeZeroInfoModelItemViewModel logicalNodeZeroInfoModelItemViewModel =
                infoModelItemViewModels.FirstOrDefault((model => model.Model == lDevice.LogicalNodeZero.Value)) as
                    LogicalNodeZeroInfoModelItemViewModel ?? _ln0ViewModelCreator();
            logicalNodeZeroInfoModelItemViewModel.Model = lDevice.LogicalNodeZero.Value;

            if (!infoModelItemViewModels.Contains(logicalNodeZeroInfoModelItemViewModel))
                infoModelItemViewModels.Add(logicalNodeZeroInfoModelItemViewModel);

            logicalNodeZeroInfoModelItemViewModel.ChildInfoModelItemViewModels =
                GetDoi(lDevice.LogicalNodeZero.Value.DoiCollection.ToList(), isFcSortingEnabled, isDbOnly,
                    logicalNodeZeroInfoModelItemViewModel.ChildInfoModelItemViewModels);
            foreach (var lDeviceLogicalNode in lDevice.LogicalNodes)
            {
                if (isDbOnly && !_infoModelService.ContainsDbRecursive(lDeviceLogicalNode))
                {
                    continue;
                }
                LogicalNodeInfoModelItemViewModel logicalNodeInfoModelItemViewModel =
                    infoModelItemViewModels.FirstOrDefault((model => model.Model == lDeviceLogicalNode)) as
                        LogicalNodeInfoModelItemViewModel ?? _lnViewModelCreator();
                logicalNodeInfoModelItemViewModel.Model = lDeviceLogicalNode;
                logicalNodeInfoModelItemViewModel.ChildInfoModelItemViewModels =
                    GetDoi(lDeviceLogicalNode.DoiCollection.ToList(), isFcSortingEnabled, isDbOnly,
                        logicalNodeInfoModelItemViewModel.ChildInfoModelItemViewModels);

                if (!infoModelItemViewModels.Contains(logicalNodeInfoModelItemViewModel))
                    infoModelItemViewModels.Add(logicalNodeInfoModelItemViewModel);
            }

            return infoModelItemViewModels;
        }

        private ObservableCollection<IInfoModelItemViewModel> GetDoi(List<IDoi> dois, bool isFcSortingEnabled, bool isDbOnly = false,
            ObservableCollection<IInfoModelItemViewModel> existingInfoModelItemViewModels = null)
        {
            ObservableCollection<IInfoModelItemViewModel> infoModelItemViewModels = existingInfoModelItemViewModels ??
                new ObservableCollection<IInfoModelItemViewModel>();

            foreach (var doi in dois)
            {
                if (isDbOnly && !_infoModelService.ContainsDbRecursive(doi))
                {
                    continue;
                }
                DoiInfoModelItemViewModel doiInfoModelItemViewModel =
                    infoModelItemViewModels.FirstOrDefault((model =>
                        model.Model == doi)) as DoiInfoModelItemViewModel ?? _doiCreator();
                doiInfoModelItemViewModel.Model = doi;
                var isChecked = doiInfoModelItemViewModel.IsChecked;
                doiInfoModelItemViewModel.Checked?.Invoke(false);
                doiInfoModelItemViewModel.IsChecked = false;
                doiInfoModelItemViewModel.ChildInfoModelItemViewModels.Clear();
                if (isFcSortingEnabled)
                {
                    var fcs = _infoModelService.GetAllFcs(doi.DaiCollection.ToList(), doi.SdiCollection.ToList()).Distinct().ToList();
                    if (isDbOnly)
                    {
                        fcs = fcs.Where(fc => fc == InformationModelKeys.DataAttributeHeaderKeys.dbFc).ToList();
                    }
                    foreach (var fc in fcs)
                    {
                        SetFcTreeItemViewModel fcTreeItemViewModel = _fcSetCreator();
                        fcTreeItemViewModel.SetFc(fc, doi);
                        GetChildListByFc(doi.DaiCollection.ToList(), doi.SdiCollection.ToList(), fc, isDbOnly).ForEach((treeItem =>
                        {
                            fcTreeItemViewModel.ChildInfoModelItemViewModels.Add(treeItem);
                            treeItem.Parent = fcTreeItemViewModel;
                        }));
                        doiInfoModelItemViewModel.ChildInfoModelItemViewModels.Add(fcTreeItemViewModel);
                        fcTreeItemViewModel.Parent = doiInfoModelItemViewModel;
                    }

                }
                else
                {
                    doiInfoModelItemViewModel.ChildInfoModelItemViewModels = GetSdi(doi.SdiCollection.ToList(), isDbOnly, doiInfoModelItemViewModel.ChildInfoModelItemViewModels);
                    var dais = GetDais(doi.DaiCollection.ToList(), isDbOnly);
                    foreach (var daiVm in dais)
                    {
                        doiInfoModelItemViewModel.ChildInfoModelItemViewModels.Add(daiVm);
                    }

                    foreach (var childInfoModelItemViewModel in doiInfoModelItemViewModel.ChildInfoModelItemViewModels)
                    {
                        childInfoModelItemViewModel.Parent = doiInfoModelItemViewModel;
                    }
                }

                doiInfoModelItemViewModel.Checked?.Invoke(isChecked);
                doiInfoModelItemViewModel.IsChecked = isChecked;
                if (!infoModelItemViewModels.Contains(doiInfoModelItemViewModel))
                    infoModelItemViewModels.Add(doiInfoModelItemViewModel);
            }

            return infoModelItemViewModels;
        }



        private List<TreeItemViewModelBase> GetChildListByFc(List<IDai> dais, List<ISdi> sdis, string fc, bool isDbOnly = false)
        {
            List<TreeItemViewModelBase> fcItems = new List<TreeItemViewModelBase>();

            if (isDbOnly && !string.Equals(fc, InformationModelKeys.DataAttributeHeaderKeys.dbFc, StringComparison.CurrentCultureIgnoreCase))
            {
                return fcItems;
            }

            foreach (var dai in dais)
            {
                var da = _dataTypeTemplatesModelService.GetDaOfDai(dai, _biscProject.MainSclModel.Value);
                if (da == null) continue;
                if (fc == da.Fc)
                {
                    fcItems.Add(GetDaiInfoModelItemViewModel(dai));
                }
            }

            foreach (var sdi in sdis)
            {
                var fcItemsInSdi = GetChildListByFc(sdi.DaiCollection.ToList(), sdi.SdiCollection.ToList(), fc);

                if (fcItemsInSdi.Any())
                {
                    var sdiTreeItem = _sdiCreator();
                    sdiTreeItem.Model = sdi;
                    fcItemsInSdi.ForEach((treeItem =>
                    {
                        sdiTreeItem.ChildInfoModelItemViewModels.Add(treeItem);
                        treeItem.Parent = sdiTreeItem;
                    }));
                    fcItems.Add(sdiTreeItem);
                }

            }

            return fcItems;
        }

        private ObservableCollection<IInfoModelItemViewModel> GetSdi(List<ISdi> sdis, bool isDbOnly,
            ObservableCollection<IInfoModelItemViewModel> existingInfoModelItemViewModels = null)
        {
            ObservableCollection<IInfoModelItemViewModel> infoModelItemViewModels = existingInfoModelItemViewModels ??
                new ObservableCollection<IInfoModelItemViewModel>();

            foreach (var sdi in sdis)
            {
                if (isDbOnly && !_infoModelService.ContainsDbRecursive(sdi))
                {
                    continue;
                }
                SdiInfoModelItemViewModel sdiInfoModelItemViewModel = infoModelItemViewModels.FirstOrDefault((model => model.Model == sdi)) as SdiInfoModelItemViewModel ?? _sdiCreator();
                sdiInfoModelItemViewModel.Model = sdi;
                sdiInfoModelItemViewModel.ChildInfoModelItemViewModels = GetSdi(sdi.SdiCollection.ToList(), isDbOnly);
                var dais = GetDais(sdi.DaiCollection.ToList(), isDbOnly);
                foreach (var daiVm in dais)
                {
                    sdiInfoModelItemViewModel.ChildInfoModelItemViewModels.Add(daiVm);
                }
                foreach (IInfoModelItemViewModel childInfoModelItemViewModel in sdiInfoModelItemViewModel.ChildInfoModelItemViewModels)
                {
                    childInfoModelItemViewModel.Parent = sdiInfoModelItemViewModel;
                }
                infoModelItemViewModels.Add(sdiInfoModelItemViewModel);
            }

            return infoModelItemViewModels;
        }

        private ObservableCollection<IInfoModelItemViewModel> GetDais(List<IDai> dais,
            bool isDbOnly)
        {
            ObservableCollection<IInfoModelItemViewModel> infoModelItemViewModels =
                new ObservableCollection<IInfoModelItemViewModel>();
            if (isDbOnly)
            {
                dais = dais.Where(dai => _infoModelService.ContainsDbRecursive(dai)).ToList();
            }

            dais.ForEach(dai => infoModelItemViewModels.Add(GetDaiInfoModelItemViewModel(dai)));

            return infoModelItemViewModels;
        }

        private DaiInfoModelItemViewModel GetDaiInfoModelItemViewModel(IDai dai)
        {
            DaiInfoModelItemViewModel daiInfoModelItemViewModel = _daiCreator();
            if (String.Equals(dai.Name, InformationModelKeys.DataAttributeHeaderKeys.db,
                    StringComparison.CurrentCultureIgnoreCase) ||
                String.Equals(dai.Name, InformationModelKeys.DataAttributeHeaderKeys.zeroDb,
                    StringComparison.CurrentCultureIgnoreCase))
            {
                daiInfoModelItemViewModel.ValueValidationFunction = _valueValidatorsHelper.ValidateDbOrZeroDb;
                daiInfoModelItemViewModel.ValueToolTip = _valueValidatorsHelper.DbOrZeroDbToolTip;
            }
            daiInfoModelItemViewModel.Model = dai;
            return daiInfoModelItemViewModel;
        }

    }
}