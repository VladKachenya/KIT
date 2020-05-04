using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Presentation.Interfaces.ViewModels.Base;
using BISC.Presentation.Infrastructure.ChangeTracker;
using BISC.Presentation.Infrastructure.ViewModel;

namespace BISC.Modules.Reports.Presentation.Interfaces.ViewModels
{
    public interface IReportControlViewModel : IReportElementBase<IReportControl>,IObjectWithChangeTracker,IEditableViewModel
    {
        string ParentLdName { get;}
        string ParentLnName { get;}
        bool IsChenged { get; set; }
        string ElementName { get; }
        Brush TypeColorBrush { get; }
        string Name { get; set; }
        int ConfigurationRevision { get; set; }
        string ReportID { get;}
        bool IsBuffered { get; set; }
        int BufferTime { get; set; }
        string SelectidDataSetName { get; set; }
        List<string> AvailableDatasets { get; set; }
        bool IsDynamic { get; }
        int IntegrutyPeriod { get; set; }

        ICommand UndoChengestCommand { get; }
        ICommand UpdateAvailableDatasetsCommand { get; }
        IReportEnabledViewModel ReportEnabledViewModel { get; }
        ITriggerOptionsViewModel TriggerOptionsViewModel { get; }
        IOprionalFildsViewModel OprionalFildsViewModel { get; }
        bool GiBool { get; set; }
        void SetParentLDevice(ILDevice lDevice);

    }
}
