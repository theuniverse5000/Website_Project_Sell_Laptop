using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class LichSuTieuDiemRepository : ILichSuTieuDiemRepository
    {
        private readonly ApplicationDbContext _context;

        public LichSuTieuDiemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(LichSuTieuDiem obj)
        {
            //var check = await _context.LichSuTieuDiems.AnyAsync(x=>x.BillId==obj.Id);
            try
            {
                await _context.LichSuTieuDiems.AddAsync(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            var lichSuTieuDiem = await _context.LichSuTieuDiems.FindAsync(id);
            try
            {
                lichSuTieuDiem.TrangThai = 0;
                _context.LichSuTieuDiems.Update(lichSuTieuDiem);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<List<LichSuTieuDiem>> GetAllLichSuTieuDiems()
        {
            var allList = await _context.LichSuTieuDiems.ToListAsync();// Lấy toàn bộ danh sách lịch sử tiêu điểm.
            var list = allList.Where(x => x.TrangThai != 0).ToList(); // lấy danh sách lịch sử tiêu điểm với trạng thái khác 0.
            return list;
        }

        public async Task<bool> Update(LichSuTieuDiem obj)
        {
            var _lichSuTieuDiem = await _context.LichSuTieuDiems.FindAsync(obj.Id);
            if (_lichSuTieuDiem == null) // nếu không tìm thấy lịch sử tiêu điểm với obj.Id thì trả về false.
            {
                return false;
            }
            try
            {
                _lichSuTieuDiem.SoDiemDaDung = obj.SoDiemDaDung;
                _lichSuTieuDiem.NgaySD = obj.NgaySD;
                _lichSuTieuDiem.SoDiemCong = obj.SoDiemCong;
                _lichSuTieuDiem.TrangThai = obj.TrangThai;
                _context.LichSuTieuDiems.Update(_lichSuTieuDiem);
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
