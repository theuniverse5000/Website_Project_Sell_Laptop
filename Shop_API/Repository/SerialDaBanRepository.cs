using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class SerialDaBanRepository : ISerialDaBanRepository
    {
        private readonly ApplicationDbContext _context;
        public SerialDaBanRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(SerialDaBan obj)
        {
            var checkSerialDaBan = await _context.SerialDaBans.AnyAsync(x => x.SerialNumber == obj.SerialNumber);
            if (checkSerialDaBan == true || obj == null)
            {
                return false;
            }
            try
            {
                await _context.SerialDaBans.AddAsync(obj);
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
            var serialDaBan = await _context.SerialDaBans.FindAsync(id);
            if (serialDaBan == null)
            {
                return false;
            }
            try
            {
                serialDaBan.Status = 0;
                _context.SerialDaBans.Update(serialDaBan);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<SerialDaBan>> GetAll()
        {
            var listAll = await _context.SerialDaBans.ToListAsync();
            var listSerialDaban = listAll.Where(x => x.Status > 0).ToList();
            return listSerialDaban;
        }

        public async Task<bool> Update(SerialDaBan obj)
        {
            var serialDaBan = await _context.SerialDaBans.FindAsync(obj.Id);
            if (serialDaBan == null || obj == null)
            {
                return false;
            }

            try
            {
                serialDaBan.SerialNumber = obj.SerialNumber;
                serialDaBan.Status = obj.Status;
                _context.SerialDaBans.Update(serialDaBan);
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
