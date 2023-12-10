using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Helpers
{
    public static class UserHelper
    {
        public static User ToApplicationUser(UserRegisterDto dto)
        {
            return new User()
            {
                Id = Guid.NewGuid(),
                UserName = dto.UserName,
                NormalizedUserName = dto.UserName.ToUpper(),
                Email = dto.Email,
                NormalizedEmail = dto.Email.ToUpper(),
                FullName = dto.FullName,
            };
        }
    }
}
