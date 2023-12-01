using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Shop_Models.Entities;
using System.Net.Http.Headers;

namespace AdminApp.Controllers
{
    public class SerialController : Controller
    {
        private readonly ILogger<SerialController> _logger;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;
        int Check = 1;
        public SerialController(ILogger<SerialController> logger, IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _config = config;
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }
        //public async Task<IActionResult> ImportProductsFromExcel(IFormFile excelFile)
        //{
        //    try
        //    {
        //        using (var package = new ExcelPackage(excelFile.OpenReadStream()))
        //        {
        //            var worksheet = package.Workbook.Worksheets[0];
        //            var rowCount = worksheet.Dimension.Rows;
        //            var products = new List<ProductDetail>();
        //            var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin");
        //            //for (int row = 2; row <= rowCount; row++)
        //            //{
        //            //    var product = new ProductDetail
        //            //    {
        //            //        Id = Guid.NewGuid(),
        //            //        Code = worksheet.Cells[row, 1].Text,
        //            //        ImportPrice = float.Parse(worksheet.Cells[row, 2].Text),
        //            //        Price = float.Parse(worksheet.Cells[row, 3].Text),
        //            //        Upgrade = worksheet.Cells[row, 4].Text,
        //            //        Description = worksheet.Cells[row, 5].Text,
        //            //        Status = int.Parse(worksheet.Cells[row, 6].Text),
        //            //        ProductId = Guid.Parse("D29B5AF1-AF18-4A6C-A4C6-82468DDA11B3"),
        //            //        ColorId = null,
        //            //        SerialId = null,
        //            //        CpuId = null,
        //            //        HardDriveId = null,
        //            //        ScreenId = null,
        //            //        CardVGAId = null,
        //            //    };
        //            //    products.Add(product);
        //            var result = await client.PostAsJsonAsync($"/api/ProductDetail/CreateMany", products);
        //            //if (result.IsSuccessStatusCode)
        //            //{
        //            //    return RedirectToAction("Index", "ProductDetail");
        //            //}

        //            //return BadRequest(result);
        //            //  }
        //            return RedirectToAction("Index", "ProductDetail");


        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Error: " + ex.Message);
        //    }
        //}


        public async Task<IActionResult> GetSerial()
        {
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                string jwtToken = HttpContext.Session.GetString("AccessToken");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                HttpResponseMessage response = await client.GetAsync($"/api/Serial/GetSerialPG");

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

        public async Task<JsonResult> CreateSerialWithOne(Serial p)
        {
            string? apiKey = _config.GetSection("TokenGetApiAdmin").Value;
            string? urlApi = _config.GetSection("UrlApiAdmin").Value;
            using (HttpClient client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                //    client.DefaultRequestHeaders.Add("Key-Domain", apiKey);
                HttpResponseMessage response = await client.PostAsJsonAsync($"/api/Serial/CreateSerial", p);
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

        public async Task<JsonResult> UpdateSerial(Serial p)
        {
            try
            {
                string? apiKey = _config.GetSection("TokenGetApiAdmin").Value;
                string? urlApi = _config.GetSection("UrlApiAdmin").Value;

                using (HttpClient client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
                {
                    string jwtToken = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                    // Gửi yêu cầu PUT dưới dạng JSON
                    HttpResponseMessage response = await client.PutAsJsonAsync($"/api/Serial", p);

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


        public async Task<JsonResult> DeleteSerial(Guid id)
        {
            string? apiKey = _config.GetSection("TokenGetApiAdmin").Value;
            string? urlApi = _config.GetSection("UrlApiAdmin").Value;
            using (HttpClient client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                string jwtToken = HttpContext.Session.GetString("AccessToken");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                HttpResponseMessage response = await client.DeleteAsync($"/api/Serial/id?id={id}");
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
        public async Task<IActionResult> ImportFromExcel(IFormFile excelFile)
        {
            try
            {
                using (var package = new ExcelPackage(excelFile.OpenReadStream()))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;
                    var serials = new List<Serial>();
                    string jwtToken = HttpContext.Session.GetString("AccessToken");
                    var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var serial = new Serial
                        {
                            Id = Guid.NewGuid(),
                            SerialNumber = worksheet.Cells[row, 1].Text,
                            Status = Convert.ToInt32(worksheet.Cells[row, 2].Text),
                            ProductDetailId = Guid.Parse("6600DB54-6954-451E-88AB-2A1EA4902D02"),

                        };
                        serials.Add(serial);
                    }
                    var result = await client.PostAsJsonAsync($"/api/Serial/CreateMany", serials);
                    return RedirectToAction("Index", "Serial");


                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }

    }
}
