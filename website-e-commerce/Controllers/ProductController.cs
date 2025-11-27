using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using website_e_commerce.Extensions;
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
        private const string CompareSessionKey = "CompareProducts";

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

        // ================================
        // DANH SÁCH SẢN PHẨM + BỘ LỌC
        // ================================
        public IActionResult Index(
            string? query,
            string? sortOrder,
            int? categoryId,
            string? filter,
            int page = 1)
        {
            int pageSize = 12;

            // Lấy toàn bộ sản phẩm
            var products = _productRepository.GetAll().ToList();

            // ==========================
            // AUTO CATEGORY KHI SEARCH
            // ==========================
            if (!string.IsNullOrEmpty(query) && categoryId == null)
            {
                string keyword = query.ToLower().Trim();

                var first = products.FirstOrDefault(p =>
                    p.Name.ToLower().Contains(keyword) ||
                    (p.Category != null && p.Category.Name.ToLower().Contains(keyword)));

                if (first != null)
                    categoryId = first.CategoryId;
            }

            // ==========================
            // LOAD BỘ LỌC (CATEGORY HOẶC MẶC ĐỊNH)
            // ==========================
            if (categoryId == null || categoryId == 0)
            {
                ViewBag.CategoryId = 0;

                ViewBag.Filters = new
                {
                    CategoryId = 0,
                    Groups = new List<object>
                    {
                        new {
                            Id = 100,
                            GroupName = "Hãng",
                            Options = _context.Brands
                                .Where(b => b.IsActive)
                                .Select(b => new { Label = b.Name, Value = $"brand={b.Name}" })
                                .ToList()
                        },
                        new {
                            Id = 200,
                            GroupName = "Giá tiền",
                            Options = new List<object>
                            {
                                new { Label = "Dưới 5 triệu", Value = "price<5000000" },
                                new { Label = "5 - 10 triệu", Value = "price=5-10" },
                                new { Label = "10 - 20 triệu", Value = "price=10-20" },
                                new { Label = "Trên 20 triệu", Value = "price>20000000" }
                            }
                        }
                    }
                };
            }
            else
            {
                // Có danh mục ⇒ load filter theo category
                var filterCategory = _context.FilterCategories
                    .Include(fc => fc.Groups)
                    .ThenInclude(g => g.Options)
                    .FirstOrDefault(fc => fc.CategoryId == categoryId);

                ViewBag.CategoryId = categoryId;
                ViewBag.Filters = filterCategory;
            }

            // ==========================
            // ÁP DỤNG BỘ LỌC
            // ==========================
            if (!string.IsNullOrEmpty(filter))
            {
                // ==== BRAND ====
                if (filter.StartsWith("brand="))
                {
                    var brandName = filter.Replace("brand=", "").ToLower();
                    products = products.Where(p =>
                        p.Brand != null &&
                        p.Brand.Name.ToLower() == brandName
                    ).ToList();
                }

                // ==== GIÁ: price<5000000 ====
                if (filter.StartsWith("price<"))
                {
                    decimal max = decimal.Parse(filter.Replace("price<", ""));
                    products = products.Where(p => p.NewPrice < max).ToList();
                }

                // ==== GIÁ: price>20000000 ====
                if (filter.StartsWith("price>"))
                {
                    decimal min = decimal.Parse(filter.Replace("price>", ""));
                    products = products.Where(p => p.NewPrice > min).ToList();
                }

                // ==== GIÁ: price=5-10 ====
                if (filter.StartsWith("price=") && filter.Contains("-"))
                {
                    var parts = filter.Replace("price=", "").Split('-');
                    decimal min = decimal.Parse(parts[0]) * 1_000_000;
                    decimal max = decimal.Parse(parts[1]) * 1_000_000;

                    products = products
                        .Where(p => p.NewPrice >= min && p.NewPrice <= max)
                        .ToList();
                }

                // ==== ROM ====
                if (filter.StartsWith("rom="))
                {
                    var rom = filter.Replace("rom=", "");
                    products = products.Where(p => p.Specifications?.Contains(rom) == true).ToList();
                }

                // ==== RAM ====
                if (filter.StartsWith("ram="))
                {
                    var ram = filter.Replace("ram=", "");
                    products = products.Where(p => p.Specifications?.Contains(ram) == true).ToList();
                }

                // ==== CPU ====
                if (filter.StartsWith("cpu="))
                {
                    var cpu = filter.Replace("cpu=", "");
                    products = products.Where(p => p.Specifications?.Contains(cpu) == true).ToList();
                }

                // ==== GPU ====
                if (filter.StartsWith("gpu="))
                {
                    var gpu = filter.Replace("gpu=", "");
                    products = products.Where(p => p.Specifications?.Contains(gpu) == true).ToList();
                }

                // ==== SIZE ====
                if (filter.StartsWith("size="))
                {
                    var size = filter.Replace("size=", "");
                    products = products.Where(p => p.Specifications?.Contains(size) == true).ToList();
                }
            }

            // ==========================
            // TÌM KIẾM
            // ==========================
            if (!string.IsNullOrEmpty(query))
            {
                string keyword = query.ToLower().Trim();

                products = products
                    .Where(p =>
                        p.Name.ToLower().Contains(keyword) ||
                        (p.Brand != null && p.Brand.Name.ToLower().Contains(keyword)) ||
                        (p.Category != null && p.Category.Name.ToLower().Contains(keyword))
                    )
                    .ToList();

                ViewBag.SearchKeyword = query;
            }
            else
            {
                ViewBag.SearchKeyword = "Tất cả sản phẩm";
            }

            // ==========================
            // SẮP XẾP
            // ==========================
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

            // ==========================
            // PHÂN TRANG
            // ==========================
            int totalProducts = products.Count();
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            var paginated = products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SortOrder = sortOrder;
            ViewBag.Filter = filter;

            // ==========================
            // YÊU THÍCH
            // ==========================
            List<int> favoriteIds = new();
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!string.IsNullOrEmpty(userId))
                {
                    favoriteIds = _context.FavoriteProducts
                        .Where(fp => fp.UserId == userId)
                        .Select(fp => fp.ProductId)
                        .ToList();
                }
            }
            ViewBag.FavoriteIds = favoriteIds;

            return View(paginated);
        }

        // ================================
        // SEARCH SUGGESTIONS (API GỢI Ý)
        // ================================
        [HttpGet]
        public async Task<IActionResult> SearchSuggestions(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return Json(new List<object>());

            var result = await _context.Products
                .Where(p => p.Name.Contains(term))
                .OrderBy(p => p.Name)
                .Take(10)
                .Select(p => new
                {
                    id = p.Id,
                    name = p.Name,
                    price = p.NewPrice,      // ⚠ Nếu bạn muốn giá khuyến mãi
                    image = p.ImageUrl
                })
                .ToListAsync();

            return Json(result);
        }


        // ================================
        // FAVORITE LIST
        // ================================
        [Authorize]
        public IActionResult Favorites()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return RedirectToPage("/Account/Login", new { area = "Identity" });

            var favoriteProducts = _context.Products
                .Where(p => p.FavoriteProducts.Any(fp => fp.UserId == userId))
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .ToList();

            return View(favoriteProducts);
        }

        // ================================
        // TOGGLE FAVORITE
        // ================================
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleFavorite(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var favorite = _context.FavoriteProducts
                .FirstOrDefault(fp => fp.UserId == userId && fp.ProductId == productId);

            bool isFavorite;

            if (favorite == null)
            {
                _context.FavoriteProducts.Add(new FavoriteProduct
                {
                    UserId = userId,
                    ProductId = productId
                });
                isFavorite = true;
            }
            else
            {
                _context.FavoriteProducts.Remove(favorite);
                isFavorite = false;
            }

            _context.SaveChanges();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return Json(new { success = true, isFavorite });

            var referer = Request.Headers["Referer"].ToString();
            return !string.IsNullOrEmpty(referer)
                ? Redirect(referer)
                : RedirectToAction(nameof(Index));
        }

        // ================================
        // CATEGORY = REDIRECT INDEX
        // ================================
        public IActionResult Category(string name)
        {
            if (string.IsNullOrEmpty(name))
                return RedirectToAction("Index");

            string key = name.Trim().ToLower().Replace(" ", "");

            var category = _context.Categories
                .FirstOrDefault(c => c.Name.ToLower().Replace(" ", "") == key);

            if (category == null)
                return RedirectToAction("Index", new { query = name });

            return RedirectToAction("Index", new { categoryId = category.Id });
        }

        // ================================
        // ADD (FPTSHOP STYLE)
        // ================================
        public IActionResult Add()
        {
            ViewBag.Categories = new SelectList(_categoryRepository.GetAllCategories(), "Id", "Name");
            ViewBag.Brands = new SelectList(_context.Brands.Where(b => b.IsActive), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(
             Product product,
             IFormFile? thumbnail,
             List<IFormFile>? galleryImages)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_categoryRepository.GetAllCategories(), "Id", "Name", product.CategoryId);
                ViewBag.Brands = new SelectList(_context.Brands.Where(b => b.IsActive), "Id", "Name", product.BrandId);
                return View(product);
            }

            // ============== ẢNH CHÍNH ==============
            if (thumbnail == null)
            {
                ModelState.AddModelError("", "Bạn phải chọn ảnh đại diện.");
                return View(product);
            }

            product.ImageUrl = await SaveImage(thumbnail);

            // ============== GIÁ SAU GIẢM ==============
            if (product.OldPrice > 0 && product.DiscountPercent > 0)
            {
                product.NewPrice = product.OldPrice - (product.OldPrice * product.DiscountPercent / 100);
            }
            if (product.NewPrice <= 0)
                product.NewPrice = product.OldPrice;

            product.WebQuantity = Math.Max(0, product.WebQuantity);

            // ============== LƯU SẢN PHẨM ==============
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // ============== ẢNH GALLERY (nhiều ảnh) ==============
            if (galleryImages != null)
            {
                foreach (var img in galleryImages)
                {
                    if (img != null && img.Length > 0)
                    {
                        string url = await SaveImage(img);

                        _context.ProductImages.Add(new ProductImage
                        {
                            ProductId = product.Id,
                            ImageUrl = url
                        });
                    }
                }
                await _context.SaveChangesAsync();
            }

            TempData["success"] = "Đã thêm sản phẩm thành công!";
            return RedirectToAction(nameof(Index));
        }

        // ================================
        // STOCK IMPORT
        // ================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ImportToStock(int id, int importQuantity)
        {
            var product = _productRepository.GetById(id);

            if (product == null)
                return NotFound();

            if (importQuantity <= 0)
            {
                TempData["error"] = "Số lượng nhập phải lớn hơn 0.";
                return RedirectToAction(nameof(Index));
            }

            product.StockQuantity += importQuantity;
            _productRepository.Update(product);

            _context.StockHistory.Add(new StockHistory
            {
                ProductId = id,
                ActionType = "Import",
                Quantity = importQuantity,
                Note = "Nhập kho",
                CreatedAt = DateTime.Now
            });

            _context.SaveChanges();

            TempData["success"] = $"Đã nhập thêm {importQuantity} sản phẩm.";
            return RedirectToAction(nameof(Index));
        }

        // ================================
        // STOCK EXPORT
        // ================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExportToWeb(int id, int exportQuantity)
        {
            var product = _productRepository.GetById(id);

            if (product == null)
                return NotFound();

            if (exportQuantity <= 0)
            {
                TempData["error"] = "Số lượng phải lớn hơn 0.";
                return RedirectToAction(nameof(Index));
            }

            if (product.StockQuantity < exportQuantity)
            {
                TempData["error"] =
                    $"Không đủ hàng trong kho. Hiện còn {product.StockQuantity}.";
                return RedirectToAction(nameof(Index));
            }

            product.StockQuantity -= exportQuantity;
            product.WebQuantity += exportQuantity;

            _productRepository.Update(product);

            _context.StockHistory.Add(new StockHistory
            {
                ProductId = id,
                ActionType = "Export",
                Quantity = exportQuantity,
                Note = "Xuất hàng lên web",
                CreatedAt = DateTime.Now
            });

            _context.SaveChanges();

            TempData["success"] = $"Đã xuất {exportQuantity} sản phẩm.";
            return RedirectToAction(nameof(Index));
        }

        // ================================
        // DETAILS
        // ================================
        public IActionResult Details(int id)
        {
            var product = _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            product.StockQuantity = Math.Max(0, product.StockQuantity);
            product.WebQuantity = Math.Max(0, product.WebQuantity);

            ViewBag.Reviews = _reviewRepository.GetReviewsByProductId(id).ToList();

            ViewBag.RelatedProducts = _context.Products
                .Where(p => p.CategoryId == product.CategoryId && p.Id != product.Id)
                .Take(8)
                .ToList();

            return View(product);
        }


        // ================================
        // DELETE
        // ================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _productRepository.GetById(id);

            if (product == null)
                return NotFound();

            _productRepository.Delete(id);

            TempData["success"] = "Đã xoá sản phẩm.";
            return RedirectToAction(nameof(Index));
        }

        // ================================
        // SO SÁNH
        // ================================
        private List<int> GetCompareList()
        {
            var list = HttpContext.Session.GetObjectFromJson<List<int>>(CompareSessionKey);
            return list ?? new List<int>();
        }

        private void SaveCompareList(List<int> productIds)
        {
            HttpContext.Session.SetObjectAsJson(CompareSessionKey, productIds);
        }

        public IActionResult Compare()
        {
            var compareList = GetCompareList();

            var products = compareList.Select(id => _productRepository.GetById(id))
                .Where(p => p != null).ToList();

            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCompare(int id)
        {
            var product = _productRepository.GetById(id);

            if (product == null)
            {
                TempData["error"] = "Sản phẩm không tồn tại.";
                return RedirectToAction(nameof(Index));
            }

            var compareList = GetCompareList();

            if (!compareList.Contains(id))
            {
                if (compareList.Count >= 3)
                {
                    TempData["error"] = "Chỉ so sánh tối đa 3 sản phẩm.";
                }
                else
                {
                    compareList.Add(id);
                    SaveCompareList(compareList);

                    TempData["success"] = $"Đã thêm '{product.Name}' vào danh sách so sánh.";
                }
            }
            else
            {
                TempData["info"] = "Sản phẩm đã có trong danh sách.";
            }

            var referer = Request.Headers["Referer"].ToString();
            return !string.IsNullOrEmpty(referer)
                ? Redirect(referer)
                : RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCompare(int id)
        {
            var compareList = GetCompareList();

            if (compareList.Contains(id))
            {
                compareList.Remove(id);
                SaveCompareList(compareList);
            }

            return RedirectToAction(nameof(Compare));
        }

        // ================================
        // LƯU ẢNH
        // ================================
        private async Task<string> SaveImage(IFormFile image)
        {
            string folder = Path.Combine(_env.WebRootPath, "images", "products");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            string filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return "/images/products/" + fileName;
        }
    }
}
