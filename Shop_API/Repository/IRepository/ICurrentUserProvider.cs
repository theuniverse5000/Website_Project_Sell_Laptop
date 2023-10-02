using Shop_Models.Dto.User;
using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface ICurrentUserProvider
    {
        Task<UserDto> GetCurrentUserInfo();
    }
}
