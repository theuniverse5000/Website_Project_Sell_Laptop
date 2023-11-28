using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_Models.Entities;
using Shop_Models.Momo;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Add Service cho 1 Httpclient và cấu hình 
builder.Services.AddHttpClient("PhuongThaoHttpWeb", thao =>
{
    thao.BaseAddress = new Uri(builder.Configuration["UrlApiAdmin"]);
    thao.DefaultRequestHeaders.Add("Key-Domain", builder.Configuration["TokenGetApiAdmin"]);
});

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
{

    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie();
builder.Services.AddHttpClient("MyHttpClient", client =>
{
    client.DefaultRequestHeaders.Add("key-domain", builder.Configuration["TokenGetApiAdmin"]);
});
builder.Services.AddAuthentication()
    .AddFacebook(options =>
    {
        options.AppId = builder.Configuration["Facebook:AppId"];
        options.AppSecret = builder.Configuration["Facebook:AppSecret"];
        options.SaveTokens = true;
        options.CallbackPath = builder.Configuration["Facebook:CallbackPath"];
    })
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Google:AppId"];
        options.ClientSecret = builder.Configuration["Google:AppSecret"];
        options.ClaimActions.MapJsonKey("Picture", "picture", "url");
        options.SaveTokens = true;
        options.CallbackPath = builder.Configuration["Google:CallbackPath"];
    });

builder.Services.AddScoped<IVnPayService, VnPayService>();
builder.Services.Configure<MomoOptionModel>(builder.Configuration.GetSection("MomoAPI"));
builder.Services.AddScoped<IMomoService, MomoService>();

// Đăng ký HttpContextAccessor
builder.Services.AddHttpContextAccessor();

//cấu hình nén phản hồi
builder.Services.AddResponseCompression(otp =>
{
    otp.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
});
builder.Services.AddControllersWithViews();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
});
builder.Services.AddControllersWithViews();



builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ADMIN", policy => { policy.RequireAuthenticatedUser(); policy.RequireRole("ADMIN"); });
    options.AddPolicy("CLIENT", policy => { policy.RequireAuthenticatedUser(); policy.RequireRole("CLIENT"); });
    options.AddPolicy("ClinetOld", policy => { policy.RequireAuthenticatedUser(); policy.RequireRole("ClientOld"); });

});


builder.Services.AddSession();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseSession();

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
