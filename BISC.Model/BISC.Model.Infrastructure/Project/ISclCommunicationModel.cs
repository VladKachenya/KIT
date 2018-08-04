using System.Collections.Generic;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project.Communication;

namespace BISC.Model.Infrastructure.Project
{
    public interface ISclCommunicationModel:IModelElement
    {
        List<ISubNetwork> SubNetworks { get; }
    }
}