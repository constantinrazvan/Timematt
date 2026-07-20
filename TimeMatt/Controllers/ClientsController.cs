using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using TimeMatt.Identity;
using TimeMatt.Models;
using TimeMatt.Services;
using TimeMatt.ViewModels;

namespace TimeMatt.Controllers;

public class ClientsController : Controller
{
    private readonly IClientService _clientService;
    private readonly IProjectService _projectService;
    private readonly ITimeTrackingService _timeTrackingService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailSender _emailSender;

    public ClientsController(
        IClientService clientService,
        IProjectService projectService,
        ITimeTrackingService timeTrackingService,
        UserManager<ApplicationUser> userManager,
        IEmailSender emailSender)
    {
        _clientService = clientService;
        _projectService = projectService;
        _timeTrackingService = timeTrackingService;
        _userManager = userManager;
        _emailSender = emailSender;
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateClientViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingUser = await _userManager.FindByEmailAsync(model.Email);
        if (existingUser is not null)
        {
            ModelState.AddModelError(nameof(model.Email), "An account with this email already exists.");
            return BadRequest(ModelState);
        }

        var client = _clientService.Add(model.Name, model.Company, model.Email);

        var temporaryPassword = GenerateTemporaryPassword();
        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            FullName = model.Name,
            EmailConfirmed = true
        };

        var createResult = await _userManager.CreateAsync(user, temporaryPassword);
        if (!createResult.Succeeded)
        {
            return BadRequest(createResult.Errors.Select(e => e.Description));
        }

        await _userManager.AddToRoleAsync(user, "Client");
        await SendInviteEmailAsync(user, temporaryPassword);

        return Json(new
        {
            success = true,
            client = new
            {
                id = client.Id,
                name = client.Name,
                company = client.Company,
                email = client.Email,
                avatar = client.Avatar
            }
        });
    }

    private async Task SendInviteEmailAsync(ApplicationUser user, string temporaryPassword)
    {
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        var resetLink = Url.Action("ResetPassword", "Auth", new { email = user.Email, token = encodedToken }, Request.Scheme);

        var body = $"""
            <p>Hi {HtmlEncoder.Default.Encode(user.FullName)},</p>
            <p>You've been invited to your TextMatt client workspace.</p>
            <p><strong>Email:</strong> {HtmlEncoder.Default.Encode(user.Email!)}<br />
            <strong>Temporary password:</strong> {HtmlEncoder.Default.Encode(temporaryPassword)}</p>
            <p>Click the link below to set your own password. This link is valid for 48 hours.</p>
            <p><a href="{resetLink}">Set your password</a></p>
            <p>Once you've set a password you can sign in and access your dashboard.</p>
            """;

        await _emailSender.SendAsync(user.Email!, "You've been invited to TextMatt", body);
    }

    private static string GenerateTemporaryPassword()
    {
        const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz23456789!@#$%";
        var bytes = RandomNumberGenerator.GetBytes(12);
        var sb = new StringBuilder(12);
        foreach (var b in bytes)
        {
            sb.Append(chars[b % chars.Length]);
        }

        return sb.ToString();
    }
}
