using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Project.Communication;

namespace BISC.Model.Infrastructure.Services.Communication
{
    public interface ISclCommunicationModelService
    {
        void AddDefaultConnectedAccessPointForDevice(ISclModel sclModel,string deviceName,string ip);
        void AddConnectedAccessPoint(ISclModel sclModel,IConnectedAccessPoint connectedAccessPoint);
        IConnectedAccessPoint GetConnectedAccessPoint(ISclModel sclModel, string deviceName);
        void DeleteAccessPoint(ISclModel sclModel, string iedName);
        void AddGse(IGse gse, ISclModel sclModel,string iedName);
    }
}