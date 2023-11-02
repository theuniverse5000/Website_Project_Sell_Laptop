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
        public async Task<IActionResult> ShowBill()
        {
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpWeb"))
            {
                HttpResponseMessage response = await client.GetAsync("/api/Bill/PGetBillByInvoiceCode?invoiceCode=123");

                if (response.IsSuccessStatusCode)
                {
                    var resultString = await response.Content.ReadAsStringAsync(); // Đảm bảo đọc nội dung của response

                    // Deserialize JSON response to ResponseDto
                    var resultResponse = JsonConvert.DeserializeObject<ResponseDto>(resultString);
                    var billS = JsonConvert.DeserializeObject<BillDto>(resultResponse.Result.ToString());
                    if (billS != null)
                    {
                        ViewBag.Bill = billS;
                        ViewBag.ListBillItem = billS.BillDetail; // Sử dụng trực tiếp thuộc tính BillDetail từ result1
                    }
                    else
                    {
                        ViewBag.ListBillItem = null;
                    }

                    // Bây giờ bạn đã có dữ liệu và có thể sử dụng chúng ở trong ViewBag hoặc trả về View theo cách thích hợp
                }
                else
                {
                    ViewBag.ListBillItem = null; // Xử lý khi response không thành công
                }

                return View();
            }

        }
    }
}
