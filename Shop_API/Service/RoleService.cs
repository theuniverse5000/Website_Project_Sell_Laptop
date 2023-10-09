using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Service.IService;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Service
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<Role> _roleManager;

        public RoleService(ApplicationDbContext context, RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<bool> CreatRole(RoleCreateDto p)
        {
            try
            {
                var Role = new Role()
                {
                    Name = p.Name.ToUpper(),
                    Id = Guid.NewGuid(),
                    Status=1,
                    ConcurrencyStamp = p.concurrencyStamp,
                    NormalizedName = p.normalizedName,
                };
                var result = await _roleManager.CreateAsync(Role);

                //if (!result.Succeeded)
                //{
                //    return new ArgumentException("ád");
                //        public object? Result { get; set; }
                //public bool IsSuccess { get; set; } = true;
                //public int Code { get; set; } = 200;
                //public string Message { get; set; } = "Thành công";
                //public int Count { get; set; } = 0;
                //}
                //var respone = new ResponseDto()
                //{
                //    Result = result,
                //    IsSuccess=result.Succeeded,
                //};
                //if(respone.IsSuccess ) 
                //{
                //    respone.Result=result.Errors;
                //}
                return result.Succeeded;
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<bool> DelRole(Guid id)
        {
            try
            {
                var obj = await _roleManager.FindByIdAsync(id.ToString());
                obj.Status = 1;
                await _roleManager.UpdateAsync(obj);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> EditRole(Guid id, RoleUpdateDto roleUpdate)
        {
            try
            {
                var obj = await _roleManager.FindByIdAsync(id.ToString());
                obj.Status = roleUpdate.status;
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

        public async Task<List<Role>> GetAllRole()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }

        public Task<List<Role>> GetAllRoleActive()
        {
            throw new NotImplementedException();
        }

        public async Task<Role> GetRoleById(Guid id)
        {

            return await _roleManager.FindByIdAsync(id.ToString());
        }
    }
}
