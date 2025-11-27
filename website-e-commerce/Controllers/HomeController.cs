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
        private readonly IFlashSaleRepository _flashRepo;

        public HomeController(
            ILogger<HomeController> logger,
            ApplicationDbContext context,
            INewsRepository newsRepo,
            IFlashSaleRepository flashRepo)
        {
            _logger = logger;
            _context = context;
            _newsRepo = newsRepo;
            _flashRepo = flashRepo;
        }

        public IActionResult Index()
        {
            var now = DateTime.Now;
            var flashSales = _context.FlashSales
      .Include(x => x.Product)
      .Where(x => x.IsActive && x.EndTime > now)
      .OrderBy(x => x.StartTime)
      .ToList();

            // TẠO SLOT GIỜ CHUẨN
            var slots = flashSales
      .Select(x => new DateTime(
          x.StartTime.Year,
          x.StartTime.Month,
          x.StartTime.Day,
          x.StartTime.Hour,
          x.StartTime.Minute,
          0))
      .Distinct()
      .OrderBy(t => t)
      .Select(t => new { StartTime = t })
      .ToList();


            ViewBag.FlashSales = flashSales;
            ViewBag.TimeSlots = slots;

            var latestProducts = _context.Products
         .OrderByDescending(x => x.Id)
         .Take(8)
         .ToList();

            ViewBag.LatestProducts = latestProducts;

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
