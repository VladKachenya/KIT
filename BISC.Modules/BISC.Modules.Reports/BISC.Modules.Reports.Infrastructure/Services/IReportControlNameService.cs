namespace BISC.Modules.Reports.Infrastructure.Services
{
    public interface IReportControlNameService
    {
        bool GetIsDynamic(string reportControlName);
    }
}