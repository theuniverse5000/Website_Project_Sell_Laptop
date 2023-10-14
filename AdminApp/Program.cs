using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));


// Add services to the container.
builder.Services.AddControllersWithViews();
// Add Service cho 1 Httpclient và cấu hình 
builder.Services.AddHttpClient("PhuongThaoHttpAdmin", thao =>
{
    thao.BaseAddress = new Uri(builder.Configuration["UrlApiAdmin"]);
    thao.DefaultRequestHeaders.Add("Key-Domain", builder.Configuration["TokenGetApiAdmin"]);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();
