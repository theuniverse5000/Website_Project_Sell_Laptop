using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface ISerialRepository
    {
        Task<bool> Create(Serial obj);
        Task<bool> CreateMany(List<Serial> listObj);
        Task<bool> Update(Serial obj);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<Serial>> GetAll();
    }
}
