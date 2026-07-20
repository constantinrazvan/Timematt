namespace TimeMatt.Models;

public class ActivityItem
{
    public int Id { get; set; }
    public string Icon { get; set; } = "bi-clock-history";
    public string IconColor { get; set; } = "primary";
    public string Text { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}
