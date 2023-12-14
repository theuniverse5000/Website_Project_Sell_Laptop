using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(ProductType obj)
        {
            var check = await _context.ProductTypes.AnyAsync(x => x.Name == obj.Name);
            if (check || obj == null)
            {
                return false;
            }
            try
            {
                await _context.ProductTypes.AddAsync(obj);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ResponseDto> CreateReturnDto(ProductType obj)
        {
            //obj.Name = string.Join("", obj.Name.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));  
            obj.Name = obj.Name.TrimEnd();

            var checkMa = await _context.ProductTypes.AnyAsync(x => x.Name == obj.Name);
            if (obj == null || checkMa == true)
            {
                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = false,
                    Code = 400,
                    Message = "Trùng Mã",
                };
            }
            try
            {
                await _context.ProductTypes.AddAsync(obj);
                await _context.SaveChangesAsync();
                return new ResponseDto
                {
                    Result = obj,
                    IsSuccess = true,
                    Code = 200,
                    Message = "Thêm thành công",
                };
            }
            catch (Exception)
            {
                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = false,
                    Code = 500,
                    Message = "Lỗi Hệ Thống",
                };
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            var productType = await _context.ProductTypes.FindAsync(id);
            if (productType == null)
            {
                return false;
            }
            try
            {
                productType.Status = 0;
                _context.ProductTypes.Update(productType);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<ProductType>> GetAll()
        {
            var listAll = await _context.ProductTypes.ToListAsync();
            var list = listAll.Where(x => x.Status > 0).ToList();
            return list;
        }

        public async Task<ProductType> GetById(Guid id)
        {
            var result = await _context.ProductTypes.FindAsync(id);
            return result;
        }

        public async Task<bool> Update(ProductType obj)
        {
            obj.Name = obj.Name.TrimEnd();
            var productType = await _context.ProductTypes.FindAsync(obj.Id);
            if (obj == null || productType == null)
            {
                return false;
            }
            try
            {
                productType.Name = obj.Name;
                productType.Status = obj.Status;
                _context.ProductTypes.Update(productType);
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
