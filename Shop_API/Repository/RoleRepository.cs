using Shop_API.AppDbContext;
using Shop_Models.Entities;
using Microsoft.EntityFrameworkCore;
using Shop_API.Repository.IRepository;

namespace Shop_API.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RoleRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(Role obj)
        {
            var checkRoleName = await _dbContext.Roles.AnyAsync(x => x.RoleName == obj.RoleName);
            if (obj == null || checkRoleName == true)
            {
                return false;
            }
            try
            {
                await _dbContext.Roles.AddAsync(obj);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            var role = await _dbContext.Roles.FindAsync(id);
            if (role == null)
            {
                return false;
            }
            try
            {
                role.Status = 0;
                _dbContext.Roles.Update(role);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Role>> GetAllRoles()
        {
            var list = await _dbContext.Roles.ToListAsync();// lấy tất cả role
            var listRoles = list.Where(x => x.Status != 0).ToList();// lấy tất cả role với điều kiện trạng thái khác 0
            return listRoles;
        }

        public async Task<bool> Update(Role obj)
        {
            var role = await _dbContext.Roles.FindAsync(obj.Id);
            if (role == null)
            {
                return false;
            }
            try
            {
                role.RoleName = obj.RoleName;
                role.Descripion = obj.Descripion;
                role.Status = obj.Status;
                _dbContext.Roles.Update(role);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
