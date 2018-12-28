using System.Collections.Generic;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Modules.DataSets.Infrastructure.ViewModels;

namespace BISC.Modules.DataSets.Presentation.Services.Interfaces
{
    public interface IFtpDataSetModelService
    {
        Task<OperationResult> WriteDatasetsToDevice(string ip, List<IDataSetViewModel> dataSetsToSave);

    }
}