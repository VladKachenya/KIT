using BISC.Model.Infrastructure.Keys;
using BISC.Model.Infrastructure.Project;

namespace BISC.Model.Global.Model
{
    public class SclModel: ModelElement,ISclModel
    {
        public SclModel()
        {
            ElementName = ModelKeys.SclModelKey;
            Version = "2007";
            Revision = "B";
        }

        public string Version { get; set; }
        public string Revision { get; set; }
    }
}
