using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(User obj)
        {
            var _user = await _context.Users.AnyAsync(x => x.Username == obj.Username); // tìm đối tượng có cùng tên đăng nhập
            if (obj == null || _user == true) // nếu đối tượng tồn tại hoặc giá trị truyền vào rỗng thì trả về false.
            {
                return false;
            }
            try
            {
                await _context.Users.AddAsync(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Guid Id)
        {
            // tạo 1 biến _deleteUser và gán cho nó kết quả của đoạn mã lấy 1 bản ghi
            // từ bảng cơ sở dữ liệu"_context.Users" với phương thức FindAsync bằng khóa chính Id
            var _deleteUser = await _context.Users.FindAsync(Id);
            if (_deleteUser == null)
            {
                return false;
            }
            try
            {
                _deleteUser.Status = 0;
                _context.Users.Update(_deleteUser);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<List<User>> GetAllUsers()
        {
            var allList = await _context.Users.ToListAsync();// Lấy tất cả danh sách user
            var list = allList.Where(x => x.Status != 0).ToList();// lấy tất cả danh sách user với điều kiện status khác 0.
            return list;
        }

        public async Task<bool> Update(User obj)
        {
            // tạo 1 biến _update và gán cho nó kết quả của đoạn mã lấy 1 bản ghi
            // từ bảng cơ sở dữ liệu"_context.Users" với phương thức FindAsync bằng khóa chính obj.Id
            var _update = await _context.Users.FindAsync(obj.Id);
            if (_update == null) // Nếu đối tượng muốn tìm rỗng thì trả về null
            {
                return false;
            }
            try
            {
                _update.Password = obj.Password;
                _update.FullName = obj.FullName;
                _update.Address = obj.Address;
                _update.PhoneNumber = obj.PhoneNumber;
                _update.Status = obj.Status;
                _context.Users.Update(_update);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
