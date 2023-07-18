using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class SerialRepository : ISerialRepository
    {
        private readonly ApplicationDbContext _context;
        public SerialRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(Serial obj)
        {
            var checkMa = await _context.Serials.AnyAsync(x => x.SerialNumber == obj.SerialNumber);
            if (obj == null || checkMa == true)
            {
                return false;
            }
            try
            {
                await _context.Serials.AddAsync(obj);
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
            var Serial = await _context.Serials.FindAsync(id);
            if (Serial == null)
            {
                return false;
            }
            try
            {
                Serial.Status = 0;
                _context.Serials.Update(Serial);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Serial>> GetAll()
        {
            var list = await _context.Serials.ToListAsync();
            var listSerial = list.Where(x => x.Status > 0).ToList();
            return listSerial;
        }

        public async Task<bool> Update(Serial obj)
        {
            var Serial = await _context.Serials.FindAsync(obj.Id);
            if (Serial == null)
            {
                return false;
            }
            try
            {
                Serial.SerialNumber = obj.SerialNumber;
                Serial.Status = obj.Status;
                _context.Serials.Update(Serial);
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
