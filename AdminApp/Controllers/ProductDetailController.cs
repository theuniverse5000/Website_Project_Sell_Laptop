using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Controllers
{
    public class ProductDetailController : Controller
    {
        private readonly ILogger<ProductDetailController> _logger;
        private readonly IConfiguration _config;
        HttpClient client = new HttpClient();
        public ProductDetailController(ILogger<ProductDetailController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
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
