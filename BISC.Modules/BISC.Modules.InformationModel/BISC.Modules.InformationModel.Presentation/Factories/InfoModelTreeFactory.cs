using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Presentation.Interfaces;
using BISC.Modules.InformationModel.Presentation.Interfaces.Factories;
using BISC.Modules.InformationModel.Presentation.ViewModels.Base;
using BISC.Modules.InformationModel.Presentation.ViewModels.InfoModelTree;

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

        public InfoModelTreeFactory(Func<LogicalNodeInfoModelItemViewModel> lnViewModelCreator,
            Func<LogicalNodeZeroInfoModelItemViewModel> ln0ViewModelCreator,
            Func<LDeviceInfoModelItemViewModel> ldeviceViewModelCreator, Func<DoiInfoModelItemViewModel> doiCreator,
            Func<DaiInfoModelItemViewModel> daiCreator, Func<SdiInfoModelItemViewModel> sdiCreator,
            IDataTypeTemplatesModelService dataTypeTemplatesModelService, IBiscProject biscProject, Func<SetFcTreeItemViewModel> fcSetCreator)
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
        }



        public ObservableCollection<IInfoModelItemViewModel> CreateFullInfoModelTree(List<ILDevice> lDevices,
            bool isFcSortingEnabled, ObservableCollection<IInfoModelItemViewModel> existingInfoModelItemViewModels = null)
        {
            ObservableCollection<IInfoModelItemViewModel> infoModelItemViewModels =
                existingInfoModelItemViewModels ?? new ObservableCollection<IInfoModelItemViewModel>();

            foreach (var lDevice in lDevices)
            {
                LDeviceInfoModelItemViewModel lDeviceInfoModelItemViewModel =
                    infoModelItemViewModels.FirstOrDefault((model => model.Model == lDevice)) as
                        LDeviceInfoModelItemViewModel ?? _ldeviceViewModelCreator();
                lDeviceInfoModelItemViewModel.Model = lDevice;
                lDeviceInfoModelItemViewModel.ChildInfoModelItemViewModels = CreateLDeviceInfoModelTree(lDevice,
                    isFcSortingEnabled, lDeviceInfoModelItemViewModel.ChildInfoModelItemViewModels);
                if (!infoModelItemViewModels.Contains(lDeviceInfoModelItemViewModel))
                {
                    infoModelItemViewModels.Add(lDeviceInfoModelItemViewModel);
                }
            }

            return infoModelItemViewModels;
        }

        private ObservableCollection<IInfoModelItemViewModel> GetDoi(List<IDoi> dois, bool isFcSortingEnabled,
            ObservableCollection<IInfoModelItemViewModel> existingInfoModelItemViewModels = null)
        {
            ObservableCollection<IInfoModelItemViewModel> infoModelItemViewModels = existingInfoModelItemViewModels ??
                new ObservableCollection<IInfoModelItemViewModel>();

            foreach (var doi in dois)
            {
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
                    var fcs = GetAllFcs(doi.DaiCollection.ToList(), doi.SdiCollection.ToList()).Distinct().ToList();
                    foreach (var fc in fcs)
                    {
                        SetFcTreeItemViewModel fcTreeItemViewModel = _fcSetCreator();
                        fcTreeItemViewModel.SetFc(fc,doi);
                        GetChildListByFc(doi.DaiCollection.ToList(), doi.SdiCollection.ToList(), fc).ForEach((treeItem =>
                            fcTreeItemViewModel.ChildInfoModelItemViewModels.Add(treeItem)));
                        doiInfoModelItemViewModel.ChildInfoModelItemViewModels.Add(fcTreeItemViewModel);
                    }

                }
                else
                {
                    doiInfoModelItemViewModel.ChildInfoModelItemViewModels = GetSdi(doi.SdiCollection.ToList(), doiInfoModelItemViewModel.ChildInfoModelItemViewModels);
                    var dais = GetDais(doi.DaiCollection.ToList());
                    foreach (var daiVm in dais)
                    {
                        doiInfoModelItemViewModel.ChildInfoModelItemViewModels.Add(daiVm);
                    }
                }

                doiInfoModelItemViewModel.Checked?.Invoke(isChecked);
                doiInfoModelItemViewModel.IsChecked = isChecked;
                if (!infoModelItemViewModels.Contains(doiInfoModelItemViewModel))
                    infoModelItemViewModels.Add(doiInfoModelItemViewModel);
            }

            return infoModelItemViewModels;
        }

        private List<string> GetAllFcs(List<IDai> dais, List<ISdi> sdis)
        {
            List<string> fcList = new List<string>();
            foreach (var dai in dais)
            {
                var da = _dataTypeTemplatesModelService.GetDaOfDai(dai, _biscProject.MainSclModel.Value);
                if (da == null)
                {
                    continue;
                }
                fcList.Add(da.Fc);
            }

            foreach (var sdi in sdis)
            {
                fcList.AddRange(GetAllFcs(sdi.DaiCollection.ToList(), sdi.SdiCollection.ToList()));
            }

            return fcList;
        }

        private List<TreeItemViewModelBase> GetChildListByFc(List<IDai> dais, List<ISdi> sdis, string fc)
        {
            List<TreeItemViewModelBase> fcItems = new List<TreeItemViewModelBase>();

            foreach (var dai in dais)
            {
                var da = _dataTypeTemplatesModelService.GetDaOfDai(dai, _biscProject.MainSclModel.Value);
                if(da==null)continue;
                if (fc == da.Fc)
                {
                    var daiTreeItem = _daiCreator();
                    daiTreeItem.Model = dai;
                    fcItems.Add(daiTreeItem);
                }
            }

            foreach (var sdi in sdis)
            {
                var fcItemsInSdi = GetChildListByFc(sdi.DaiCollection.ToList(), sdi.SdiCollection.ToList(), fc);

                if (fcItemsInSdi.Any())
                {
                    var sdiTreeItem = _sdiCreator();
                    sdiTreeItem.Model = sdi;
                    fcItemsInSdi.ForEach((treeItem => sdiTreeItem.ChildInfoModelItemViewModels.Add(treeItem))); 
                    fcItems.Add(sdiTreeItem);
                }

            }


            return fcItems;
        }




        private ObservableCollection<IInfoModelItemViewModel> GetSdi(List<ISdi> sdis,
            ObservableCollection<IInfoModelItemViewModel> existingInfoModelItemViewModels = null)
        {
            ObservableCollection<IInfoModelItemViewModel> infoModelItemViewModels = existingInfoModelItemViewModels ??
                new ObservableCollection<IInfoModelItemViewModel>();

            foreach (var sdi in sdis)
            {
                SdiInfoModelItemViewModel sdiInfoModelItemViewModel = infoModelItemViewModels.FirstOrDefault((model => model.Model == sdi)) as SdiInfoModelItemViewModel ?? _sdiCreator();
                sdiInfoModelItemViewModel.Model = sdi;
                sdiInfoModelItemViewModel.ChildInfoModelItemViewModels = GetSdi(sdi.SdiCollection.ToList());
                var dais = GetDais(sdi.DaiCollection.ToList());
                foreach (var daiVm in dais)
                {
                    sdiInfoModelItemViewModel.ChildInfoModelItemViewModels.Add(daiVm);
                }

                infoModelItemViewModels.Add(sdiInfoModelItemViewModel);
            }

            return infoModelItemViewModels;
        }

        private ObservableCollection<IInfoModelItemViewModel> GetDais(List<IDai> dais)
        {
            ObservableCollection<IInfoModelItemViewModel> infoModelItemViewModels =
                new ObservableCollection<IInfoModelItemViewModel>();

            foreach (var dai in dais)
            {
                DaiInfoModelItemViewModel daiInfoModelItemViewModel = _daiCreator();
                daiInfoModelItemViewModel.Model = dai;
                infoModelItemViewModels.Add(daiInfoModelItemViewModel);
            }

            return infoModelItemViewModels;
        }

        public ObservableCollection<IInfoModelItemViewModel> CreateLDeviceInfoModelTree(ILDevice lDevice,
            bool isFcSortingEnabled,
            ObservableCollection<IInfoModelItemViewModel> existingInfoModelItemViewModels = null)
        {
            ObservableCollection<IInfoModelItemViewModel> infoModelItemViewModels = existingInfoModelItemViewModels ??
                                                                                    new ObservableCollection<
                                                                                        IInfoModelItemViewModel>();

            LogicalNodeZeroInfoModelItemViewModel logicalNodeZeroInfoModelItemViewModel =
                infoModelItemViewModels.FirstOrDefault((model => model.Model == lDevice.LogicalNodeZero.Value)) as
                    LogicalNodeZeroInfoModelItemViewModel ?? _ln0ViewModelCreator();
            logicalNodeZeroInfoModelItemViewModel.Model = lDevice.LogicalNodeZero.Value;

            if (!infoModelItemViewModels.Contains(logicalNodeZeroInfoModelItemViewModel))
                infoModelItemViewModels.Add(logicalNodeZeroInfoModelItemViewModel);

            logicalNodeZeroInfoModelItemViewModel.ChildInfoModelItemViewModels =
                GetDoi(lDevice.LogicalNodeZero.Value.DoiCollection.ToList(), isFcSortingEnabled,
                    logicalNodeZeroInfoModelItemViewModel.ChildInfoModelItemViewModels);
            foreach (var lDeviceLogicalNode in lDevice.LogicalNodes)
            {
                LogicalNodeInfoModelItemViewModel logicalNodeInfoModelItemViewModel =
                    infoModelItemViewModels.FirstOrDefault((model => model.Model == lDeviceLogicalNode)) as
                        LogicalNodeInfoModelItemViewModel ?? _lnViewModelCreator();
                logicalNodeInfoModelItemViewModel.Model = lDeviceLogicalNode;
                logicalNodeInfoModelItemViewModel.ChildInfoModelItemViewModels =
                    GetDoi(lDeviceLogicalNode.DoiCollection.ToList(), isFcSortingEnabled,
                        logicalNodeInfoModelItemViewModel.ChildInfoModelItemViewModels);

                if (!infoModelItemViewModels.Contains(logicalNodeInfoModelItemViewModel))
                    infoModelItemViewModels.Add(logicalNodeInfoModelItemViewModel);
            }

            return infoModelItemViewModels;
        }
    }
}