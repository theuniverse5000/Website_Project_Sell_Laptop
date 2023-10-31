using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Models.Dto;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory=httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient("PhuongThaoHttpWeb");
            var apiUrl = "/api/ProductDetail/PGetProductDetail";
            var apiRespone = await httpClient.GetStringAsync(apiUrl);
            var Respone = apiRespone.ToString();
            var responeModel = JsonConvert.DeserializeObject<ResponseDto>(Respone);
            var content = JsonConvert.DeserializeObject<List<ProductDetailDto>>(responeModel.Result.ToString());
            ViewBag.ProductDetails = content;
            return View();
        }
        public IActionResult GoLogin()
        {
            return RedirectToAction("Login","Login");
        }
        
        public async Task<IActionResult> ProductDetail(Guid id)
        {
            var httpClient = _httpClientFactory.CreateClient("PhuongThaoHttpWeb");
            var apiUrl = $@"/api/ProductDetail/ProductDetailById?guid={id}";
            var apiRespone = await httpClient.GetStringAsync(apiUrl);
            var content = JsonConvert.DeserializeObject<ProductDto>(apiRespone);
            ViewBag.ProductDetail = content;
            return PartialView("_Detail",content);
        }
    }
}
