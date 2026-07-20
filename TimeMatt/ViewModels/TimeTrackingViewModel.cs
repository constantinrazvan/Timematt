using TimeMatt.Models;

namespace TimeMatt.ViewModels;

public class TimeSessionViewModel
{
    public int Id { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string? TaskTitle { get; set; }
    public string Color { get; set; } = "#7a4c8b";
    public string Description { get; set; } = string.Empty;
    public double Hours { get; set; }
    public bool Billable { get; set; }
}

public class TaskOptionViewModel
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string Title { get; set; } = string.Empty;
}

public class TimeTrackingViewModel
{
    public List<Project> TrackableProjects { get; set; } = new();
    public List<TaskOptionViewModel> AllTasks { get; set; } = new();
    public List<TimeSessionViewModel> TodaySessions { get; set; } = new();
    public double HoursToday { get; set; }
    public double HoursThisWeek { get; set; }
    public double HoursThisMonth { get; set; }
    public List<string> WeekDayLabels { get; set; } = new();
    public List<double> WeekDayHours { get; set; } = new();
}
