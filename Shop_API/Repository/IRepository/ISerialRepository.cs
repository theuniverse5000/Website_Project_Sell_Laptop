using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface ISerialRepository
    {
        Task<ResponseDto> Create(Serial obj);
        Task<bool> CreateMany(List<Serial> listObj);
        Task<ResponseDto> Update(Serial obj);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<Serial>> GetAll();
    }
}
