//using Microsoft.IdentityModel.Tokens;
//using Shop_API.Repository.IRepository;
//using Shop_API.Service;
//using Shop_Models.Dto.User;
//using Shop_Models.Entities;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace Shop_API.Repository
//{
//    public class CurrentUserProvider : ICurrentUserProvider
//    {
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public CurrentUserProvider(IHttpContextAccessor httpContextAccessor)
//        {
//            _httpContextAccessor = httpContextAccessor;

//        }

//        public async Task<UserDto> GetCurrentUserInfo()
//        {
//            var secretKeyBytes = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("theuniverse500"));
//            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
//            var tokenHandler = new JwtSecurityTokenHandler();
//            var tokenValidationParameters = new TokenValidationParameters
//            {
//                ValidateIssuerSigningKey = true,
//                IssuerSigningKey = secretKeyBytes,
//                ValidateIssuer = false,
//                ValidateAudience = false
//            };
//            if (authorizationHeader != null)
//            {
//                var token = authorizationHeader.Replace("Bearer ", string.Empty);
//                if (await AccountService.checkAccessToken(token))
//                {
//                    ClaimsPrincipal principal;
//                    try
//                    {
//                        principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
//                    }
//                    catch (SecurityTokenException)
//                    {
//                        throw;
//                    }
//                }
//            }

//            return null;
//        }
//    }

//}
