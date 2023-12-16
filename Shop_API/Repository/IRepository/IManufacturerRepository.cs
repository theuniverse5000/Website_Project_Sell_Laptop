using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface IManufacturerRepository
    {
        Task<ResponseDto> Create(Manufacturer obj);
        Task<bool> Update(Manufacturer obj);
        Task<bool> Delete(Guid idobj);

        Task<IEnumerable<Manufacturer>> GetAll();

        Task<Manufacturer> GetById(Guid id);

    }
}
