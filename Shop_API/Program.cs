using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository;
using Shop_API.Repository.IRepository;
using Shop_API.Service;
using Shop_API.Service.IService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
//                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
//            ValidateIssuer = false,
//            ValidateAudience = false
//        };
//    });
// Add DI 
builder.Services.AddScoped<ApplicationDbContext, ApplicationDbContext>();
builder.Services.AddTransient<IRamRepository, RamRepository>();
builder.Services.AddTransient<ICardVGARepository, CardVGARepository>();
builder.Services.AddTransient<ICpuRepository, CpuRepository>();
builder.Services.AddTransient<IHardDriveRepository, HardDriveRepository>();
builder.Services.AddTransient<ISanPhamGiamGiaRepository, SanPhamGiamGiaRepository>();
builder.Services.AddTransient<IGiamGiaRepository, GiamGiaRepository>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<IColorRepository, ColorRepository>();
builder.Services.AddTransient<IScreenRepository, ScreenRepository>();
builder.Services.AddTransient<IProductDetailRepository, ProductDetailRepository>();
builder.Services.AddTransient<IManufacturerRepository, ManufacturerRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ISerialRepository, SerialRepository>();
builder.Services.AddTransient<IImageRepository, ImageRepository>();
builder.Services.AddTransient<IVoucherRepository, VoucherRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IViDiemRepository, ViDiemRepository>();
builder.Services.AddTransient<IQuyDoiDiemRepository, QuyDoiDiemRepository>();
builder.Services.AddTransient<ILichSuTieuDiemRepository, LichSuTieuDiemRepository>();
builder.Services.AddTransient<ICartRepository, CartRepository>();
builder.Services.AddTransient<ICartDetailRepository, CartDetailRepository>();
builder.Services.AddTransient<IBillRepository, BillRepository>();
builder.Services.AddTransient<IBillDetailRepository, BillDetailRepository>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<ISerialDaBanRepository, SerialDaBanRepository>();
builder.Services.AddTransient<IProductTypeRepository, ProductTypeRepository>();
builder.Services.AddTransient<IGiamGiaHangLoatServices, GiamGiaHangLoatServices>();


builder.Services.AddTransient<ICartService, CartService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
