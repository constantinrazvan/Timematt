using Microsoft.AspNetCore.Mvc;
using TimeMatt.Models;
using TimeMatt.Services;
using TimeMatt.ViewModels;

namespace TimeMatt.Controllers;

public class TasksController : Controller
{
    private readonly TaskService _taskService;
    private readonly ProjectService _projectService;
    private readonly ClientService _clientService;

    public TasksController(TaskService taskService, ProjectService projectService, ClientService clientService)
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
}
