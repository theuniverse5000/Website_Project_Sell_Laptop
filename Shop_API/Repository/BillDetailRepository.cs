using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class BillDetailRepository : IBillDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public BillDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateBillDetail(BillDetail obj)
        {
            try
            {
                if (obj == null) return false;
                else
                {
                    await _context.BillDetails.AddAsync(obj);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteBillDetail(Guid id)
        {
            try
            {
                var x = await _context.BillDetails.FindAsync(id);
                if (x == null) return false;
                else
                {
                    _context.BillDetails.Remove(x);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<List<BillDetail>> GetAllBillDetails()
        {
            return await _context.BillDetails.ToListAsync();
        }

        public async Task<BillDetail> GetBillDetailById(Guid id)
        {
            return await _context.BillDetails.FindAsync(id);
        }
        public async Task<bool> UpdateBillDetail(BillDetail obj)
        {
            try
            {
                var x = await _context.BillDetails.FindAsync(obj.Id);
                if (x == null) return false;
                else
                {
                    //     x.Quantity = obj.Quantity;
                    x.Price = obj.Price;
                    _context.BillDetails.Update(x);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
