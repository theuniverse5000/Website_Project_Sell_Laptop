using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Service.IService;
using Shop_Models.Dto.Role;
using Shop_Models.Entities;

namespace Shop_API.Service
{
    public class PositionService: IPositionService
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<Position> _roleManager;

        public PositionService(RoleManager<Position> roleManager)
        {
            _roleManager = roleManager;
            _context = new ApplicationDbContext();
        }

        public async Task<bool> CreatPosition(RoleCreateDto p)
        {
            try
            {
                var Role = new Position()
                {
                    Name = p.Name,
                    Id = Guid.NewGuid(),
                    status = 0,
                    ConcurrencyStamp = p.concurrencyStamp,
                    NormalizedName = p.normalizedName,
                };
                var result = await _roleManager.CreateAsync(Role);
                if (result.Succeeded)
                    return true;
                else return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> DelPosition(Guid id)
        {
            try
            {
                var obj = await _roleManager.FindByIdAsync(id.ToString());
                obj.status = 1;
                await _roleManager.UpdateAsync(obj);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> EditPosition(Guid id, RoleUpdateDto roleUpdate)
        {
            try
            {
                var obj = await _roleManager.FindByIdAsync(id.ToString());
                obj.status = roleUpdate.status;
                obj.Name = roleUpdate.Name;
                obj.NormalizedName = roleUpdate.normalizedName;
                obj.ConcurrencyStamp = roleUpdate.concurrencyStamp;
                await _roleManager.UpdateAsync(obj);
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

            public async Task<List<Position>> GetAllPosition()
            {
                return await _context.Roles.ToListAsync();
            }
        public async Task<List<Position>> GetAllPositionActive()
        {
            return await _context.Roles.AsQueryable().Where(p => p.Status != 1).ToListAsync();
        }
        public async Task<Position> GetPositionById(Guid id)
        {

            return await _roleManager.FindByIdAsync(id.ToString());
        }
    }
}
