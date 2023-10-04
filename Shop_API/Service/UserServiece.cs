using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Service.IService;
using Shop_Models.Dto;
using Shop_Models.Dto.User;
using Shop_Models.Entities;

namespace Shop_API.Service
{
    public class UserServiece : IUserServiece
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        public UserServiece(UserManager<User> userManager, SignInManager<User> signInManager,
            IConfiguration configuration)
        {
            _context = new ApplicationDbContext();
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<bool> DelUsers(List<Guid> Ids)
        {
            try
            {
                List<User> Users = new List<User>();
                foreach (var id in Ids)
                {
                    Users = await _context.Users.Where(p => p.Id == id).ToListAsync();
                }


                foreach (var item in Users)
                {
                    item.Status = 1;
                }
                _context.Users.UpdateRange(Users);
                await _context.SaveChangesAsync();

                //obj.Status = 1; // ta sẽ kh xóa mà thay đổi trạng thái từ hđ sang kh hđ
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<bool> CreatUser(UserCreateDto p)
        {
            try
            {
                var user = new User()
                {
                    UserName = p.UserName,
                    PhoneNumber = p.PhoneNumber,
                    Status = 0,   // quy uoc 0 có nghĩa là đang hđ
                    Email = p.Email,
                    Password = p.Password,

                };
                var result = await _userManager.CreateAsync(user, p.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Client");
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> DelUser(Guid id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                user.Status = 1;
                _context.Users.Update(user);
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> EditUser(Guid id, UserEditDto p)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                user.UserName = p.UserName;
                user.Password = p.Password;
                user.Status = p.Status;
                user.PhoneNumber = p.PhoneNumber;
                user.Email = p.Email;

                // Lấy các vai trò hiện tại của người dùng
                var currentRoles = await _userManager.GetRolesAsync(user);

                // Chuyển đổi roleIds sang dạng mảng string
                var roleNameArray = p.roleNames.Select(x => x.ToString()).ToArray();

                // Tìm các vai trò cần thêm
                var rolesToAdd = roleNameArray.Except(currentRoles);
                foreach (var item in rolesToAdd)
                {
                    Console.WriteLine(item);
                }
                // Tìm các vai trò cần xóa
                var rolesToRemove = currentRoles.Except(roleNameArray);

                // Thêm các vai trò mới cho người dùng
                await _userManager.AddToRolesAsync(user, rolesToAdd);

                // Xóa các vai trò cần xóa khỏi người dùng
                await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return true;
                }
                return false;


            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<UserDto>> GetAllUser()
        {

            var list = await _context.Users.ToListAsync();
            var ListUserVM = new List<UserDto>();
            foreach (var item in list)
            {
                var User = new UserDto();
                User.UserName = item.UserName;
                User.PhoneNumber = item.PhoneNumber;
                User.Password = item.Password;
                User.Email = item.Email;
                User.Status = item.Status;
                User.Id = item.Id;
                ListUserVM.Add(User);
            }
            foreach (var item in ListUserVM)
            {
                var RolesOfUser = await _userManager.GetRolesAsync(item);
                item.roleNames = string.Join(",", RolesOfUser);
            }
            return ListUserVM.ToList();
        }
        public async Task<List<UserDto>> GetAllUserActive()
        {
            var list = await _context.Users.ToListAsync();
            var ListUserVM = new List<UserDto>();
            foreach (var item in list)
            {
                var User = new UserDto();
                User.UserName = item.UserName;
                User.PhoneNumber = item.PhoneNumber;
                User.Password = item.Password;
                User.Email = item.Email;
                User.Status = item.Status;
                User.Id = item.Id;
                ListUserVM.Add(User);
            }
            foreach (var item in ListUserVM)
            {
                var RolesOfUser = await _userManager.GetRolesAsync(item);
                item.roleNames = string.Join(",", RolesOfUser);
            }
            return ListUserVM.Where(p => p.Status != 1).ToList();
        }
        public async Task<User> GetUserById(Guid id)
        {
            var list = await _context.Users.AsQueryable().ToListAsync();
            return list.FirstOrDefault(c => c.Id == id);
        }
        public async Task<UserDto> GetUserByName(string name)
        {
            User user = await _context.Users.AsQueryable().FirstOrDefaultAsync(c => c.UserName == name);
            UserDto userVM = new UserDto();
            userVM.UserName = user.UserName;
            userVM.PhoneNumber = user.PhoneNumber;
            userVM.Email = user.Email;
            userVM.Id = user.Id;
            userVM.Status = user.Status;
            return userVM;
        }
    }
}
