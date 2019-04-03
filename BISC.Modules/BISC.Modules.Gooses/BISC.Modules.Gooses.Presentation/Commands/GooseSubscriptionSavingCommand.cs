using BISC.Infrastructure.Global.Common;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix.Entities;
using BISC.Modules.Gooses.Presentation.ViewModels.Subscriptions;
using BISC.Presentation.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Model.Model;

namespace BISC.Modules.Gooses.Presentation.Commands
{
    public class GooseSubscriptionSavingCommand : ISavingCommand
    {
        private readonly IDeviceModelService _deviceModelService;
        private readonly IBiscProject _biscProject;
        private readonly IGoosesModelService _goosesModelService;
        private readonly IGooseSavingService _gooseSavingService;
        private DataTable _gooseSubscriptionTable;
        private List<GooseDescriptionEntity> _gooseDescriptionEntities;

        public GooseSubscriptionSavingCommand(IDeviceModelService deviceModelService, IBiscProject biscProject,
            IGoosesModelService goosesModelService, IGooseSavingService gooseSavingService)
        {
            _deviceModelService = deviceModelService;
            _biscProject = biscProject;
            _goosesModelService = goosesModelService;
            _gooseSavingService = gooseSavingService;
        }

        public void Initialize(DataTable gooseSubscriptionTable)
        {
            _gooseSubscriptionTable = gooseSubscriptionTable;
        }

        #region Implementation of ISavingCommand

        
        public async Task<OperationResult<SavingCommandResultEnum>> SaveAsync()
        {
            var table = _gooseSubscriptionTable.DefaultView.ToTable();
            for (int colimnIndex = 1; colimnIndex < table.Columns.Count; colimnIndex++)
            {
                var deviceId = table.Columns[colimnIndex].Caption;
                // Тут необходимо делать поиск по Guid
                var device = _deviceModelService.GetDeviceByGuid(_biscProject.MainSclModel.Value, Guid.Parse(deviceId));
                List<IGooseInputModelInfo> takenGooses = new List<IGooseInputModelInfo>();
                for (int rowIndex = 0; rowIndex < table.Rows.Count; rowIndex++)
                {
                    var rowValuew = table.Rows[rowIndex].ItemArray[colimnIndex] as SubscriptionValue;
                    
                    if (rowValuew.IsValueEditable && rowValuew.IsSelected.HasValue && (bool)rowValuew.IsSelected)
                    {
                        var helperEntity = table.Rows[rowIndex][0] as GooseDescriptionEntity;
                        var typle = helperEntity.GooseInputModelInfo;
                        takenGooses.Add(typle);
                    }
                }
                await _gooseSavingService.SaveSubscriptionGooces(device, takenGooses);
            }
            return new OperationResult<SavingCommandResultEnum>(SavingCommandResultEnum.SavedOk);
        }


        public Task<bool> IsSavingByFtpNeeded()
        {
            return Task.FromResult(false);
        }

        public Task<OperationResult> ValidateBeforeSave()
        {
            return Task.FromResult(OperationResult.SucceedResult);
        }

        #endregion
    }
}
