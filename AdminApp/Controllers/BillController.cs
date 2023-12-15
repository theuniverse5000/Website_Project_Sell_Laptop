using Microsoft.AspNetCore.Mvc;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.Net.Http.Headers;

namespace AdminApp.Controllers
{

    public class BillController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;
        public BillController(IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetListBills()
        {
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                var accessToken = Request.Cookies["account"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                HttpResponseMessage response = await client.GetAsync($"/api/Bill/GetListBill");

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
        public async Task<IActionResult> GetListBillDetailByInvoiceCode(string invoiceCode)
        {
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                var accessToken = Request.Cookies["account"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                HttpResponseMessage response = await client.GetAsync($"/api/Bill/GetBillDetailByInvoiceCode?invoiceCode={invoiceCode}");

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
        public async Task<IActionResult> EditBill(Bill bill)
        {
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                var accessToken = Request.Cookies["account"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                HttpResponseMessage response = await client.PutAsJsonAsync<Bill>($"/api/Bill/UpdateBill", bill);

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



        [HttpPost("TichDiemMuaHangAsync1")]
        public async Task<IActionResult> TichDiemMuaHangAsync1(string invoiceCode, double? TongTienThanhToan)
        {
            try
            {
                using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
                {
                    var accessToken = Request.Cookies["account"];
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    // Pass the necessary data in the request body
                    var requestData = new { InvoiceCode = invoiceCode, TongTienThanhToan = TongTienThanhToan };
                    HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:44333/api/ChucNangTichDiem/TichDiemMuaHangAsync", requestData);

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
            catch (Exception ex)
            {
                // Log or handle the exception
                return Json(new { success = false });
            }
        }



    }
}
