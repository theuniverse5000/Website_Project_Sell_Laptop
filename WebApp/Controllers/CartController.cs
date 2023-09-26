using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly IConfiguration _config;
        HttpClient client = new HttpClient();
        public CartController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet]
        //    [Route("UserCart")]

        public async Task<IActionResult> UserCart()
        {
            //string? apiKey = _config.GetSection("TokenGetApiAdmin").Value;
            //string? urlApi = _config.GetSection("UrlApiAdmin").Value;
            //using (HttpClient client = new HttpClient())
            //{
            //    client.DefaultRequestHeaders.Add("Key-Domain", apiKey);
            //    HttpResponseMessage response = client.GetAsync($"{urlApi}/api/Cart/GetCartJoinForUser?username=thienthan").Result;
            //    if (response.IsSuccessStatusCode)
            //    {
            //        var result = response.Content.ReadAsStringAsync().Result;
            //        ViewBag.CartItem = result;
            //    }
            //    else
            //    {
            //        ViewBag.CartItem = "Co loi xay ra";
            //    }
            //}
            return PartialView("_UserCart");
        }
    }
}
