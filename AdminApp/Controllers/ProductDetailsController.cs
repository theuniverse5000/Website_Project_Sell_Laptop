using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Controllers
{
	public class ProductDetailsController : Controller
	{
        private readonly ILogger<ProductDetailsController> _logger;

        public ProductDetailsController(ILogger<ProductDetailsController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
		{
			return View();
		}
		public IActionResult AllProductDetails()
		{
			return View();
		}
	}
}
