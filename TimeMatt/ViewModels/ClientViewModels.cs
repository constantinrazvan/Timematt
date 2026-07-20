using System.ComponentModel.DataAnnotations;
using TimeMatt.Models;

namespace TimeMatt.ViewModels;

public class CreateClientViewModel
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Company { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}

public class ClientListItemViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public int ActiveProjects { get; set; }
    public double TotalWorkedHours { get; set; }
}

public class ClientDetailsViewModel
{
    public Client Client { get; set; } = new();
    public List<Project> Projects { get; set; } = new();
    public double TotalWorkedHours { get; set; }
    public decimal TotalRevenue { get; set; }
    public List<ActivityItem> RecentActivity { get; set; } = new();
}
