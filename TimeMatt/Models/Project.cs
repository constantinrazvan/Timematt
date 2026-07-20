namespace TimeMatt.Models;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ClientId { get; set; }
    public ProjectStatus Status { get; set; }
    public PaymentType PaymentType { get; set; }
    public decimal Budget { get; set; }
    public decimal HourlyRate { get; set; }
    public int Progress { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime Deadline { get; set; }
    public double HoursWorked { get; set; }
    public double EstimatedHours { get; set; }
    public string Color { get; set; } = "#6366f1";

    public decimal RevenueToDate => PaymentType == PaymentType.Fixed
        ? Math.Round(Budget * Progress / 100m, 0)
        : Math.Round((decimal)HoursWorked * HourlyRate, 0);
}
