using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly ApplicationDbContext _context;
        public VoucherRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(Voucher obj)
        {
            var checkMa = await _context.Vouchers.AnyAsync(x => x.MaVoucher == obj.MaVoucher);// tìm mã, trả về true nếu đã có, false nếu chưa có
            if (obj == null || checkMa == true)
            {
                return false;
            }
            try
            {
                await _context.Vouchers.AddAsync(obj);
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
            var vou = await _context.Vouchers.FindAsync(id);
            if (vou == null)
            {
                return false;
            }
            try
            {
                _context.Vouchers.Update(vou);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Voucher>> GetAllImage()
        {
            var list = await _context.Vouchers.ToListAsync();// lấy tất cả ram
            return list;
        }

        public async Task<bool> Update(Voucher obj)
        {
            var vou = await _context.Vouchers.FindAsync(obj.Id);
            if (vou == null)
            {
                return false;
            }
            try
            {

                vou.Status = obj.Status;
                vou.TenVoucher = obj.TenVoucher;
                vou.MaVoucher = obj.MaVoucher;
                vou.StarDay = obj.StarDay;
                vou.SoLuong = obj.SoLuong;
                vou.EndDay = obj.EndDay;
                vou.GiaTri = obj.GiaTri;
                vou.Bills = obj.Bills;
                vou.Id = obj.Id;

                _context.Vouchers.Update(vou);
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

