using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using website_e_commerce.Models;
using website_e_commerce.Repositories;

namespace website_e_commerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INewsRepository _newsRepo;

        public HomeController(ILogger<HomeController> logger, INewsRepository newsRepo)
        {
            _logger = logger;
            _newsRepo = newsRepo;
        }

        public IActionResult Index()
        {
            // 📰 Lấy 5 tin mới nhất
            var latestNews = _newsRepo.GetAllNews()
                .OrderByDescending(n => n.PublishedDate)
                .Take(5)
                .ToList();

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
