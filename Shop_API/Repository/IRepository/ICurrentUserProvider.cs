using Shop_Models.Dto;

namespace Shop_API.Repository.IRepository
{
    public interface ICurrentUserProvider
    {
        Task<UserDto> GetCurrentUserInfo();
    }
}
