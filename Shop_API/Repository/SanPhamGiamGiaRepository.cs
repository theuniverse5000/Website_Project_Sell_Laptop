using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class SanPhamGiamGiaRepository : ISanPhamGiamGiaRepository
    {
        private readonly ApplicationDbContext _context;
        public SanPhamGiamGiaRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(SanPhamGiamGia obj)
        {
            try
            {
                await _context.SanPhamGiamGias.AddAsync(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            var sanPhamGiamGia = await _context.SanPhamGiamGias.FindAsync(id);
            if (sanPhamGiamGia == null)
            {
                return false;
            }
            try
            {
                _context.SanPhamGiamGias.Remove(sanPhamGiamGia);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<SanPhamGiamGia>> GetAllSanPhamGiamGias()
        {
            return await _context.SanPhamGiamGias.ToListAsync();
        }

        public async Task<bool> Update(SanPhamGiamGia obj)
        {
            var sanPhamGiamGia = await _context.SanPhamGiamGias.FindAsync(obj.Id);

            if (sanPhamGiamGia == null)
            {
                return false;
            }
            try
            {
                sanPhamGiamGia.DonGia = obj.DonGia;
                sanPhamGiamGia.SoTienConLai = obj.SoTienConLai;
                sanPhamGiamGia.TrangThai = obj.TrangThai;
                sanPhamGiamGia.ProductDetailId = obj.ProductDetailId;
                sanPhamGiamGia.GiamGiaId = obj.GiamGiaId;

                _context.SanPhamGiamGias.Update(sanPhamGiamGia);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
