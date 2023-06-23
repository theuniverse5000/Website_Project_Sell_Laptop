using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class ScreenRepository : IScreenRepository
    {
        private readonly ApplicationDbContext _context;
        public ScreenRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(Screen obj)
        {
            var checkMa = await _context.Screens.AnyAsync(x => x.Ma == obj.Ma);// tìm mã, trả về true nếu đã có, false nếu chưa có
            if (obj == null || checkMa == true)
            {
                return false;
            }
            try
            {
                await _context.Screens.AddAsync(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            var screen = await _context.Screens.FindAsync(id);
            if (screen == null)
            {
                return false;
            }
            try
            {
                screen.TrangThai = 0;
                _context.Screens.Update(screen);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Screen>> GetAll()
        {
            var list = await _context.Screens.ToListAsync();// lấy tất cả ram
            var listScreen = list.Where(x => x.TrangThai > 0).ToList();// lấy tất cả ram với điều kiện trạng thái khác 0
            return listScreen;
        }

        public async Task<bool> Update(Screen obj)
        {
            var screen = await _context.Screens.FindAsync(obj.Id);
            if (screen == null)
            {
                return false;
            }
            try
            {
                screen.KichCo = obj.KichCo;
                screen.TanSo = obj.TanSo;
                screen.ChatLieu = obj.ChatLieu;
                screen.TrangThai = obj.TrangThai;
                _context.Screens.Update(screen);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
