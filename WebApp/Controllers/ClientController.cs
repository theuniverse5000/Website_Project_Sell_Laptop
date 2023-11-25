using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace WebApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;
        public ClientController(IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddProductToCart(string code)
        {
            string getUsername = "@Thaothienthan0";
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpWeb"))
            {
                HttpResponseMessage response = await client.PostAsJsonAsync($"/api/Cart/AddCart?userName={getUsername}&codeProductDetail={code}", string.Empty);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("ShowCart");
                }
                return View();
            }
        }
        [Route("giỏ-hàng")]
        public async Task<IActionResult> ShowCart()
        {
            //var UsernameToCart = HttpContext.Session.GetString("CheckLogin");
            //if (UsernameToCart == null)
            //{
            //    // var Cart = SessionService.GetObjFromSession(HttpContext.Session, "Cart");
            //    return RedirectToAction("IndexForSession");
            //}
            string userName = "@Thaothienthan0";
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpWeb"))
            {
                HttpResponseMessage response = await client.GetAsync($"/api/Voucher/GetAllVouchers");

                if (response.IsSuccessStatusCode)
                {
                    var resultString = await response.Content.ReadAsStringAsync();
                    // var resultResponse = JsonConvert.DeserializeObject<ResponseDto>(resultString);
                    ViewBag.listVoucher = JsonConvert.DeserializeObject<IEnumerable<Voucher>>(resultString);

                }


            }
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpWeb"))
            {
                HttpResponseMessage response = await client.GetAsync($"/api/Cart/GetCartJoinForUser?userName={userName}");

                if (response.IsSuccessStatusCode)
                {
                    var resultString = await response.Content.ReadAsStringAsync();
                    var resultResponse = JsonConvert.DeserializeObject<ResponseDto>(resultString);
                    ViewBag.cartItem = JsonConvert.DeserializeObject<IEnumerable<CartItemDto>>(resultResponse.Result.ToString());
                    return View();
                }
                ViewBag.cartItem = null;
                return View();
            }

        }
        [HttpGet()]
        public async Task<IActionResult> GetListVoucher()
        {
            //var UsernameToCart = HttpContext.Session.GetString("CheckLogin");
            //if (UsernameToCart == null)
            //{
            //    // var Cart = SessionService.GetObjFromSession(HttpContext.Session, "Cart");
            //    return RedirectToAction("IndexForSession");
            //}
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpWeb"))
            {
                HttpResponseMessage response = await client.GetAsync($"/api/Voucher/GetAllVouchers");

                if (response.IsSuccessStatusCode)
                {
                    var resultString = await response.Content.ReadAsStringAsync();
                    var resultResponse = JsonConvert.DeserializeObject<ResponseDto>(resultString);
                    ViewBag.listVoucher = JsonConvert.DeserializeObject<IEnumerable<CartItemDto>>(resultResponse.Result.ToString());
                    return View();
                }
                ViewBag.listVoucher = null;
                return View();
            }

        }
        [HttpPost]
        public async void IncreaseQuantity(string idProductDetail)
        {
            var httpClient = _httpClientFactory.CreateClient("PhuongThaoHttpWeb");
            var apiurl = $"/api/Cart/CongQuantity?idCartDetail={Guid.Parse(idProductDetail)}";
            var responeApi = await httpClient.PutAsync(apiurl, null);
        }
        [HttpPost]
        public async void DecreaseQuantity(string idProductDetail)
        {
            var httpClient = _httpClientFactory.CreateClient("PhuongThaoHttpWeb");
            var apiUrl = $"/api/Cart/TruQuantityCartDetail?idCartDetail={idProductDetail}";
            var responeApi = httpClient.PutAsync(apiUrl, null);
        }
        public async Task<IActionResult> CreateBill(string? codeVoucher)
        {
            string userName = "@Thaothienthan0";
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpWeb"))
            {
                HttpResponseMessage response = await client.PostAsJsonAsync($"/api/Bill/CreateBill?username={userName}&maVoucher={codeVoucher}", String.Empty);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("ShowBill");
                }
                return View();
            }
        }
        public async Task<IActionResult> ShowBill()
        {
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpWeb"))
            {
                HttpResponseMessage response = await client.GetAsync("/api/Bill/PGetBillByInvoiceCode?invoiceCode=Bill1cj64tTPLq");

                if (response.IsSuccessStatusCode)
                {
                    var resultString = await response.Content.ReadAsStringAsync();
                    var resultResponse = JsonConvert.DeserializeObject<ResponseDto>(resultString);
                    var billS = JsonConvert.DeserializeObject<BillDto>(resultResponse.Result.ToString());
                    if (billS != null)
                    {
                        ViewBag.Bill = billS;
                        ViewBag.ListBillItem = billS.BillDetail;
                    }
                    else
                    {
                        ViewBag.ListBillItem = null;
                    }
                }
                else
                {
                    ViewBag.ListBillItem = null;
                }
                return View();
            }
        }
    }
}
