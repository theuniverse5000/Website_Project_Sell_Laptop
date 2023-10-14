using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface IRamRepository
    {
        Task<bool> Create(Ram obj);
        Task<bool> Update(Ram obj);
        Task<bool> Delete(Guid id);
        Task<List<Ram>> GetAllRams();
        Task<Ram> GetById(Guid id);
    }
}
