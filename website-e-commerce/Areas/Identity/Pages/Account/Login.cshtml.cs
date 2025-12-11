using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using website_e_commerce.Models;

namespace website_e_commerce.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Vui lòng nhập email")]
            [EmailAddress(ErrorMessage = "Email không hợp lệ")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
            [DataType(DataType.Password)]
            [Display(Name = "Mật khẩu")]
            public string Password { get; set; }

            [Display(Name = "Ghi nhớ đăng nhập")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Xóa cookie đăng nhập bên ngoài
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Tìm user theo email
            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Thông tin đăng nhập không đúng.");
                return Page();
            }

            // 🔸 Xem user này có phải Admin không
            var isAdmin = await _userManager.IsInRoleAsync(user, SD.Role_Admin);

            // Nếu không phải admin và đang bị khóa -> chuyển đến Lockout
            if (!isAdmin &&
                user.LockoutEnd.HasValue &&
                user.LockoutEnd.Value > DateTimeOffset.UtcNow)
            {
                _logger.LogWarning("Tài khoản {Email} đang bị khóa tới {Time}.", user.Email, user.LockoutEnd);
                return RedirectToPage("./Lockout");
            }

            // Đăng nhập (không dùng lockout mặc định)
            var result = await _signInManager.PasswordSignInAsync(
                user.UserName,
                Input.Password,
                Input.RememberMe,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                _logger.LogInformation("Người dùng {Email} đăng nhập thành công.", user.Email);

                // Reset lockout cho mọi loại tài khoản khi đăng nhập thành công
                user.AccessFailedCount = 0;
                user.LockoutEnd = null;
                await _userManager.UpdateAsync(user);

                return LocalRedirect(returnUrl);
            }

            if (result.RequiresTwoFactor)
            {
                return RedirectToPage("./LoginWith2fa",
                    new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
            }

            // ================== ĐĂNG NHẬP THẤT BẠI ==================

            if (isAdmin)
            {
                // ⚠️ Admin: KHÔNG lockout, chỉ báo lỗi
                ModelState.AddModelError(string.Empty, "Thông tin đăng nhập không đúng.");
                return Page();
            }

            // User thường: áp dụng rule 5 lần & 10 lần
            user.AccessFailedCount += 1;

            if (user.AccessFailedCount >= 10)
            {
                // Khóa dài (coi như vĩnh viễn)
                user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(100);
                await _userManager.UpdateAsync(user);

                _logger.LogWarning("Tài khoản {Email} bị khóa dài sau {Count} lần đăng nhập sai.",
                    user.Email, user.AccessFailedCount);

                return RedirectToPage("./Lockout");
            }
            else if (user.AccessFailedCount >= 5)
            {
                // Khóa tạm thời 1 phút
                user.LockoutEnd = DateTimeOffset.UtcNow.AddMinutes(1);
                await _userManager.UpdateAsync(user);

                _logger.LogWarning("Tài khoản {Email} bị tạm khóa 1 phút sau {Count} lần đăng nhập sai.",
                    user.Email, user.AccessFailedCount);

                return RedirectToPage("./Lockout");
            }
            else
            {
                // Chưa tới 5 lần -> chỉ báo lỗi
                await _userManager.UpdateAsync(user);
                ModelState.AddModelError(string.Empty, "Thông tin đăng nhập không đúng.");
                return Page();
            }
        }
    }
}
