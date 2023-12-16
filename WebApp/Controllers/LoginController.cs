using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.Security.Claims;
using System.Text;

namespace WebApp.Controllers;
[AllowAnonymous]
public class LoginController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public LoginController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
    {
        _httpClientFactory = httpClientFactory;
        _httpContextAccessor = httpContextAccessor;
    }

    public IActionResult Login(string ReturnUrl = "/")
    {
        string accessToken = HttpContext.Request.Cookies["access_token"];
        if (accessToken != null)
        {
            return RedirectToAction("Index", "Home");
        }
        return RedirectToAction("Login", "Home");
    }
    [HttpPost]
    public async Task<IActionResult> LoginWithJWT(string username, string password)
    {
        LoginRequestDto login = new LoginRequestDto();
        login.UserName = username;
        login.Password = password;
        var apiUrl = "/api/Authentication/Login";
        var httpclient = _httpClientFactory.CreateClient("PhuongThaoHttpWeb");
        var requestdata = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
        var respone = await httpclient.PostAsync(apiUrl, requestdata);
        var jsonRespone = await respone.Content.ReadAsStringAsync();
        var apiUrl2 = $"https://localhost:44333/api/ChucNangTichDiem/GetStatusOfPointWallet?usename={username}";
        var httpclien2t = _httpClientFactory.CreateClient("PhuongThaoHttpWeb");
        var respone2 = await httpclient.GetAsync(apiUrl2);
        var jsonRespone2 = await respone2.Content.ReadAsStringAsync();
        if (respone.IsSuccessStatusCode)
        {
            var loginRespones = JsonConvert.DeserializeObject<LoginResponesDto>(jsonRespone);
            var StatusPointWallet = JsonConvert.DeserializeObject<int>(jsonRespone2);
            var trangthaividiem = StatusPointWallet.ToString();
            HttpContext.Session.SetString("username", username);
            HttpContext.Session.SetString("AccessToken", loginRespones.Token);
            HttpContext.Session.SetString("StatusPointWallet", trangthaividiem);
            string jwtToken = HttpContext.Session.GetString("AccessToken");
            return RedirectToAction("Index", "Home");
        }
        else return BadRequest("Error");
    }

    [HttpPost]
    public async Task<IActionResult> AccountSignUp(string userName, string email, string password, string fullName)
    {
        UserRegisterDto userLogin = new UserRegisterDto();
        userLogin.UserName = userName;
        userLogin.Email = email;
        userLogin.Password = password;
        userLogin.FullName = fullName;
        userLogin.IsAdmin = false;
        var apiUrl = "https://localhost:44333/User/Register";
        var httpclient = _httpClientFactory.CreateClient("PhuongThaoHttpWeb");
        var requestdata = new StringContent(JsonConvert.SerializeObject(userLogin), Encoding.UTF8, "application/json");
        var respone = await httpclient.PostAsync(apiUrl, requestdata);
        var jsonRespone = await respone.Content.ReadAsStringAsync();
        if (respone.IsSuccessStatusCode)
        {
            var loginRespones = JsonConvert.DeserializeObject<UserRegisterDto>(jsonRespone);
            HttpContext.Session.SetString("username", userName);
            return RedirectToAction("Index", "Home");
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }

    }
    public async Task<IActionResult> LogOut()
    {
        HttpContext.Session.Remove("AccessToken");
        HttpContext.Session.Remove("username");
        //  await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
    public IActionResult LoginWithGoogle(string returnUrl = "/")
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = Url.Action(nameof(GoogleLoginCallback), new { returnUrl })
        };

        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    public async Task<IActionResult> GoogleLoginCallback(string returnUrl = "/")
    {
        var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        if (authenticateResult.Succeeded)
        {


            var email = authenticateResult.Principal.FindFirstValue(ClaimTypes.Email);
            var name = authenticateResult.Principal.FindFirstValue(ClaimTypes.Name);

            // Lưu thông tin đăng nhập vào cookie
            HttpContext.Response.Cookies.Append("access_token", "YOUR_ACCESS_TOKEN", new CookieOptions
            {
                Expires = DateTime.Now.AddHours(3),
            });

            return LocalRedirect(returnUrl);
        }
        else
        {


            return RedirectToAction("Login");
        }
    }
    public IActionResult LoginWithFacebook(string returnUrl = "/")
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = Url.Action(nameof(FacebookLoginCallback), new { returnUrl })
        };

        return Challenge(properties, FacebookDefaults.AuthenticationScheme);
    }

    public async Task<IActionResult> FacebookLoginCallback(string returnUrl = "/")
    {
        var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        if (authenticateResult.Succeeded)
        {

            var email = authenticateResult.Principal.FindFirstValue(ClaimTypes.Email);
            var name = authenticateResult.Principal.FindFirstValue(ClaimTypes.Name);

            HttpContext.Response.Cookies.Append("access_token", "YOUR_ACCESS_TOKEN", new CookieOptions
            {
                Expires = DateTime.Now.AddHours(3),
            });

            return LocalRedirect(returnUrl);
        }
        else
        {

            return RedirectToAction("Login");
        }
    }
}
