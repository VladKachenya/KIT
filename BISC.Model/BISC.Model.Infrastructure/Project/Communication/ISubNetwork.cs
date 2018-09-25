using System.Collections.Generic;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Model.Infrastructure.Project.Communication
{
    public interface ISubNetwork : IModelElement
    {
        string Name { get; set; }
        string Desc { get; set; }
        string Type { get; set; }
        ChildModelsList<IConnectedAccessPoint> ConnectedAccessPoints { get; }
    }
}