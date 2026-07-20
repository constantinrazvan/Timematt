using Microsoft.AspNetCore.Mvc;
using TimeMatt.Models;
using TimeMatt.Services;
using TimeMatt.ViewModels;

namespace TimeMatt.Controllers;

public class ProjectsController : Controller
{
    private readonly IProjectService _projectService;
    private readonly IClientService _clientService;
    private readonly ITaskService _taskService;

    public ProjectsController(IProjectService projectService, IClientService clientService, ITaskService taskService)
    {
        _projectService = projectService;
        _clientService = clientService;
        _taskService = taskService;
    }

    public IActionResult Index()
    {
        ViewBag.Clients = _clientService.GetAll();

        var items = _projectService.GetAll().Select(p => new ProjectListItemViewModel
        {
            Id = p.Id,
            Name = p.Name,
            ClientName = _clientService.GetById(p.ClientId)?.Company ?? "",
            Status = p.Status,
            PaymentType = p.PaymentType,
            Budget = p.Budget,
            HourlyRate = p.HourlyRate,
            Progress = _taskService.GetProgressForProject(p.Id, p.Progress),
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

        var tasks = _taskService.GetByProjectId(id);
        var displayProgress = _taskService.GetProgressForProject(id, project.Progress);
        var revenueToDate = project.PaymentType == PaymentType.Fixed
            ? Math.Round(project.Budget * displayProgress / 100m, 0)
            : project.RevenueToDate;

        var vm = new ProjectDetailsViewModel
        {
            Project = project,
            Client = client,
            Tasks = tasks,
            Files = _projectService.GetFilesByProjectId(id),
            Comments = _projectService.GetCommentsByProjectId(id),
            RevenueToDate = revenueToDate,
            DisplayProgress = displayProgress,
            AllClients = _clientService.GetAll()
        };

        return View(vm);
    }
}
