using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Models.Dto;
using System.Net.Http.Headers;

namespace WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;
        public CartController(IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> UserCart()
        {
            var httpClient = _httpClientFactory.CreateClient("PhuongThaoHttpWeb");
            var accessToken = JsonConvert.DeserializeObject<TokenDto>(Request.Cookies["access_token"]);
            //Get userCart
            var apiUrl = "/api/Cart/GetCartJoinForUser?username=duykhanh12";
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.AccessToken);
            var respone = await httpClient.GetAsync(apiUrl);
            var responeContent = respone.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseDto>(responeContent.Result.ToString());
            var listProductDetail= JsonConvert.DeserializeObject<List<CartItemDto>>(result.Result.ToString());
            //var listProductOfCart = JsonConvert.DeserializeObject<CartItemDto>(content.Result.ToString());

            return PartialView("_UserCart");
        }
    }
}
