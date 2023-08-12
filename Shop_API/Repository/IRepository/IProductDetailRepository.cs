using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface IProductDetailRepository
    {
        Task<bool> Create(ProductDetail obj);
        Task<bool> Update(ProductDetail obj);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<ProductDetail>> GetAll();
        Task<int> GetCountProductDetail();// Truy vấn ra số lượng sản phẩm là số lượng serial
        Task<IEnumerable<ProductDetailDto>> GetAllProductDetail();
        Task<IEnumerable<ProductDetailDto>> GetProductDetail(string? search, double? from, double? to, string? sortBy, int page = 1);
        //    Task<ProductDetailView> GetAllProductDetailById(Guid id);
        Task<bool> UpdateSoLuong(Guid id, int soLuong);
    }
}
