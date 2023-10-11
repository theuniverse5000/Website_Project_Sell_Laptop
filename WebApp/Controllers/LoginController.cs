using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;
using Shop_Models.Dto;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace WebApp.Controllers;
[AllowAnonymous]
public class LoginController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public LoginController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
        _httpClient = httpClient;

    }
   
    public IActionResult Login(string ReturnUrl = "/")
    {
        var jwtToken = _httpContextAccessor.HttpContext.Session.GetString("_tokenAuthorization");
        LoginRequestDto objLoginModel = new LoginRequestDto();
        objLoginModel.ReturnUrl = ReturnUrl;

        return PartialView("_Login");
    }
    
    [HttpPost]
   
    public async Task<IActionResult> LoginWithJWT(string abc)
    {
        var b = abc;
        //var result = await _httpClient.PostAsJsonAsync("https://localhost:7286/api/Account/Login", loginRequest);
        //if (result.IsSuccessStatusCode)
        //{
            //var token = await result.Content.ReadAsStringAsync();
            //var objToken = JsonConvert.Deserializ
            //eObject<LoginResponesDto>(token);
            //if (objToken != null)
            //{
            //    TokenDto jsonObject = JsonConvert.DeserializeObject<TokenDto>(objToken.Data.ToString());
            //    if (jsonObject != null)
            //    {
            //        _httpContextAccessor.HttpContext.Session.SetString("_tokenAuthorization", jsonObject.AccessToken);
            //        var handler = new JwtSecurityTokenHandler();

            //        var jwt = handler.ReadJwtToken(jsonObject.AccessToken);

            //        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            //        var value = jwt.Claims;
            //        identity.AddClaims(value);


            //        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jsonObject.AccessToken);

                    
            //    }

            //}
        //    return RedirectToPage("/_Host");
        //    return BadRequest("Token is null");
        //}

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
