using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using website_e_commerce.Models;
using website_e_commerce.Repositories;
using System.Diagnostics;

namespace website_e_commerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly INewsRepository _newsRepo;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, INewsRepository newsRepo)
        {
            _logger = logger;
            _context = context;
            _newsRepo = newsRepo;
        }

        // 🏠 Trang chủ
        public IActionResult Index()
        {
            // ✅ Lấy 8 sản phẩm mới nhất
            var latestProducts = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .OrderByDescending(p => p.Id)
                .Take(8)
                .ToList();

            // ✅ Lấy 5 tin tức mới nhất
            var latestNews = _newsRepo.GetAllNews()
                .OrderByDescending(n => n.PublishedDate)
                .Take(5)
                .ToList();

            ViewBag.LatestProducts = latestProducts;
            ViewBag.RecentNews = latestNews;
            ViewBag.NewsCount = latestNews.Count;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
