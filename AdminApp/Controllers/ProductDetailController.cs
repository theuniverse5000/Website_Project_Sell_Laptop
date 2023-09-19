using Microsoft.AspNetCore.Mvc;
using Shop_Models.Dto;

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
        public async Task<IActionResult> GetProductDetail()
        {

            string? apiKey = _config.GetSection("TokenGetApiAdmin").Value;
            string? urlApi = _config.GetSection("UrlApiAdmin").Value;
            using (HttpClient client = new HttpClient())
            {
                // client.DefaultRequestHeaders.Add("Key-Domain", apiKey);
                HttpResponseMessage response = client.GetAsync($"{urlApi}/api/ProductDetail/GetProductDetailsFSP").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return Content(result, "application/json");
                }
                else
                {
                    return Json(null);
                }

            }
        }
        public JsonResult CreateProductDetail(ProductDetailDto model)
        {
            if (ModelState.IsValid)
            {
                return Json(model);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Json(new { Errors = errors });
            }
        }
        public IActionResult Create()
        {

            return View();

        }
        [HttpPost]
        public IActionResult Create(string description)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Json(new { Errors = errors });
            }
        }
    }
}
