using BISC.Model.Infrastructure.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Reports.Infrastructure.Model
{
    public interface IOptFields : IModelElement
    {
        bool SeqNum { get; set; }
        bool TimeStamp { get; set; }
        bool DataSet { get; set; }
        bool ReasonCode { get; set; }
        bool DataRef { get; set; }
        bool EntryID { get; set; }
        bool ConfigRef { get; set; }
        bool BufOvfl { get; set; }
        bool Segmentation { get; set; }
    }
}
