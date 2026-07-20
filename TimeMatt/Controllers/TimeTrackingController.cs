using Microsoft.AspNetCore.Mvc;
using TimeMatt.Models;
using TimeMatt.Services;
using TimeMatt.ViewModels;

namespace TimeMatt.Controllers;

public class TimeTrackingController : Controller
{
    private readonly TimeTrackingService _timeTrackingService;
    private readonly ProjectService _projectService;

    public TimeTrackingController(TimeTrackingService timeTrackingService, ProjectService projectService)
    {
        _timeTrackingService = timeTrackingService;
        _projectService = projectService;
    }

    public IActionResult Index()
    {
        var weeklyHours = _timeTrackingService.GetHoursByDayThisWeek();

        var vm = new TimeTrackingViewModel
        {
            ActiveProjects = _projectService.GetAll().Where(p => p.Status == ProjectStatus.Active).ToList(),
            TodaySessions = _timeTrackingService.GetToday().Select(e =>
            {
                var project = _projectService.GetById(e.ProjectId);
                return new TimeSessionViewModel
                {
                    Id = e.Id,
                    ProjectName = project?.Name ?? "",
                    Color = project?.Color ?? "#6366f1",
                    Description = e.Description,
                    Hours = e.Hours,
                    Billable = e.Billable
                };
            }).ToList(),
            HoursToday = _timeTrackingService.GetHoursToday(),
            HoursThisWeek = _timeTrackingService.GetHoursThisWeek(),
            HoursThisMonth = _timeTrackingService.GetHoursThisMonth(),
            WeekDayLabels = weeklyHours.Keys.Select(d => d.ToString().Substring(0, 3)).ToList(),
            WeekDayHours = weeklyHours.Values.ToList()
        };

        return View(vm);
    }
}
