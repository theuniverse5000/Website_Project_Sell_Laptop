using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_API.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace Shop_API.Service
{
    public class GiamGiaHangLoatServices : IGiamGiaHangLoatServices
    {
        private readonly ApplicationDbContext _context; 
        private readonly ISanPhamGiamGiaRepository _sanPhamGiamGiaRepository; 

        public GiamGiaHangLoatServices(ApplicationDbContext context, ISanPhamGiamGiaRepository sanPhamGiamGiaRepository)
        {
            _context = context;
            _sanPhamGiamGiaRepository = sanPhamGiamGiaRepository;
        }

        public void ApplyBulkDiscount(Guid giamGiaId, float percentDiscount)
        {
            var productsToUpdate = _context.SanPhamGiamGias
                .Where(spgg => spgg.GiamGiaId == giamGiaId)
                .Include(spgg => spgg.ProductDetail)
                .ToList();

            foreach (var product in productsToUpdate)
            {
                // Áp dụng giảm giá hàng loạt
                product.ProductDetail.Price *= (1 - (percentDiscount / 100));

                _context.SaveChanges();
            }
        }
    }
}
