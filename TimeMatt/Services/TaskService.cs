using TimeMatt.Models;

namespace TimeMatt.Services;

public class TaskService : ITaskService
{
    private readonly List<ProjectTask> _tasks;

    public TaskService()
    {
        var today = DateTime.Today;
        _tasks = new List<ProjectTask>
        {
            new() { Id = 1, Title = "Homepage Design", Description = "Design the new homepage layout including hero section, feature highlights and footer.", ProjectId = 1, Column = TaskColumn.Done, Priority = TaskPriority.High, EstimatedHours = 16, WorkedHours = 15.5, Deadline = today.AddDays(-20) },
            new() { Id = 2, Title = "Authentication", Description = "Implement login, registration and password reset flows for the CRM platform.", ProjectId = 2, Column = TaskColumn.Done, Priority = TaskPriority.Urgent, EstimatedHours = 24, WorkedHours = 26, Deadline = today.AddDays(-8) },
            new() { Id = 3, Title = "Dashboard", Description = "Build the main CRM dashboard with lead pipeline widgets and activity feed.", ProjectId = 2, Column = TaskColumn.InProgress, Priority = TaskPriority.High, EstimatedHours = 32, WorkedHours = 18, Deadline = today.AddDays(6) },
            new() { Id = 4, Title = "Landing Page", Description = "Final polish pass on the marketing landing page ahead of launch.", ProjectId = 1, Column = TaskColumn.Review, Priority = TaskPriority.Medium, EstimatedHours = 12, WorkedHours = 11, Deadline = today.AddDays(2) },
            new() { Id = 5, Title = "Bug Fixes", Description = "Triage and fix reported checkout bugs from the latest QA pass.", ProjectId = 5, Column = TaskColumn.InProgress, Priority = TaskPriority.Urgent, EstimatedHours = 8, WorkedHours = 5, Deadline = today.AddDays(1) },
            new() { Id = 6, Title = "Checkout Flow", Description = "Rebuild the checkout flow with the new payment step and order summary.", ProjectId = 5, Column = TaskColumn.Review, Priority = TaskPriority.High, EstimatedHours = 20, WorkedHours = 19, Deadline = today.AddDays(3) },
            new() { Id = 7, Title = "Product Catalog API", Description = "Expose a paginated product catalog endpoint with filtering support.", ProjectId = 2, Column = TaskColumn.Todo, Priority = TaskPriority.Medium, EstimatedHours = 18, WorkedHours = 0, Deadline = today.AddDays(14) },
            new() { Id = 8, Title = "Onboarding Screens", Description = "Design the first-run onboarding screens for the loyalty rewards app.", ProjectId = 4, Column = TaskColumn.Todo, Priority = TaskPriority.Low, EstimatedHours = 14, WorkedHours = 0, Deadline = today.AddDays(25) },
            new() { Id = 9, Title = "Push Notifications", Description = "Wire up push notification opt-in and reward alerts.", ProjectId = 4, Column = TaskColumn.Todo, Priority = TaskPriority.Medium, EstimatedHours = 10, WorkedHours = 2, Deadline = today.AddDays(18) },
            new() { Id = 10, Title = "Logo Concepts", Description = "Explore three initial logo directions for the new brand identity.", ProjectId = 6, Column = TaskColumn.InProgress, Priority = TaskPriority.Medium, EstimatedHours = 10, WorkedHours = 4, Deadline = today.AddDays(9) },
            new() { Id = 11, Title = "SEO Audit", Description = "Run a full technical SEO audit on the corporate website.", ProjectId = 1, Column = TaskColumn.Todo, Priority = TaskPriority.Low, EstimatedHours = 6, WorkedHours = 0, Deadline = today.AddDays(20) },
            new() { Id = 12, Title = "Payment Gateway Testing", Description = "Test the payment gateway integration across supported card types.", ProjectId = 5, Column = TaskColumn.Done, Priority = TaskPriority.Urgent, EstimatedHours = 14, WorkedHours = 15, Deadline = today.AddDays(-5) },
        };
    }

    public List<ProjectTask> GetAll() => _tasks;

    public ProjectTask? GetById(int id) => _tasks.FirstOrDefault(t => t.Id == id);

    public List<ProjectTask> GetByProjectId(int projectId) => _tasks.Where(t => t.ProjectId == projectId).ToList();

    public List<ProjectTask> GetPending() => _tasks.Where(t => t.Column != TaskColumn.Done).ToList();

    public int GetProgressForProject(int projectId, int fallbackPercent)
    {
        var tasks = GetByProjectId(projectId);
        if (tasks.Count == 0)
        {
            return fallbackPercent;
        }

        var estimated = tasks.Sum(t => t.EstimatedHours);
        if (estimated <= 0)
        {
            return fallbackPercent;
        }

        var worked = tasks.Sum(t => t.WorkedHours);
        return (int)Math.Round(Math.Min(100, worked / estimated * 100));
    }
}
