using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model.Communication;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Project.Communication;
using BISC.Model.Infrastructure.Services.Communication;

namespace BISC.Model.Global.Services.CommunicationModel
{
    public class SclCommunicationModelService : ISclCommunicationModelService
    {
        private string GetNextApName(List<string> existingApNames)
        {
            string nextApName = String.Empty;
            string apPart = "AP";
            var count = 0;
            do
            {
                if (existingApNames.All(s => s != apPart + count.ToString()))
                {
                    nextApName = apPart + count.ToString();
                }

                count++;
            } while (string.IsNullOrEmpty(nextApName));

            return nextApName;
        }

        public void AddDefaultConnectedAccessPointForDevice(ISclModel sclModel, string deviceName, string ip)
        {
            IConnectedAccessPoint connectedAccessPoint = new ConnectedAccessPoint();
            List<IConnectedAccessPoint> connectedAccessPoints = new List<IConnectedAccessPoint>();
            sclModel.GetAllChildrenOfType(ref connectedAccessPoints);
            connectedAccessPoint.ApName = GetNextApName(connectedAccessPoints.Select((point => point.ApName)).ToList());
            connectedAccessPoint.IedName = deviceName;
            SclAddress sclAddress = new SclAddress();
            sclAddress.SetProperty("IP", ip);
            sclAddress.SetProperty("OSI-AE-Qualifier", "23");
            sclAddress.SetProperty("OSI-AP-Title", "1,3,9999,23");
            sclAddress.SetProperty("OSI-PSEL", "00000001");
            sclAddress.SetProperty("OSI-SSEL", "0001");
            sclAddress.SetProperty("IP-SUBNET", "255.255.255.0");
            sclAddress.SetProperty("IP-GATEWAY", ip);
            connectedAccessPoint.SclAddresses.Add(sclAddress);
            AddCommunicationToSclIfItNeeded(sclModel);
            (sclModel.ChildModelElements.First((element => element is ISclCommunicationModel)) as
                ISclCommunicationModel)?.SubNetworks[0].ConnectedAccessPoints.Add(connectedAccessPoint);
        }

        private void AddSubnetworkIfNeeded(ISclCommunicationModel sclCommunicationModel)
        {
            if (!sclCommunicationModel.SubNetworks.Any())
            {
                sclCommunicationModel.SubNetworks.Add(new SubNetwork() {Name = "WA1", Type = "8-MMS"});
            }
        }



        private void AddCommunicationToSclIfItNeeded(ISclModel sclModel)
        {
            var communticationModel =
                sclModel.ChildModelElements.FirstOrDefault((element => element is ISclCommunicationModel)) as
                    ISclCommunicationModel;
            if (communticationModel == null)
            {
                communticationModel = new SclCommunicationModel();
                sclModel.ChildModelElements.Add(communticationModel);
            }

            AddSubnetworkIfNeeded(communticationModel);

        }


        public void AddConnectedAccessPoint(ISclModel sclModel, IConnectedAccessPoint connectedAccessPoint)
        {
            AddCommunicationToSclIfItNeeded(sclModel);
            (sclModel.ChildModelElements.First((element => element is ISclCommunicationModel)) as
                ISclCommunicationModel)?.SubNetworks[0].ConnectedAccessPoints.Add(connectedAccessPoint);
        }

        public IConnectedAccessPoint GetConnectedAccessPoint(ISclModel sclModel, string deviceName)
        {
            return (sclModel.ChildModelElements.First((element => element is ISclCommunicationModel)) as
                    ISclCommunicationModel)?.SubNetworks[0].ConnectedAccessPoints
                .First((point => point.IedName == deviceName));
        }

        public void DeleteAccessPoint(ISclModel sclModel, string iedName)
        {
            ISclCommunicationModel sclCommunicationModel =
                (sclModel.ChildModelElements.FirstOrDefault((element => (element is ISclCommunicationModel))) as
                    ISclCommunicationModel);
            var connectedAp = sclCommunicationModel?.SubNetworks[0].ConnectedAccessPoints
                .FirstOrDefault((point => point.IedName == iedName));
            if (connectedAp != null)
            {
                sclCommunicationModel.SubNetworks[0].ConnectedAccessPoints.Remove(connectedAp);
            }

        }

        public void AddGse(IGse gse, ISclModel sclModel, string iedName)
        {
            ISclCommunicationModel sclCommunicationModel =
                (sclModel.ChildModelElements.FirstOrDefault((element => (element is ISclCommunicationModel))) as
                    ISclCommunicationModel);
            var connectedAp = sclCommunicationModel?.SubNetworks[0].ConnectedAccessPoints
                .FirstOrDefault((point => point.IedName == iedName));
            if (connectedAp != null)
            {
                connectedAp.GseList.Add(gse);
            }
        }

        public List<IGse> GetGsesForDevice(string deviceName, ISclModel sclModel)
        {
            ISclCommunicationModel sclCommunicationModel =
                (sclModel.ChildModelElements.FirstOrDefault((element => (element is ISclCommunicationModel))) as
                    ISclCommunicationModel);
            var connectedAp = sclCommunicationModel?.SubNetworks[0].ConnectedAccessPoints
                .FirstOrDefault((point => point.IedName == deviceName));
            if (connectedAp != null)
            {
                return connectedAp.GseList.ToList();
            }

            return null;
        }

        public string GetIpOfDevice(string deviceName, ISclModel sclModel)
        {
            IConnectedAccessPoint connectedAccessPoint = GetConnectedAccessPoint(sclModel, deviceName);
            var address = connectedAccessPoint.SclAddresses.FirstOrDefault();
            return address?.GetProperty("IP");
        }
    }
}
