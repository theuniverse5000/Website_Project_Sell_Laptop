using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.Net.Http.Headers;
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
        var apiUrl3 = $"https://localhost:44333/User/GetUserInfo?usename={username}";
        var respone3 = await httpclient.GetAsync(apiUrl3);
        var jsonRespone3 = await respone3.Content.ReadAsStringAsync();
        if (respone.IsSuccessStatusCode)
        {
            var loginRespones = JsonConvert.DeserializeObject<LoginResponesDto>(jsonRespone);
            var StatusPointWallet = JsonConvert.DeserializeObject<int>(jsonRespone2);
            var info = JsonConvert.DeserializeObject<UserDto>(jsonRespone3);
            var trangthaividiem = StatusPointWallet.ToString();
            HttpContext.Session.SetString("full_name", info.Name);
            HttpContext.Session.SetString("address", info.Address);
            HttpContext.Session.SetString("phoneNumber", info.PhoneNumber);
            HttpContext.Session.SetString("username", username);
            HttpContext.Session.SetString("AccessToken", loginRespones.Token);
            HttpContext.Session.SetString("StatusPointWallet", trangthaividiem);
            string jwtToken = HttpContext.Session.GetString("AccessToken");
            return RedirectToAction("Index", "Home");
        }
        else return BadRequest("Error");
    }

    [HttpPost]
    public async Task<IActionResult> AccountSignUp(string userName, string email, string password, string fullName, string phoneNumber, string address)
    {
        UserRegisterDto userLogin = new UserRegisterDto();
        userLogin.UserName = userName;
        userLogin.Email = email;
        userLogin.Password = password;
        userLogin.FullName = fullName;
        userLogin.PhoneNumber = phoneNumber;
        userLogin.Address = address;
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

    [HttpPost]
    public async Task<IActionResult> EditInfo(string fullName, string phoneNumber, string address)
    {
        try
        {
            // Create a DTO to hold the data
            ChangeContactInfoDto info = new ChangeContactInfoDto
            {
                FullName = fullName,
                NewPhoneNumber = phoneNumber,
                NewAddress = address
            };

            // Get the bearer token from the request cookies
            var accessToken = HttpContext.Session.GetString("AccessToken");

            // Create the HttpClient instance from the factory
            var httpclient = _httpClientFactory.CreateClient("PhuongThaoHttpWeb");

            // Set the authorization header with the bearer token
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // Set the API endpoint
            var apiUrl = "https://localhost:44333/User/ChangeContactInfo";

            // Serialize the DTO to JSON
            var requestdata = new StringContent(JsonConvert.SerializeObject(info), Encoding.UTF8, "application/json");

            // Make the HTTP POST request
            var response = await httpclient.PostAsync(apiUrl, requestdata);

            // Read the response content
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response if needed
                var infoRespones = JsonConvert.DeserializeObject<ChangeContactInfoDto>(jsonResponse);

                // Update session variables
                HttpContext.Session.SetString("full_name", fullName);
                HttpContext.Session.SetString("phoneNumber", phoneNumber);
                HttpContext.Session.SetString("address", address);
                return RedirectToAction("info", "Home");
            }
            else
            {
                // Handle unsuccessful response, maybe log the error
                // Redirect or return to the info action in the Home controller
                return RedirectToAction("info", "Home");
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions, log the error, and redirect to the info action with an error message
            return RedirectToAction("info", "Home", new { error = ex.Message });
        }
    }


    [HttpPost]
    public async Task<IActionResult> ChangePassword(string OldPassword, string NewPassword)
    {
        try
        {
            // Create a DTO to hold the data
            ChangePasswordDto changePassword = new ChangePasswordDto
            {
                OldPassword = OldPassword,
                NewPassword = NewPassword,
            
            };

            // Get the bearer token from the request cookies
            var accessToken = HttpContext.Session.GetString("AccessToken");

            // Create the HttpClient instance from the factory
            var httpclient = _httpClientFactory.CreateClient("PhuongThaoHttpWeb");

            // Set the authorization header with the bearer token
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // Set the API endpoint
            var apiUrl = "https://localhost:44333/User/ChangePassword";

            // Serialize the DTO to JSON
            var requestdata = new StringContent(JsonConvert.SerializeObject(changePassword), Encoding.UTF8, "application/json");

            // Make the HTTP POST request
            var response = await httpclient.PostAsync(apiUrl, requestdata);

            // Read the response content
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response if needed
                var infoRespones = JsonConvert.DeserializeObject<ChangeContactInfoDto>(jsonResponse);

                // Update session variables
              
                return RedirectToAction("info", "Home");
            }
            else
            {
                // Handle unsuccessful response, maybe log the error
                // Redirect or return to the info action in the Home controller
                return RedirectToAction("info", "Home");
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions, log the error, and redirect to the info action with an error message
            return RedirectToAction("info", "Home", new { error = ex.Message });
        }
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
