using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository;
using Shop_API.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ApplicationDbContext, ApplicationDbContext>();
builder.Services.AddTransient<IRamRepository, RamRepository>();
builder.Services.AddTransient<IScreenRepository, ScreenRepository>();
builder.Services.AddTransient<IProductDetailRepository, ProductDetailRepository>();
builder.Services.AddTransient<IImeiRepository, ImeiRepository>();


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
