using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class BillRepository : IBillRepository
    {
        private readonly ApplicationDbContext _context;
        public BillRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(Bill obj)
        {
            try
            {
                await _context.Bills.AddAsync(obj);
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
            var bill = await _context.Bills.FindAsync(id);
            if (bill == null)
            {
                return false;
            }
            try
            {
                bill.Status = 0;
                _context.Bills.Update(bill);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Bill>> GetAll()
        {
            return await _context.Bills.ToListAsync();
        }
        public async Task<Bill> GetBillByInvoiceCode(string invoiceCode)
        {
            return await _context.Bills.AsNoTracking().FirstOrDefaultAsync(x => x.InvoiceCode == invoiceCode);
        }

        public async Task<IEnumerable<BillDetailDto>> GetBillDetailByInvoiceCode(string invoiceCode)
        {
            try
            {
                //var billDetails = await (
                //    from x in _context.Bills.AsNoTracking().Where(a => a.InvoiceCode == invoiceCode)
                //    join y in _context.BillDetails.AsNoTracking() on x.Id equals y.BillId
                //    join z in _context.Serials.AsNoTracking() on y.Id equals z.BillDetailId
                //    join o in _context.ProductDetails.AsNoTracking().Where(a => a.Status > 0) on z.ProductDetailId equals o.Id
                //    select new BillDetailDto
                //    {
                //        IdBill = x.Id,
                //        CodeBill = x.InvoiceCode,
                //        MaProductDetail = o.Code,
                //        Price = o.Price,
                //        Status = x.Status,
                //        Description = o.Description,
                //        Quantity = y.Quantity,
                //        ThongSoRam = o.Ram.ThongSo,
                //        TenCpu = o.Cpu.Ten,
                //        ThongSoHardDrive = o.HardDrive.ThongSo,
                //        NameColor = o.Color.Name,
                //        TenCardVGA = o.CardVGA.Ten,
                //        ThongSoCardVGA = o.CardVGA.ThongSo,
                //        KichCoManHinh = o.Screen.KichCo,
                //        TanSoManHinh = o.Screen.TanSo,
                //        ChatLieuManHinh = o.Screen.ChatLieu,
                //        NameProduct = o.Product.Name
                //    }).ToListAsync();

                // left join
                var billDetails = await (
    from x in _context.Bills.AsNoTracking().Where(a => a.InvoiceCode == invoiceCode)
    join y in _context.BillDetails.AsNoTracking() on x.Id equals y.BillId into billDetailsGroup
    from y in billDetailsGroup.DefaultIfEmpty()
    join z in _context.Serials.AsNoTracking() on y.Id equals z.BillDetailId into serialsGroup
    from z in serialsGroup.DefaultIfEmpty()
    join o in _context.ProductDetails.AsNoTracking().Where(a => a.Status > 0) on z.ProductDetailId equals o.Id into productDetailsGroup
    from o in productDetailsGroup.DefaultIfEmpty()
    select new BillDetailDto
    {
        MaProductDetail = o != null ? o.Code : null,
        Price = o != null ? o.Price : 0,
        Status = x.Status,
        Description = o != null ? o.Description : null,
        Quantity = y != null ? y.Quantity : 0,
        ThongSoRam = o != null && o.Ram != null ? o.Ram.ThongSo : null,
        TenCpu = o != null && o.Cpu != null ? o.Cpu.Ten : null,
        ThongSoHardDrive = o != null && o.HardDrive != null ? o.HardDrive.ThongSo : null,
        NameColor = o != null && o.Color != null ? o.Color.Name : null,
        TenCardVGA = o != null && o.CardVGA != null ? o.CardVGA.Ten : null,
        ThongSoCardVGA = o != null && o.CardVGA != null ? o.CardVGA.ThongSo : null,
        KichCoManHinh = o != null && o.Screen != null ? o.Screen.KichCo : null,
        TanSoManHinh = o != null && o.Screen != null ? o.Screen.TanSo : null,
        ChatLieuManHinh = o != null && o.Screen != null ? o.Screen.ChatLieu : null,
        NameProduct = o != null && o.Product != null ? o.Product.Name : null,
        SerialNumber = o != null && o.Serials != null ? o.Serials.FirstOrDefault().SerialNumber : null
    }).ToListAsync();


                return billDetails;
            }
            catch (Exception)
            {
                return null;
            }

        }


        public Task<IEnumerable<BillDetailDto>> GetBillDetail(string username)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> Update(Bill obj)
        {
            var bill = await _context.Bills.FindAsync(obj.Id);
            if (bill == null)
            {
                return false;
            }
            try
            {
                bill.PhoneNumber = obj.PhoneNumber;
                bill.FullName = obj.FullName;
                bill.Address = obj.Address;
                bill.Status = obj.Status;
                _context.Bills.Update(bill);
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
