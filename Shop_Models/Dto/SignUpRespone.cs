using Microsoft.AspNetCore.Identity;

namespace Shop_Models.Dto
{
    public class SignUpRespone
    {
        public string Mess { get; set; }
        public List<IdentityError>? Data { get; set; }
    }
}