using Microsoft.AspNetCore.Mvc;
using TimeMatt.Services;
using TimeMatt.ViewModels;

namespace TimeMatt.Controllers;

public class ProjectsController : Controller
{
    private readonly ProjectService _projectService;
    private readonly ClientService _clientService;
    private readonly TaskService _taskService;

    public ProjectsController(ProjectService projectService, ClientService clientService, TaskService taskService)
    {
        _projectService = projectService;
        _clientService = clientService;
        _taskService = taskService;
    }

    public IActionResult Index()
    {
        var items = _projectService.GetAll().Select(p => new ProjectListItemViewModel
        {
            Id = p.Id,
            Name = p.Name,
            ClientName = _clientService.GetById(p.ClientId)?.Company ?? "",
            Status = p.Status,
            PaymentType = p.PaymentType,
            Budget = p.Budget,
            HourlyRate = p.HourlyRate,
            Progress = p.Progress,
            Deadline = p.Deadline,
            HoursWorked = p.HoursWorked,
            Color = p.Color
        }).ToList();

        return View(items);
    }

    public IActionResult Details(int id)
    {
        var project = _projectService.GetById(id);
        if (project is null)
        {
            return NotFound();
        }

        var client = _clientService.GetById(project.ClientId);
        if (client is null)
        {
            return NotFound();
        }

        var vm = new ProjectDetailsViewModel
        {
            Project = project,
            Client = client,
            Tasks = _taskService.GetByProjectId(id),
            Files = _projectService.GetFilesByProjectId(id),
            Comments = _projectService.GetCommentsByProjectId(id),
            RevenueToDate = project.RevenueToDate
        };

        return View(vm);
    }
}
