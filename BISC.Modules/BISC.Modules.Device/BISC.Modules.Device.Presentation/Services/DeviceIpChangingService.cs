using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Device.Presentation.Interfaces.Services;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Device.Presentation.Services
{
    public class DeviceIpChangingService : IDeviceIpChangingService
    {
        private readonly ITreeManagementService _treeManagementService;
        private readonly IDeviceIdentificationService _deviceIdentificationService;
        private readonly ITabManagementService _tabManagementService;
        private readonly IDeviceAddingService _deviceAddingService;

        public DeviceIpChangingService(ITreeManagementService treeManagementService, 
            IDeviceIdentificationService deviceIdentificationService, 
            ITabManagementService tabManagementService, 
            IDeviceAddingService deviceAddingService)
        {
            _treeManagementService = treeManagementService;
            _deviceIdentificationService = deviceIdentificationService;
            _tabManagementService = tabManagementService;
            _deviceAddingService = deviceAddingService;
        }

        public bool ChengeDeviceIp(IDevice device, string newIp, UiEntityIdentifier uiEntityIdToUpdate)
        {
            _deviceIdentificationService.ChengeDeviceIp(device, newIp);

            _tabManagementService.CloseTabWithChildren(uiEntityIdToUpdate.ItemId.ToString());

            _treeManagementService.DeleteTreeItem(uiEntityIdToUpdate);

            _deviceAddingService.AddDeviceToTree(device);

            return true;
        }
    }
}