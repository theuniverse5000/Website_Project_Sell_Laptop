using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface ISerialDaBanRepository
    {
        Task<bool> Create(SerialDaBan obj);
        Task<bool> Update(SerialDaBan obj);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<SerialDaBan>> GetAll();

    }
}
