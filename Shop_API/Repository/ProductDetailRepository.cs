﻿using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.HelpperModel;
using Shop_API.Repository.IRepository;
using Shop_API.RequestModel;
using Shop_Models.Dto;
using Shop_Models.Entities;
using Shop_Models.HelpperModel;

namespace Shop_API.Repository
{
    public class ProductDetailRepository : IProductDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public static int Page_Size { get; set; } = 10;
        public ProductDetailRepository(ApplicationDbContext context)
        {
            _context = context;
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

        public async Task<Pagination<ProductDetail>> GetAll(ProductDetailQueryModel productDetailQueryModel)
        {
            var page = productDetailQueryModel.CurrentPage ?? 1;
            var size = productDetailQueryModel.PageSize ?? 20;

            var query = _context.ProductDetails
            .Where(x => x.Status > 0).AsQueryable();

            if (string.IsNullOrEmpty(productDetailQueryModel.Sort))
            {
                query = query.OrderBy(x => x.Product.Name).AsQueryable();
            }
            var resut = await query.GetPagedAsync<ProductDetail>(page, size);
            return resut;

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
        public async Task<IEnumerable<ProductDetailDto>> PGetProductDetail(int? getNumber, string? codeProductDetail, string? search, double? from, double? to, string? sortBy, int page = 10)
        {
            var query = _context.ProductDetails
          .AsNoTracking()
          .Where(a => a.Status > 0 && (codeProductDetail != null ? a.Code == codeProductDetail : true))
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
              NameManufacturer = a.Product.Manufacturer.Name
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



        public async Task<IEnumerable<ProductDetailDto>> GetProductDetailsByPromotionType(string promotionType)
        {
            try
            {
                //  int soLuongProductDetail = GetCountProductDetail();
                List<ProductDetailDto> listProductDetails = new List<ProductDetailDto>();
                listProductDetails = (
                            from a in await _context.ProductDetails.ToListAsync()
                            join b in await _context.Rams.ToListAsync() on a.RamId equals b.Id
                            join c in await _context.Cpus.ToListAsync() on a.CpuId equals c.Id
                            join d in await _context.HardDrives.ToListAsync() on a.HardDriveId equals d.Id
                            join e in await _context.Colors.ToListAsync() on a.ColorId equals e.Id
                            join f in await _context.CardVGAs.ToListAsync() on a.CardVGAId equals f.Id
                            join g in await _context.Screens.ToListAsync() on a.ScreenId equals g.Id
                            join i in await _context.Products.ToListAsync() on a.ProductId equals i.Id
                            join k in await _context.Manufacturers.ToListAsync() on i.ManufacturerId equals k.Id
                            join l in await _context.ProductTypes.ToListAsync() on i.ProductTypeId equals l.Id
                            join n in await _context.SanPhamGiamGias.ToListAsync() on a.Id equals n.ProductDetailId
                            join m in await _context.GiamGias.ToListAsync() on n.GiamGiaId equals m.Id

                            where m.LoaiGiamGia == promotionType // Lọc theo loại khuyến mãi
                            select new ProductDetailDto
                            {
                                Id = a.Id,
                                Code = a.Code,
                                ImportPrice = a.ImportPrice,
                                Price = a.Price,
                                Status = a.Status,
                                Upgrade = a.Upgrade,
                                Description = a.Description,
                                //   AvailableQuantity = soLuongProductDetail,
                                ThongSoRam = b.ThongSo,
                                MaRam = b.Ma,
                                TenCpu = c.Ten,
                                MaCpu = c.Ma,
                                ThongSoHardDrive = d.ThongSo,
                                MaHardDrive = d.Ma,
                                NameColor = e.Name,
                                MaColor = e.Ma,
                                MaCardVGA = f.Ma,
                                TenCardVGA = f.Ten,
                                ThongSoCardVGA = f.ThongSo,
                                MaManHinh = g.Ma,
                                KichCoManHinh = g.KichCo,
                                TanSoManHinh = g.TanSo,
                                ChatLieuManHinh = g.ChatLieu,
                                NameProduct = i.Name,
                                NameProductType = l.Name,
                                NameManufacturer = k.Name
                                //LinkImage = h.LinkImage
                            }
                   ).ToList();

                return listProductDetails.Where(x => x.Status > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> Update(ProductDetailDto productDetail)
        {
            try
            {
                var existingProductDetail = await _context.ProductDetails.FindAsync(productDetail.Id);

                if (existingProductDetail == null)
                {
                    return false;
                }

                // Cập nhật thông tin sản phẩm từ DTO
                existingProductDetail.ImportPrice = (float)productDetail.ImportPrice;
                existingProductDetail.Price = (float)productDetail.Price;
                existingProductDetail.Description = productDetail.Description;
                existingProductDetail.Status = productDetail.Status;

                // Cập nhật vào cơ sở dữ liệu
                _context.ProductDetails.Update(existingProductDetail);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<ProductDetail> GetByCode(string code)
        {
            var result = await _context.ProductDetails.FirstOrDefaultAsync(x => x.Code == code && x.Status > 0);
            return result;
        }

        public Task<ProductDetailDto> GetProductDetail()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductDetailDto>> GetProductDetailPubic(string? search, string? typeProduct, double? from, double? to, string? sortBy, int page = 1)
        {
            var query = _context.ProductDetails
                .AsNoTracking()
                .Where(a => a.Status > 0)
                .Select(a => new ProductDetailDto
                {
                    Id = a.Id,
                    Code = a.Code,
                    ImportPrice = a.ImportPrice,
                    Price = a.Price,
                    Status = a.Status,
                    Upgrade = a.Upgrade,
                    Description = a.Description,
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
                    NameManufacturer = a.Product.Manufacturer.Name
                });

            #region Filltering
            if (!string.IsNullOrEmpty(typeProduct))
            {
                query = query.Where(x => x.NameProductType == typeProduct);
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
            #endregion

            #region Sorting
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
                    default:
                        break;
                }
            }
            #endregion

            #region Paging
            var pageSize = Page_Size;
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            if (page < 1)
            {
                page = 1;
            }
            else if (page > totalPages)
            {
                page = totalPages;
            }

            query = query.Skip((page - 1) * pageSize).Take(pageSize);
            #endregion
            var result = await query.ToListAsync();
            return result; ;
        }

        public async Task<ProductDetail> GetById(Guid guid)
        {
            var result = await _context.ProductDetails.FindAsync(guid); return result;
        }

        public async Task<ProductDetailDto> GetProductDetailByIdReturnDto(Guid id)
        {
            var productDetail = await _context.ProductDetails
                .AsNoTracking()
                .Where(a => a.Status > 0 && a.Id == id)
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
                    NameManufacturer = a.Product.Manufacturer.Name
                })
                .FirstOrDefaultAsync();

            if (productDetail != null)
            {
                productDetail.AvailableQuantity = GetCountProductDetail(productDetail.Code);
            }

            return productDetail;
        }

    }
}