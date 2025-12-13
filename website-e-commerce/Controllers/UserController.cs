using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using website_e_commerce.Models;

namespace website_e_commerce.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        // Email admin mặc định – phải trùng với Program.cs
        private const string DefaultAdminEmail = "admin@gmail.com";

        public UserController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: /User?searchEmail=...
        public async Task<IActionResult> Index(string? searchEmail)
        {
            // Ẩn admin mặc định khỏi list
            var query = _userManager.Users
                .Where(u => u.Email != DefaultAdminEmail);

            // Lọc theo email nếu có
            if (!string.IsNullOrWhiteSpace(searchEmail))
            {
                searchEmail = searchEmail.Trim();
                query = query.Where(u => u.Email.Contains(searchEmail));
            }

            var users = await query.ToListAsync();
            var list = new List<UserListVM>();

            foreach (var u in users)
            {
                var roles = await _userManager.GetRolesAsync(u);

                list.Add(new UserListVM
                {
                    Id = u.Id,
                    HoTen = u.HoTen,
                    Email = u.Email,
                    SoDienThoai = u.PhoneNumber,
                    Role = roles.FirstOrDefault() ?? "Chưa có",
                    IsLocked = u.LockoutEnd.HasValue && u.LockoutEnd > DateTimeOffset.UtcNow
                });
            }

            ViewData["CurrentFilter"] = searchEmail;
            return View(list);
        }

        // GET: /User/Edit/{id}
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            // Không cho sửa admin mặc định
            if (user.Email == DefaultAdminEmail)
                return RedirectToAction(nameof(Index));

            var roles = await _userManager.GetRolesAsync(user);
            var allRoles = await _roleManager.Roles.ToListAsync(); // lấy ra List trước rồi mới switch

            var model = new UserEditVM
            {
                Id = user.Id,
                Ho = user.Ho,
                Ten = user.Ten,
                Email = user.Email,
                SoDienThoai = user.PhoneNumber,
                Role = roles.FirstOrDefault(),

                RoleList = allRoles
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
                    .ToList()
            };

            return View(model);
        }

        // POST: /User/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditVM model)
        {
            if (!ModelState.IsValid)
            {
                var allRoles = await _roleManager.Roles.ToListAsync();

                model.RoleList = allRoles
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
                    .ToList();

                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null) return NotFound();

            // Không cho chỉnh admin mặc định
            if (user.Email == DefaultAdminEmail)
                return RedirectToAction(nameof(Index));

            // Cập nhật thông tin cơ bản
            user.Ho = model.Ho;
            user.Ten = model.Ten;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.PhoneNumber = model.SoDienThoai;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                var allRoles = await _roleManager.Roles.ToListAsync();

                model.RoleList = allRoles
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
                    .ToList();

                return View(model);
            }

            // Cập nhật Role
            var currentRoles = await _userManager.GetRolesAsync(user);
            var newRole = model.Role;

            if (!string.IsNullOrEmpty(newRole))
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                await _userManager.AddToRoleAsync(user, newRole);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: /User/LockUnlock/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LockUnlock(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            // Không cho khóa admin mặc định
            if (user.Email == DefaultAdminEmail)
                return RedirectToAction(nameof(Index));

            if (user.LockoutEnd.HasValue && user.LockoutEnd > DateTimeOffset.UtcNow)
            {
                // đang khóa => mở
                user.LockoutEnd = null;
            }
            else
            {
                // đang hoạt động => khóa 100 năm
                user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(100);
            }

            await _userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Index));
        }

        // POST: /User/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            // Không cho xóa admin mặc định
            if (user.Email == DefaultAdminEmail)
                return RedirectToAction(nameof(Index));

            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }
    }
}
