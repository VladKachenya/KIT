using BISC.Model.Infrastructure.Elements;
using BISC.Presentation.Infrastructure.ChangeTracker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BISC.Modules.Reports.Infrastructure.Presentation.ViewModels.Base
{
    public interface IReportElementBase <T> : IObjectWithChangeTracker where T : IModelElement
    {
        T Model { get; set; }
        void ActivateElement();
        T GetUpdatedModel();
        void UpdateViewModel();
    }
}
