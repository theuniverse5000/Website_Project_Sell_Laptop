using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface ICartRepository
    {
        Task<bool> Create(Cart obj);
        Task<bool> Update(Cart obj);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<Cart>> GetAll();
        Task<Cart> GetCartByUsername(string username);
        Task<IEnumerable<CartItemDto>> GetCartItem(string username);
        //  Task<IEnumerable<CartItemDto>> GetAllCarts();
    }
}
