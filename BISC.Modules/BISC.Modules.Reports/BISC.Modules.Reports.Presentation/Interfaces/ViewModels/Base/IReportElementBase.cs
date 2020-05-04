using BISC.Model.Infrastructure.Elements;
using BISC.Presentation.Infrastructure.ChangeTracker;

namespace BISC.Modules.Reports.Presentation.Interfaces.ViewModels.Base
{
    public interface IReportElementBase <T> : IObjectWithChangeTracker where T : IModelElement
    {
        T Model { get; set; }
        void ActivateElement();
        T GetUpdatedModel();
        void UpdateViewModel();
    }
}
