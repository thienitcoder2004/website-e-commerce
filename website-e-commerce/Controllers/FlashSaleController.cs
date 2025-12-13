using Microsoft.AspNetCore.Mvc;
using website_e_commerce.Models;
using website_e_commerce.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace website_e_commerce.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class FlashSaleController : Controller
    {
        private readonly IFlashSaleRepository _repo;
        private readonly ApplicationDbContext _db;

        public FlashSaleController(IFlashSaleRepository repo, ApplicationDbContext db)
        {
            _repo = repo;
            _db = db;
        }

        // LIST
        public IActionResult Index()
        {
            var list = _repo.GetAll();
            return View(list);
        }

        // ADD
        public IActionResult Add()
        {
            ViewBag.Products = _db.Products.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Add(FlashSale fs)
        {
            if (fs.EndTime <= fs.StartTime)
            {
                ModelState.AddModelError("", "Thời gian không hợp lệ!");
                ViewBag.Products = _db.Products.ToList();
                return View(fs);
            }

            _repo.Add(fs);

            TempData["SuccessMessage"] = "Tạo Flash Sale thành công!";
            return RedirectToAction("Index");
        }

        // EDIT
        public IActionResult Edit(int id)
        {
            var fs = _repo.GetById(id);
            if (fs == null) return NotFound();

            ViewBag.Products = _db.Products.ToList();
            return View(fs);
        }

        [HttpPost]
        public IActionResult Edit(FlashSale fs)
        {
            if (fs.EndTime <= fs.StartTime)
            {
                ModelState.AddModelError("", "Thời gian không hợp lệ!");
                ViewBag.Products = _db.Products.ToList();
                return View(fs);
            }

            _repo.Update(fs);

            TempData["SuccessMessage"] = "Cập nhật Flash Sale thành công!";
            return RedirectToAction("Index");
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            _repo.Delete(id);

            TempData["SuccessMessage"] = "Đã xóa Flash Sale!";
            return RedirectToAction("Index");
        }
    }
}
