using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Models.Dto;

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
            string getUsername = "@Thaothienthan1";
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
            string userName = "@Thaothienthan1";
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
        public async Task<IActionResult> ShowBill()
        {
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpWeb"))
            {
                HttpResponseMessage response = await client.GetAsync("/api/Bill/PGetBillByInvoiceCode?invoiceCode=123");

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
