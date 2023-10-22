using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
