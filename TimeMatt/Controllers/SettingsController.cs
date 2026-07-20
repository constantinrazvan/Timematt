using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TimeMatt.Identity;
using TimeMatt.ViewModels;

namespace TimeMatt.Controllers;

public class SettingsController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public SettingsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Index()
    {
        var vm = new SettingsViewModel
        {
            Name = User.FindFirstValue(ClaimTypes.Name) ?? "Alex Morgan",
            Email = User.FindFirstValue(ClaimTypes.Email) ?? "alex.morgan@textmatt.dev"
        };

        ViewBag.ChangePassword = TempData["ChangePasswordResult"] is string json
            ? System.Text.Json.JsonSerializer.Deserialize<ChangePasswordViewModel>(json)
            : new ChangePasswordViewModel();

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        var result = new ChangePasswordViewModel();

        if (!ModelState.IsValid)
        {
            result.ErrorMessage = "Please fix the highlighted fields.";
        }
        else
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null)
            {
                return Challenge();
            }

            var changeResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (changeResult.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                result.Success = true;
            }
            else
            {
                result.ErrorMessage = string.Join(" ", changeResult.Errors.Select(e => e.Description));
            }
        }

        TempData["ChangePasswordResult"] = System.Text.Json.JsonSerializer.Serialize(result);
        return RedirectToAction(nameof(Index));
    }
}
