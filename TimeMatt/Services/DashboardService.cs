using TimeMatt.Models;

namespace TimeMatt.Services;

public class DashboardService
{
    private readonly ClientService _clientService;
    private readonly ProjectService _projectService;
    private readonly TaskService _taskService;
    private readonly TimeTrackingService _timeTrackingService;

    private readonly List<ActivityItem> _activity;

    public DashboardService(ClientService clientService, ProjectService projectService, TaskService taskService, TimeTrackingService timeTrackingService)
    {
        _clientService = clientService;
        _projectService = projectService;
        _taskService = taskService;
        _timeTrackingService = timeTrackingService;

        var today = DateTime.Today;
        _activity = new List<ActivityItem>
        {
            new() { Id = 1, Icon = "bi-check2-circle", IconColor = "success", Text = "Marked \"Payment Gateway Testing\" as Done on E-commerce Redesign", Timestamp = today.AddHours(-2) },
            new() { Id = 2, Icon = "bi-chat-left-text", IconColor = "primary", Text = "Sarah Bennett commented on Corporate Website", Timestamp = today.AddHours(-6) },
            new() { Id = 3, Icon = "bi-stopwatch", IconColor = "warning", Text = "Logged 1.75h on CRM Platform - Dashboard widgets", Timestamp = today.AddHours(-8) },
            new() { Id = 4, Icon = "bi-file-earmark-arrow-up", IconColor = "info", Text = "Uploaded Checkout-Flow.fig to E-commerce Redesign", Timestamp = today.AddDays(-1).AddHours(-3) },
            new() { Id = 5, Icon = "bi-person-plus", IconColor = "success", Text = "Added Priya Nair as a new client", Timestamp = today.AddDays(-2).AddHours(-1) },
            new() { Id = 6, Icon = "bi-kanban", IconColor = "primary", Text = "Moved \"Dashboard\" to In Progress on CRM Platform", Timestamp = today.AddDays(-2).AddHours(-5) },
        };
    }

    public int GetActiveProjectsCount() => _projectService.GetAll().Count(p => p.Status == ProjectStatus.Active);

    public int GetTotalClientsCount() => _clientService.GetAll().Count;

    public double GetHoursToday() => _timeTrackingService.GetHoursToday();

    public double GetHoursThisWeek() => _timeTrackingService.GetHoursThisWeek();

    public decimal GetEstimatedRevenue() =>
        _projectService.GetAll()
            .Where(p => p.Status != ProjectStatus.Archived)
            .Sum(p => p.RevenueToDate);

    public int GetPendingTasksCount() => _taskService.GetPending().Count;

    public Dictionary<DayOfWeek, double> GetWeeklyHoursChartData() => _timeTrackingService.GetHoursByDayThisWeek();

    public List<(Project Project, decimal Revenue)> GetRevenueByProject() =>
        _projectService.GetAll()
            .Where(p => p.Status != ProjectStatus.Archived && p.Status != ProjectStatus.Draft)
            .Select(p => (p, p.RevenueToDate))
            .OrderByDescending(x => x.Item2)
            .Take(6)
            .ToList();

    public List<ActivityItem> GetRecentActivity() => _activity.OrderByDescending(a => a.Timestamp).Take(6).ToList();

    public List<Project> GetUpcomingDeadlines() =>
        _projectService.GetAll()
            .Where(p => p.Status != ProjectStatus.Completed && p.Status != ProjectStatus.Archived && p.Deadline >= DateTime.Today)
            .OrderBy(p => p.Deadline)
            .Take(5)
            .ToList();

    public List<Project> GetRecentProjects() =>
        _projectService.GetAll().OrderByDescending(p => p.StartDate).Take(5).ToList();

    public Client? GetClient(int id) => _clientService.GetById(id);
}
