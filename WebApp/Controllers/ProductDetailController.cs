using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ProductDetailController : Controller
    {
        private readonly ILogger<ProductDetailController> _logger;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;
        private string? urlApi;
        public ProductDetailController(ILogger<ProductDetailController> logger, IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _config = config;
            _httpClientFactory = httpClientFactory;
            urlApi = _config.GetSection("UrlApiAdmin").Value;
        }
        public IActionResult Index()
        {

            return View();
        }
        public async Task<IActionResult> GetProductDetail()
        {
            string? apiKey = _config.GetSection("TokenGetApiAdmin").Value;
            var client = _httpClientFactory.CreateClient();
            string result = await client.GetStringAsync($"{urlApi}/api/ProductDetail/GetProductDetailsFSP");
            return Content(result, "application/json");
        }
    }
}
