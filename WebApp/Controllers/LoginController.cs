using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
public class LoginController : Controller
{
    public IActionResult Login()
    {
        return View("_Login");
    }
}
