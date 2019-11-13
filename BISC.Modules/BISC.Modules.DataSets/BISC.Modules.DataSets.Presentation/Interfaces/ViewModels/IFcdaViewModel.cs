using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.ViewModels.Base;

namespace BISC.Modules.DataSets.Presentation.Interfaces.ViewModels
{
    public interface IFcdaViewModel: IDataSetElementBaseViewModel<IFcda>, IFunctionalConstrainter
    {
        IFcda GetFcda();
        string FullName { get; }
        IWeigher ParentWeiger { get; set; }
    }
}
