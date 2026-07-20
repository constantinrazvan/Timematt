using TimeMatt.Models;

namespace TimeMatt.Services;

public interface IDashboardService
{
    int GetActiveProjectsCount();
    int GetTotalClientsCount();
    decimal GetEstimatedRevenue();
    int GetPendingTasksCount();
    List<(string Label, double Hours)> GetWeeklyHoursChartData();
    List<(Project Project, decimal Revenue)> GetRevenueByProject();
    List<ActivityItem> GetRecentActivity();
    List<Project> GetUpcomingDeadlines();
    List<Project> GetRecentProjects();
    Client? GetClient(int id);
}
