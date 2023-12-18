using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Controllers
{
    public class Statistical : Controller
    {
        public IActionResult Index()
        {
            if (Request.Cookies["account"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
