using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Controllers
{
    public class PointWalletController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        public PointWalletController( IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {

            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            if (Request.Cookies["account"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public async Task<IActionResult> GetPointWallet()
        {
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                HttpResponseMessage response = await client.GetAsync($"/api/ViDiem/GetPointWallet");

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
    }
}
