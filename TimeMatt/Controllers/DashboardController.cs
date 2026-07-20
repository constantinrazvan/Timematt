using Microsoft.AspNetCore.Mvc;
using TimeMatt.Services;
using TimeMatt.ViewModels;

namespace TimeMatt.Controllers;

public class DashboardController : Controller
{
    private readonly DashboardService _dashboardService;

    public DashboardController(DashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    public IActionResult Index()
    {
        var weeklyHours = _dashboardService.GetWeeklyHoursChartData();
        var revenueByProject = _dashboardService.GetRevenueByProject();

        var vm = new DashboardViewModel
        {
            ActiveProjects = _dashboardService.GetActiveProjectsCount(),
            TotalClients = _dashboardService.GetTotalClientsCount(),
            HoursToday = _dashboardService.GetHoursToday(),
            HoursThisWeek = _dashboardService.GetHoursThisWeek(),
            EstimatedRevenue = _dashboardService.GetEstimatedRevenue(),
            PendingTasks = _dashboardService.GetPendingTasksCount(),

            WeekDayLabels = weeklyHours.Keys.Select(d => d.ToString().Substring(0, 3)).ToList(),
            WeekDayHours = weeklyHours.Values.ToList(),

            RevenueProjectLabels = revenueByProject.Select(r => r.Project.Name).ToList(),
            RevenueProjectValues = revenueByProject.Select(r => r.Revenue).ToList(),

            RecentActivity = _dashboardService.GetRecentActivity(),

            UpcomingDeadlines = _dashboardService.GetUpcomingDeadlines().Select(p => new DeadlineItemViewModel
            {
                ProjectId = p.Id,
                ProjectName = p.Name,
                ClientName = _dashboardService.GetClient(p.ClientId)?.Company ?? "",
                Deadline = p.Deadline,
                DaysLeft = (p.Deadline.Date - DateTime.Today).Days
            }).ToList(),

            RecentProjects = _dashboardService.GetRecentProjects().Select(p => new ProjectSummaryViewModel
            {
                Id = p.Id,
                Name = p.Name,
                ClientName = _dashboardService.GetClient(p.ClientId)?.Company ?? "",
                Status = p.Status,
                Progress = p.Progress,
                Color = p.Color
            }).ToList()
        };

        return View(vm);
    }
}
