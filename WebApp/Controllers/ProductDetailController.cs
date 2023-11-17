using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.Net.Http.Headers;

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
        [HttpPost]
        public async void AddProductToCart(string productDetailCode)
        {
            var httpClient = _httpClientFactory.CreateClient("PhuongThaoHttpWeb");
            var accessToken = JsonConvert.DeserializeObject<TokenDto>(Request.Cookies["access_token"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.AccessToken);
            var apiUrl_ProductDetail = $@"/api/Cart/AddCart?codeProductDetail={productDetailCode}";
            var responeApi =await httpClient.PostAsync(apiUrl_ProductDetail, null);
        }
        [HttpPost]
        public async void IncreaseQuantity(string idProductDetail)
        {
            var httpClient = _httpClientFactory.CreateClient("PhuongThaoHttpWeb");
            var apiurl = $"/api/Cart/CongQuantity?idCartDetail={Guid.Parse(idProductDetail)}";
            var responeApi = await httpClient.PutAsync(apiurl, null);
        }
        [HttpPost]
        public async void DecreaseQuantity(string idProductDetail)
        {
            var httpClient = _httpClientFactory.CreateClient("PhuongThaoHttpWeb");
            var apiUrl = $"/api/Cart/TruQuantityCartDetail?idCartDetail={idProductDetail}";
            var responeApi = httpClient.PutAsync(apiUrl, null);
        }

    }
}
