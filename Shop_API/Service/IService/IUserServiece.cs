using Shop_Models.Dto;
using Shop_Models.Dto.User;
using Shop_Models.Entities;

namespace Shop_API.Service.IService
{
    public interface IUserServiece
    {
        public Task<bool> DelUsers(List<Guid> Ids);
        public Task<bool> CreatUser(UserCreateDto p);
        public Task<bool> EditUser(Guid id, UserEditDto p);
        public Task<bool> DelUser(Guid id);
        public Task<List<UserDto>> GetAllUser();
        public Task<List<UserDto>> GetAllUserActive();
        public Task<User> GetUserById(Guid id);
        public Task<UserDto> GetUserByName(string name);


    }
}
