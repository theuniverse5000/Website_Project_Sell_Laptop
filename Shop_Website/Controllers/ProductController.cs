using Microsoft.AspNetCore.Mvc;

namespace Shop_Website.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
