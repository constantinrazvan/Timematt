namespace TimeMatt.Models;

public class ProjectTask
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int ProjectId { get; set; }
    public TaskColumn Column { get; set; }
    public TaskPriority Priority { get; set; }
    public double EstimatedHours { get; set; }
    public double WorkedHours { get; set; }
    public DateTime Deadline { get; set; }
}
