using Microsoft.AspNetCore.Mvc;
using TimeMatt.Services;

namespace TimeMatt.Controllers;

public class NotificationsController : Controller
{
    private readonly INotificationService _notificationService;

    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public IActionResult Index()
    {
        return View(_notificationService.GetAll());
    }
}
