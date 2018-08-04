using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Model.Infrastructure.Project.Communication
{
   public interface IConnectedAccessPoint : IModelElement
    {
        string IedName { get; set; }
        string ApName { get; set; }
        List<ISclAddress> SclAddresses { get; }
        List<IGse> GseList { get; }
    }
}
