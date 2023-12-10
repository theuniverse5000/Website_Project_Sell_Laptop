using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace Shop_API.Repository
{
    public class ProductDetailRepository : IProductDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public static int Page_Size { get; set; } = 10;
        private readonly ResponseDto responseDto;
        public ProductDetailRepository(ApplicationDbContext context)
        {
            _context = context;
            responseDto = new ResponseDto();
        }
        public async Task<List<ProductDetail>> GetAll()
        {

            return await _context.ProductDetails.Where(x => x.Status > 0).ToListAsync();
        }
        public async Task<bool> Create(ProductDetail obj)
        {
            var checkMa = await _context.ProductDetails.AnyAsync(x => x.Code == obj.Code);
            if (obj == null || checkMa == true)
            {
                return false;
            }
            try
            {

                await _context.ProductDetails.AddAsync(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ResponseDto> CreateReturnDto(ProductDetail obj)
        {
            var checkMa = await _context.ProductDetails.AnyAsync(x => x.Code == obj.Code);
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
            if (obj.ProductId==Guid.Empty)
            {
                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = false,
                    Code = 400,
                    Message = "Bắt buộc phải chọn tên sản phẩm",

                };
            }
            if (obj.ImportPrice == 0 || obj.Price == 0)
            {
                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = false,
                    Code = 400,
                    Message = "Giá phải lớn hơn 0",

                };
            }

            try
            {

                await _context.ProductDetails.AddAsync(obj);
                await _context.SaveChangesAsync();
                return new ResponseDto
                {
                    Result = obj,
                    IsSuccess = true,
                    Code = 200,
                    Message = "Thành Công",

                };
            }
            catch (Exception)
            {
                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = false,
                    Code = 500,
                    Message = "Lỗi hệ thống",

                };
            }
        }


        public async Task<bool> CreateMany(List<ProductDetail> list)
        {
            foreach (var i in list)
            {
                var checkMa = await _context.ProductDetails.AnyAsync(x => x.Code == i.Code);
                if (checkMa == true)
                {
                    return false;
                }
            }
            try
            {
                await _context.ProductDetails.AddRangeAsync(list);
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
            var productDetail = await _context.ProductDetails.FindAsync(id);
            if (productDetail == null)
            {
                return false;
            }
            try
            {
                productDetail.Status = 0;
                _context.ProductDetails.Update(productDetail);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private int GetCountProductDetail(string codeProductDetail)
        {
            int getCount = _context.ProductDetails.Where(x => x.Status > 0 && x.Code == codeProductDetail)
           .Join(_context.Serials, a => a.Id, b => b.ProductDetailId, (a, b) => new ProductDetail
           {
               Id = a.Id
           })
           .Count();
            return getCount;
        }
        public async Task<IEnumerable<ProductDetailDto>> PGetProductDetail2(int? getNumber, string? codeProductDetail, int? status, string? search, double? from, double? to, string? sortBy, int page = 10)
        {
            var query = _context.ProductDetails
          .AsNoTracking()
          .Include(a => a.Imagess)
          .Where(a => (status == null || a.Status == status) && (codeProductDetail != null ? a.Code == codeProductDetail : true))
          .Select(a => new ProductDetailDto
          {
              Id = a.Id,
              Code = a.Code,
              ImportPrice = a.ImportPrice,
              Price = a.Price,
              Status = a.Status,
              Upgrade = a.Upgrade,
              Description = a.Description,
              AvailableQuantity = 1,
              ThongSoRam = a.Ram.ThongSo,
              MaRam = a.Ram.Ma,
              TenCpu = a.Cpu.Ten,
              MaCpu = a.Cpu.Ma,
              ThongSoHardDrive = a.HardDrive.ThongSo,
              MaHardDrive = a.HardDrive.Ma,
              NameColor = a.Color.Name,
              MaColor = a.Color.Ma,
              MaCardVGA = a.CardVGA.Ma,
              TenCardVGA = a.CardVGA.Ten,
              ThongSoCardVGA = a.CardVGA.ThongSo,
              MaManHinh = a.Screen.Ma,
              KichCoManHinh = a.Screen.KichCo,
              TanSoManHinh = a.Screen.TanSo,
              ChatLieuManHinh = a.Screen.ChatLieu,
              NameProduct = a.Product.Name,
              NameProductType = a.Product.ProductType.Name,
              NameManufacturer = a.Product.Manufacturer.Name,
              LinkImage = _context.Images
                    .Where(image => image.ProductDetailId == a.Id && image.Ma == "Anh1")
                    .Select(image => image.LinkImage)
                    .FirstOrDefault(),
              OtherImages = _context.Images
                    .Where(image => image.ProductDetailId == a.Id && image.Ma != "Anh1")
                    .Select(image => image.LinkImage)
                    .ToList(),

          });
            if (getNumber > 0)
            {
                query = query.Take(Convert.ToInt32(getNumber));
            }
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.NameProduct.Contains(search));
            }
            if (from.HasValue)
            {
                query = query.Where(x => x.Price >= from);
            }
            if (to.HasValue)
            {
                query = query.Where(x => x.Price <= to);
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "nameproduct_desc":
                        query = query.OrderByDescending(x => x.NameProduct);
                        break;
                    case "price_asc":
                        query = query.OrderBy(x => x.Price);
                        break;
                    case "price_desc":
                        query = query.OrderByDescending(x => x.Price);
                        break;
                }
            }

            var pageSize = Page_Size;
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            page = Math.Clamp(page, 1, totalPages);
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var result = await query.ToListAsync();

            foreach (var productDetail in result)
            {
                productDetail.AvailableQuantity = GetCountProductDetail(productDetail.Code);
            }

            return result;
        }


        public async Task<IEnumerable<ProductDetailDto>> PGetProductDetail(int? getNumber, string? codeProductDetail, int? status, string? search, double? from, double? to, string? sortBy, int page = 10)
        {
            var query = _context.ProductDetails
                .AsNoTracking()
                .Include(a => a.Imagess)
                .Where(a => (status == null || a.Status == status) && (codeProductDetail != null ? a.Code == codeProductDetail : true))
                .Select(a => new ProductDetailDto
                {
                    Id = a.Id,
                    Code = a.Code,
                    ImportPrice = a.ImportPrice,
                    Price = a.Price,
                    Status = a.Status,
                    Upgrade = a.Upgrade,
                    Description = a.Description,
                    AvailableQuantity = 1,
                    ThongSoRam = a.Ram.ThongSo,
                    MaRam = a.Ram.Ma,
                    TenCpu = a.Cpu.Ten,
                    MaCpu = a.Cpu.Ma,
                    ThongSoHardDrive = a.HardDrive.ThongSo,
                    MaHardDrive = a.HardDrive.Ma,
                    NameColor = a.Color.Name,
                    MaColor = a.Color.Ma,
                    MaCardVGA = a.CardVGA.Ma,
                    TenCardVGA = a.CardVGA.Ten,
                    ThongSoCardVGA = a.CardVGA.ThongSo,
                    MaManHinh = a.Screen.Ma,
                    KichCoManHinh = a.Screen.KichCo,
                    TanSoManHinh = a.Screen.TanSo,
                    ChatLieuManHinh = a.Screen.ChatLieu,
                    NameProduct = a.Product.Name,
                    NameProductType = a.Product.ProductType.Name,
                    NameManufacturer = a.Product.Manufacturer.Name,
                    LinkImage = (a.Imagess.FirstOrDefault(image => image.Ma == "Anh1") != null) ? a.Imagess.FirstOrDefault(image => image.Ma == "Anh1").LinkImage : null,
                    OtherImages = (a.Imagess.Where(image => image.Ma != "Anh1").Select(image => image.LinkImage).ToList()),
                });

            if (getNumber > 0)
            {
                query = query.Take(Convert.ToInt32(getNumber));
            }
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.NameProduct.Contains(search));
            }
            if (from.HasValue)
            {
                query = query.Where(x => x.Price >= from);
            }
            if (to.HasValue)
            {
                query = query.Where(x => x.Price <= to);
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "nameproduct_desc":
                        query = query.OrderByDescending(x => x.NameProduct);
                        break;
                    case "price_asc":
                        query = query.OrderBy(x => x.Price);
                        break;
                    case "price_desc":
                        query = query.OrderByDescending(x => x.Price);
                        break;
                }
            }

            var pageSize = Page_Size;
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            page = Math.Clamp(page, 1, totalPages);
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var result = await query.ToListAsync();

            foreach (var productDetail in result)
            {
                productDetail.AvailableQuantity = GetCountProductDetail(productDetail.Code);
            }

            return result;
        }


        public async Task<bool> Update(ProductDetail obj)
        {
            var productDetail = await _context.ProductDetails.FindAsync(obj.Id);
            if (productDetail == null)
            {
                return false;
            }
            try
            {
                productDetail.ImportPrice = obj.ImportPrice;
                productDetail.Price = obj.Price;
                productDetail.Upgrade = obj.Upgrade;
                productDetail.Description = obj.Description;
                productDetail.Status = obj.Status;
                _context.ProductDetails.Update(productDetail);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> UpdateSoLuong(Guid id, int soLuong)
        // truyền vào 1 id + sl sản phẩm bán ra trong billdetail, giảm sl sp sau khi thanh toán
        {
            try
            {
                var productDetail = await _context.ProductDetails.FindAsync(id);
                if (productDetail == null)
                {
                    return false;
                }
                //productDetail.AvailableQuantity -= soLuong;
                _context.ProductDetails.Update(productDetail);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<ProductDetail> GetById(Guid guid)
        {
            return await _context.ProductDetails.FindAsync(guid);
        }
    }
}