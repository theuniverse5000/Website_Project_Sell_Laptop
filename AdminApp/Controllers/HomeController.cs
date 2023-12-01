using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Models.Dto;
using System.Security.Claims;
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
            _httpClientFactory = httpClientFactory;
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

        public async Task<IActionResult> Login()
        {

            if (HttpContext.Request.Cookies.ContainsKey("account"))
            {
                return View("Index");
            }
            else
            {
                return View();
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
        public async Task<IActionResult> LoginWithJWT([FromForm] LoginRequestDto loginRequestDto)
        {
            var httpclient = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin");
            var requestdata = new StringContent(JsonConvert.SerializeObject(loginRequestDto), Encoding.UTF8, "application/json");
            var respone = await httpclient.PostAsync("api/Account/Login", requestdata);
            //var LoginRespones = JsonConvert.DeserializeObject<LoginResponesDto>(respone.Content);
            var jsonRespone = await respone.Content.ReadAsStringAsync();
            var LoginRespones = JsonConvert.DeserializeObject<LoginResponesDto>(jsonRespone);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,loginRequestDto.UserName)
            };
            var identity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var pricipal = new ClaimsPrincipal(identity);
            var props = new AuthenticationProperties();
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, pricipal, props).Wait();
            //HttpContext.Response.Cookies.Append("account", LoginRespones.Data.ToString(), new CookieOptions
            //{
            //    Expires = DateTime.Now.AddHours(3),
            //});

            return Content(jsonRespone, "application/json");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            Response.Cookies.Delete("account");
            return RedirectToAction("Index", "Home");
        }
    }
}