using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var apiUrl= "{{baseUrl}}/api/ProductDetail/GetAllNoJoin"
            return View();
        }
        public IActionResult GoLogin()
        {
            return RedirectToAction("Login","Login");
        }
        public IActionResult ProductDetail()
        {
            return PartialView("_Detail");
        }
    }
}
