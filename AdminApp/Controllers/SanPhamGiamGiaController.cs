using Microsoft.AspNetCore.Mvc;
using Shop_Models.Entities;
using System.Net.Http.Headers;

namespace AdminApp.Controllers
{
    public class SanPhamGiamGiaController : Controller
    {
        private readonly ILogger<SanPhamGiamGiaController> _logger;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;
        int Check = 1;
        public SanPhamGiamGiaController(ILogger<SanPhamGiamGiaController> logger, IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _config = config;
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetSPGG()
        {
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                //string jwtToken = HttpContext.Session.GetString("AccessToken");
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                var accessToken = Request.Cookies["account"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                HttpResponseMessage response = await client.GetAsync($"/api/SanPhamGiamGia/GetSPGGPG");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var count = result.Count();
                    ViewBag.Count = count;


                    return Content(result, "application/json");
                }
                else
                {
                    return Json(null);
                }
            }
        }

        public async Task<JsonResult> CreateSPGGWithOne(SanPhamGiamGia p)
        {
            string? apiKey = _config.GetSection("TokenGetApiAdmin").Value;
            string? urlApi = _config.GetSection("UrlApiAdmin").Value;
            using (HttpClient client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                var accessToken = Request.Cookies["account"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                HttpResponseMessage response = await client.PostAsJsonAsync($"/api/Serial/CreateSPGG", p);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    ViewBag.CartItem = result;
                    Check = 1;
                    return Json(new { status = "success" });
                }
                else
                {
                    Check = 0;
                    return Json(new { status = "error" });
                }

            }
        }

        public async Task<JsonResult> UpdateSPGG(SanPhamGiamGia p)
        {
            try
            {
                string? apiKey = _config.GetSection("TokenGetApiAdmin").Value;
                string? urlApi = _config.GetSection("UrlApiAdmin").Value;

                using (HttpClient client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
                {
                    var accessToken = Request.Cookies["account"];
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    string jwtToken = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                    // Gửi yêu cầu PUT dưới dạng JSON
                    HttpResponseMessage response = await client.PutAsJsonAsync($"/api/SanPhamGiamGia", p);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        ViewBag.CartItem = result;
                        return Json(result);
                    }
                    else
                    {
                        return Json(null);
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý exception ở đây và trả về một phản hồi JSON cho biết lý do thất bại
                return Json(new { status = "Error", message = ex.Message });
            }
        }


        public async Task<JsonResult> DeleteSPGG(Guid id)
        {
            string? apiKey = _config.GetSection("TokenGetApiAdmin").Value;
            string? urlApi = _config.GetSection("UrlApiAdmin").Value;
            using (HttpClient client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                var accessToken = Request.Cookies["account"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                HttpResponseMessage response = await client.DeleteAsync($"/api/SanPhamGiamGia/id?id={id}");
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
