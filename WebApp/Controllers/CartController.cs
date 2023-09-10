using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class CartController : Controller
    {
        public IActionResult UserCart()
        {
            return PartialView("_UserCart");
        }
    }
}
