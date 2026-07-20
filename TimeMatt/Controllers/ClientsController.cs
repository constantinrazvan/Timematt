using Microsoft.AspNetCore.Mvc;
using TimeMatt.Models;
using TimeMatt.Services;
using TimeMatt.ViewModels;

namespace TimeMatt.Controllers;

public class ClientsController : Controller
{
    private readonly ClientService _clientService;
    private readonly ProjectService _projectService;
    private readonly TimeTrackingService _timeTrackingService;

    public ClientsController(ClientService clientService, ProjectService projectService, TimeTrackingService timeTrackingService)
    {
        _clientService = clientService;
        _projectService = projectService;
        _timeTrackingService = timeTrackingService;
    }

    public IActionResult Index()
    {
        var hoursByProject = _timeTrackingService.GetHoursByProject();

        var items = _clientService.GetAll().Select(c =>
        {
            var projects = _projectService.GetByClientId(c.Id);
            var totalHours = projects.Sum(p => p.HoursWorked);
            return new ClientListItemViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Company = c.Company,
                Email = c.Email,
                Avatar = c.Avatar,
                ActiveProjects = projects.Count(p => p.Status == ProjectStatus.Active),
                TotalWorkedHours = totalHours
            };
        }).ToList();

        return View(items);
    }

    public IActionResult Details(int id)
    {
        var client = _clientService.GetById(id);
        if (client is null)
        {
            return NotFound();
        }

        var projects = _projectService.GetByClientId(id);

        var vm = new ClientDetailsViewModel
        {
            Client = client,
            Projects = projects,
            TotalWorkedHours = projects.Sum(p => p.HoursWorked),
            TotalRevenue = projects.Sum(p => p.RevenueToDate),
            RecentActivity = projects
                .SelectMany(p => _projectService.GetCommentsByProjectId(p.Id))
                .OrderByDescending(c => c.PostedAt)
                .Take(5)
                .Select(c => new ActivityItem
                {
                    Icon = "bi-chat-left-text",
                    IconColor = "primary",
                    Text = $"{c.Author}: \"{c.Message}\"",
                    Timestamp = c.PostedAt
                })
                .ToList()
        };

        return View(vm);
    }
}
