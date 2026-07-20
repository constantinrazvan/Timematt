using TimeMatt.Models;

namespace TimeMatt.Services;

public class TimeTrackingService
{
    private readonly List<TimeEntry> _entries;

    public TimeTrackingService()
    {
        var today = DateTime.Today;
        _entries = new List<TimeEntry>
        {
            // Today
            new() { Id = 1, ProjectId = 1, TaskId = 4, Description = "Landing page final polish", Date = today, Hours = 2.5, Billable = true },
            new() { Id = 2, ProjectId = 2, TaskId = 3, Description = "Dashboard widgets", Date = today, Hours = 1.75, Billable = true },
            new() { Id = 3, ProjectId = 5, TaskId = 5, Description = "Checkout bug triage", Date = today, Hours = 1, Billable = true },

            // This week
            new() { Id = 4, ProjectId = 1, Description = "Homepage design revisions", Date = today.AddDays(-1), Hours = 4, Billable = true },
            new() { Id = 5, ProjectId = 2, Description = "Auth flow implementation", Date = today.AddDays(-1), Hours = 3.5, Billable = true },
            new() { Id = 6, ProjectId = 5, Description = "Checkout flow QA", Date = today.AddDays(-2), Hours = 5, Billable = true },
            new() { Id = 7, ProjectId = 4, Description = "Onboarding wireframes", Date = today.AddDays(-2), Hours = 2, Billable = false },
            new() { Id = 8, ProjectId = 2, Description = "API integration", Date = today.AddDays(-3), Hours = 6, Billable = true },
            new() { Id = 9, ProjectId = 1, Description = "Content updates", Date = today.AddDays(-3), Hours = 1.5, Billable = true },
            new() { Id = 10, ProjectId = 6, Description = "Logo concept sketches", Date = today.AddDays(-4), Hours = 3, Billable = true },
            new() { Id = 11, ProjectId = 5, Description = "Payment gateway testing", Date = today.AddDays(-4), Hours = 4.5, Billable = true },
            new() { Id = 12, ProjectId = 2, Description = "Dashboard layout", Date = today.AddDays(-5), Hours = 3, Billable = true },
            new() { Id = 13, ProjectId = 1, Description = "SEO audit", Date = today.AddDays(-5), Hours = 2, Billable = true },
            new() { Id = 19, ProjectId = 4, Description = "Onboarding screens review", Date = today.AddDays(-6), Hours = 2.5, Billable = false },

            // Earlier this month
            new() { Id = 14, ProjectId = 3, Description = "Final portfolio delivery", Date = today.AddDays(-12), Hours = 4, Billable = true },
            new() { Id = 15, ProjectId = 4, Description = "Loyalty rewards logic", Date = today.AddDays(-15), Hours = 5, Billable = true },
            new() { Id = 16, ProjectId = 2, Description = "CRM data modeling", Date = today.AddDays(-18), Hours = 6.5, Billable = true },
            new() { Id = 17, ProjectId = 1, Description = "Corporate site kickoff", Date = today.AddDays(-22), Hours = 3, Billable = true },
            new() { Id = 18, ProjectId = 7, Description = "Internal tools handoff", Date = today.AddDays(-26), Hours = 2.5, Billable = false },
        };
    }

    public List<TimeEntry> GetAll() => _entries;

    public List<TimeEntry> GetToday() => _entries.Where(e => e.Date.Date == DateTime.Today).ToList();

    public List<TimeEntry> GetThisWeek()
    {
        var start = DateTime.Today.AddDays(-6);
        return _entries.Where(e => e.Date.Date >= start && e.Date.Date <= DateTime.Today).ToList();
    }

    public List<TimeEntry> GetThisMonth()
    {
        var today = DateTime.Today;
        return _entries.Where(e => e.Date.Year == today.Year && e.Date.Month == today.Month).ToList();
    }

    public double GetHoursToday() => GetToday().Sum(e => e.Hours);

    public double GetHoursThisWeek() => GetThisWeek().Sum(e => e.Hours);

    public double GetHoursThisMonth() => GetThisMonth().Sum(e => e.Hours);

    public List<(string Label, double Hours)> GetHoursForLastSevenDays()
    {
        var result = new List<(string Label, double Hours)>();
        for (var i = 6; i >= 0; i--)
        {
            var day = DateTime.Today.AddDays(-i);
            var hours = _entries.Where(e => e.Date.Date == day).Sum(e => e.Hours);
            result.Add((day.ToString("ddd"), hours));
        }
        return result;
    }

    public Dictionary<int, double> GetHoursByProject() =>
        _entries.GroupBy(e => e.ProjectId).ToDictionary(g => g.Key, g => g.Sum(e => e.Hours));
}
