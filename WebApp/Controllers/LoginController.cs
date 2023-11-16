using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;
using Shop_Models.Dto;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Azure.Core;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.DotNet.MSIdentity.Shared;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication.Facebook;
using Shop_Models.Entities;

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
        if (accessToken!=null)
        {
            return RedirectToAction("Index", "Home");
        }
        return RedirectToAction("Login", "Home");
    }
    [HttpPost]
    public async Task<IActionResult> LoginWithJWT([FromBody] LoginRequestDto loginRequestDto)
    {
        var apiUrl = "/api/Account/Login";
        var httpclient = _httpClientFactory.CreateClient("PhuongThaoHttpWeb");
        var requestdata = new StringContent(JsonConvert.SerializeObject(loginRequestDto), Encoding.UTF8, "application/json");
        var respone = await httpclient.PostAsync(apiUrl, requestdata);
        //var LoginRespones = JsonConvert.DeserializeObject<LoginResponesDto>(respone.Content);
        var jsonRespone = await respone.Content.ReadAsStringAsync();
        var LoginRespones = JsonConvert.DeserializeObject<LoginResponesDto>(jsonRespone);
        // Lưu trữ access token trong một cookie
        HttpContext.Response.Cookies.Append("access_token", LoginRespones.Data.ToString(), new CookieOptions
        {
            Expires = DateTime.Now.AddHours(3), // Thời gian sống của cookie (ở đây là 30 ngày)
        });

        return RedirectToAction("Index", "Home");
    }
    public async Task<IActionResult> LogOut()
    {
        //SignOutAsync is Extension method for SignOut
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //Redirect to home page
        return LocalRedirect("/");
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

    public async Task<IActionResult> SignUp()
    {
        return PartialView("_SignUp");
    }
    [HttpPost]
    public async void AccountSignUp(
        [FromBody] SignUpDto signUpdata)
    {
        var httpClient = _httpClientFactory.CreateClient("PhuongThaoHttpWeb");
        var apiUrl = "/api/Account/SignUp";
        var requestData = new StringContent(JsonConvert.SerializeObject(signUpdata), Encoding.UTF8, "application/json");
        var respone = await httpClient.PostAsync(apiUrl, requestData);
        var content = JsonConvert.DeserializeObject<ResponseDto>( await respone.Content.ReadAsStringAsync());
        var signUpRespone = JsonConvert.DeserializeObject<SignUpRespone>(content.Result.ToString());
        if (signUpRespone.Mess=="true")
        {
            var userInformation = JsonConvert.DeserializeObject<User>(signUpRespone.Data.ToString());
            LoginRequestDto userLogin = new LoginRequestDto()
            {
                UserName=userInformation.UserName,
                Password=userInformation.Password,
                RememberMe=true,
            };
            RedirectToAction("LoginWithJWT", userLogin);
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
