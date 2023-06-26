using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;
using Shop_Models.ViewModels;

namespace Shop_API.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(Cart obj)
        {
            var checkId = await _context.Carts.AnyAsync(x => x.UserId == obj.UserId);
            if (obj == null || checkId == true)
            {
                return false;
            }
            try
            {
                await _context.Carts.AddAsync(obj);
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
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return false;
            }
            try
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Cart>> GetAll()
        {
            return await _context.Carts.ToListAsync();
        }

        public async Task<Cart> GetById(Guid id)
        {
            var cart = await _context.Carts.FindAsync(id);
            return cart;
        }

        public Task<IEnumerable<CartItem>> GetCartItem()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(Cart obj)
        {
            var cart = await _context.Carts.FindAsync(obj.UserId);
            if (cart == null)
            {
                return false;
            }
            try
            {
                cart.Description = obj.Description;
                _context.Carts.Update(cart);
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
