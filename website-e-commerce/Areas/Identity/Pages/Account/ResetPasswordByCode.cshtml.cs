using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using website_e_commerce.Models;
using System.ComponentModel.DataAnnotations;
using System.Globalization; // ⬅ cần cho DateTimeStyles

namespace website_e_commerce.Areas.Identity.Pages.Account
{
    public class ResetPasswordByCodeModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ResetPasswordByCodeModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        [Required(ErrorMessage = "Vui lòng nhập mã OTP")]
        public string Otp { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải ít nhất 6 ký tự")]
        public string NewPassword { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var otp = TempData["ResetOTP"]?.ToString();
            var email = TempData["ResetUser"]?.ToString();
            var expiryStr = TempData["ResetOTPExpiry"]?.ToString();   // ⏰ lấy thời gian hết hạn

            // Không có dữ liệu -> coi như phiên đã hết hạn
            if (otp == null || email == null || expiryStr == null)
            {
                ModelState.AddModelError("", "Phiên đặt lại mật khẩu đã hết hạn. Vui lòng thử lại.");
                return Page();
            }

            // ⏰ KIỂM TRA: OTP đã quá 5 phút chưa
            if (!DateTime.TryParse(expiryStr, null, DateTimeStyles.RoundtripKind, out var expiry)
                || DateTime.UtcNow > expiry)
            {
                ModelState.AddModelError("", "Mã xác nhận đã hết hạn. Vui lòng yêu cầu mã mới.");
                return Page();
            }

            // Giữ logic cũ kiểm tra trùng OTP
            if (Otp != otp)
            {
                ModelState.AddModelError("", "Mã xác nhận không chính xác!");
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                ModelState.AddModelError("", "Không tìm thấy tài khoản!");
                return Page();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, NewPassword);

            if (result.Succeeded)
            {
                // 🌟 THÊM: mở khóa & reset đếm sai sau khi đặt lại mật khẩu thành công
                user.AccessFailedCount = 0;
                user.LockoutEnd = null;
                await _userManager.UpdateAsync(user);

                TempData["SuccessMessage"] = "Đặt lại mật khẩu thành công!";
                return RedirectToPage("Login");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return Page();
        }
    }
}
