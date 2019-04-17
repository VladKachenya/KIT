using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;

namespace BISC.Modules.Gooses.Infrastructure.Services
{
    public interface IGoosesModelService
    {
        void AddGseControl(string lnName, string ldName, IModelElement devcice, IGooseControl gooseControl);
        List<IGooseControl> GetGooseControlsOfDevice(IDevice device);
        void DeleteAllDeviceReferencesInGooseControlsInModel(IBiscProject biscProject, string iedName);
        
        IGooseDeviceInput GetGooseDeviceInputOfProject(IBiscProject biscProject, IDevice device);
        void SetGooseInputModelInfosToProject(IBiscProject biscProject, IDevice device, List<IGooseInputModelInfo> inputs);

        List<IGooseInputModelInfo> GetGooseInputModelInfos(IDevice device, IBiscProject biscProject = null);

        void DeleteGooseCbAndGseByName(string name, IDevice device);

        void DeleteAllGoosesFromDevice(IDevice device);

    }
}