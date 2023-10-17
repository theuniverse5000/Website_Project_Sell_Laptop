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
        if(accessToken != null)
        {
            return Content(accessToken);
        }

        return PartialView("_Login");
    }                                                                                                                                                                                                                               
    [HttpPost]
    public async Task<IActionResult> LoginWithJWT([FromBody]LoginRequestDto  loginRequestDto)
    {
        var apiUrl = "https://localhost:7286/api/Account/Login";
        var httpclient = _httpClientFactory.CreateClient("MyHttpClient");
        var requestdata= new StringContent(JsonConvert.SerializeObject(loginRequestDto),Encoding.UTF8, "application/json");
        var respone = await httpclient.PostAsync(apiUrl, requestdata);
        //var LoginRespones = JsonConvert.DeserializeObject<LoginResponesDto>(respone.Content);
        var jsonRespone = await respone.Content.ReadAsStringAsync();
        var LoginRespones= JsonConvert.DeserializeObject<LoginResponesDto>(jsonRespone);
        // Lưu trữ access token trong một cookie
        HttpContext.Response.Cookies.Append("access_token", LoginRespones.Data.ToString(), new CookieOptions
        {
            Expires = DateTime.Now.AddHours(3), // Thời gian sống của cookie (ở đây là 30 ngày)
        });

        return RedirectToAction("Login");
    }
    public async Task<IActionResult> LogOut()
    {
        //SignOutAsync is Extension method for SignOut
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //Redirect to home page
        return LocalRedirect("/");
    }
}
