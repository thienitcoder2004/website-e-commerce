using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using website_e_commerce.Models;
using website_e_commerce.Services;
using System.ComponentModel.DataAnnotations;

namespace website_e_commerce.Areas.Identity.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EmailService _emailService;

        public ForgotPasswordModel(UserManager<ApplicationUser> userManager, EmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        [BindProperty]
        [Required(ErrorMessage = "Vui lòng nhập email hoặc số điện thoại")]
        public string Input { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var user = await _userManager.FindByEmailAsync(Input)
                ?? _userManager.Users.FirstOrDefault(u => u.PhoneNumber == Input);

            if (user == null)
            {
                ModelState.AddModelError("", "Không tìm thấy tài khoản!");
                return Page();
            }

            // 🔹 Tạo mã OTP 6 số
            var otp = new Random().Next(100000, 999999).ToString();

            TempData["ResetOTP"] = otp;
            TempData["ResetUser"] = user.Email;

            // 🔹 Gửi email OTP
            await _emailService.SendEmailAsync(user.Email, "Mã xác nhận đặt lại mật khẩu",
                $"<h3>Mã xác nhận của bạn là: <b>{otp}</b></h3>");

            return RedirectToPage("ResetPasswordByCode");
        }
    }
}
