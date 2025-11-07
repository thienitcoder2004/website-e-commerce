using Microsoft.AspNetCore.Mvc;
using website_e_commerce.Models;
using website_e_commerce.Repositories;

namespace website_e_commerce.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandRepository _brandRepo;
        private readonly IWebHostEnvironment _env;

        public BrandController(IBrandRepository brandRepo, IWebHostEnvironment env)
        {
            _brandRepo = brandRepo;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var brands = await _brandRepo.GetAllAsync();
            return View(brands);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brand brand, IFormFile? logoFile)
        {
            if (ModelState.IsValid)
            {
                if (logoFile != null)
                {
                    string folder = Path.Combine(_env.WebRootPath, "images/brands");
                    Directory.CreateDirectory(folder);
                    string fileName = Guid.NewGuid() + Path.GetExtension(logoFile.FileName);
                    string path = Path.Combine(folder, fileName);

                    using var stream = new FileStream(path, FileMode.Create);
                    await logoFile.CopyToAsync(stream);

                    brand.LogoUrl = "/images/brands/" + fileName;
                }

                await _brandRepo.AddAsync(brand);
                TempData["SuccessMessage"] = "✅ Thêm thương hiệu thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var brand = await _brandRepo.GetByIdAsync(id);
            if (brand == null) return NotFound();
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Brand brand, IFormFile? logoFile)
        {
            if (id != brand.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var existing = await _brandRepo.GetByIdAsync(id);
                if (existing == null) return NotFound();

                if (logoFile != null)
                {
                    string folder = Path.Combine(_env.WebRootPath, "images/brands");
                    Directory.CreateDirectory(folder);
                    string fileName = Guid.NewGuid() + Path.GetExtension(logoFile.FileName);
                    string path = Path.Combine(folder, fileName);

                    using var stream = new FileStream(path, FileMode.Create);
                    await logoFile.CopyToAsync(stream);

                    brand.LogoUrl = "/images/brands/" + fileName;
                }
                else
                {
                    brand.LogoUrl = existing.LogoUrl;
                }

                await _brandRepo.UpdateAsync(brand);
                TempData["SuccessMessage"] = "✏️ Cập nhật thương hiệu thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _brandRepo.GetByIdAsync(id);
            if (brand == null) return NotFound();
            return View(brand);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _brandRepo.DeleteAsync(id);
            TempData["SuccessMessage"] = "🗑️ Đã xóa thương hiệu thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}
