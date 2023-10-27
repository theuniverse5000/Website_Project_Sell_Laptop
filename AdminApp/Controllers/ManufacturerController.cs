using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_API.AppDbContext;
using static Org.BouncyCastle.Math.EC.ECCurve;
using System.Net.Http;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace AdminApp.Controllers
{
  
    public class ManufacturerController : Controller
    {

        private readonly ILogger<ManufacturerController> _logger;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;
        //private string? urlApi;
        //private readonly IManufacturerRepository _manufacturer;

        public ManufacturerController(ILogger<ManufacturerController> logger, IConfiguration config, IHttpClientFactory httpClientFactory/*, IManufacturerRepository manufacturerRepository*/)
        {
            _logger = logger;
            _config = config;            //urlApi = _config.GetSection("UrlApiAdmin").Value;
            _httpClientFactory = httpClientFactory;


            //_manufacturer = manufacturerRepository;
        }
        public IActionResult Index()
        {

            return View();
        }
        //public async Task<IActionResult> GetList()
        //{
        //    string? apiKey = _config.GetSection("TokenGetApiAdmin").Value;
        //    var client = _httpClientFactory.CreateClient();
        //    string result = await client.GetStringAsync($"{urlApi}/api/Manufacturer");
        //    return Content(result, "application/json");
        //}

        //public async Task<IActionResult> GetListViewBag()
        //{

        //  return  ViewBag.ListManufacturer = new Microsoft.AspNetCore.Mvc.Rendering.SelectList((System.Collections.IEnumerable)_manufacturer.GetAll(),"Id","Name");


        //}

        public async Task<IActionResult> GetList()
        {

            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                HttpResponseMessage response = await client.GetAsync($"https://localhost:44333/api/Manufacturer");

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

        public async Task<IActionResult> GetManufacturer()
        {


            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                HttpResponseMessage response = await client.GetAsync($"https://localhost:44333/api/Manufacturer/GetManuFSP");

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

        public async Task<JsonResult> CreateManufacturer(Manufacturer p)
        {
            string? apiKey = _config.GetSection("TokenGetApiAdmin").Value;
            string? urlApi = _config.GetSection("UrlApiAdmin").Value;
            using (HttpClient client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                //    client.DefaultRequestHeaders.Add("Key-Domain", apiKey);
                HttpResponseMessage response = await client.PostAsJsonAsync($"/api/Manufacturer/CreateManu", p);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    ViewBag.CartItem = result;
                 
                    return Json(new { status = "success" });
                }
                else
                {
                    
                    return Json(new { status = "error" });
                }

            }
        }

        public async Task<JsonResult> UpdateManufacturer(Manufacturer p)
        {
            try
            {
                string? apiKey = _config.GetSection("TokenGetApiAdmin").Value;
                string? urlApi = _config.GetSection("UrlApiAdmin").Value;

                using (HttpClient client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
                {
                    // Gửi yêu cầu PUT dưới dạng JSON
                    HttpResponseMessage response = await client.PutAsJsonAsync($"/api/Manufacturer", p);

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


        public async Task<JsonResult> DeleteManufacturer(Guid id)
        {
            string? apiKey = _config.GetSection("TokenGetApiAdmin").Value;
            string? urlApi = _config.GetSection("UrlApiAdmin").Value;
            using (HttpClient client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                //    client.DefaultRequestHeaders.Add("Key-Domain", apiKey);
                HttpResponseMessage response = await client.DeleteAsync($"/api/Manufacturer/id?id={id}");
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


        public async Task<JsonResult> DeleteManufacturer2(Guid id)
        {
            try
            {
                string apiKey = _config.GetSection("TokenGetApiAdmin").Value;
                string urlApi = _config.GetSection("UrlApiAdmin").Value;

                using (HttpClient client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
                {
                    // Gửi yêu cầu DELETE với id trong URL
                    //HttpResponseMessage response = await client.DeleteAsync($"{urlApi}/api/Manufacturer/{id}");
                    HttpResponseMessage response = await client.DeleteAsync($"/api/Manufacturer/id?id={id}");

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
