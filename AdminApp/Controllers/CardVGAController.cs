using Microsoft.AspNetCore.Mvc;
using Shop_Models.Entities;
using System.Net.Http.Headers;

namespace AdminApp.Controllers
{
    public class CardVGAController : Controller
    {
        private readonly ILogger<CardVGAController> _logger;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;
        int Check = 1;

        public CardVGAController(ILogger<CardVGAController> logger, IConfiguration config,IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _config = config;
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {

            return View();
        }

        public async Task<IActionResult> GetCardVGA()
        {


            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                var accessToken = Request.Cookies["account"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                HttpResponseMessage response = await client.GetAsync($"/api/CardVGA/GetCardVGAFSP");

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

        public async Task<JsonResult> CreateCardVGA(CardVGA p)
        {
            string? apiKey = _config.GetSection("TokenGetApiAdmin").Value;
            string? urlApi = _config.GetSection("UrlApiAdmin").Value;
            using (HttpClient client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                var accessToken = Request.Cookies["account"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                //    client.DefaultRequestHeaders.Add("Key-Domain", apiKey);
                HttpResponseMessage response = await client.PostAsJsonAsync($"/api/CardVGA/CreateCardVGA", p);
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

        public async Task<JsonResult> UpdateCardVGA(CardVGA p)
        {
            try
            {
                string? apiKey = _config.GetSection("TokenGetApiAdmin").Value;
                string? urlApi = _config.GetSection("UrlApiAdmin").Value;

                using (HttpClient client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
                {
                    var accessToken = Request.Cookies["account"];
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    // Gửi yêu cầu PUT dưới dạng JSON
                    HttpResponseMessage response = await client.PutAsJsonAsync($"/api/CardVGA", p);

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


        public async Task<JsonResult> DeleteCardVGA(Guid id)
        {
            string? apiKey = _config.GetSection("TokenGetApiAdmin").Value;
            string? urlApi = _config.GetSection("UrlApiAdmin").Value;
            using (HttpClient client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                var accessToken = Request.Cookies["account"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                //    client.DefaultRequestHeaders.Add("Key-Domain", apiKey);
                HttpResponseMessage response = await client.DeleteAsync($"/api/CardVGA/id?id={id}");
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


        public async Task<JsonResult> DeleteCardVGA2(Guid id)
        {
            try
            {
                string apiKey = _config.GetSection("TokenGetApiAdmin").Value;
                string urlApi = _config.GetSection("UrlApiAdmin").Value;

                using (HttpClient client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
                {
                    var accessToken = Request.Cookies["account"];
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    // Gửi yêu cầu DELETE với id trong URL
                    //HttpResponseMessage response = await client.DeleteAsync($"{urlApi}/api/CardVGA/{id}");
                    HttpResponseMessage response = await client.DeleteAsync($"/api/CardVGA/id?id={id}");

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



    }
}
