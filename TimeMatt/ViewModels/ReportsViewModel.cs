namespace TimeMatt.ViewModels;

public class ReportsViewModel
{
    public List<string> ProjectLabels { get; set; } = new();
    public List<double> HoursPerProject { get; set; } = new();
    public List<decimal> RevenuePerProject { get; set; } = new();

    public List<string> WeekDayLabels { get; set; } = new();
    public List<double> HoursThisWeek { get; set; } = new();

    public List<string> MonthWeekLabels { get; set; } = new();
    public List<double> HoursThisMonth { get; set; } = new();

    public double TotalHoursThisMonth { get; set; }
    public decimal TotalRevenueThisMonth { get; set; }
}
