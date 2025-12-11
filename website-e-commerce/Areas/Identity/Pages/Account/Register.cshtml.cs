using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using website_e_commerce.Models;

namespace website_e_commerce.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string? ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Vui lòng nhập họ")]
            [Display(Name = "Họ")]
            public string Ho { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập tên")]
            [Display(Name = "Tên")]
            public string Ten { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập email")]
            [EmailAddress(ErrorMessage = "Email không hợp lệ")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
            [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
            [Display(Name = "Số điện thoại")]
            public string SoDienThoai { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
            [StringLength(100, ErrorMessage = "Mật khẩu phải có ít nhất {2} ký tự.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Mật khẩu")]
            public string Password { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu")]
            [DataType(DataType.Password)]
            [Display(Name = "Xác nhận mật khẩu")]
            [Compare("Password", ErrorMessage = "Mật khẩu không khớp.")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "Quyền (Role)")]
            public string? Role { get; set; }

            [ValidateNever]
            public IEnumerable<SelectListItem> RoleList { get; set; }
        }

        // ================================
        // GET
        // ================================
        public async Task OnGetAsync(string? returnUrl = null)
        {
            ReturnUrl = returnUrl;

            // Tạo role nếu chưa tồn tại
            if (!await _roleManager.RoleExistsAsync(SD.Role_Customer))
            {
                await _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer));
                await _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee));
                await _roleManager.CreateAsync(new IdentityRole(SD.Role_StockManager));
                await _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin));
            }

            Input = new InputModel
            {
                RoleList = _roleManager.Roles
                    .ToList()               // tránh lỗi expression tree + switch expression
                    .Select(r => new SelectListItem
                    {
                        Value = r.Name,
                        Text = r.Name switch
                        {
                            SD.Role_Admin => "Quản trị viên",
                            SD.Role_Employee => "Nhân viên bán hàng",
                            SD.Role_StockManager => "Nhân viên kho hàng",
                            SD.Role_Customer => "Khách hàng",
                            _ => r.Name
                        }
                    })
            };

            ExternalLogins = (await _signInManager
                    .GetExternalAuthenticationSchemesAsync())
                .ToList();
        }

        // ================================
        // POST
        // ================================
        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ReturnUrl = returnUrl;

            ExternalLogins = (await _signInManager
                    .GetExternalAuthenticationSchemesAsync())
                .ToList();

            if (ModelState.IsValid)
            {
                var user = CreateUser();

                user.Ho = Input.Ho;
                user.Ten = Input.Ten;
                user.PhoneNumber = Input.SoDienThoai;

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Người dùng đã tạo tài khoản mới thành công.");

                    // Gán role: nếu null → Customer
                    string selectedRole = string.IsNullOrEmpty(Input.Role)
                        ? SD.Role_Customer
                        : Input.Role;

                    await _userManager.AddToRoleAsync(user, selectedRole);

                    // Gửi email xác nhận
                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(
                        Input.Email,
                        "Xác nhận tài khoản của bạn",
                        $"Vui lòng xác nhận tài khoản bằng cách <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>nhấn vào đây</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }

            // Nếu ModelState lỗi → load lại RoleList
            Input.RoleList = _roleManager.Roles
                .ToList()
                .Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name switch
                    {
                        SD.Role_Admin => "Quản trị viên",
                        SD.Role_Employee => "Nhân viên bán hàng",
                        SD.Role_StockManager => "Nhân viên kho hàng",
                        SD.Role_Customer => "Khách hàng",
                        _ => r.Name
                    }
                });

            return Page();
        }

        // ================================
        // Helpers
        // ================================
        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Không thể tạo instance của '{nameof(ApplicationUser)}'. " +
                                                    "Hãy đảm bảo class không abstract và có constructor không tham số.");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
                throw new NotSupportedException("User store phải hỗ trợ Email.");

            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
