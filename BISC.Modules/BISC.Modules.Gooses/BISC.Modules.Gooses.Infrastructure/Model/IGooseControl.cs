﻿using System.Collections.Generic;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.Gooses.Infrastructure.Model
{
    public interface IGooseControl:IModelElement
    {
        string Name { get; set; }
        string DataSet { get; set; }
        int ConfRev { get; set; }
        string AppId { get; set; }
        //bool IsDynamic { get; }
        bool FixedOffs { get; set; }
        string GooseType { get; set; }
        //ChildModelsList<ISubscriberDevice> SubscriberDevice { get;}
    }
}