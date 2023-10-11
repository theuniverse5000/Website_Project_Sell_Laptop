using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.Security.Claims;
namespace WebApp.Controllers;

public class AuthenticateController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public AuthenticateController(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult ExternalLogin(string provider, string returnUrl = "")
    {
        var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Authenticate", new { returnUrl });
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        return Challenge(properties, provider);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ExternalLoginCallback(string returnUrl = "")
    {
        try
        {
            var external = await _signInManager.GetExternalLoginInfoAsync();

            var user = new UserDto()
            {
                Email = external.Principal.FindFirstValue(ClaimTypes.Email),
                Picture = GetAvatarLink(external),
                Provider = external.ProviderDisplayName,
                Name = external.Principal.Identity?.Name ?? ""
            };

            var userExists = await _userManager.FindByEmailAsync(user.Email);

            if (userExists != null)
            {
                await _signInManager.SignInAsync(userExists, true);
            }
            else
            {
                var identityUser = new User()
                {
                    Email = user.Email,
                    UserName = user.Email.Split("@")[0],
                    NormalizedEmail = user.Email.ToUpper(),
                    NormalizedUserName = user.Email.Split("@")[0].ToUpper(),
                };

                const string defaultPassword = "PhuongThao@123";

                await _userManager.CreateAsync(identityUser, defaultPassword);

                var newUser = await _userManager.FindByEmailAsync(user.Email);

                await _signInManager.SignInAsync(newUser, true);
            }

            // TO DO HERE
            // Code tiếp phần lưu các thông tin khác người dùng ở đây

            return View("UserInformation", user);
        }
        catch (Exception)
        {
            return new RedirectResult($"~/#error-oauth2");
        }
    }

    private static string GetAvatarLink(ExternalLoginInfo info)
    {
        const string defaultAvatar = "https://i.imgur.com/LIorKnU.jpg";
        var loginProvider = info.LoginProvider.ToLower();
        var avatarLink = "";
        var claims = info.Principal.Claims;
        var gg = claims.SingleOrDefault(c => c.Type == "Picture")?.Value;

        avatarLink = loginProvider switch
        {
            "facebook" => $"https://graph.facebook.com/{info.ProviderKey}/picture?type=large",
            "google" => string.IsNullOrEmpty(gg) ? defaultAvatar : gg,
            _ => defaultAvatar
        };

        return avatarLink;
    }
}

