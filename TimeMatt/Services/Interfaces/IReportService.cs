using TimeMatt.ViewModels;

namespace TimeMatt.Services;

public interface IReportService
{
    ReportExportViewModel BuildReport();
    string BuildCsv(ReportExportViewModel report);
}
