using TimeMatt.Models;

namespace TimeMatt.Services;

public interface INotificationService
{
    List<NotificationItem> GetAll();
    int GetUnreadCount();
}
