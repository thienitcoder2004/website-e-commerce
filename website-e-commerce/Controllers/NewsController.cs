using Microsoft.AspNetCore.Mvc;
using website_e_commerce.Models;          // ✅ SD nằm ở đây
using website_e_commerce.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace website_e_commerce.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsRepository _newsRepo;
        private readonly IWebHostEnvironment _env;

        public NewsController(INewsRepository newsRepo, IWebHostEnvironment env)
        {
            _newsRepo = newsRepo;
            _env = env;
        }

        public IActionResult Index(int page = 1)
        {
            bool isStaff = User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_Employee);

            var query = _newsRepo.GetAllNews().AsQueryable();

            // User chỉ xem tin đã đăng
            if (!isStaff)
                query = query.Where(x => x.IsPublished);

            // Tổng tin để admin hiển thị đúng
            int totalNews = query.Count();
            ViewBag.TotalNews = totalNews;

            // ✅ USER: trả FULL LIST, không phân trang
            if (!isStaff)
            {
                ViewBag.CurrentPage = 1;
                ViewBag.TotalPages = 1;

                return View(query
                    .OrderByDescending(x => x.PublishedDate)
                    .ToList());
            }

            // ✅ ADMIN/EMPLOYEE: phân trang 5 tin
            int pageSize = 5;
            int totalPages = (int)Math.Ceiling(totalNews / (double)pageSize);
            if (totalPages <= 0) totalPages = 1;

            if (page < 1) page = 1;
            if (page > totalPages) page = totalPages;

            var list = query
                .OrderByDescending(x => x.PublishedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(list);
        }

        public IActionResult Detail(int id)
        {
            var news = _newsRepo.GetNewsById(id);
            if (news == null) return NotFound();

            ViewBag.Related = _newsRepo.GetAllNews()
                .Where(x => x.Id != id && x.IsPublished)
                .OrderByDescending(x => x.PublishedDate)
                .Take(4).ToList();

            news.ViewCount++;
            _newsRepo.UpdateNews(news);

            return View(news);
        }

        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult Add() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult Add(News model, IFormFile? ImageFile)
        {
            if (!ModelState.IsValid) return View(model);

            if (ImageFile != null)
            {
                string folder = Path.Combine(_env.WebRootPath, "images/news");
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                string file = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                string path = Path.Combine(folder, file);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }

                model.ImageUrl = "/images/news/" + file;
            }

            model.PublishedDate = DateTime.Now;
            _newsRepo.AddNews(model);

            TempData["Message"] = "✅ Thêm bài viết thành công!";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult Edit(int id)
        {
            var news = _newsRepo.GetNewsById(id);
            if (news == null) return NotFound();
            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(News model, IFormFile? ImageFile)
        {
            if (!ModelState.IsValid) return View(model);

            if (ImageFile != null)
            {
                string folder = Path.Combine(_env.WebRootPath, "images/news");
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                string file = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                string path = Path.Combine(folder, file);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }

                model.ImageUrl = "/images/news/" + file;
            }

            model.UpdatedDate = DateTime.Now;
            _newsRepo.UpdateNews(model);

            TempData["Message"] = "✏️ Cập nhật bài viết thành công!";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult Delete(int id)
        {
            var news = _newsRepo.GetNewsById(id);
            if (news == null) return NotFound();
            return View(news);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult DeleteConfirmed(int id)
        {
            _newsRepo.DeleteNews(id);
            TempData["Message"] = "🗑️ Đã xóa bài viết!";
            return RedirectToAction(nameof(Index));
        }
    }
}
