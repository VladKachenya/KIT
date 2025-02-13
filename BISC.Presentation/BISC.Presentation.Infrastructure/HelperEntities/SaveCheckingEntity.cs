﻿using System;
using BISC.Presentation.Infrastructure.ChangeTracker;
using BISC.Presentation.Infrastructure.Commands;

namespace BISC.Presentation.Infrastructure.HelperEntities
{
    public class SaveCheckingEntity
    {
        public SaveCheckingEntity(IChangeTracker changeTracker, string entityFriendlyName,ISavingCommand savingCommand, Guid deviceGuid, string regionName=null)
        {

            ChangeTracker = changeTracker;
            EntityFriendlyName = entityFriendlyName;
            SavingCommand = savingCommand;
            DeviceGuid = deviceGuid;
            RegionName = regionName;
        }

        public static string NavigationKey = "SaveCheckingEntity";
        public IChangeTracker ChangeTracker { get; }
        public string EntityFriendlyName { get; }
        public ISavingCommand SavingCommand { get; }
        public string RegionName { get; }
        public Guid DeviceGuid { get; }
    }
}