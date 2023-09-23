using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_API.Repository;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace AdminApp.Controllers
{
    public class ProductDetailController : Controller
    {
        private readonly ILogger<ProductDetailController> _logger;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;
        private string? urlApi;

        public ProductDetailController(ILogger<ProductDetailController> logger, IConfiguration config, IHttpClientFactory httpClientFactory )
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
    

        public async Task<JsonResult> CreateProductDetail(ProductDetailDto productRequest)
        {
            if (ModelState.IsValid)
            {
                ProductDetail productDetail = new ProductDetail()
                {
                    Id = Guid.NewGuid(),
                    Code = productRequest.Code
                };
                string? apiKey = _config.GetSection("TokenGetApiAdmin").Value;

                var client = _httpClientFactory.CreateClient();
                var result = await client.PostAsJsonAsync($"{urlApi}/api/ProductDetail", productDetail);
                if (result.IsSuccessStatusCode)
                {
                    return Json(productDetail);
                }
                return Json(null);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Json(new { Errors = errors });
            }
        }
        public async Task<IActionResult> Create()
        {
            var client = _httpClientFactory.CreateClient();

            string getProduct = await client.GetStringAsync($"{urlApi}/api/Product");
            ViewBag.GetProduct = JsonConvert.DeserializeObject<List<Product>>(getProduct);

            string getManufacturer = await client.GetStringAsync($"{urlApi}/api/Manufacturer");
            ViewBag.GetManufacturer =  JsonConvert.DeserializeObject<List<Manufacturer>>(getManufacturer);

            string getProductType = await client.GetStringAsync($"{urlApi}/api/ProductType");
            ViewBag.GetProductType = JsonConvert.DeserializeObject<List<ProductType>>(getProductType);
            
            string getCPU = await client.GetStringAsync($"{urlApi}/api/Cpu");
            ViewBag.GetCPU = JsonConvert.DeserializeObject<List<Cpu>>(getCPU);

            string getRam = await client.GetStringAsync($"{urlApi}/api/Ram");
            ViewBag.GetRam = JsonConvert.DeserializeObject<List<Ram>>(getRam);

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(string description)
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
