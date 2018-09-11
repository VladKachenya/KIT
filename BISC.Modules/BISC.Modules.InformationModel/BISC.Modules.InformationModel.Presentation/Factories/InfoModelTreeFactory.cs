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

        public InfoModelTreeFactory(Func<LogicalNodeInfoModelItemViewModel> lnViewModelCreator,
            Func<LogicalNodeZeroInfoModelItemViewModel> ln0ViewModelCreator,
            Func<LDeviceInfoModelItemViewModel> ldeviceViewModelCreator, Func<DoiInfoModelItemViewModel> doiCreator,
            Func<DaiInfoModelItemViewModel> daiCreator, Func<SdiInfoModelItemViewModel> sdiCreator,
            IDataTypeTemplatesModelService dataTypeTemplatesModelService, IBiscProject biscProject)
        {
            _lnViewModelCreator = lnViewModelCreator;
            _ln0ViewModelCreator = ln0ViewModelCreator;
            _ldeviceViewModelCreator = ldeviceViewModelCreator;
            _doiCreator = doiCreator;
            _daiCreator = daiCreator;
            _sdiCreator = sdiCreator;
            _dataTypeTemplatesModelService = dataTypeTemplatesModelService;
            _biscProject = biscProject;
        }



        public ObservableCollection<IInfoModelItemViewModel> CreateFullInfoModelTree(List<ILDevice> lDevices,
            bool isFcSortingEnabled,
            ObservableCollection<IInfoModelItemViewModel> existingInfoModelItemViewModels = null)
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
                infoModelItemViewModels.Add(lDeviceInfoModelItemViewModel);
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
                DoiInfoModelItemViewModel doiInfoModelItemViewModel = infoModelItemViewModels.FirstOrDefault((model => model.Model == doi)) as DoiInfoModelItemViewModel ?? _doiCreator();
                doiInfoModelItemViewModel.Model = doi;

                if (isFcSortingEnabled)
                {
                    var fcs = GetAllFcs(doi.DaiCollection, doi.SdiCollection);
                }
                else
                {

                }

                doiInfoModelItemViewModel.ChildInfoModelItemViewModels = GetSdi(doi.SdiCollection, isFcSortingEnabled, doiInfoModelItemViewModel.ChildInfoModelItemViewModels);
                var dais = GetDais(doi.DaiCollection);
                foreach (var daiVm in dais)
                {
                    doiInfoModelItemViewModel.ChildInfoModelItemViewModels.Add(daiVm);
                }

                infoModelItemViewModels.Add(doiInfoModelItemViewModel);
            }

            return infoModelItemViewModels;
        }

        private List<string> GetAllFcs(List<IDai> dais, List<ISdi> sdis)
        {
            List<string> fcList = new List<string>();
            foreach (var dai in dais)
            {

                var da = _dataTypeTemplatesModelService.GetDaOfDai(dai, _biscProject.MainSclModel);
                if (da == null)
                {

                }
                fcList.Add(da.Fc);
            }

            foreach (var sdi in sdis)
            {
                fcList.AddRange(GetAllFcs(sdi.DaiCollection, sdi.SdiCollection));
            }

            return fcList;
        }



        private ObservableCollection<IInfoModelItemViewModel> GetSdi(List<ISdi> sdis, bool isFcSortingEnabled,
            ObservableCollection<IInfoModelItemViewModel> existingInfoModelItemViewModels = null)
        {
            ObservableCollection<IInfoModelItemViewModel> infoModelItemViewModels = existingInfoModelItemViewModels ??
                new ObservableCollection<IInfoModelItemViewModel>();

            foreach (var sdi in sdis)
            {
                SdiInfoModelItemViewModel sdiInfoModelItemViewModel = infoModelItemViewModels.FirstOrDefault((model => model.Model == sdi)) as SdiInfoModelItemViewModel ?? _sdiCreator();
                sdiInfoModelItemViewModel.Model = sdi;
                sdiInfoModelItemViewModel.ChildInfoModelItemViewModels = GetSdi(sdi.SdiCollection, isFcSortingEnabled);
                var dais = GetDais(sdi.DaiCollection);
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
                infoModelItemViewModels.FirstOrDefault((model => model.Model == lDevice.LogicalNodeZero)) as
                    LogicalNodeZeroInfoModelItemViewModel ?? _ln0ViewModelCreator();
            logicalNodeZeroInfoModelItemViewModel.Model = lDevice.LogicalNodeZero;

            if (!infoModelItemViewModels.Contains(logicalNodeZeroInfoModelItemViewModel))
                infoModelItemViewModels.Add(logicalNodeZeroInfoModelItemViewModel);

            logicalNodeZeroInfoModelItemViewModel.ChildInfoModelItemViewModels =
                GetDoi(lDevice.LogicalNodeZero.DoiCollection, isFcSortingEnabled,
                    logicalNodeZeroInfoModelItemViewModel.ChildInfoModelItemViewModels);
            foreach (var lDeviceLogicalNode in lDevice.LogicalNodes)
            {
                LogicalNodeInfoModelItemViewModel logicalNodeInfoModelItemViewModel =
                    infoModelItemViewModels.FirstOrDefault((model => model.Model == lDeviceLogicalNode)) as
                        LogicalNodeInfoModelItemViewModel ?? _lnViewModelCreator();
                logicalNodeInfoModelItemViewModel.Model = lDeviceLogicalNode;
                logicalNodeInfoModelItemViewModel.ChildInfoModelItemViewModels =
                    GetDoi(lDeviceLogicalNode.DoiCollection, isFcSortingEnabled,
                        logicalNodeInfoModelItemViewModel.ChildInfoModelItemViewModels);
                infoModelItemViewModels.Add(logicalNodeInfoModelItemViewModel);
            }

            return infoModelItemViewModels;
        }
    }
}