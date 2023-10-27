using Microsoft.AspNetCore.Mvc;
using Shop_API.Controllers;

namespace AdminApp.Controllers
{
    public class PointWalletController : Controller
    {
        private readonly ILogger<ViDiemController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        public PointWalletController(ILogger<ViDiemController> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {

            _configuration = configuration;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
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
