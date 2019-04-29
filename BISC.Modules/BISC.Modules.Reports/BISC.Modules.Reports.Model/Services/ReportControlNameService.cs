using BISC.Modules.Reports.Infrastructure.Services;

namespace BISC.Modules.Reports.Model.Services
{
    public class ReportControlNameService : IReportControlNameService
    {
        private const string BRCB_ = nameof(BRCB_);
        private const string URCB_ = nameof(URCB_);

        private readonly char[] _nambers = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'};
        //public bool GetIsDynamic(string dataSetName) => dataSetName.Trim(_nambers) != "DS";

        public bool GetIsDynamic(string reportControlName)
        {
            var nameBody = reportControlName.Trim(_nambers);
            return nameBody != BRCB_ && nameBody != URCB_;
        }
    }
}