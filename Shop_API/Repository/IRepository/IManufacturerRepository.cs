using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface IManufacturerRepository
    {
        Task<bool> Create(Manufacturer obj);
        Task<bool> Update(Manufacturer obj);
        Task<bool> Delete(Guid idobj);

        Task<IEnumerable<Manufacturer>> GetAll();

    }
}
