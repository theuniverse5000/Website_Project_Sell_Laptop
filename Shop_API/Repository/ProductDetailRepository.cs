using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;
using Shop_Models.ViewModels;

namespace Shop_API.Repository
{
    public class ProductDetailRepository : IProductDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(ProductDetail obj)
        {
            //var checkMa = await _context.ProductDetails.AnyAsync(x => x.Ma == obj.Ma);// tìm mã, trả về true nếu đã có, false nếu chưa có
            //if (obj == null || checkMa == true)
            //{
            //    return false;
            //}
            //try
            //{
            //    await _context.ProductDetails.AddAsync(obj);
            //    await _context.SaveChangesAsync();
            //    return true;
            //}
            //catch (Exception)
            //{
            return false;
            //}
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDetail>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDetailView>> GetAllProductDetail()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDetailView>> GetAllProductDetailById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(ProductDetail obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateSoLuong(Guid id, int soLuong)
        {
            throw new NotImplementedException();
        }
    }
}
