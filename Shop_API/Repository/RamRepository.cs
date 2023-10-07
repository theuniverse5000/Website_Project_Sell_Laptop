using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class RamRepository : IRamRepository
    {
        private readonly ApplicationDbContext _context;
        public RamRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(Ram obj)
        {
            var checkMa = await _context.Rams.AnyAsync(x => x.Ma == obj.Ma);// tìm mã, trả về true nếu đã có, false nếu chưa có
            if (obj == null || checkMa == true)
            {
                return false;
            }
            try
            {
                await _context.Rams.AddAsync(obj);
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
            var ram = await _context.Rams.FindAsync(id);
            if (ram == null)
            {
                return false;
            }
            try
            {
                ram.TrangThai = 0;
                _context.Rams.Update(ram);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<List<Ram>> GetAllRams()
        {
            var list = await _context.Rams.ToListAsync();// lấy tất cả ram
            var listRam = list.Where(x => x.TrangThai != 0).ToList();// lấy tất cả ram với điều kiện trạng thái khác 0
            return listRam;
        }
        public async Task<bool> Update(Ram obj)
        {
            var ram = await _context.Rams.FindAsync(obj.Id);
            if (ram == null)
            {
                return false;
            }
            try
            {
                ram.Ma = obj.Ma;
                ram.ThongSo = obj.ThongSo;
                //ram.TrangThai = obj.TrangThai;
                _context.Rams.Update(ram);
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
