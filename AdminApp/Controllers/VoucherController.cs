using Microsoft.AspNetCore.Mvc;
using Shop_Models.Entities;
using System.Net.Http.Headers;

namespace AdminApp.Controllers
{
    public class VoucherController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;
        public VoucherController(IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;


        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetListVoucher()
        {
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                var accessToken = Request.Cookies["account"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                HttpResponseMessage response = await client.GetAsync("https://localhost:44333/api/Voucher/GetAllVoucherPGs");
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
        [HttpPost]
        public async Task<IActionResult> Createone(Voucher voucher)
        {
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                var accessToken = Request.Cookies["account"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.PostAsJsonAsync($"https://localhost:44333/api/Voucher/CreateVoucher2", voucher);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();/*.ConfigureAwait(false);*/
                    return Content(result, "application/json");
                }
                else
                {
                    //return Content(null);
                    var errorResult = await response.Content.ReadAsStringAsync();
                    return Content(errorResult, "application/json");
                }
            }
        }


        public async Task<IActionResult> EditVoucher(Voucher p)
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
                    HttpResponseMessage response = await client.PutAsJsonAsync($"https://localhost:44333/api/Voucher/UpdateVoucher", p);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        ViewBag.CartItem = result;
                        return Content(result, "application/json");
                    }
                    else
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        ViewBag.CartItem = result;
                        return Content(result, "application/json");
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý exception ở đây và trả về một phản hồi JSON cho biết lý do thất bại
                return Json(new { status = "Error", message = ex.Message });
            }
        }


        public async Task<IActionResult> Duyet(Guid id)
        {
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                var accessToken = Request.Cookies["account"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                HttpResponseMessage response = await client.PutAsync($"https://localhost:44333/api/Voucher/DuyetVoucher?id={id}", null);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    return Json(result);
                }
                else
                {
                    return Json(null);
                }
            }
        }

        public async Task<IActionResult> HuyDuyet(Guid id)
        {
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                var accessToken = Request.Cookies["account"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                HttpResponseMessage response = await client.PutAsync($"https://localhost:44333/api/Voucher/HuyDuyetVoucher?id={id}", null);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

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
