using Microsoft.AspNetCore.Mvc;
using TimeMatt.Models;
using TimeMatt.Services;
using TimeMatt.ViewModels;

namespace TimeMatt.Controllers;

public class TimeTrackingController : Controller
{
    private readonly ITimeTrackingService _timeTrackingService;
    private readonly IProjectService _projectService;
    private readonly ITaskService _taskService;

    public TimeTrackingController(ITimeTrackingService timeTrackingService, IProjectService projectService, ITaskService taskService)
    {
        _timeTrackingService = timeTrackingService;
        _projectService = projectService;
        _taskService = taskService;
    }

    public IActionResult Index(int? projectId, int? taskId)
    {
        var weeklyHours = _timeTrackingService.GetHoursForLastSevenDays();
        var trackableProjects = _projectService.GetAll()
            .Where(p => p.Status != ProjectStatus.Archived && p.Status != ProjectStatus.Completed)
            .ToList();
        var trackableProjectIds = trackableProjects.Select(p => p.Id).ToHashSet();

        ViewBag.PreselectedProjectId = projectId;
        ViewBag.PreselectedTaskId = taskId;

        var vm = new TimeTrackingViewModel
        {
            TrackableProjects = trackableProjects,
            AllTasks = _taskService.GetAll()
                .Where(t => trackableProjectIds.Contains(t.ProjectId))
                .Select(t => new TaskOptionViewModel { Id = t.Id, ProjectId = t.ProjectId, Title = t.Title })
                .ToList(),
            TodaySessions = _timeTrackingService.GetToday().Select(e =>
            {
                var project = _projectService.GetById(e.ProjectId);
                var task = e.TaskId.HasValue ? _taskService.GetById(e.TaskId.Value) : null;
                return new TimeSessionViewModel
                {
                    Id = e.Id,
                    ProjectName = project?.Name ?? "",
                    TaskTitle = task?.Title,
                    Color = project?.Color ?? "#7a4c8b",
                    Description = e.Description,
                    Hours = e.Hours,
                    Billable = e.Billable
                };
            }).ToList(),
            HoursToday = _timeTrackingService.GetHoursToday(),
            HoursThisWeek = _timeTrackingService.GetHoursThisWeek(),
            HoursThisMonth = _timeTrackingService.GetHoursThisMonth(),
            WeekDayLabels = weeklyHours.Select(d => d.Label).ToList(),
            WeekDayHours = weeklyHours.Select(d => d.Hours).ToList()
        };

        return View(vm);
    }
}
