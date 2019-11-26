using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Connection.Infrastructure.Connection;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.DataSets.Infrastructure.Factorys;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.DataSets.Infrastructure.ViewModels;
using BISC.Modules.DataSets.Model.Mappers;
using BISC.Modules.DataSets.Presentation.Services.Interfaces;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Saving;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;

namespace BISC.Modules.DataSets.Presentation.Services
{
    public class DataSetSavingService : IDeviceElementSavingService
    {
        private readonly IFtpDataSetModelService _ftpDataSetModelService;
        private readonly IConnectionPoolService _connectionPoolService;

        public DataSetSavingService(IFtpDataSetModelService ftpDataSetModelService,IConnectionPoolService connectionPoolService)
        {
            _ftpDataSetModelService = ftpDataSetModelService;
            _connectionPoolService = connectionPoolService;
        }
		

        //#region Implementation of IDataSetSavingService



        //public async Task SaveDataSets(List<IDataSetViewModel> dataSetsToSave, IModelElement device, bool isSavingInDevice)
        //{
        //    try
        //    {

        //        var dataSetsExisting = _datasetModelService.GetAllDataSetOfDevice(device);

        //        foreach (var dataSetExiting in dataSetsExisting)
        //        {
        //            if (!dataSetsToSave.Any((model => model.EditableNamePart == dataSetExiting.Name)))
        //            {
                      
        //                var ln = dataSetExiting.ParentModelElement as ILogicalNode;
        //                var ldevice = ln.ParentModelElement as ILDevice;
        //                if (isSavingInDevice)
        //                {
        //                    var res = await _connectionPoolService
        //                        .GetConnection(_sclCommunicationModelService.GetIpOfDevice((device as IDevice).Name,
        //                            _biscProject.MainSclModel.Value)).MmsConnection.DeleteDataSet(ln.Name, ldevice.Inst, (device as IDevice).Name, dataSetExiting.Name);
        //                    if (!res.IsSucceed)
        //                    {
        //                        _loggingService.LogMessage($"Не удалось удалить DataSet {dataSetExiting.Name} в устройстве: {res.GetFirstError()}", SeverityEnum.Warning);
        //                    }
        //                    else
        //                    {
        //                        _loggingService.LogMessage($"DataSet {dataSetExiting.Name} удален в устройстве: {(device as IDevice).Name}", SeverityEnum.Info);
        //                    }
        //                }
        //                ln.ChildModelElements.Remove(dataSetsExisting.First((set =>
        //                    set.Name == dataSetExiting.Name)));
        //            }
        //        }

        //        foreach (var dataSetToSave in dataSetsToSave)
        //        {
        //            if (dataSetToSave.IsEditable)
        //            {
        //                if (!dataSetToSave.ChangeTracker.GetIsModifiedRecursive()) { continue; }

        //                var ldevice = _infoModelService.GetLDevicesFromDevices(device)
        //                    .FirstOrDefault((lDevice => lDevice.Inst == dataSetToSave.SelectedParentLd));
        //                var ln = ldevice.AlLogicalNodes.FirstOrDefault(
        //                    (node => node.Name == dataSetToSave.SelectedParentLn));


        //                if (dataSetsExisting.Any((set => set.Name == dataSetToSave.EditableNamePart)))
        //                {
        //                    if (isSavingInDevice)
        //                    {
        //                        var res = await _connectionPoolService
        //                             .GetConnection(_sclCommunicationModelService.GetIpOfDevice((device as IDevice).Name,
        //                                 _biscProject.MainSclModel.Value)).MmsConnection.DeleteDataSet(ln.Name, ldevice.Inst, (device as IDevice).Name, dataSetToSave.EditableNamePart);
        //                        if (!res.IsSucceed)
        //                        {
        //                            _loggingService.LogMessage($"Не удалось удалить DataSet {dataSetToSave.EditableNamePart} в устройстве: {res.GetFirstError()}", SeverityEnum.Warning);
        //                        }
        //                        else
        //                        {
        //                            _loggingService.LogMessage($"DataSet {dataSetToSave.EditableNamePart} удален в устройстве: {(device as IDevice).Name}", SeverityEnum.Info);
        //                        }
        //                    }
        //                    ln.ChildModelElements.Remove(dataSetsExisting.First((set =>
        //                        set.Name == dataSetToSave.EditableNamePart)));

        //                }

        //                IDataSet dataSet = _dataSetFactory.CreateDataSet(ln, dataSetToSave.EditableNamePart,
        //                    dataSetToSave.FcdaViewModels.Select((model => model.GetFcda())).ToList());
        //                if (isSavingInDevice)
        //                {
        //                  var res=  await _connectionPoolService
        //                          .GetConnection(_sclCommunicationModelService.GetIpOfDevice((device as IDevice).Name,
        //                              _biscProject.MainSclModel.Value)).MmsConnection.AddDataSet(ln.Name, ldevice.Inst, (device as IDevice).Name, dataSetToSave.EditableNamePart, dataSet.FcdaList.ToDtos((device as IDevice).Name));
        //                    if (!res.IsSucceed)
        //                    {
        //                        _loggingService.LogMessage($"Не удалось {dataSetToSave.EditableNamePart} добавить DataSet в устройстве: {res.GetFirstError()}", SeverityEnum.Warning);
        //                    }
        //                    else
        //                    {
        //                        _loggingService.LogMessage($"DataSet {dataSetToSave.EditableNamePart} добавлен в устройство: {(device as IDevice).Name}", SeverityEnum.Info);
        //                    }
        //                }
        //            }

        //        }

        //        _projectService.SaveCurrentProject();
        //        _loggingService.LogMessage($"DataSets устройства {(device as IDevice).Name} успешно сохранены",
        //            SeverityEnum.Info);
        //    }
        //    catch (Exception e)
        //    {
        //        _loggingService.LogMessage($"DataSets устройства {(device as IDevice).Name} сохранены c ошибкой: {e.Message}",
        //            SeverityEnum.Warning);
        //    }
        //}

        //#endregion


        #region Implementation of IDeviceElementSavingService

        public int Priority => 10;
        public async Task<OperationResult> Save(IDevice device)
        {
            if (_connectionPoolService.GetConnection(device.Ip).IsConnected)
            {
                var allDatasets=new List<IDataSet>();
                device.GetAllChildrenOfType(ref allDatasets);
                var dsToSaveByFtp = allDatasets.Where(ds => ds.IsDynamic).ToList();
              return  await _ftpDataSetModelService.WriteDatasetsToDevice(device.Ip, dsToSaveByFtp);
            }
            return new OperationResult("Ошибка загрузки DataSet");
        }

        #endregion
    }
}
