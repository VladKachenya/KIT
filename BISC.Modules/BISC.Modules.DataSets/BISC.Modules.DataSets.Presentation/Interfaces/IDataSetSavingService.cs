using System.Collections.Generic;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.ViewModels;

namespace BISC.Modules.DataSets.Presentation.Services.Interfaces
{
    public interface IDataSetSavingService
    {
        Task SaveDataSets(List<IDataSetViewModel> dataSetsToSave, IModelElement device,bool isSavingInDevice);
    }
}