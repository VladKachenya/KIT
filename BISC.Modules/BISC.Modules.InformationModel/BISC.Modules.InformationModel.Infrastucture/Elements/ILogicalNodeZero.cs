﻿using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.InformationModel.Infrastucture.Elements
{
    public interface ILogicalNodeZero:ILogicalNode
    {
         ChildModelProperty<ISettingControl> SettingControl { get; }

    }
}