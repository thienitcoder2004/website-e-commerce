using Microsoft.AspNetCore.Mvc;
using website_e_commerce.Models;
using website_e_commerce.Repositories;

namespace website_e_commerce.Controllers.Admin
{
    public class PromotionController : Controller
    {
        private readonly IPromotionRepository _promotionRepo;

        public PromotionController(IPromotionRepository promotionRepo)
        {
            _promotionRepo = promotionRepo;
        }

        // 📦 Danh sách
        public async Task<IActionResult> Index()
        {
            var promos = await _promotionRepo.GetAllAsync();
            return View(promos);
        }

        // ➕ Hiển thị form thêm
        public IActionResult Add() => View();

        // ✅ Xử lý thêm mới
        [HttpPost]
        public async Task<IActionResult> Add(PromotionCode promo)
        {
            // ⚙️ Chuẩn hóa dữ liệu trước khi lưu
            if (promo.MaxUsage.HasValue && promo.MaxUsage <= 0)
                promo.MaxUsage = null; // coi như không giới hạn

            if (ModelState.IsValid)
            {
                await _promotionRepo.AddAsync(promo);
                TempData["Success"] = "✅ Đã thêm mã khuyến mãi mới!";
                return RedirectToAction(nameof(Index));
            }
            return View(promo);
        }

        // ✏️ Sửa
        public async Task<IActionResult> Edit(int id)
        {
            var promo = await _promotionRepo.GetByIdAsync(id);
            if (promo == null) return NotFound();
            return View(promo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PromotionCode promo)
        {
            if (id != promo.Id) return NotFound();

            if (promo.MaxUsage.HasValue && promo.MaxUsage <= 0)
                promo.MaxUsage = null; // coi như không giới hạn

            if (ModelState.IsValid)
            {
                await _promotionRepo.UpdateAsync(promo);
                TempData["Success"] = "✅ Cập nhật thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(promo);
        }

        // 🗑️ Xóa
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _promotionRepo.DeleteAsync(id);
            TempData["Success"] = "🗑️ Đã xóa mã khuyến mãi!";
            return RedirectToAction(nameof(Index));
        }
    }
}
