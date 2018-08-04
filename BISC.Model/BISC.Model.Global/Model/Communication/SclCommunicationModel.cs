using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Project.Communication;

namespace BISC.Model.Global.Model.Communication
{
    public class SclCommunicationModel:ModelElement,ISclCommunicationModel
    {
        public SclCommunicationModel()
        {
            SubNetworks=new List<ISubNetwork>();
        }
        public List<ISubNetwork> SubNetworks { get; }
    }
}
