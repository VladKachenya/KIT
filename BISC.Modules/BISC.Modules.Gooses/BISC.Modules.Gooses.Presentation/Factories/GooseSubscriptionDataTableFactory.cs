using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Factorys;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Presentation.Interfaces.Factories;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix.Entities;
using BISC.Modules.Gooses.Presentation.ViewModels.Subscriptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Device.Infrastructure.Keys;

namespace BISC.Modules.Gooses.Presentation.Factories
{
    public class GooseSubscriptionDataTableFactory : IGooseSubscriptionDataTableFactory
    {
        private readonly IDeviceModelService _deviceModelService;
        private readonly IBiscProject _biscProject;
        private readonly IGoosesModelService _goosesModelService;
        private readonly IGooseInputModelInfoFactory _gooseInputModelIngoFactory;
        private readonly ILoggingService _loggingService;

        public GooseSubscriptionDataTableFactory(IDeviceModelService deviceModelService, IBiscProject biscProject, IGoosesModelService goosesModelService,
            IGooseInputModelInfoFactory gooseInputModelIngoFactory, ILoggingService loggingService )
        {
            _deviceModelService = deviceModelService;
            _biscProject = biscProject;
            _goosesModelService = goosesModelService;
            _gooseInputModelIngoFactory = gooseInputModelIngoFactory;
            _loggingService = loggingService;
        }

        public DataTable GetGooseSubscriptionDataTableFactory()
        {
            var table = new DataTable();
            var devicesInProject = _deviceModelService.GetDevicesFromModel(_biscProject.MainSclModel.Value);

            //Создание колонок
            table.Columns.Add(new DataColumn("GooseControl", typeof(GooseDescriptionEntity)));
            table.Columns[0].ReadOnly = true;

            foreach (var deviceInProject in devicesInProject)
            {
                // Если устройство не БЕМН то не добавляем его в таблицн 
                if (deviceInProject.Manufacturer != DeviceKeys.DeviceManufacturer.BemnManufacturer)
                {
                    continue;
                    //lockColumnIndexes.Add(devicesInProject.IndexOf(deviceInProject) + 1);
                }
                var col = new DataColumn(deviceInProject.Name, typeof(SubscriptionValue));
                col.Caption = deviceInProject.DeviceGuid.ToString();
                table.Columns.Add(col);
                
            }

            // Полученин всех текущих GooseControlModelInfo
            var inputsList = new List<IGooseInputModelInfo>();
            foreach (var deviceInProject in devicesInProject)
            {
                var deviceInputs = _goosesModelService.GetGooseInputModelInfos(deviceInProject);
                foreach (var input in deviceInputs)
                {
                    if (inputsList.Any(el => el.ModelElementCompareTo(input)))
                    {
                        continue;
                    }
                    inputsList.Add(input);
                }
            }
            var rowValues = new List<GooseDescriptionEntity>();
            //Создание строк на основании GooseControls из project
            foreach (var deviceInProject in devicesInProject)
            {
                var gooseControls = _goosesModelService.GetGooseControlsOfDevice(deviceInProject);
                foreach (var gooseControl in gooseControls)
                {

                    // rowValues.Add(new GooseDescriptionEntity(gooseControl, deviceInProject));
                    GooseDescriptionEntity row;
                    try
                    {
                        row = new GooseDescriptionEntity(
                            _gooseInputModelIngoFactory.CreateGooseInputModelInfo(deviceInProject, gooseControl),
                            deviceInProject);
                    }
                    catch (Exception e)
                    {
                        _loggingService.LogMessage(e.Message, SeverityEnum.Critical);
                        continue;
                    }
                   
                    rowValues.Add(row);
                    table.Rows.Add(row);
                }
            }
            //Создание строк на основании inputsList
            foreach (var input in inputsList)
            {
                if (rowValues.Any(el => el.GooseInputModelInfo.ModelElementCompareTo(input)))
                {
                    continue;
                }
                table.Rows.Add(new GooseDescriptionEntity(input, null));

            }



            // Заполнение таблицы
            for (int colIndex = 1; colIndex < table.Columns.Count; colIndex++)
            {
                var colDevGuid = Guid.Parse(table.Columns[colIndex].Caption);
                var colDevice = _deviceModelService.GetDeviceByGuid(_biscProject.MainSclModel.Value, colDevGuid);
                var gooseInputModelInfoEntites = _goosesModelService
                    .GetGooseDeviceInputOfProject(_biscProject, colDevice).GooseInputModelInfoList;
               

                for (int rowIndex = 0; rowIndex < table.Rows.Count; rowIndex++)
                {
                    var rowEntity = table.Rows[rowIndex][0] as GooseDescriptionEntity;

                    // Блокирование подписок на самого себя 
                    if (rowEntity.ParientDevice?.DeviceGuid == colDevGuid)
                    {
                        table.Rows[rowIndex][colIndex] = new SubscriptionValue(null, false);
                        continue;
                    }

                    if (gooseInputModelInfoEntites == null)
                    {
                        table.Rows[rowIndex][colIndex] = new SubscriptionValue(false);
                        continue;
                    }

                    // Чеки для устройств которых нету в проекте
                    if (rowEntity.ParientDevice == null)
                    {
                        if (gooseInputModelInfoEntites.Any(
                            el => el.ModelElementCompareTo(rowEntity.GooseInputModelInfo)))
                        {
                            table.Rows[rowIndex][colIndex] = new SubscriptionValue(true);
                        }
                        else
                        {
                            table.Rows[rowIndex][colIndex] = new SubscriptionValue(null, false);
                        }
                        continue;
                    }

                    // Чеки для устройств которые есть в проекте
                    if (gooseInputModelInfoEntites.Any(
                        el => el.ModelElementCompareTo(rowEntity.GooseInputModelInfo)))
                    {
                        table.Rows[rowIndex][colIndex] = new SubscriptionValue(true);
                    }
                    else
                    {
                        table.Rows[rowIndex][colIndex] = new SubscriptionValue(false);

                    }
                }
            }

            return table;
        }
    }
}