using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class HardDriveRepository : IHardDriveRepository
    {
        private readonly ApplicationDbContext _context;

        public HardDriveRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(HardDrive obj)
        {
            var checkMa = await _context.HardDrives.AnyAsync(x => x.Ma == obj.Ma);
            if (obj == null || checkMa)
            {
                return false;
            }
            try
            {
                await _context.HardDrives.AddAsync(obj);
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
            var hardDrive = await _context.HardDrives.FindAsync(id);
            if (hardDrive == null)
            {
                return false;
            }
            try
            {
                hardDrive.TrangThai = 0;
                _context.HardDrives.Update(hardDrive);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<HardDrive>> GetAllHardDrives()
        {
            var list = await _context.HardDrives.ToListAsync();
            var listHardDrives = list.Where(x => x.TrangThai != 0).ToList();
            return listHardDrives;
        }

        public async Task<bool> Update(HardDrive obj)
        {
            var hardDrive = await _context.HardDrives.FindAsync(obj.Id);
            if (hardDrive == null)
            {
                return false;
            }
            try
            {
                hardDrive.ThongSo = obj.ThongSo;
                hardDrive.TrangThai = obj.TrangThai;
                _context.HardDrives.Update(hardDrive);
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
