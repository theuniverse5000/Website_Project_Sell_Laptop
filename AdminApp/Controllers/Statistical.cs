using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Controllers
{
    public class Statistical : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
