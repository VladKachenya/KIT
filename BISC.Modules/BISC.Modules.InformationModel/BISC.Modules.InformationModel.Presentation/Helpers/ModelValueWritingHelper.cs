using System.Collections.Generic;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Modules.InformationModel.Model.Elements;
using BISC.Modules.InformationModel.Presentation.Interfaces;
using BISC.Modules.InformationModel.Presentation.ViewModels.InfoModelTree;

namespace BISC.Modules.InformationModel.Presentation.Helpers
{
    public class ModelValueWritingHelper
    {
        private readonly ILoggingService _loggingService;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IInfoModelService _infoModelService;

        public ModelValueWritingHelper(
            ILoggingService loggingService,
            IConnectionPoolService connectionPoolService,
            IInfoModelService infoModelService)
        {
            _loggingService = loggingService;
            _connectionPoolService = connectionPoolService;
            _infoModelService = infoModelService;
        }

        public async Task WriteValue(IInfoModelItemViewModel treeItem, IDevice device)
        {
            var modelToUpdate = treeItem.Model as IDai;
            var viewModelToUpdate = treeItem as DaiInfoModelItemViewModel;
            if (modelToUpdate == null)
            {
                _loggingService.LogMessage($"Тип {treeItem.Model.GetType()} не является {typeof(IDai)} \nЗначение не может быть записанно", SeverityEnum.Warning);
                return;
            }

            if (viewModelToUpdate == null)
            {
                _loggingService.LogMessage($"Тип {treeItem.GetType()} не является {typeof(DaiInfoModelItemViewModel)} \nЗначение не может быть записанно", SeverityEnum.Warning);
                return;
            }

            var path = new List<string>();
            var model = modelToUpdate as IModelElement;
            while (!(model is ILogicalNode))
            {
                if (model is INameable name)
                {
                    path.Add(name.Name);
                }
                model = model.ParentModelElement;
            }
            path.Reverse();

            ILogicalNode logicalNode = model as ILogicalNode;
            ILDevice logicalDevice = logicalNode.ParentModelElement as ILDevice;


            var lnSignature = logicalNode.LnClass + logicalNode.Inst;

            var ldSignature = logicalDevice.Inst;

            if (_connectionPoolService.GetConnection(device.Ip).IsConnected)
            {
                await _connectionPoolService.GetConnection(device.Ip).MmsConnection.WriteDaiValueAsync("CF",
                    device.Name,
                    lnSignature, ldSignature, path, viewModelToUpdate.Value);
            }

            modelToUpdate.Value.Value = new Val();
            modelToUpdate.Value.Value.Value = viewModelToUpdate.Value;


            viewModelToUpdate.CheckValue();
        }
    }
}