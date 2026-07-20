using TimeMatt.Models;

namespace TimeMatt.Services;

public interface ITimeTrackingService
{
    List<TimeEntry> GetAll();
    List<TimeEntry> GetToday();
    List<TimeEntry> GetThisWeek();
    List<TimeEntry> GetThisMonth();
    double GetHoursToday();
    double GetHoursThisWeek();
    double GetHoursThisMonth();
    List<(string Label, double Hours)> GetHoursForLastSevenDays();
    Dictionary<int, double> GetHoursByProject();
}
