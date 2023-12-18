using Azure;
using MailKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using Shop_API.Constants;
using Shop_API.Helpers;
using Shop_API.Repository;
using Shop_API.Repository.IRepository;
using Shop_API.Service.IService;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.Net;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Shop_API.Controllers
{
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly IPagingRepository _iPagingRepository;
        private readonly ResponseDto _reponse;
        private readonly IUserRepository _repository;
        private readonly ISendMailService _sendMailService;


        public UserController(UserManager<User> userManager, RoleManager<Role> roleManager, IUserRepository repository, IConfiguration config, IPagingRepository iPagingRepository, ISendMailService iSendMail)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _repository = repository;
            _config = config;
            _sendMailService = iSendMail;
            _reponse = new ResponseDto();
            _iPagingRepository = iPagingRepository;
        }

        [HttpPost]
        public async Task<IActionResult> InitRole()
        {
            foreach (var role in SecurityRoles.Roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new Role
                    {
                        Name = role,
                        NormalizedName = role.ToUpper(),
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                    });
                }
            }

            return Ok("Done");
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            var existUsername = await _userManager.FindByNameAsync(dto.UserName);

            if (existUsername != null) return new BadRequestObjectResult($"Username {dto.UserName} has already been taken");

            var appUser = UserHelper.ToApplicationUser(dto);
            appUser.Status = 1; 

            var result1 = await _userManager.CreateAsync(appUser, dto.Password);

            if (!result1.Succeeded) return new BadRequestObjectResult(result1.Errors);

            List<string> roles = new();

            if (dto.IsAdmin) roles.AddRange(SecurityRoles.Roles.ToList());
            else roles.Add("User");

            var result2 = await _userManager.AddToRolesAsync(appUser, roles);

            if (!result2.Succeeded) return new BadRequestObjectResult(result2.Errors);

            // Generate email confirmation token
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);

            var encodedToken = WebUtility.UrlEncode(token);


            // Compose the email content
            var mailContent = new EmailDto
            {
                To = appUser.Email,
                Subject = "Confirm your email",
                Body = $"Please confirm your account by clicking <a href='https://localhost:44333/User/ConfirmEmail?userId={appUser.Id}&token={encodedToken}'>here</a>."
            };

            // Send the confirmation email
            await _sendMailService.SendMail(mailContent);

            return Ok(new { Message = "Registration successful. Please check your email to confirm your account." });
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(Guid userId, string token)
        {
            if (userId == Guid.Empty || string.IsNullOrEmpty(token))
            {
                return BadRequest("Invalid token or user ID");
            }

            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return BadRequest("User not found");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                // Xác nhận email thành công, có thể thực hiện các hành động khác nếu cần
                return Ok("Email confirmed successfully");
            }
            else
            {
                // Xác nhận email thất bại
                return BadRequest("Email confirmation failed");
            }
        }

        [HttpGet]
        public IActionResult GetUsersFSP(string? search, double? from, double? to, string? sortBy, int page)
        {
            _reponse.Result = _iPagingRepository.GetAllUser(search, from, to, sortBy, page);
            var count = _reponse.Count = _iPagingRepository.GetAllUser(search, from, to, sortBy, page).Count;
            return Ok(_reponse);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangeContactInfo([FromBody] ChangeContactInfoDto dto)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            // Update address if provided
            if (!string.IsNullOrEmpty(dto.NewAddress))
            {
                currentUser.Address = dto.NewAddress;
            }

            // Update phone number if provided
            if (!string.IsNullOrEmpty(dto.NewPhoneNumber))
            {
                currentUser.PhoneNumber = dto.NewPhoneNumber;
            }

            // Save changes
            var result = await _userManager.UpdateAsync(currentUser);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "Contact information changed successfully" });
        }

        [HttpPost]
        [Authorize] // Assuming only authenticated users can change their password
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            // Get the current user
            var currentUser = await _userManager.GetUserAsync(User);

            // Verify the old password
            var passwordVerificationResult = await _userManager.CheckPasswordAsync(currentUser, dto.OldPassword);

            if (!passwordVerificationResult)
            {
                return BadRequest("Invalid old password");
            }

            // Change the password
            var result = await _userManager.ChangePasswordAsync(currentUser, dto.OldPassword, dto.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "Password changed successfully" });
        }


    }
}
