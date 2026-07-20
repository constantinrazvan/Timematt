using Microsoft.AspNetCore.Mvc;
using TimeMatt.ViewModels;

namespace TimeMatt.Controllers;

public class SettingsController : Controller
{
    public IActionResult Index()
    {
        return View(new SettingsViewModel());
    }
}
