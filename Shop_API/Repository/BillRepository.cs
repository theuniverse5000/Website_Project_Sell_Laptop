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
                var billDetails = await (
                    from x in _context.Bills.AsNoTracking().Where(a => a.InvoiceCode == invoiceCode)
                    join y in _context.BillDetails.AsNoTracking() on x.Id equals y.BillId
                    //join z in _context.Serials.AsNoTracking() on y.Id equals z.BillDetailId
                    //join o in _context.ProductDetails.AsNoTracking().Where(a => a.Status > 0) on z.ProductDetailId equals o.Id
                    select new BillDetailDto
                    {
                        CodeProductDetail = y.CodeProductDetail,
                        Quantity = y.Quantity,
                        Price = y.Price
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
