using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using website_e_commerce.Models;

public class AddressModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public AddressModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public List<UserAddress> Addresses { get; set; } = new();

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public class InputModel
    {
        public string HoTen { get; set; }
        public string DienThoai { get; set; }
        public string DiaChi { get; set; }
        public string PhuongXa { get; set; }
        public string QuanHuyen { get; set; }
        public string TinhThanh { get; set; }
        public bool IsDefault { get; set; }
    }

    public async Task OnGetAsync()
    {
        var userId = _userManager.GetUserId(User);
        Addresses = await _context.UserAddresses
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.IsDefault)
            .ToListAsync();
    }

    // ========== CREATE ==========
    public async Task<IActionResult> OnPostCreateAsync()
    {
        var userId = _userManager.GetUserId(User);

        if (Input.IsDefault)
        {
            foreach (var a in _context.UserAddresses.Where(x => x.UserId == userId))
                a.IsDefault = false;
        }

        _context.UserAddresses.Add(new UserAddress
        {
            UserId = userId,
            HoTen = Input.HoTen,
            DienThoai = Input.DienThoai,
            DiaChi = Input.DiaChi,
            PhuongXa = Input.PhuongXa,
            QuanHuyen = Input.QuanHuyen,
            TinhThanh = Input.TinhThanh,
            IsDefault = Input.IsDefault
        });

        await _context.SaveChangesAsync();
        return RedirectToPage();
    }

    // ========== EDIT ==========
    public async Task<IActionResult> OnPostEditAsync(int id)
    {
        var userId = _userManager.GetUserId(User);

        var address = await _context.UserAddresses
            .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

        if (address == null) return RedirectToPage();

        if (Input.IsDefault)
        {
            foreach (var a in _context.UserAddresses.Where(x => x.UserId == userId))
                a.IsDefault = false;
        }

        address.HoTen = Input.HoTen;
        address.DienThoai = Input.DienThoai;
        address.DiaChi = Input.DiaChi;
        address.PhuongXa = Input.PhuongXa;
        address.QuanHuyen = Input.QuanHuyen;
        address.TinhThanh = Input.TinhThanh;
        address.IsDefault = Input.IsDefault;

        await _context.SaveChangesAsync();
        return RedirectToPage();
    }

    // ========== SET DEFAULT ==========
    public async Task<IActionResult> OnPostSetDefaultAsync(int id)
    {
        var userId = _userManager.GetUserId(User);

        foreach (var a in _context.UserAddresses.Where(x => x.UserId == userId))
            a.IsDefault = false;

        var current = await _context.UserAddresses
            .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

        if (current != null)
        {
            current.IsDefault = true;
            await _context.SaveChangesAsync();
        }

        return RedirectToPage();
    }

    // ========== DELETE ==========
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var userId = _userManager.GetUserId(User);

        var address = await _context.UserAddresses
            .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

        if (address == null) return RedirectToPage();

        // Không cho xóa địa chỉ mặc định
        if (address.IsDefault)
        {
            TempData["Msg"] = "Không thể xóa địa chỉ mặc định.";
            return RedirectToPage();
        }

        // ✅ THÊM: Nếu địa chỉ đã được dùng trong đơn hàng -> chặn xóa
        var usedInOrders = await _context.Orders.AnyAsync(o => o.UserAddressId == id);
        if (usedInOrders)
        {
            TempData["Msg"] = "Không thể xóa vì địa chỉ này đã được sử dụng trong đơn hàng.";
            return RedirectToPage();
        }

        _context.UserAddresses.Remove(address);
        await _context.SaveChangesAsync();

        TempData["Msg"] = "Đã xóa địa chỉ thành công.";
        return RedirectToPage();
    }

}
