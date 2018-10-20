using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.DataSets.Infrastructure.Factorys;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.DataSets.Infrastructure.ViewModels;
using BISC.Modules.DataSets.Presentation.Services.Interfaces;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;

namespace BISC.Modules.DataSets.Presentation.Services
{
  public  class DataSetSavingService:IDataSetSavingService
    {
        private readonly IDatasetModelService _datasetModelService;
        private readonly IInfoModelService _infoModelService;
        private readonly IDataSetFactory _dataSetFactory;
        private readonly IProjectService _projectService;
        private readonly ILoggingService _loggingService;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly ISclCommunicationModelService _sclCommunicationModelService;
        private readonly IBiscProject _biscProject;

        public DataSetSavingService(IDatasetModelService datasetModelService, IInfoModelService infoModelService,IDataSetFactory dataSetFactory,
            IProjectService projectService,ILoggingService loggingService,IConnectionPoolService connectionPoolService,ISclCommunicationModelService sclCommunicationModelService,IBiscProject biscProject)
        {
            _datasetModelService = datasetModelService;
            _infoModelService = infoModelService;
            _dataSetFactory = dataSetFactory;
            _projectService = projectService;
            _loggingService = loggingService;
            _connectionPoolService = connectionPoolService;
            _sclCommunicationModelService = sclCommunicationModelService;
            _biscProject = biscProject;
        }

        private void SetParentLnToModel(IDataSet dataSet)
        {
            if (dataSet.ParentModelElement != null)
            {
                if (dataSet is ILogicalNode parentNode)
                {
                    parentNode.ChildModelElements.Remove(dataSet);
                    dataSet.ParentModelElement = null;
                }
            }


            if (dataSet.ParentModelElement == null)
            {
            //    var ldevice = _infoModelService.GetLDevicesFromDevices(dataSet.GetFirstParentOfType<IDevice>())
             //       .FirstOrDefault((device => device.Inst == SelectedParentLd));

            //    if (SelectedParentLn == ldevice.LogicalNodeZero.Value.Name)
                {
                  //  dataSet.ParentModelElement = ldevice.LogicalNodeZero.Value;
                    return;
                }

             //   var parentLn = ldevice.LogicalNodes.FirstOrDefault((node => node.Name == SelectedParentLn));
            //    if (parentLn != null)
                {
              //      dataSet.ParentModelElement = parentLn;
                    return;
                }
            }
        }
  

        #region Implementation of IDataSetSavingService
        

        public void SaveDataSets(List<IDataSetViewModel> dataSetsToSave, IModelElement device,bool isSavingInDevice)
        {
            try
            {

                var dataSetsExisting = _datasetModelService.GetAllDataSetOfDevice(device);
                foreach (var dataSetToSave in dataSetsToSave)
                {
                    if (dataSetToSave.IsEditeble)
                    {
                        if (!dataSetToSave.ChangeTracker.GetIsModifiedRecursive()){continue;}
                      
                        var ldevice = _infoModelService.GetLDevicesFromDevices(device)
                            .FirstOrDefault((lDevice => lDevice.Inst == dataSetToSave.SelectedParentLd));
                        var ln = ldevice.AlLogicalNodes.FirstOrDefault(
                            (node => node.Name == dataSetToSave.SelectedParentLn));
                        if (dataSetsExisting.Any((set => set.Name == dataSetToSave.EditableNamePart)))
                        {
                            if (isSavingInDevice)
                            {
                                //_connectionPoolService
                                //    .GetConnection(_sclCommunicationModelService.GetIpOfDevice((device as IDevice).Name,
                                //        _biscProject.MainSclModel.Value)).MmsConnection.DeleteDataSet();
                            }
                            ln.ChildModelElements.Remove(dataSetsExisting.First((set =>
                                set.Name == dataSetToSave.EditableNamePart)));
                          
                        }

                        IDataSet dataSet = _dataSetFactory.CreateDataSet(ln, dataSetToSave.EditableNamePart,
                            dataSetToSave.FcdaViewModels.Select((model => model.GetFcda())).ToList());
                      
                    }

                }

                _projectService.SaveCurrentProject();
                _loggingService.LogMessage($"DataSets устройства {(device as IDevice).Name} успешно сохранены",
                    SeverityEnum.Info);
            }
            catch (Exception e)
            {
                _loggingService.LogMessage($"DataSets устройства {(device as IDevice).Name} сохранены c ошибкой: {e.Message}",
                    SeverityEnum.Warning);
            }
        }

        #endregion

    
    }
}
