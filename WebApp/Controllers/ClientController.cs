using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Models.Dto;
using Shop_Models.Entities;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;
        private static string getUsername { get; set; } = null;
        public ClientController(IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
            //getUsername = SessionExtensions.GetString(HttpContext.Session, "userName");

        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddProductToCart(string code)
        {
            // string getUsername = "@Thaothienthan0";
            if (getUsername == null)
            {
                using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpWeb"))
                {
                    HttpResponseMessage response = await client.GetAsync($"/api/ProductDetail/PGetProductDetail?codeProductDetail={code}&status=1");
                    var resultString = await response.Content.ReadAsStringAsync();
                    var resultResponse = JsonConvert.DeserializeObject<ResponseDto>(resultString);
                    var product = JsonConvert.DeserializeObject<List<ProductDetailDto>>(resultResponse.Result.ToString()).FirstOrDefault(x => x.Code == code);
                    var Cart = SessionService.GetObjFromSession(HttpContext.Session, "Cart");
                    //CartDetail CartDetails = Cart.FirstOrDefault(a => a.IdProductDetails == Id);
                    CartItemDto s = new CartItemDto();
                    //  s = Cart.Where(a => a.IdProductDetail == Id).FirstOrDefault();
                    if (Cart.Count == 0)
                    {
                        s.MaProductDetail = code;
                        s.Quantity = 1;
                        s.Price = (float)(product.Price);
                        Cart.Add(s);
                        SessionService.SetObjToSession(HttpContext.Session, "Cart", Cart);
                        return RedirectToAction("ShowCart");
                    }
                    else
                    {
                        if (SessionService.CheckObjInList(code, Cart))
                        {
                            CartItemDto thao = Cart.FirstOrDefault(x => x.MaProductDetail == code);
                            thao.Quantity += 1;
                            SessionService.SetObjToSession(HttpContext.Session, "Cart", Cart);
                            return RedirectToAction("ShowCart");
                        }
                        else
                        {
                            s.MaProductDetail = code;
                            s.Quantity = 1;
                            Cart.Add(s);
                            SessionService.SetObjToSession(HttpContext.Session, "Cart", Cart);
                            return RedirectToAction("ShowCart");
                        }
                    }
                }


            }
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
            var Cart = SessionService.GetObjFromSession(HttpContext.Session, "Cart");

            ViewBag.cartItem = Cart;
            return View();
            string userName = "@Thaothienthan0";
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
                HttpResponseMessage response = await client.GetAsync("/api/Bill/PGetBillByInvoiceCode?invoiceCode=BilluIpqvWonFp");

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
