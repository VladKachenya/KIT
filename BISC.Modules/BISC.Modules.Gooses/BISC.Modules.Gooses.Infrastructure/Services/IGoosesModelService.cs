using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Gooses.Infrastructure.Model;

namespace BISC.Modules.Gooses.Infrastructure.Services
{
    public interface IGoosesModelService
    {
        void AddGseControl(string lnName, string ldName, IModelElement devcice, IGooseControl gooseControl);
    }
}