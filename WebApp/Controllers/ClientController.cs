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
        private static string getUsername { get; set; } = string.Empty;
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
            getUsername = HttpContext.Session.GetString("username");
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
                        s.Id = Guid.NewGuid();
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
            getUsername = HttpContext.Session.GetString("username");
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpWeb"))
            {
                HttpResponseMessage response = await client.GetAsync($"/api/Voucher/GetAllVoucher");

                if (response.IsSuccessStatusCode)
                {
                    var resultString = await response.Content.ReadAsStringAsync();
                    // var resultResponse = JsonConvert.DeserializeObject<ResponseDto>(resultString);
                    ViewBag.listVoucher = JsonConvert.DeserializeObject<IEnumerable<Voucher>>(resultString);
                }
            }
            if (getUsername != null)
            {
                using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpWeb"))
                {
                    HttpResponseMessage response = await client.GetAsync($"/api/Cart/GetCartJoinForUser?userName={getUsername}");

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
            else
            {
                var Cart = SessionService.GetObjFromSession(HttpContext.Session, "Cart");

                ViewBag.cartItem = Cart;
                return View();
            }
        }
        [HttpGet()]
        public async Task<IActionResult> GetListVoucher()
        {
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
        public async void IncreaseQuantity(Guid idCartDetail)
        {
            var Cart = SessionService.GetObjFromSession(HttpContext.Session, "Cart");
            CartItemDto thao = Cart.FirstOrDefault(x => x.Id == idCartDetail);
            thao.Quantity += 1;
            SessionService.SetObjToSession(HttpContext.Session, "Cart", Cart);
            var httpClient = _httpClientFactory.CreateClient("PhuongThaoHttpWeb");
            var apiurl = $"/api/Cart/CongQuantity?idCartDetail={idCartDetail}";
            var responeApi = await httpClient.PutAsJsonAsync(apiurl, string.Empty);
        }
        [HttpPost]
        public async void DecreaseQuantity(Guid idCartDetail)
        {
            var Cart = SessionService.GetObjFromSession(HttpContext.Session, "Cart");
            CartItemDto thao = Cart.FirstOrDefault(x => x.Id == idCartDetail);
            thao.Quantity -= 1;
            SessionService.SetObjToSession(HttpContext.Session, "Cart", Cart);
            var httpClient = _httpClientFactory.CreateClient("PhuongThaoHttpWeb");
            var apiUrl = $"/api/Cart/TruQuantityCartDetail?idCartDetail={idCartDetail}";
            var responeApi = await httpClient.PutAsJsonAsync(apiUrl, string.Empty);
        }

        public async Task<IActionResult> DeleteCartDetail(Guid idCartDetail)
        {
            var Cart = SessionService.GetObjFromSession(HttpContext.Session, "Cart");
            CartItemDto thao = Cart.FirstOrDefault(x => x.Id == idCartDetail);
            Cart.Remove(thao);
            SessionService.SetObjToSession(HttpContext.Session, "Cart", Cart);
            var httpClient = _httpClientFactory.CreateClient("PhuongThaoHttpWeb");
            var apiUrl = $"/api/Cart/Delete?idCartDetail={idCartDetail}";
            var responeApi = await httpClient.DeleteAsync(apiUrl);
            return RedirectToAction("ShowCart", "Client");
        }
        public async Task<IActionResult> CreateBill(RequestBillDto request, double? totalAmount, bool useAllPoints)
        {
            request.CartItem = new List<CartItemDto>();
            request.Usename = HttpContext.Session.GetString("username");
            var cartSession = SessionService.GetObjFromSession(HttpContext.Session, "Cart");
            foreach (var i in cartSession)
            {
                request.CartItem.Add(i);
            }
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpWeb"))
            {
                HttpResponseMessage response = await client.PostAsJsonAsync($"/api/Bill/CreateBill", request);
                if (response.IsSuccessStatusCode)
                {

                    var codeBill = await response.Content.ReadAsStringAsync();
                    if (useAllPoints == true)
                    {
                        //datHang = 1000000;
                        HttpResponseMessage response2 = await client.PostAsJsonAsync($"https://localhost:44333/api/ChucNangTichDiem/TieuDiemMuaHangAsync?invoiceCode={codeBill}&TongTienThanhToan={totalAmount}", String.Empty);

                        var resultString2 = await response2.Content.ReadAsStringAsync();
                        var resultResponse2 = JsonConvert.DeserializeObject<ResponseDto>(resultString2);
                    }

                    return RedirectToAction("ShowBill", new { invoiceCode = $"{codeBill}" });
                }
                return View();
            }
        }
        [Route("hóa-đơn")]
        public async Task<IActionResult> ShowBill(string? invoiceCode)
        {
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpWeb"))
            {
                HttpResponseMessage response = await client.GetAsync($"/api/Bill/PGetBillByInvoiceCode?invoiceCode={invoiceCode}");

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
        public async Task<IActionResult> ShowBill2(string? invoiceCode)
        {
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpWeb"))
            {
                HttpResponseMessage response = await client.GetAsync($"/api/Bill/PGetBillByInvoiceCode?invoiceCode={invoiceCode}");

                if (response.IsSuccessStatusCode)
                {
                    var resultString = await response.Content.ReadAsStringAsync();
                    var resultResponse = JsonConvert.DeserializeObject<ResponseDto>(resultString);
                    var billS = JsonConvert.DeserializeObject<BillDto>(resultResponse.Result.ToString());

                    if (billS != null)
                    {
                        ViewBag.Bill = billS;
                        ViewBag.ListBillItem = billS.BillDetail;
                        // Trả về JSON chứa thông tin hóa đơn
                        return Json(new { success = true, bill = billS });
                    }
                }

                // Trả về JSON thông báo lỗi
                return Json(new { success = false, message = "Không tìm thấy hóa đơn" });
            }
        }
        public async Task<IActionResult> AddSanPhamGiamGiaToCart(string code)
        {
            getUsername = HttpContext.Session.GetString("username");
            if (getUsername == null)
            {
                using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpWeb"))
                {
                    HttpResponseMessage response = await client.GetAsync($"/api/SanPhamGiamGia/GetSPGGPG?codeProductDetail={code}");
                    var resultString = await response.Content.ReadAsStringAsync();
                    var resultResponse = JsonConvert.DeserializeObject<ResponseDto>(resultString);
                    var product = JsonConvert.DeserializeObject<List<SanPhamGiamGiaDto>>(resultResponse.Result.ToString()).FirstOrDefault(x => x.ProductDetailCode == code);
                    var Cart = SessionService.GetObjFromSession(HttpContext.Session, "Cart");
                    CartItemDto s = new CartItemDto();
                    if (Cart.Count == 0)
                    {
                        s.MaProductDetail = code;
                        s.Quantity = 1;
                        s.Price = (float)(product.SoTienConLai);
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
    }
}
