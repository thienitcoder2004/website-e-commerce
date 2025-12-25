using Microsoft.AspNetCore.Mvc;
using website_e_commerce.Models;
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
            int pageSize = 6;
            var list = _newsRepo.GetAllNews().ToList();

            int totalPages = (int)Math.Ceiling(list.Count / (double)pageSize);
            list = list.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(list);
        }

        public IActionResult Detail(int id)
        {
            var news = _newsRepo.GetNewsById(id);
            if (news == null) return NotFound();

            ViewBag.Related = _newsRepo.GetAllNews()
                .Where(x => x.Id != id)
                .Take(4).ToList();

            news.ViewCount++;
            _newsRepo.UpdateNews(news);

            return View(news);
        }

        public IActionResult Add() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        public IActionResult Delete(int id)
        {
            var news = _newsRepo.GetNewsById(id);
            if (news == null) return NotFound();
            return View(news);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _newsRepo.DeleteNews(id);
            TempData["Message"] = "🗑️ Đã xóa bài viết!";
            return RedirectToAction(nameof(Index));
        }

    }
}
