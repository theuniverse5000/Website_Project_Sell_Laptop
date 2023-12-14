using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface IProductTypeRepository
    {
        Task<bool> Create(ProductType obj);
        Task<bool> Update(ProductType obj);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<ProductType>> GetAll();
        Task<ProductType> GetById(Guid id);
        Task<ResponseDto> CreateReturnDto(ProductType obj);
    }
}
