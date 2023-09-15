using Microsoft.AspNetCore.Mvc;
using Shop_Models.Entities;

namespace AdminApp.Controllers
{
    public class ProductTypeController : Controller
    {
        private readonly ILogger<ProductTypeController> _logger;
        private readonly IConfiguration _config;
        HttpClient client = new HttpClient();
        public ProductTypeController(ILogger<ProductTypeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetProductType()
        {
            string? apiKey = _config.GetSection("TokenGetApiAdmin").Value;
            string? urlApi = _config.GetSection("UrlApiAdmin").Value;
            using (HttpClient client = new HttpClient())
            {
                //    client.DefaultRequestHeaders.Add("Key-Domain", apiKey);
                HttpResponseMessage response = client.GetAsync($"{urlApi}/api/ProductType").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    ViewBag.CartItem = result;
                    return Json(result);
                }
                else
                {
                    return Json(null);
                }

            }
        }
        public JsonResult CreateProductType(ProductType p)
        {
            string? apiKey = _config.GetSection("TokenGetApiAdmin").Value;
            string? urlApi = _config.GetSection("UrlApiAdmin").Value;
            using (HttpClient client = new HttpClient())
            {
                //    client.DefaultRequestHeaders.Add("Key-Domain", apiKey);
                HttpResponseMessage response = client.PostAsJsonAsync($"{urlApi}/api/ProductType", p).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    ViewBag.CartItem = result;
                    return Json(result);
                }
                else
                {
                    return Json(null);
                }

            }
        }
    }
}
