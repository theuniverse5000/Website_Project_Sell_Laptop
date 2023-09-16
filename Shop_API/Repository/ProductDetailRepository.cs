using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

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

        public async Task<IEnumerable<ProductDetail>> GetAll()
        {
            var list = await _context.ProductDetails.ToListAsync();
            var listProductDetail = list.Where(x => x.Status > 0).ToList();
            return listProductDetail;
        }


        public async Task<IEnumerable<ProductDetailDto>> GetAllProductDetail()
        {
            // ProductDetailView là 1 ViewModel ảo, tạo ra để hiện thị các thuộc tính của 1 đối tượng
            // mà mình mong muốn = cách gán giá trị vào cho nó
            int soLuongProductDetail = await GetCountProductDetail();
            List<ProductDetailDto> listProductDetails = new List<ProductDetailDto>();
            listProductDetails = (
                        from a in await _context.ProductDetails.ToListAsync()
                        join b in await _context.Rams.ToListAsync() on a.RamId equals b.Id
                        join c in await _context.Cpus.ToListAsync() on a.CpuId equals c.Id
                        join d in await _context.HardDrives.ToListAsync() on a.HardDriveId equals d.Id
                        join e in await _context.Colors.ToListAsync() on a.ColorId equals e.Id
                        join f in await _context.CardVGAs.ToListAsync() on a.CardVGAId equals f.Id
                        join g in await _context.Screens.ToListAsync() on a.ScreenId equals g.Id
                        // join h in await _context.Images.ToListAsync() on a.Id equals h.ProductDetailId
                        join i in await _context.Products.ToListAsync() on a.ProductId equals i.Id
                        join k in await _context.Manufacturers.ToListAsync() on i.ManufacturerId equals k.Id
                        join l in await _context.ProductTypes.ToListAsync() on i.ProductTypeId equals l.Id

                        select new ProductDetailDto
                        {
                            Id = a.Id,
                            Code = a.Code,
                            ImportPrice = a.ImportPrice,
                            Price = a.Price,
                            Status = a.Status,
                            Upgrade = a.Upgrade,
                            Description = a.Description,
                            AvailableQuantity = soLuongProductDetail,
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
                            //  LinkImage = h.LinkImage
                        }

               ).ToList();
            return listProductDetails.Where(x => x.Status > 0);
        }

        public async Task<int> GetCountProductDetail()
        {
            List<ProductDetailDto> listProductDetails = new List<ProductDetailDto>();
            listProductDetails = (
                        from a in await _context.ProductDetails.ToListAsync()
                        join b in await _context.Rams.ToListAsync() on a.RamId equals b.Id
                        join c in await _context.Cpus.ToListAsync() on a.CpuId equals c.Id
                        join d in await _context.HardDrives.ToListAsync() on a.HardDriveId equals d.Id
                        join e in await _context.Colors.ToListAsync() on a.ColorId equals e.Id
                        join f in await _context.CardVGAs.ToListAsync() on a.CardVGAId equals f.Id
                        join g in await _context.Screens.ToListAsync() on a.ScreenId equals g.Id
                        // join h in await _context.Images.ToListAsync() on a.Id equals h.ProductDetailId
                        join i in await _context.Products.ToListAsync() on a.ProductId equals i.Id
                        join k in await _context.Manufacturers.ToListAsync() on i.ManufacturerId equals k.Id
                        join l in await _context.ProductTypes.ToListAsync() on i.ProductTypeId equals l.Id
                        join m in await _context.Serials.ToListAsync() on a.Id equals m.ProductDetailId
                        select new ProductDetailDto
                        {
                            Id = a.Id,
                            Code = a.Code,
                            ImportPrice = a.ImportPrice,
                            Price = a.Price,
                            Status = a.Status,
                            Description = a.Description,
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
                            NameManufacturer = k.Name,
                            //  LinkImage = h.LinkImage
                        }

               ).ToList();
            int soLuong = listProductDetails.Where(x => x.Status > 0).Count();
            return soLuong;
        }

        public async Task<IEnumerable<ProductDetailDto>> GetProductDetail(string? search, double? from, double? to, string? sortBy, int page = 1)
        {
            var listProductDetail = await GetAllProductDetail();
            #region Filltering
            if (!string.IsNullOrEmpty(search))
            {
                listProductDetail = listProductDetail.Where(x => x.NameProduct.Contains(search));
            }
            if (from.HasValue)
            {
                listProductDetail = listProductDetail.Where(x => x.Price >= from);
            }
            if (to.HasValue)
            {
                listProductDetail = listProductDetail.Where(x => x.Price <= to);
            }
            #endregion
            #region Sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "nameproduct_desc":
                        listProductDetail = listProductDetail.OrderByDescending(x => x.NameProduct);
                        break;
                    case "price_asc":
                        listProductDetail = listProductDetail.OrderBy(x => x.Price);
                        break;
                    case "price_desc":
                        listProductDetail = listProductDetail.OrderByDescending(x => x.Price);
                        break;
                    default:
                        break;
                }
            }
            #endregion
            #region Paging
            listProductDetail = listProductDetail.Skip((page - 1) * Page_Size).Take(Page_Size);
            #endregion
            return listProductDetail;
        }

        //public async Task<ProductDetailView> GetAllProductDetailById(Guid id)
        //{
        //    var productDetailX = GetAllProductDetail().Result.Where(x => x.Id == id).ToList();
        //    return productDetailX;
        //}

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
                // productDetail.AvailableQuantity = obj.AvailableQuantity;
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
                int soLuongProductDetail = await GetCountProductDetail();
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
                            join m in await _context.GiamGias.ToListAsync() on i.ManufacturerId equals m.Id

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
                                AvailableQuantity = soLuongProductDetail,
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
    }
}