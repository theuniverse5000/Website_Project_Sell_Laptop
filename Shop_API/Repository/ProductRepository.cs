using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext dbContext;
        public ProductRepository(ApplicationDbContext applicationDb)
        {
          
            dbContext = applicationDb;  
        }
        public async Task<bool> Create(Product obj)
        {
            var x = await dbContext.Products.AnyAsync(p=>p.Name == obj.Name);
            if (x== true && obj == null)
            {
                return false;
            }
            try
            {
                await dbContext.Products.AddAsync(obj);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }

        public async Task<bool> Delete(Guid idobj)
        {
            var x = await dbContext.Products.FindAsync(idobj);
            if (x == null)
            {
                return false;
            }
            try
            {
                x.Status = 0;
                dbContext.Products.Update(x);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var list = await dbContext.Products.ToListAsync();
            var listx = list.Where(x => x.Status > 0).ToList();
            return listx;
        }

        public async Task<bool> Update(Product obj)
        {
            var x = await dbContext.Products.FindAsync(obj.Id);
            if (x == null)
            {
                return false;
            }
            try
            {
                x.Name = obj.Name;
                x.Status = obj.Status;
                x.ManufacturerId = obj.ManufacturerId;
                dbContext.Products.Update(x);
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
