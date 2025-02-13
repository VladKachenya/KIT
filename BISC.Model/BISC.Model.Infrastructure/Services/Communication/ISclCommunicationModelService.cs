﻿using System;
using System.Collections.Generic;
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

        void ReplaceAccessPointIp(ISclModel sclModel, string iedName, string newIp);
        void ReplaceAccessPointIdeName(ISclModel sclModel, string iedName, string newIdeName);

        void AddGse(IGse gse, ISclModel sclModel,string iedName);
        List<IGse> GetGsesForDevice(string deviceName,ISclModel sclModel);
        string GetIpOfDevice(string deviceName, ISclModel sclModel);
        void DeleteGseOfDevice(string deviceName, string cbName, ISclModel sclModel);

    }
}