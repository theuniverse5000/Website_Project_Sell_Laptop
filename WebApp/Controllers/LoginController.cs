using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
public class LoginController : Controller
{
    public IActionResult Login()
    {
        return PartialView("_Login");   
    }
    public IActionResult SignUp()
    {
        return PartialView("_SignUp");
    }
}
