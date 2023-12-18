using Shop_Models.Dto;
using Shop_Models.Entities;
namespace Shop_API.Repository.IRepository
{
    public interface IProductDetailRepository
    {
        Task<bool> Create(ProductDetail obj);
        Task<bool> CreateMany(List<ProductDetail> list);
        Task<bool> Update(ProductDetail obj);
        Task<bool> Delete(Guid id);
        Task<List<ProductDetail>> GetAll();
        Task<IEnumerable<ProductDetailDto>> PGetProductDetail(int? getNumber, string? codeProductDetail, int? status, string? search, double? from, double? to, string? sortBy, int? page,
            string? productType, string? namufacturer, string? ram, string? CPU, string? cardVGA);
        Task<bool> UpdateSoLuong(Guid id, int soLuong);
        Task<ProductDetail> GetById(Guid guid);
        Task<ResponseDto> CreateReturnDto(ProductDetail obj);
    }
}
