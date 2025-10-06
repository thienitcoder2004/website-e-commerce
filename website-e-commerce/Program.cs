using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using website_e_commerce.Models;
using website_e_commerce.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICategoryRepository, MockCategoryRepository>();
builder.Services.AddScoped<IProductRepository, MockProductRepository>();
builder.Services.AddScoped<INewsRepository, MockNewsRepository>();
builder.Services.AddScoped<IReviewRepository, MockReviewRepository>();

builder.Services.AddAuthorization();
builder.Services.AddRazorPages();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseSession();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapRazorPages();

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "folderRouting",
    pattern: "{folder}",
    defaults: new { controller = "Home", action = "Index" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Gọi hàm SeedRolesAndAdmin
//await SeedRolesAndAdmin(app);
//static async Task SeedRolesAndAdmin(IApplicationBuilder app)
//{
//    using var scope = app.ApplicationServices.CreateScope();
//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

//    // Tạo các role nếu chưa có
//    string[] roles = { SD.Role_Admin, SD.Role_Employee, SD.Role_Customer };
//    foreach (var role in roles)
//    {
//        if (!await roleManager.RoleExistsAsync(role))
//        {
//            await roleManager.CreateAsync(new IdentityRole(role));
//        }
//    }

//    // Tạo tài khoản admin mặc định nếu chưa có
//    string adminEmail = "admin@gmail.com";
//    string adminPassword = "Admin@123";

//    var adminUser = await userManager.FindByEmailAsync(adminEmail);
//    if (adminUser == null)
//    {
//        adminUser = new ApplicationUser
//        {
//            UserName = adminEmail,
//            Email = adminEmail,
//            FullName = "Admin",
//            EmailConfirmed = true
//        };
//        var result = await userManager.CreateAsync(adminUser, adminPassword);
//        if (result.Succeeded)
//        {
//            await userManager.AddToRoleAsync(adminUser, SD.Role_Admin);
//        }
//    }
//}

app.Run();
