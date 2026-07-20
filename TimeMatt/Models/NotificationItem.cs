namespace TimeMatt.Models;

public class NotificationItem
{
    public int Id { get; set; }
    public string Icon { get; set; } = "bi-bell";
    public string IconColor { get; set; } = "primary";
    public string Title { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public bool IsRead { get; set; }
}
