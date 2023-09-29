using Shop_Models.Dto.Role;
using Shop_Models.Entities;

namespace Shop_API.Service.IService
{
    public interface IPositionService
    {
        public Task<bool> CreatPosition(RoleCreateDto p);

        public Task<bool> EditPosition(Guid id, RoleUpdateDto roleUpdate);
        public Task<bool> DelPosition(Guid id);
        public Task<List<Position>> GetAllPosition();
        public Task<List<Position>> GetAllPositionActive();
        public Task<Position> GetPositionById(Guid id);
    }
}
