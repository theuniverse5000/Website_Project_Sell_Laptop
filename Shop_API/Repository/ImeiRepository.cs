using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class ImeiRepository : IImeiRepository
    {
        private readonly ApplicationDbContext _context;
        public ImeiRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(Imei obj)
        {
            var checkMa = await _context.Imeis.AnyAsync(x => x.SoImei == obj.SoImei);
            if (obj == null || checkMa == true)
            {
                return false;
            }
            try
            {
                await _context.Imeis.AddAsync(obj);
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
            var imei = await _context.Imeis.FindAsync(id);
            if (imei == null)
            {
                return false;
            }
            try
            {
                imei.TrangThai = 0;
                _context.Imeis.Update(imei);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Imei>> GetAll()
        {
            var list = await _context.Imeis.ToListAsync();
            var listImei = list.Where(x => x.TrangThai > 0).ToList();
            return listImei;
        }

        public async Task<bool> Update(Imei obj)
        {
            var imei = await _context.Imeis.FindAsync(obj.Id);
            if (imei == null)
            {
                return false;
            }
            try
            {
                imei.SoImei = obj.SoImei;
                imei.TrangThai = obj.TrangThai;
                _context.Imeis.Update(imei);
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
