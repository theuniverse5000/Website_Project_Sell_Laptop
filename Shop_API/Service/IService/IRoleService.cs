using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Service.IService
{
    public interface IRoleService
    {
        public Task<bool> CreatRole(RoleCreateDto p);

        public Task<bool> EditRole(Guid id, RoleUpdateDto roleUpdate);
        public Task<bool> DelRole(Guid id);
        public Task<List<Role>> GetAllRole();
        public Task<List<Role>> GetAllRoleActive();
        public Task<Role> GetRoleById(Guid id);
    }
}