using TimeMatt.Models;

namespace TimeMatt.Services;

public class NotificationService : INotificationService
{
    private readonly List<NotificationItem> _notifications;

    public NotificationService()
    {
        var today = DateTime.Today;
        _notifications = new List<NotificationItem>
        {
            new() { Id = 1, Icon = "bi-alarm", IconColor = "danger", Title = "\"Checkout Flow\" is due in 3 days on E-commerce Redesign", Timestamp = today.AddHours(-2), IsRead = false },
            new() { Id = 2, Icon = "bi-chat-left-text", IconColor = "primary", Title = "Sarah Bennett commented on Corporate Website", Timestamp = today.AddHours(-6), IsRead = false },
            new() { Id = 3, Icon = "bi-check2-circle", IconColor = "success", Title = "Payment Gateway Testing marked as Done", Timestamp = today.AddDays(-1), IsRead = false },
            new() { Id = 4, Icon = "bi-person-plus", IconColor = "success", Title = "Priya Nair was added as a new client", Timestamp = today.AddDays(-2), IsRead = true },
            new() { Id = 5, Icon = "bi-file-earmark-arrow-up", IconColor = "info", Title = "Checkout-Flow.fig was uploaded to E-commerce Redesign", Timestamp = today.AddDays(-2).AddHours(-4), IsRead = true },
            new() { Id = 6, Icon = "bi-kanban", IconColor = "primary", Title = "\"Dashboard\" moved to In Progress on CRM Platform", Timestamp = today.AddDays(-3), IsRead = true },
            new() { Id = 7, Icon = "bi-stopwatch", IconColor = "warning", Title = "Logged 6.5h on CRM Platform - Data modeling", Timestamp = today.AddDays(-4), IsRead = true },
            new() { Id = 8, Icon = "bi-cash-coin", IconColor = "success", Title = "Portfolio Website marked as Completed", Timestamp = today.AddDays(-12), IsRead = true },
            new() { Id = 9, Icon = "bi-exclamation-triangle", IconColor = "danger", Title = "Mobile App deadline moved up - review updated timeline", Timestamp = today.AddDays(-15), IsRead = true },
        };
    }

    public List<NotificationItem> GetAll() => _notifications.OrderByDescending(n => n.Timestamp).ToList();

    public int GetUnreadCount() => _notifications.Count(n => !n.IsRead);
}
