using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Models.Entities;
using System.Net.Http.Headers;

namespace AdminApp.Controllers
{
    public class ManagePostController : Controller
    {
        private readonly ILogger<ManagePostController> _logger;
        private readonly IConfiguration _config;
        IHttpClientFactory _httpClientFactory;
        public ManagePostController(IHttpClientFactory httpClientFactory, ILogger<ManagePostController> logger, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _config = config;
        }
        public IActionResult Index()
        {
            if (Request.Cookies["account"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        public IActionResult _75584461()
        {
            if (Request.Cookies["account"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public async Task<IActionResult> GetAll(bool? status)
        {
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                var accessToken = Request.Cookies["account"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                // Sử dụng tham số status để tạo đường dẫn tương ứng
                var statusQueryParam = status.HasValue ? $"?status={status}" : "";

                var response = await client.GetAsync($"https://localhost:44333/api/ManagePost/GGetManagePostDtosFSP{statusQueryParam}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return Content(result, "application/json");
                }

                return Json(null);
            }
        }

        //public async Task<IActionResult> GetAll()
        //{
        //    using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
        //    {
        //        var accessToken = Request.Cookies["account"];
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        //        var reponse = await client.GetAsync($"/api/ManagePost/GetAllReturnReposon");
        //        if (reponse.IsSuccessStatusCode)
        //        {
        //            var result = await reponse.Content.ReadAsStringAsync();
        //            return Content(result, "application/json");
        //        }
        //        return Json(null);
        //    }
        //}


        public async Task<IActionResult> Create(ManagePost obj)
        {
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                var accessToken = Request.Cookies["account"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var reponse = await client.PostAsJsonAsync($"/api/ManagePost/CreateMP", obj);
                if (reponse.IsSuccessStatusCode)
                {
                    //var result = reponse.Content.ReadAsStringAsync();
                    return View("Index");
                }
                return Json(null);
            }
        }

        public  IActionResult CreateWithOtherPage()
        {

            if (Request.Cookies["account"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> CreateWithOtherPage(ManagePost obj)
        {

            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                var accessToken = Request.Cookies["account"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var reponse = await client.PostAsJsonAsync($"/api/ManagePost/CreateMP", obj);
                if (reponse.IsSuccessStatusCode)
                {
                    return Json(new { success = true });
                }
                else return Json(new { success = false }); ;
              

            }

        }

        [HttpGet]
        public async Task<IActionResult> DetailsManagePost(Guid id)
        {
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                var accessToken = Request.Cookies["account"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var reponse = await client.GetAsync($"/api/ManagePost/GetByIdManagePost?Id={id}");
                if (reponse.IsSuccessStatusCode)
                {
                    var data = await reponse.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ManagePost>(data);
                return View(result);
                }return View("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> DetailsManagePost(ManagePost obj)
        {
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                var accessToken = Request.Cookies["account"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var reponse = await client.PutAsJsonAsync($"/api/ManagePost", obj);
                if (reponse.IsSuccessStatusCode)
                {
                    
                    return Json(new { success = true });
                }
                else {

                    return Json(new { success = false });
                }
            }

        }

        public async Task<JsonResult> DeleteManage(ManagePost p)
        {
            string? apiKey = _config.GetSection("TokenGetApiAdmin").Value;
            string? urlApi = _config.GetSection("UrlApiAdmin").Value;
            using (HttpClient client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                var accessToken = Request.Cookies["account"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                HttpResponseMessage response = await client.DeleteAsync($"https://localhost:44333/api/ManagePost/id?id={p.Id}");
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


    }
}
