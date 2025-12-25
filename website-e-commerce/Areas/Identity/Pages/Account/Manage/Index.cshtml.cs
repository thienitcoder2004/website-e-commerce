#nullable disable
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using website_e_commerce.Models;

namespace website_e_commerce.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }
        public string AvatarUrl { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Ho { get; set; }

            [Required]
            public string Ten { get; set; }

            [Phone]
            public string PhoneNumber { get; set; }

            public IFormFile AvatarFile { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            Username = await _userManager.GetUserNameAsync(user);
            AvatarUrl = user.AvatarUrl ?? "/images/avatar-default.png";

            Input = new InputModel
            {
                Ho = user.Ho,
                Ten = user.Ten,
                PhoneNumber = user.PhoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            // Cập nhật họ tên
            user.Ho = Input.Ho;
            user.Ten = Input.Ten;

            // Cập nhật số điện thoại
            if (Input.PhoneNumber != user.PhoneNumber)
            {
                await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
            }

            // Upload avatar
            if (Input.AvatarFile != null)
            {
                var folder = Path.Combine("wwwroot", "uploads", "avatars");
                Directory.CreateDirectory(folder);

                var fileName = $"{user.Id}{Path.GetExtension(Input.AvatarFile.FileName)}";
                var filePath = Path.Combine(folder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await Input.AvatarFile.CopyToAsync(stream);

                user.AvatarUrl = "/uploads/avatars/" + fileName;
            }

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);

            StatusMessage = "✅ Cập nhật hồ sơ thành công";
            return RedirectToPage();
        }
    }
}
