using AdminApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using Shop_Models.Dto;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace AdminApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory=httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Request.Cookies.ContainsKey("account"))
            {
                return View();
            }
            else
            {
                return View("Login");
            }
        }
        public async Task<IActionResult> Main()
        {
            return View("Index");
        }
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginWithJWT([FromBody] LoginRequestDto loginRequestDto)
        {
            var apiUrl = "https://localhost:7286/api/Account/Login";
            var httpclient = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin");
            var requestdata = new StringContent(JsonConvert.SerializeObject(loginRequestDto), Encoding.UTF8, "application/json");
            var respone = await httpclient.PostAsync(apiUrl, requestdata);
            //var LoginRespones = JsonConvert.DeserializeObject<LoginResponesDto>(respone.Content);
            var jsonRespone = await respone.Content.ReadAsStringAsync();
            var LoginRespones = JsonConvert.DeserializeObject<LoginResponesDto>(jsonRespone);
            // Lưu trữ access token trong một cookie
            HttpContext.Response.Cookies.Append("account", LoginRespones.Data.ToString(), new CookieOptions
            {
                Expires = DateTime.Now.AddHours(3), // Thời gian sống của cookie (ở đây là 30 ngày)
            });

            return RedirectToAction("Index");
        }
    }
}