using Shop_Models.Entities;
using Shop_Models.ViewModels;

namespace Shop_API.Repository.IRepository
{
    public interface ICartRepository
    {
        Task<bool> Create(Cart obj);
        Task<bool> Update(Cart obj);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<Cart>> GetAll();
        Task<Cart> GetById(Guid id);
        Task<IEnumerable<Cart>> GetCartItem();
    }
}
