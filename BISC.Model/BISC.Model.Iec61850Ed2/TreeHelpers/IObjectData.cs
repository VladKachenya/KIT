using System.Collections.Generic;
using BISC.Model.Iec61850Ed2.SclModelTemplates;

namespace BISC.Model.Iec61850Ed2.TreeHelpers
{
    public interface IObjectData
    {
        string name { get; set; }
        List<tDAI> DAI { get; set; }
        List<tSDI> SDI { get; set; }
       string FC { get; set; }
    }
}