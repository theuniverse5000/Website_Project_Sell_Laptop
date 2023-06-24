using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext _context;
        public ImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(Image obj)
        {
            var checkMa = await _context.Images.AnyAsync(x => x.Ma == obj.Ma);// tìm mã, trả về true nếu đã có, false nếu chưa có
            if (obj == null || checkMa == true)
            {
                return false;
            }
            try
            {
                await _context.Images.AddAsync(obj);
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
            var img = await _context.Images.FindAsync(id);
            if (img == null)
            {
                return false;
            }
            try
            {
                img.Status = 0;
                _context.Images.Update(img);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Image>> GetAllImage()
        {
            var list = await _context.Images.AsQueryable().Where(x=>x.Status!=0).ToListAsync();// lấy tất cả ram
            return list;
        }

        public async Task<bool> Update(Image obj)
        {
            var img = await _context.Images.FindAsync(obj.Id);
            if (img == null)
            {
                return false;
            }
            try
            {

              
                img.Status = obj.Status;
                img.LinkImage = obj.LinkImage;
                _context.Images.Update(img);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
