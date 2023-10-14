using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ManufacturerRepository(ApplicationDbContext applicationDb)
        {
            dbContext = applicationDb;
        }

        public async Task<bool> Create(Manufacturer obj)
        {
            var checktt = await dbContext.Manufacturers.AnyAsync(p => p.Name == obj.Name);
            if (obj == null && checktt == true)
            {
                return false;
            }
            try
            {
                await dbContext.Manufacturers.AddAsync(obj);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }

        public async Task<bool> Delete(Guid id)
        {
            var manu = await dbContext.Manufacturers.FindAsync(id);
            if (manu == null)
            {
                return false;
            }
            try
            {

                manu.Status = 0;
                dbContext.Manufacturers.Update(manu);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Manufacturer> GetById(Guid id)
        {
            var result = await dbContext.Manufacturers.FindAsync(id);
            return result;
        }

        public async Task<IEnumerable<Manufacturer>> GetAll()
        {
            var list = await dbContext.Manufacturers.ToListAsync();
            var listx = list.Where(x => x.Status > 0).ToList();
            return listx;
        }

        public async Task<bool> Update(Manufacturer obj)
        {
            var manu = await dbContext.Manufacturers.FindAsync(obj.Id);
            if (manu == null)
            {
                return false;
            }
            try
            {
                manu.Name = obj.Name;
                manu.Status = obj.Status;
                dbContext.Manufacturers.Update(manu);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
