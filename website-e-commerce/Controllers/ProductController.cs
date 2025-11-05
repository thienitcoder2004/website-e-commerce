using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using website_e_commerce.Models;
using website_e_commerce.Repositories;

namespace website_e_commerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IWebHostEnvironment _env;

        public ProductController(
            IProductRepository productRepository,
            IReviewRepository reviewRepository,
            ICategoryRepository categoryRepository,
            IWebHostEnvironment env)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _reviewRepository = reviewRepository;
            _env = env;
        }

        // ✅ Danh sách sản phẩm
        public IActionResult Index()
        {
            var products = _productRepository.GetAll()
                .OrderBy(p => p.Name)
                .ToList();
            return View(products);
        }

        // ✅ Thêm sản phẩm
        public IActionResult Add()
        {
            ViewBag.Categories = new SelectList(_categoryRepository.GetAllCategories(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Product product, IFormFile? imageUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_categoryRepository.GetAllCategories(), "Id", "Name", product.CategoryId);
                return View(product);
            }

            if (imageUrl != null)
                product.ImageUrl = await SaveImage(imageUrl);

            product.WebQuantity = 0;

            // ✅ Nếu người dùng nhập giá cũ và % giảm thì tự tính giá mới
            if (product.OldPrice > 0 && product.DiscountPercent > 0)
            {
                product.NewPrice = product.OldPrice - (product.OldPrice * product.DiscountPercent / 100);
            }

            _productRepository.Add(product);

            TempData["success"] = "✅ Đã thêm sản phẩm và tính giá giảm tự động.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ImportToStock(int id, int importQuantity)
        {
            var product = _productRepository.GetById(id);
            if (product == null) return NotFound();

            if (importQuantity <= 0)
            {
                TempData["error"] = "⚠️ Số lượng nhập phải lớn hơn 0.";
                return RedirectToAction(nameof(Index));
            }

            product.StockQuantity += importQuantity;
            _productRepository.Update(product);

            TempData["success"] = $"📦 Đã nhập thêm {importQuantity} sản phẩm '{product.Name}' vào kho.";
            return RedirectToAction(nameof(Index));
        }


        // ✅ Xuất sản phẩm ra web
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExportToWeb(int id, int exportQuantity)
        {
            var product = _productRepository.GetById(id);
            if (product == null) return NotFound();

            if (exportQuantity <= 0)
            {
                TempData["error"] = "⚠️ Số lượng xuất ra web phải lớn hơn 0.";
                return RedirectToAction(nameof(Index));
            }

            if (product.StockQuantity < exportQuantity)
            {
                TempData["error"] = $"⚠️ Không đủ hàng trong kho. Hiện còn {product.StockQuantity} sản phẩm.";
                return RedirectToAction(nameof(Index));
            }

            // ✅ Trừ kho, tăng web
            product.StockQuantity -= exportQuantity;
            product.WebQuantity += exportQuantity;

            _productRepository.Update(product);

            TempData["success"] = $"🚀 Đã xuất {exportQuantity} sản phẩm '{product.Name}' ra web.";
            return RedirectToAction(nameof(Index));
        }


        // ✅ Khi người dùng xem chi tiết sản phẩm
        public IActionResult Details(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
                return NotFound();

            // Nếu chưa có số lượng thì gán mặc định tránh lỗi null
            if (product.StockQuantity < 0) product.StockQuantity = 0;
            if (product.WebQuantity < 0) product.WebQuantity = 0;

            // Nếu sản phẩm chưa được xuất ra web thì cho xem thông tin, nhưng ẩn nút mua
            ViewBag.Reviews = _reviewRepository.GetReviewsByProductId(id).ToList();
            return View(product);
        }


        // ✅ Xóa sản phẩm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null) return NotFound();

            _productRepository.Delete(id);
            TempData["success"] = "🗑️ Đã xóa sản phẩm & kho tương ứng.";
            return RedirectToAction(nameof(Index));
        }

        // ✅ Hàm lưu ảnh
        private async Task<string> SaveImage(IFormFile image)
        {
            string folder = Path.Combine(_env.WebRootPath, "images", "product");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            string filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return "/images/product/" + fileName;
        }
    }
}
