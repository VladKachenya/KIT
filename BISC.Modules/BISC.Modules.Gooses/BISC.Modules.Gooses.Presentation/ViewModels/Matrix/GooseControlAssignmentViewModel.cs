using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix.Entities;
using BISC.Presentation.Infrastructure.Factories;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Matrix
{
    public class GooseControlAssignmentViewModel : NavigationViewModelBase
    {
        private readonly IDeviceModelService _deviceModelService;
        private readonly IGoosesModelService _goosesModelService;
        private readonly IBiscProject _biscProject;
        private readonly IDatasetModelService _datasetModelService;
        private readonly ICommandFactory _commandFactory;
        private  IDevice _device;
        

        public GooseControlAssignmentViewModel(IDeviceModelService deviceModelService,IGoosesModelService goosesModelService,IBiscProject biscProject,
            IDatasetModelService datasetModelService,ICommandFactory commandFactory)
        {
            _deviceModelService = deviceModelService;
            _goosesModelService = goosesModelService;
            _biscProject = biscProject;
            _datasetModelService = datasetModelService;
            _commandFactory = commandFactory;
            SaveChangesCommand = commandFactory.CreatePresentationCommand(OnSaveChanges);
            GooseControlBlockAssignmentItems=new ObservableCollection<GooseControlBlockAssignmentItem>();

        }

        private void OnSaveChanges()
        {
            var existingGooseInputsOfDevice = _goosesModelService.GetGooseInputsOfDevice(_device);
            IGooseMatrix gooseMatrix = _goosesModelService.GetGooseMatrixForDevice(_device);

            foreach (var gooseControlBlockAssignmentItem in GooseControlBlockAssignmentItems)
            {
                foreach (var fcdaAssignmentItem in gooseControlBlockAssignmentItem.FcdaAssignmentItems)
                {
                    var inputExisting= existingGooseInputsOfDevice.FirstOrDefault((input =>
                        input.ExternalGooseReferences.Any((extRef => _goosesModelService.CompareFcdaAndExtRef(extRef, fcdaAssignmentItem.Model)))));

                    var extRefExisting = inputExisting?.ExternalGooseReferences.FirstOrDefault((extRef =>
                        _goosesModelService.CompareFcdaAndExtRef(extRef, fcdaAssignmentItem.Model)));

                    bool isSubscribed = extRefExisting != null;


                    if (isSubscribed)
                    {
                        if (!fcdaAssignmentItem.IsSubscribed)
                        {
                            inputExisting.ExternalGooseReferences.Remove(extRefExisting);
                            foreach (var gooseRow in gooseMatrix.GooseRows)
                            {
                                if (extRefExisting.AsString() == gooseRow.ReferencePath)
                                {
                                    gooseMatrix.GooseRows.Remove(gooseRow);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (fcdaAssignmentItem.IsSubscribed)
                        {
                          _goosesModelService.AddGooseExternalReferenceToDevice(fcdaAssignmentItem.Model,_device,fcdaAssignmentItem.ParentDeviceName);
                        }
                    }
                }
            }
        }


        protected override void OnNavigatedFrom(BiscNavigationContext navigationContext)
        {
            base.OnNavigatedFrom(navigationContext);
        }


        public ICommand SaveChangesCommand { get; }

public ObservableCollection<GooseControlBlockAssignmentItem> GooseControlBlockAssignmentItems { get; }
        #region Overrides of NavigationViewModelBase

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            GooseControlBlockAssignmentItems.Clear();
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>("IED");
            var subscribedGooseControlsForCurrentDevice =
                _goosesModelService.GetGooseControlsSubscribed(_device, _biscProject.MainSclModel.Value);
        

            var existingGooseInputsOfDevice = _goosesModelService.GetGooseInputsOfDevice(_device);


            foreach (var subscribedGooseTuple in subscribedGooseControlsForCurrentDevice)
            {

                var dataSet = _datasetModelService.GetAllDataSetOfDevice(subscribedGooseTuple.Item1).FirstOrDefault((set =>set.Name==subscribedGooseTuple.Item2.DataSet));
                if (dataSet != null)
                {
                    GooseControlBlockAssignmentItem gooseControlBlockAssignmentItem =
                        new GooseControlBlockAssignmentItem(_commandFactory);
                    gooseControlBlockAssignmentItem.Signature = subscribedGooseTuple.Item2.AppId;

                    foreach (var fcda in dataSet.FcdaList)
                    {

                        bool isSubscribed = existingGooseInputsOfDevice.Any((input =>
                            input.ExternalGooseReferences.Any((extRef => _goosesModelService.CompareFcdaAndExtRef(extRef, fcda)))));
                        FcdaAssignmentItem fcdaAssignmentItem = new FcdaAssignmentItem();
                        fcdaAssignmentItem.Model = fcda;
                        if (string.IsNullOrEmpty(fcda.DaName))
                        {
                            fcdaAssignmentItem.Signature =
                                fcda.LdInst + "." + fcda.Prefix + fcda.LnClass + fcda.LnInst + "." +
                                fcda.DoName + "[" + fcda.Fc + "]";
                        }
                        else
                        {
                            fcdaAssignmentItem.Signature =
                                fcda.LdInst + "." + fcda.Prefix + fcda.LnClass + fcda.LnInst + "." +
                                fcda.DoName + "." + fcda.DaName + ".[" + fcda.Fc + "]";
                        }
                   
                        fcdaAssignmentItem.IsSubscribed = isSubscribed;
                        fcdaAssignmentItem.ParentDeviceName = subscribedGooseTuple.Item1.Name;
                        gooseControlBlockAssignmentItem.FcdaAssignmentItems.Add(fcdaAssignmentItem);
                    }
                    GooseControlBlockAssignmentItems.Add(gooseControlBlockAssignmentItem);
                }

            }

            base.OnNavigatedTo(navigationContext);
        }

    
        
        #endregion
    }
}