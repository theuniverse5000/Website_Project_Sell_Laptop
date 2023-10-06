using Microsoft.EntityFrameworkCore;
using Shop_Models.Entities;


namespace Shop_API.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<Ram> Rams { get; set; }
        public virtual DbSet<Cpu> Cpus { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<HardDrive> HardDrives { get; set; }
        public virtual DbSet<Screen> Screens { get; set; }
        public virtual DbSet<CardVGA> CardVGAs { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<BillDetail> BillDetails { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartDetail> CartDetails { get; set; }
        public virtual DbSet<GiamGia> GiamGias { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Serial> Serials { get; set; }
        public virtual DbSet<LichSuTieuDiem> LichSuTieuDiems { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<ProductDetail> ProductDetails { get; set; }
        public virtual DbSet<QuyDoiDiem> QuyDoiDiems { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SanPhamGiamGia> SanPhamGiamGias { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ViDiem> ViDiems { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }

        public virtual DbSet<ManagePost> ManagePosts { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }

    }

}
