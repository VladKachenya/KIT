using System.Collections.Generic;
using System.Collections.ObjectModel;
using BISC.Modules.DataSets.Presentation.HelperEntites;

namespace BISC.Modules.DataSets.Infrastructure.ViewModels.Base
{
    public interface IFunctionalConstrainter
    {
        void SetFcCollection(List<FcHelperEntity> fcHelperEntities);
        ObservableCollection<FcHelperEntity> FcCollection { get; }
        FcHelperEntity SellectedFc { get; set; }

    }
}