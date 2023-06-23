using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface IImeiRepository
    {
        Task<bool> Create(Imei obj);
        Task<bool> Update(Imei obj);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<Imei>> GetAll();
    }
}
