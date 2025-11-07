using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using website_e_commerce.Models;
using website_e_commerce.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 🧩 Cấu hình dịch vụ cơ bản
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Kết nối Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Cấu hình Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddDefaultTokenProviders()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>();

// ==========================
// Đăng nhập bằng Google & Facebook
// ==========================
builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];

        // Lấy thêm thông tin từ Google
        options.Scope.Add("profile");
        options.ClaimActions.MapJsonKey("picture", "picture", "url");
    });
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

builder.Services.AddRazorPages();
builder.Services.AddSignalR();

// Đăng ký Repository & Services
builder.Services.AddScoped<ICategoryRepository, MockCategoryRepository>();
builder.Services.AddScoped<IProductRepository, MockProductRepository>();
builder.Services.AddScoped<INewsRepository, MockNewsRepository>();
builder.Services.AddScoped<IReviewRepository, MockReviewRepository>();
builder.Services.AddScoped<IPromotionRepository, MockPromotionRepository>();
builder.Services.AddScoped<website_e_commerce.Services.EmailService>();
builder.Services.AddScoped<website_e_commerce.Services.GeminiChatService>();
builder.Services.AddScoped<IBrandRepository, MockBrandRepository>();
builder.Services.AddScoped<IStockHistoryRepository, MockStockHistoryRepository>();


builder.Services.AddAuthorization();

var app = builder.Build();

// SignalR cho thông báo tin tức
app.MapHub<website_e_commerce.Services.NewsHub>("/newsHub");

// Pipeline cấu hình
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();
app.MapRazorPages();

// Định tuyến
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

// Gọi hàm tạo/cập nhật Role & Admin
await SeedRolesAndAdmin(app);

app.Run();

// Hàm SeedRolesAndAdmin: tạo và cập nhật admin
static async Task SeedRolesAndAdmin(IApplicationBuilder app)
{
    using var scope = app.ApplicationServices.CreateScope();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    // Các role mặc định
    string[] roles = { SD.Role_Admin, SD.Role_Employee, SD.Role_Customer };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Thông tin admin mặc định
    string adminEmail = "admin@gmail.com";
    string adminPassword = "Admin@123";
    string adminHo = "Quản trị";
    string adminTen = "Viên";

    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    // Nếu chưa có admin → tạo mới
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            Ho = adminHo,
            Ten = adminTen,
            PhoneNumber = "0123456789",
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(adminUser, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, SD.Role_Admin);
            Console.WriteLine("✅ Đã tạo tài khoản admin mặc định!");
        }
        else
        {
            Console.WriteLine("❌ Lỗi khi tạo admin: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
    else
    {
        // Nếu đã có admin → cập nhật lại thông tin và role
        bool updated = false;

        if (adminUser.Ho != adminHo || adminUser.Ten != adminTen)
        {
            adminUser.Ho = adminHo;
            adminUser.Ten = adminTen;
            await userManager.UpdateAsync(adminUser);
            updated = true;
        }

        // Cập nhật lại mật khẩu (nếu cần)
        var hasPassword = await userManager.HasPasswordAsync(adminUser);
        if (hasPassword)
        {
            await userManager.RemovePasswordAsync(adminUser);
        }
        await userManager.AddPasswordAsync(adminUser, adminPassword);

        // Gán lại role Admin (nếu chưa có)
        if (!await userManager.IsInRoleAsync(adminUser, SD.Role_Admin))
        {
            await userManager.AddToRoleAsync(adminUser, SD.Role_Admin);
        }

        if (updated)
            Console.WriteLine("🔁 Đã cập nhật lại thông tin admin sẵn có.");
        else
            Console.WriteLine("ℹ️ Admin đã tồn tại, mật khẩu và quyền đã được làm mới.");
    }
}
