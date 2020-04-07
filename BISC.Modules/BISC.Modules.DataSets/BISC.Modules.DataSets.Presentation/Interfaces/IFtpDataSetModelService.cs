using System.Collections.Generic;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.ViewModels;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.DataSets.Presentation.Services.Interfaces
{
    public interface IFtpDataSetModelService
    {
        Task<OperationResult> WriteDataSetsToDevice(IDevice device, IEnumerable<IDataSet> dataSetsToSave);
    }
}