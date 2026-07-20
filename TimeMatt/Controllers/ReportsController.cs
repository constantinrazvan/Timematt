using Microsoft.AspNetCore.Mvc;
using TimeMatt.Models;
using TimeMatt.Services;
using TimeMatt.ViewModels;

namespace TimeMatt.Controllers;

public class ReportsController : Controller
{
    private readonly ProjectService _projectService;
    private readonly TimeTrackingService _timeTrackingService;

    public ReportsController(ProjectService projectService, TimeTrackingService timeTrackingService)
    {
        _projectService = projectService;
        _timeTrackingService = timeTrackingService;
    }

    public IActionResult Index()
    {
        var hoursByProject = _timeTrackingService.GetHoursByProject();
        var projects = _projectService.GetAll()
            .Where(p => hoursByProject.ContainsKey(p.Id))
            .OrderByDescending(p => hoursByProject[p.Id])
            .ToList();

        var weeklyHours = _timeTrackingService.GetHoursByDayThisWeek();
        var monthEntries = _timeTrackingService.GetThisMonth();

        var weekGroups = monthEntries
            .GroupBy(e => (e.Date.Day - 1) / 7)
            .OrderBy(g => g.Key)
            .Select((g, i) => (Label: $"Week {i + 1}", Hours: g.Sum(e => e.Hours)))
            .ToList();

        var vm = new ReportsViewModel
        {
            ProjectLabels = projects.Select(p => p.Name).ToList(),
            HoursPerProject = projects.Select(p => hoursByProject[p.Id]).ToList(),
            RevenuePerProject = projects.Select(p => p.RevenueToDate).ToList(),

            WeekDayLabels = weeklyHours.Keys.Select(d => d.ToString().Substring(0, 3)).ToList(),
            HoursThisWeek = weeklyHours.Values.ToList(),

            MonthWeekLabels = weekGroups.Select(w => w.Label).ToList(),
            HoursThisMonth = weekGroups.Select(w => w.Hours).ToList(),

            TotalHoursThisMonth = monthEntries.Sum(e => e.Hours),
            TotalRevenueThisMonth = projects.Sum(p => p.RevenueToDate)
        };

        return View(vm);
    }
}
