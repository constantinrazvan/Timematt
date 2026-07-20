using TimeMatt.Models;

namespace TimeMatt.ViewModels;

public class ReportExportViewModel
{
    public DateTime GeneratedAt { get; set; }
    public int ActiveProjects { get; set; }
    public int TotalClients { get; set; }
    public decimal EstimatedRevenue { get; set; }
    public int PendingTasks { get; set; }
    public double HoursThisMonth { get; set; }

    public List<ClientReportRow> Clients { get; set; } = new();
    public List<ProjectReportRow> Projects { get; set; } = new();
    public List<TaskReportRow> Tasks { get; set; } = new();
}

public class ClientReportRow
{
    public string Name { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int ActiveProjects { get; set; }
    public double TotalHours { get; set; }
}

public class ProjectReportRow
{
    public string Name { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public ProjectStatus Status { get; set; }
    public PaymentType PaymentType { get; set; }
    public decimal Budget { get; set; }
    public decimal HourlyRate { get; set; }
    public int Progress { get; set; }
    public double HoursWorked { get; set; }
    public decimal RevenueToDate { get; set; }
    public DateTime Deadline { get; set; }
}

public class TaskReportRow
{
    public string Title { get; set; } = string.Empty;
    public string ProjectName { get; set; } = string.Empty;
    public TaskColumn Column { get; set; }
    public TaskPriority Priority { get; set; }
    public double EstimatedHours { get; set; }
    public double WorkedHours { get; set; }
    public DateTime Deadline { get; set; }
}
