using System.Globalization;
using System.Text;
using TimeMatt.Models;
using TimeMatt.ViewModels;

namespace TimeMatt.Services;

public class ReportService : IReportService
{
    private readonly IClientService _clientService;
    private readonly IProjectService _projectService;
    private readonly ITaskService _taskService;
    private readonly ITimeTrackingService _timeTrackingService;
    private readonly IDashboardService _dashboardService;

    public ReportService(
        IClientService clientService,
        IProjectService projectService,
        ITaskService taskService,
        ITimeTrackingService timeTrackingService,
        IDashboardService dashboardService)
    {
        _clientService = clientService;
        _projectService = projectService;
        _taskService = taskService;
        _timeTrackingService = timeTrackingService;
        _dashboardService = dashboardService;
    }

    public ReportExportViewModel BuildReport()
    {
        var clientRows = _clientService.GetAll().Select(c =>
        {
            var projects = _projectService.GetByClientId(c.Id);
            return new ClientReportRow
            {
                Name = c.Name,
                Company = c.Company,
                Email = c.Email,
                ActiveProjects = projects.Count(p => p.Status == ProjectStatus.Active),
                TotalHours = projects.Sum(p => p.HoursWorked)
            };
        }).ToList();

        var projectRows = _projectService.GetAll().Select(p => new ProjectReportRow
        {
            Name = p.Name,
            ClientName = _clientService.GetById(p.ClientId)?.Company ?? "",
            Status = p.Status,
            PaymentType = p.PaymentType,
            Budget = p.Budget,
            HourlyRate = p.HourlyRate,
            Progress = _taskService.GetProgressForProject(p.Id, p.Progress),
            HoursWorked = p.HoursWorked,
            RevenueToDate = p.RevenueToDate,
            Deadline = p.Deadline
        }).ToList();

        var taskRows = _taskService.GetAll().Select(t => new TaskReportRow
        {
            Title = t.Title,
            ProjectName = _projectService.GetById(t.ProjectId)?.Name ?? "",
            Column = t.Column,
            Priority = t.Priority,
            EstimatedHours = t.EstimatedHours,
            WorkedHours = t.WorkedHours,
            Deadline = t.Deadline
        }).ToList();

        return new ReportExportViewModel
        {
            GeneratedAt = DateTime.Now,
            ActiveProjects = _dashboardService.GetActiveProjectsCount(),
            TotalClients = _dashboardService.GetTotalClientsCount(),
            EstimatedRevenue = _dashboardService.GetEstimatedRevenue(),
            PendingTasks = _dashboardService.GetPendingTasksCount(),
            HoursThisMonth = _timeTrackingService.GetHoursThisMonth(),
            Clients = clientRows,
            Projects = projectRows,
            Tasks = taskRows
        };
    }

    public string BuildCsv(ReportExportViewModel report)
    {
        var sb = new StringBuilder();
        var culture = CultureInfo.InvariantCulture;

        void Row(params string[] values) => sb.AppendLine(string.Join(",", values.Select(Escape)));
        string Escape(string value) => "\"" + (value ?? "").Replace("\"", "\"\"") + "\"";
        string NumD(double value, string format) => value.ToString(format, culture);
        string NumM(decimal value, string format) => value.ToString(format, culture);

        Row("TextMatt Report", $"Generated {report.GeneratedAt:MMM d, yyyy h:mm tt}");
        sb.AppendLine();

        Row("SUMMARY");
        Row("Metric", "Value");
        Row("Active Projects", report.ActiveProjects.ToString());
        Row("Total Clients", report.TotalClients.ToString());
        Row("Estimated Revenue", NumM(report.EstimatedRevenue, "0.##"));
        Row("Pending Tasks", report.PendingTasks.ToString());
        Row("Hours This Month", NumD(report.HoursThisMonth, "0.#"));
        sb.AppendLine();

        Row("CLIENTS");
        Row("Name", "Company", "Email", "Active Projects", "Total Hours");
        foreach (var c in report.Clients)
        {
            Row(c.Name, c.Company, c.Email, c.ActiveProjects.ToString(), NumD(c.TotalHours, "0.#"));
        }
        sb.AppendLine();

        Row("PROJECTS");
        Row("Name", "Client", "Status", "Payment Type", "Budget", "Hourly Rate", "Progress %", "Hours Worked", "Revenue To Date", "Deadline");
        foreach (var p in report.Projects)
        {
            Row(p.Name, p.ClientName, p.Status.ToString(), p.PaymentType.ToString(), NumM(p.Budget, "0.##"), NumM(p.HourlyRate, "0.##"), p.Progress.ToString(), NumD(p.HoursWorked, "0.#"), NumM(p.RevenueToDate, "0.##"), p.Deadline.ToString("yyyy-MM-dd"));
        }
        sb.AppendLine();

        Row("TASKS");
        Row("Title", "Project", "Column", "Priority", "Estimated Hours", "Worked Hours", "Deadline");
        foreach (var t in report.Tasks)
        {
            Row(t.Title, t.ProjectName, t.Column.ToString(), t.Priority.ToString(), NumD(t.EstimatedHours, "0.#"), NumD(t.WorkedHours, "0.#"), t.Deadline.ToString("yyyy-MM-dd"));
        }

        return sb.ToString();
    }
}
