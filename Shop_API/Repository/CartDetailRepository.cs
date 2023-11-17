using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class CartDetailRepository : ICartDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public CartDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(CartDetail obj)
        {

            if (obj == null)
            {
                return false;
            }
            try
            {
                await _context.CartDetails.AddAsync(obj);
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
            var cartDT = await _context.CartDetails.FindAsync(id);
            if (cartDT == null)
            {
                return false;
            }
            try
            {
                _context.CartDetails.Remove(cartDT);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<CartDetail>> GetAll()
        {
            return await _context.CartDetails.ToListAsync();
        }

        public async Task<CartDetail> GetById(Guid id)
        {
            return await _context.CartDetails.FindAsync(id);
        }

        public async Task<bool> Update(CartDetail obj)
        {
            var cartDT = await _context.CartDetails.FindAsync(obj.Id);
            if (cartDT == null)
            {
                return false;
            }
            try
            {
                cartDT.Quantity = obj.Quantity;
                _context.CartDetails.Update(cartDT);
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
