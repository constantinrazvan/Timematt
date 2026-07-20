using TimeMatt.Models;

namespace TimeMatt.ViewModels;

public class DashboardViewModel
{
    public int ActiveProjects { get; set; }
    public int TotalClients { get; set; }
    public double HoursToday { get; set; }
    public double HoursThisWeek { get; set; }
    public decimal EstimatedRevenue { get; set; }
    public int PendingTasks { get; set; }

    public List<string> WeekDayLabels { get; set; } = new();
    public List<double> WeekDayHours { get; set; } = new();

    public List<string> RevenueProjectLabels { get; set; } = new();
    public List<decimal> RevenueProjectValues { get; set; } = new();

    public List<ActivityItem> RecentActivity { get; set; } = new();
    public List<DeadlineItemViewModel> UpcomingDeadlines { get; set; } = new();
    public List<ProjectSummaryViewModel> RecentProjects { get; set; } = new();
}

public class DeadlineItemViewModel
{
    public int ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public DateTime Deadline { get; set; }
    public int DaysLeft { get; set; }
}

public class ProjectSummaryViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public ProjectStatus Status { get; set; }
    public int Progress { get; set; }
    public string Color { get; set; } = "#6366f1";
}
