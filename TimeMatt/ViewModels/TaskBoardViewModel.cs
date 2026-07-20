using TimeMatt.Models;

namespace TimeMatt.ViewModels;

public class TaskCardViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public TaskColumn Column { get; set; }
    public TaskPriority Priority { get; set; }
    public double EstimatedHours { get; set; }
    public double WorkedHours { get; set; }
    public DateTime Deadline { get; set; }
}

public class TaskBoardViewModel
{
    public List<TaskCardViewModel> Todo { get; set; } = new();
    public List<TaskCardViewModel> InProgress { get; set; } = new();
    public List<TaskCardViewModel> Review { get; set; } = new();
    public List<TaskCardViewModel> Done { get; set; } = new();
    public List<ProjectOptionViewModel> AllProjects { get; set; } = new();
}

public class ProjectOptionViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string Color { get; set; } = "#6366f1";
}
