using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface ICartDetailRepository
    {
        Task<bool> Create(CartDetail obj);
        Task<bool> Update(CartDetail obj);
        Task<bool> Delete(Guid id);
        Task<CartDetail> GetById(Guid id);
        Task<IEnumerable<CartDetail>> GetAll();

    }
}
