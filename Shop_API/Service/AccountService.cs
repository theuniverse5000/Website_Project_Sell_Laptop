
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shop_API.AppDbContext;
using Shop_API.Service.IService;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Shop_API.Service
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IRoleService _roleService;
        private static readonly string key = "theuniverse500";
        public AccountService(RoleManager<Role> roleManager, UserManager<User> userManager, SignInManager<User> signInManager, IRoleService roleService, IConfiguration configuration, ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleService= roleService;
            _roleManager=roleManager;
        }

        public async Task<LoginResponesDto> Validate(LoginRequestDto loginRequest)
        {
            //cấp token
            var token = await GenerateToken(loginRequest);
            var respone = new LoginResponesDto();
            if (token==null)
            {
                respone.Mess="Login Fail";
                respone.Successful=false;
                respone.Data=token;
            }
            else
            {
                respone.Mess="Login Successfull";
                respone.Successful=true;
                respone.Data=token;
            }

            return respone;
        }

        public async Task<TokenDto> GenerateToken(LoginRequestDto loginRequest)
        {
            var user = await _userManager.FindByNameAsync(loginRequest.UserName);
            if (user == null || await _userManager.CheckPasswordAsync(user, loginRequest.Password) == false)
            {
                return new TokenDto();
            }
            var secretKeyBytes = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var rolesOfUser = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,loginRequest.UserName,loginRequest.Password),
                new Claim("Id",user.Id.ToString(),user.RoleId.ToString()),
                new Claim("userName",user.UserName.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            };
            foreach (var role in rolesOfUser)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var ClaimIdentity = new ClaimsIdentity(claims);



            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(ClaimIdentity),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(secretKeyBytes, SecurityAlgorithms.HmacSha256),
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                IssuedAt = DateTime.UtcNow
            };
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var accessToken = jwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();
            var check = await _context.Tokens.FirstOrDefaultAsync(p => p.UserId == user.Id);
            if (check != null)
            {
                refreshToken = check.RefreshToken;
            }
            else
            {
                //Lưu database
                var refreshTokenEntity = new Token
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    RefreshToken = refreshToken,
                    IsUsed = false,
                    IsRevoked = false,
                    Iaced = DateTime.UtcNow,
                    Expired = DateTime.UtcNow.AddHours(1),

                };

                await _context.AddAsync(refreshTokenEntity);
                await _context.SaveChangesAsync();
            }

            //// lưu thông tin người dùng vào redis server
            //user.AccessToken = accessToken;
            //await cacheData.SetObj(keyUserQuanTri(user.Id.ToString(), user.UserName), user, 30);

            return new TokenDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

        }

        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }
        private static DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();

            return dateTimeInterval;
        }

        public static async Task<bool> checkAccessToken(string AccessToken)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            //CacheData cacheData = CacheData.Instance;
            byte[] secretKeyBytes = Encoding.UTF8.GetBytes(key);
            var tokenValidateParam = new TokenValidationParameters
            {
                //tự cấp token
                ValidateIssuer = false,
                ValidateAudience = false,

                //ký vào token
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

                ClockSkew = TimeSpan.Zero,

                ValidateLifetime = false
            };
            //task 1: accesstoken valid format
            var tokenInVerification = jwtTokenHandler.ValidateToken(AccessToken, tokenValidateParam, out var validatedToken);

            //task 2: check alg
            if (validatedToken is JwtSecurityToken jwtSecurityToken)
            {
                var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
                if (!result)//false
                {
                    return false;
                }
            }

            //task 3: check accessToken expire
            var utcExpireDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expireDate = ConvertUnixTimeToDateTime(utcExpireDate);
            if (expireDate > DateTime.UtcNow)
            {
                return false;
            }
            var simplePrinciple = tokenInVerification;
            var identity = simplePrinciple?.Identity as ClaimsIdentity;

            if (identity == null)
                return false;

            if (!identity.IsAuthenticated)
                return false;

            IEnumerable<Claim> claims = identity.Claims;
            var username = claims.Where(p => p.Type == "userName").FirstOrDefault()?.Value;
            var Id = claims.Where(p => p.Type == "Id").FirstOrDefault()?.Value;


            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(Id))
                return false;

            //// check keyUser
            //if (!await cacheData.IsKeyExists(keyUserQuanTri(Id, username))) return false;
            return true;
        }


        public async Task<LoginResponesDto> RenewToken(TokenDto tokenDTO)
        {
            try
            {

                if (await checkAccessToken(tokenDTO.AccessToken))
                {
                    // task 4: check refresktoken exist in DB
                    Token storedToken = _context.Tokens.FirstOrDefault(x => x.RefreshToken == tokenDTO.RefreshToken);
                    if (storedToken != null)
                    {
                        // task 5 check refresh token isuser/revoked  
                        var user = await _context.Users.FirstOrDefaultAsync(p => p.Id == storedToken.UserId);
                        if (user != null)
                        {
                            LoginRequestDto loginRequestVM = new LoginRequestDto();
                            loginRequestVM.UserName = user.UserName;
                            loginRequestVM.Password = user.Password;
                            if (!storedToken.IsUsed && user.Id == storedToken.UserId && !storedToken.IsRevoked && !storedToken.IsActive && storedToken.Expired >= DateTime.UtcNow || storedToken.Expired >= DateTime.UtcNow)
                            {
                                // update
                                storedToken.IsRevoked = true;
                                storedToken.IsUsed = true;
                                storedToken.IsActive = true;
                                _context.Update(storedToken);
                                await _context.SaveChangesAsync();
                                var tokenValidate = await GenerateToken(loginRequestVM);

                                return new LoginResponesDto { Successful = true, Mess = "Successful", Data = tokenValidate };
                            }
                        }

                    }
                    return new LoginResponesDto { Successful = false, Mess = "Token is not in DB" };

                }
                return new LoginResponesDto { Successful = false, Mess = "Token is worrong" };
            }
            catch
            {
                return new LoginResponesDto { Successful = false, Mess = "Some Thing Error" };
            }
        }

        public async Task<SignUpRespone> SignUp(SignUpDto p)
        {
            try
            {
                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    UserName = p.UserName,
                    PhoneNumber = p.PhoneNumber,
                    Status = 0,   // quy uoc 0 có nghĩa là đang hđ
                    Address = p.DiaChi,
                    Password = p.Password,
                    RoleId=p.IdRole
                };
                var result = await _userManager.CreateAsync(user, p.Password);
                SignUpRespone res = new SignUpRespone();
                if (result.Succeeded)
                {
                    var role = await _roleService.GetRoleById(p.IdRole);
                    //await _rolema.AddToRoleAsync(user, role.Name);
                    await _userManager.AddToRoleAsync(user,role.Name);
                    res.Mess=result.Succeeded.ToString();
                    res.Data=null;
                }
                else
                {
                    res.Mess=result.Succeeded.ToString();
                    res.Data = new List<IdentityError>();
                  foreach(var er in result.Errors)
                    {
                        if (er==null) continue;
                        res.Data.Add(er); 
                    }

                }
                return res;
            }
          
            catch (Exception ex)
            {
                
                throw ex; 
            }
        }

    }
}
