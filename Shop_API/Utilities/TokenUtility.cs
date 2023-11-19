using System.IdentityModel.Tokens.Jwt;

namespace Shop_API.Utilitities
{
    public static class TokenUtility
    {
        public static (Guid userId, string userName) GetTokenInfor(HttpRequest httpRequest)
        {
            var access_token= httpRequest.Headers.Authorization.ToString();
            var accessToken = access_token.Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(accessToken) as JwtSecurityToken;
            Guid userId = Guid.Parse(jsonToken?.Claims?.FirstOrDefault(c => c.Type == "Id")?.Value);
            var username = jsonToken?.Claims?.FirstOrDefault(c => c.Type == "userName")?.Value;
            return (userId, username);
        }
    }
}
