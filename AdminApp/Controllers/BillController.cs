using Microsoft.AspNetCore.Mvc;
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

    }
}
