using Microsoft.AspNetCore.Mvc;
using TimeMatt.Services;

namespace TimeMatt.Controllers;

public class AccountsController : Controller
{
    private readonly IAccountService _accountService;

    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public IActionResult Index()
    {
        return View(_accountService.GetAll());
    }
}
