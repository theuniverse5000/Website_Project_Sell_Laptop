using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;
using Shop_Models.ViewModels;

namespace Shop_API.Repository
{
    public class ProductDetailRepository : IProductDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(ProductDetail obj)
        {
            var checkMa = await _context.ProductDetails.AnyAsync(x => x.Ma == obj.Ma);
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

        public async Task<IEnumerable<ProductDetailView>> GetAllProductDetail()
        {
            // ProductDetailView là 1 ViewModel ảo, tạo ra để hiện thị các thuộc tính của 1 đối tượng
            // mà mình mong muốn = cách gán giá trị vào cho nó
            List<ProductDetailView> listProductDetails = new List<ProductDetailView>();
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
                        select new ProductDetailView
                        {
                            Id = a.Id,
                            Ma = a.Ma,
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
                            NameManufacturer = k.Name,
                            //  LinkImage = h.LinkImage
                        }

               ).ToList();
            return listProductDetails.Where(x => x.Status > 0);
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
    }
}
