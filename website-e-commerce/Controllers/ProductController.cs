using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using website_e_commerce.Models;
using website_e_commerce.Repositories;

namespace website_e_commerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IWebHostEnvironment _env;

        public ProductController(
            ApplicationDbContext context,
            IProductRepository productRepository,
            IReviewRepository reviewRepository,
            ICategoryRepository categoryRepository,
            IWebHostEnvironment env)
        {
            _context = context;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _reviewRepository = reviewRepository;
            _env = env;
        }

        // ✅ Danh sách sản phẩm (lọc theo tên)
        public IActionResult Index(string? query, string? sortOrder, int page = 1)
        {
            int pageSize = 12;
            var products = _productRepository.GetAll().ToList();

            // 🔍 Tìm kiếm
            if (!string.IsNullOrEmpty(query))
            {
                string keyword = query.ToLower().Trim();

                products = products
                    .Where(p =>
                        p.Name.ToLower().Contains(keyword) ||
                        (p.Brand != null && p.Brand.Name.ToLower().Replace(" ", "").Contains(keyword)) ||
                        (p.Category != null && p.Category.Name.ToLower().Replace(" ", "").Contains(keyword))
                    )
                    .ToList();

                ViewBag.SearchKeyword = query;
            }
            else
            {
                ViewBag.SearchKeyword = "Tất cả sản phẩm";
            }

            // 🔽 Sắp xếp
            switch (sortOrder)
            {
                case "price_asc":
                    products = products.OrderBy(p => p.NewPrice).ToList();
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.NewPrice).ToList();
                    break;
                case "name_asc":
                    products = products.OrderBy(p => p.Name).ToList();
                    break;
                case "name_desc":
                    products = products.OrderByDescending(p => p.Name).ToList();
                    break;
                default:
                    products = products.OrderBy(p => p.Id).ToList();
                    break;
            }

            // 🔢 Phân trang
            int totalProducts = products.Count();
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            var paginated = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // 📤 Gửi thông tin sang View
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.HasPreviousPage = page > 1;
            ViewBag.HasNextPage = page < totalPages;
            ViewBag.SortOrder = sortOrder;

            return View(paginated);
        }

        // ✅ Hiển thị sản phẩm theo danh mục
        public IActionResult Category(string name)
        {
            if (string.IsNullOrEmpty(name))
                return RedirectToAction("Index");

            string categoryName = name.Trim().ToLower();

            var products = _productRepository.GetAll()
                .Where(p => p.Category != null &&
                            p.Category.Name.ToLower().Replace(" ", "") == categoryName.Replace(" ", ""))
                .ToList();

            ViewBag.CategoryName = name;

            if (!products.Any())
                ViewBag.Message = $"⚠️ Hiện chưa có sản phẩm nào trong danh mục '{name}'.";

            return View("Index", products);
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

            if (product.OldPrice > 0 && product.DiscountPercent > 0)
            {
                product.NewPrice = product.OldPrice - (product.OldPrice * product.DiscountPercent / 100);
            }

            _productRepository.Add(product);
            TempData["success"] = "✅ Đã thêm sản phẩm và tính giá giảm tự động.";
            return RedirectToAction(nameof(Index));
        }

        // ✅ Nhập kho
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

            // ✅ Thêm dòng này để ghi lịch sử
            _context.StockHistory.Add(new StockHistory
            {
                ProductId = id,
                ActionType = "Import",
                Quantity = importQuantity,
                Note = "Nhập thêm vào kho",
                CreatedAt = DateTime.Now
            });
            _context.SaveChanges();

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

            product.StockQuantity -= exportQuantity;
            product.WebQuantity += exportQuantity;
            _productRepository.Update(product);

            // ✅ Ghi lịch sử xuất kho
            _context.StockHistory.Add(new StockHistory
            {
                ProductId = id,
                ActionType = "Export",
                Quantity = exportQuantity,
                Note = "Xuất sản phẩm lên web bán",
                CreatedAt = DateTime.Now
            });
            _context.SaveChanges();

            TempData["success"] = $"🚀 Đã xuất {exportQuantity} sản phẩm '{product.Name}' ra web.";
            return RedirectToAction(nameof(Index));
        }


        // ✅ Chi tiết sản phẩm
        public IActionResult Details(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
                return NotFound();

            if (product.StockQuantity < 0) product.StockQuantity = 0;
            if (product.WebQuantity < 0) product.WebQuantity = 0;

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
