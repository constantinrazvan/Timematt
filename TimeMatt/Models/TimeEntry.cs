namespace TimeMatt.Models;

public class TimeEntry
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public int? TaskId { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public double Hours { get; set; }
    public bool Billable { get; set; } = true;
}
