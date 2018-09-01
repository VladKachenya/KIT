using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public InfoModelTreeFactory(Func<LogicalNodeInfoModelItemViewModel> lnViewModelCreator,
            Func<LogicalNodeZeroInfoModelItemViewModel> ln0ViewModelCreator,
            Func<LDeviceInfoModelItemViewModel> ldeviceViewModelCreator, Func<DoiInfoModelItemViewModel> doiCreator,
            Func<DaiInfoModelItemViewModel> daiCreator, Func<SdiInfoModelItemViewModel> sdiCreator)
        {
            _lnViewModelCreator = lnViewModelCreator;
            _ln0ViewModelCreator = ln0ViewModelCreator;
            _ldeviceViewModelCreator = ldeviceViewModelCreator;
            _doiCreator = doiCreator;
            _daiCreator = daiCreator;
            _sdiCreator = sdiCreator;
        }

        public ObservableCollection<IInfoModelItemViewModel> CreateFullInfoModelTree(List<ILDevice> lDevices)
        {
            ObservableCollection<IInfoModelItemViewModel> infoModelItemViewModels =
                new ObservableCollection<IInfoModelItemViewModel>();

            foreach (var lDevice in lDevices)
            {
                LDeviceInfoModelItemViewModel lDeviceInfoModelItemViewModel = _ldeviceViewModelCreator();
                lDeviceInfoModelItemViewModel.Model = lDevice;
                lDeviceInfoModelItemViewModel.ChildInfoModelItemViewModels = CreateLDeviceInfoModelTree(lDevice);
                infoModelItemViewModels.Add(lDeviceInfoModelItemViewModel);
            }
            return infoModelItemViewModels;
        }

        private ObservableCollection<IInfoModelItemViewModel> GetDoi(List<IDoi> dois)
        {
            ObservableCollection<IInfoModelItemViewModel> infoModelItemViewModels =
                new ObservableCollection<IInfoModelItemViewModel>();

            foreach (var doi in dois)
            {
                DoiInfoModelItemViewModel doiInfoModelItemViewModel = _doiCreator();
                doiInfoModelItemViewModel.Model = doi;
                doiInfoModelItemViewModel.ChildInfoModelItemViewModels = GetSdi(doi.SdiCollection);
                var dais = GetDais(doi.DaiCollection);
                foreach (var daiVm in dais)
                {
                    doiInfoModelItemViewModel.ChildInfoModelItemViewModels.Add(daiVm);
                }
                infoModelItemViewModels.Add(doiInfoModelItemViewModel);
            }

            return infoModelItemViewModels;

        }

        private ObservableCollection<IInfoModelItemViewModel> GetSdi(List<ISdi> sdis)
        {
            ObservableCollection<IInfoModelItemViewModel> infoModelItemViewModels =
                new ObservableCollection<IInfoModelItemViewModel>();

            foreach (var sdi in sdis)
            {
                SdiInfoModelItemViewModel sdiInfoModelItemViewModel = _sdiCreator();
                sdiInfoModelItemViewModel.Model = sdi;
                sdiInfoModelItemViewModel.ChildInfoModelItemViewModels = GetSdi(sdi.SdiCollection);
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

        public ObservableCollection<IInfoModelItemViewModel> CreateLDeviceInfoModelTree(ILDevice lDevice)
        {
            ObservableCollection<IInfoModelItemViewModel> infoModelItemViewModels =
                new ObservableCollection<IInfoModelItemViewModel>();

            LogicalNodeZeroInfoModelItemViewModel logicalNodeZeroInfoModelItemViewModel = _ln0ViewModelCreator();
            logicalNodeZeroInfoModelItemViewModel.Model = lDevice.LogicalNodeZero;
            infoModelItemViewModels.Add(logicalNodeZeroInfoModelItemViewModel);
            logicalNodeZeroInfoModelItemViewModel.ChildInfoModelItemViewModels =
                GetDoi(lDevice.LogicalNodeZero.DoiCollection);
            foreach (var lDeviceLogicalNode in lDevice.LogicalNodes)
            {
                LogicalNodeInfoModelItemViewModel logicalNodeInfoModelItemViewModel = _lnViewModelCreator();
                logicalNodeInfoModelItemViewModel.Model = lDeviceLogicalNode;
                logicalNodeInfoModelItemViewModel.ChildInfoModelItemViewModels =
                    GetDoi(lDeviceLogicalNode.DoiCollection);
                infoModelItemViewModels.Add(logicalNodeInfoModelItemViewModel);
            }

            return infoModelItemViewModels;
        }
    }
}