using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_API.Repository;
using Shop_API.Service.IService;

namespace Shop_API.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServiceComponents(this IServiceCollection services)
        {
            services.AddScoped<ApplicationDbContext, ApplicationDbContext>();
            services.AddTransient<IRamRepository, RamRepository>();
            services.AddTransient<ICardVGARepository, CardVGARepository>();
            services.AddTransient<ICpuRepository, CpuRepository>();
            services.AddTransient<IHardDriveRepository, HardDriveRepository>();
            services.AddTransient<ISanPhamGiamGiaRepository, SanPhamGiamGiaRepository>();
            services.AddTransient<IGiamGiaRepository, GiamGiaRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IColorRepository, ColorRepository>();
            services.AddTransient<IScreenRepository, ScreenRepository>();
            services.AddTransient<IProductDetailRepository, ProductDetailRepository>();
            services.AddTransient<IManufacturerRepository, ManufacturerRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ISerialRepository, SerialRepository>();
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<IVoucherRepository, VoucherRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IViDiemRepository, ViDiemRepository>();
            services.AddTransient<IQuyDoiDiemRepository, QuyDoiDiemRepository>();
            services.AddTransient<ILichSuTieuDiemRepository, LichSuTieuDiemRepository>();
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<ICartDetailRepository, CartDetailRepository>();
            services.AddTransient<IBillRepository, BillRepository>();
            services.AddTransient<IBillDetailRepository, BillDetailRepository>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IProductTypeRepository, ProductTypeRepository>();
            //services.AddTransient<IGiamGiaHangLoatservices, GiamGiaHangLoatservices>();
            services.AddTransient<IBillService, BillService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IPagingRepository, PagingRepository>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IUserServiece, UserServiece>();
            services.AddTransient<ICurrentUserProvider, CurrentUserProvider>();

            return services;
        }
    }
}
