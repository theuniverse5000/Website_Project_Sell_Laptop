using Microsoft.AspNetCore.Mvc;
using Shop_Models.Entities;

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
            var client = _httpClientFactory.CreateClient("PhuongThaoHttpWeb");
            string result = await client.GetStringAsync($"{urlApi}/api/ProductDetail/GetProductDetailsFSP");
            return Content(result, "application/json");
        }
        public async void AddProductToCart(string productDetailCode)
        {
            var userName = Request.Cookies["access_token"].ToString();
            var httpClient = _httpClientFactory.CreateClient("PhuongThaoHttpWeb");
            var apiUrl_ProductDetail = $@"/api/Cart/AddCart?username={userName}&codeProductDetail={productDetailCode}";
            var responeApi = httpClient.PostAsync(apiUrl_ProductDetail, null);
        }
        public async void AddQuantity(Guid idProductDetail)
        {
            var httpClient = _httpClientFactory.CreateClient("PhuongThaoHttpWeb");
            var apiurl = $"/api/Cart/CongQuantity?idCartDetail={idProductDetail}";
            var responeApi = httpClient.PutAsync(apiurl, null);
        }
        public async void DecreaseQuantity(Guid idproductDetail)
        {
            var httpClient = _httpClientFactory.CreateClient("PhuongThaoHttpWeb");
            var apiUrl = $"/api/Cart/TruQuantityCartDetail?idCartDetail={idproductDetail}";
            var responeApi = httpClient.PutAsync(apiUrl, null);
        }

    }
}
