using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class QuyDoiDiemRepository : IQuyDoiDiemRepository
    {
        private readonly ApplicationDbContext _context;
        public QuyDoiDiemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(QuyDoiDiem obj)
        {
            try
            {
                await _context.QuyDoiDiems.AddAsync(obj);
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
            var quyDoiDiem = await _context.QuyDoiDiems.FindAsync(id);
            if (quyDoiDiem == null)
            {
                return false;
            }
            try
            {
                quyDoiDiem.TrangThai = 0;
                _context.QuyDoiDiems.Update(quyDoiDiem);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<QuyDoiDiem>> GetAllQuyDoiDiems()
        {
            var allList = await _context.QuyDoiDiems.ToListAsync(); // Lấy tất cả dữ liệu quy đổi điểm
            var list = allList.Where(x => x.TrangThai != 0).ToList();// Lấy tất cả dữ liệu quy đổi điểm với điều kiện trạng thái khác 0
            return list;
        }

        public async Task<bool> Update(QuyDoiDiem obj)
        {
            // tạo 1 biến quyDoiDiem và gán kết quả của đoạn mã lấy 1 bản ghi
            // từ bảng cơ sở dữ liệu "_context.QuyDoiDiems" với phương thức FindAsync dựa trên khóa chính obj.Id.
            var quyDoiDiem = await _context.QuyDoiDiems.FindAsync(obj.Id);

            if (quyDoiDiem == null) // Nếu quyDoiDiem rỗng thì trả về false.
            {
                return false;
            }
            try
            {
                quyDoiDiem.TienTieuDiem = obj.TienTieuDiem;
                quyDoiDiem.TienTichDiem = obj.TienTichDiem;
                quyDoiDiem.TrangThai = obj.TrangThai;
                _context.QuyDoiDiems.Update(quyDoiDiem);
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
