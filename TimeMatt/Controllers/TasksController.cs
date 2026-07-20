using Microsoft.AspNetCore.Mvc;
using TimeMatt.Models;
using TimeMatt.Services;
using TimeMatt.ViewModels;

namespace TimeMatt.Controllers;

public class TasksController : Controller
{
    private readonly ITaskService _taskService;
    private readonly IProjectService _projectService;
    private readonly IClientService _clientService;

    public TasksController(ITaskService taskService, IProjectService projectService, IClientService clientService)
    {
        _taskService = taskService;
        _projectService = projectService;
        _clientService = clientService;
    }

    public IActionResult Index()
    {
        var cards = _taskService.GetAll().Select(t =>
        {
            var project = _projectService.GetById(t.ProjectId);
            var client = project is not null ? _clientService.GetById(project.ClientId) : null;
            return new TaskCardViewModel
            {
                Id = t.Id,
                Title = t.Title,
                ProjectId = t.ProjectId,
                ProjectName = project?.Name ?? "",
                ClientName = client?.Company ?? "",
                Column = t.Column,
                Priority = t.Priority,
                EstimatedHours = t.EstimatedHours,
                WorkedHours = t.WorkedHours,
                Deadline = t.Deadline
            };
        }).ToList();

        var vm = new TaskBoardViewModel
        {
            Todo = cards.Where(c => c.Column == TaskColumn.Todo).ToList(),
            InProgress = cards.Where(c => c.Column == TaskColumn.InProgress).ToList(),
            Review = cards.Where(c => c.Column == TaskColumn.Review).ToList(),
            Done = cards.Where(c => c.Column == TaskColumn.Done).ToList(),
            AllProjects = _projectService.GetAll().Select(p => new ProjectOptionViewModel
            {
                Id = p.Id,
                Name = p.Name,
                ClientName = _clientService.GetById(p.ClientId)?.Company ?? "",
                Color = p.Color
            }).ToList()
        };

        return View(vm);
    }

    public IActionResult Details(int id)
    {
        var task = _taskService.GetById(id);
        if (task is null)
        {
            return NotFound();
        }

        var project = _projectService.GetById(task.ProjectId);
        var client = project is not null ? _clientService.GetById(project.ClientId) : null;

        var vm = new TaskDetailsViewModel
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            ProjectId = task.ProjectId,
            ProjectName = project?.Name ?? "",
            ClientName = client?.Company ?? "",
            ProjectColor = project?.Color ?? "#7a4c8b",
            Column = task.Column,
            Priority = task.Priority,
            EstimatedHours = task.EstimatedHours,
            WorkedHours = task.WorkedHours,
            Deadline = task.Deadline
        };

        return View(vm);
    }
}
