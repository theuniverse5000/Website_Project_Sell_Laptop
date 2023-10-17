using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class ViDiemRepository : IViDiemRepository
    {
        private readonly ApplicationDbContext _context;
        public ViDiemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(ViDiem obj)
        {
            try
            {
                await _context.ViDiems.AddAsync(obj);
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
            var _deleteVidiem = await _context.ViDiems.FindAsync(Id);
            if (_deleteVidiem == null)
            {
                return false;
            }
            try
            {
                _deleteVidiem.TrangThai = 0;
                _context.ViDiems.Update(_deleteVidiem);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<ViDiem>> GetAllViDiems()
        {
            var allList = await _context.ViDiems.ToListAsync(); // Lấy tất cả danh sách ví điểm
            var list = allList.Where(x => x.TrangThai != 0).ToList(); // lấy danh sách ví điểm với điều kiện trạng thái khác 0 
            return list;
        }

        public async Task<bool> Update(ViDiem obj)
        {
            var _update = await _context.ViDiems.FindAsync(obj.UserId);
            if (_update == null)
            {
                return false;
            }
            try
            {
                _update.SoDiemDaDung = obj.SoDiemDaDung;
                _update.SoDiemDaCong = obj.SoDiemDaCong;
                _update.TongDiem = obj.TongDiem;
                _update.TrangThai = obj.TrangThai;
                _context.ViDiems.Update(_update);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ViDiem> GetViDiemById(Guid? id)
        {
            var result = await _context.ViDiems.FindAsync(id);
            return result;
        }

    }
}
