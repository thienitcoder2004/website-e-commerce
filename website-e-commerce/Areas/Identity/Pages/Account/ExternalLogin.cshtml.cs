using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using website_e_commerce.Models;

namespace website_e_commerce.Areas.Identity.Pages.Account
{
    public class ExternalLoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ExternalLoginModel(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Khi người dùng truy cập trực tiếp /ExternalLogin → quay về Login
        public IActionResult OnGetAsync() => RedirectToPage("./Login");

        // Gửi yêu cầu đăng nhập sang Google
        public IActionResult OnPost(string provider, string? returnUrl = null)
        {
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        // Xử lý khi Google gọi callback về
        public async Task<IActionResult> OnGetCallbackAsync(string? returnUrl = null, string? remoteError = null)
        {
            returnUrl ??= Url.Content("~/");

            if (remoteError != null)
            {
                TempData["ErrorMessage"] = $"Lỗi đăng nhập Google: {remoteError}";
                return RedirectToPage("./Login");
            }

            // Lấy thông tin đăng nhập ngoài từ Google
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return RedirectToPage("./Login");

            // Nếu user đã có → đăng nhập luôn
            var result = await _signInManager.ExternalLoginSignInAsync(
                info.LoginProvider, info.ProviderKey, isPersistent: false);

            if (result.Succeeded)
                return LocalRedirect(returnUrl);

            // Nếu chưa có tài khoản → tạo mới
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var givenName = info.Principal.FindFirstValue(ClaimTypes.GivenName);
            var surname = info.Principal.FindFirstValue(ClaimTypes.Surname);
            var fullName = info.Principal.FindFirstValue(ClaimTypes.Name);

            // Lấy ảnh từ Google
            string? avatarUrl = info.Principal.FindFirstValue("picture");

            if (email != null)
            {
                string ho = surname ?? fullName?.Split(' ').FirstOrDefault() ?? "Người";
                string ten = givenName ?? fullName?.Split(' ').LastOrDefault() ?? "Dùng";

                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    Ho = ho,
                    Ten = ten,
                    PhoneNumber = "0000000000",
                    AvatarUrl = avatarUrl,
                    EmailConfirmed = true
                };

                var create = await _userManager.CreateAsync(user);
                if (create.Succeeded)
                {
                    // Gắn tài khoản Google vào Identity
                    await _userManager.AddLoginAsync(user, info);

                    // Gán Role Customer
                    if (!await _roleManager.RoleExistsAsync(SD.Role_Customer))
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer));

                    await _userManager.AddToRoleAsync(user, SD.Role_Customer);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return LocalRedirect(returnUrl);
                }
                else
                {
                    var errorMessages = string.Join("; ", create.Errors.Select(e => e.Description));
                    TempData["ErrorMessage"] = $"Không thể tạo tài khoản mới: {errorMessages}";
                    return RedirectToPage("./Login");
                }
            }

            TempData["ErrorMessage"] = "Không thể lấy thông tin tài khoản từ Google.";
            return RedirectToPage("./Login");
        }
    }
}
