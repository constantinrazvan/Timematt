using TimeMatt.Models;

namespace TimeMatt.ViewModels;

public class ProjectListItemViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public ProjectStatus Status { get; set; }
    public PaymentType PaymentType { get; set; }
    public decimal Budget { get; set; }
    public decimal HourlyRate { get; set; }
    public int Progress { get; set; }
    public DateTime Deadline { get; set; }
    public double HoursWorked { get; set; }
    public string Color { get; set; } = "#6366f1";
}

public class ProjectDetailsViewModel
{
    public Project Project { get; set; } = new();
    public Client Client { get; set; } = new();
    public List<ProjectTask> Tasks { get; set; } = new();
    public List<ProjectFile> Files { get; set; } = new();
    public List<ProjectComment> Comments { get; set; } = new();
    public decimal RevenueToDate { get; set; }
}
