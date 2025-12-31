using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using System.Text.Json;
using website_e_commerce.Extensions;
using website_e_commerce.Models;
using website_e_commerce.Repositories;

namespace website_e_commerce.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoppingCartController(
            IProductRepository productRepository,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _productRepository = productRepository;
            _context = context;
            _userManager = userManager;
        }

        // ==========================
        // ✅ PROMO UTILS (THÊM MỚI)
        // ==========================
        private (decimal original, decimal discount, decimal final, string? code, string? error) CalcPromo(decimal originalTotal)
        {
            var code = HttpContext.Session.GetString("PromoCode");
            if (string.IsNullOrWhiteSpace(code))
                return (originalTotal, 0m, originalTotal, null, null);

            var promo = _context.PromotionCodes.AsNoTracking()
                .FirstOrDefault(p => p.Code.ToUpper() == code.ToUpper());

            if (promo == null)
            {
                HttpContext.Session.Remove("PromoCode");
                return (originalTotal, 0m, originalTotal, null, "Mã giảm giá không tồn tại!");
            }

            var now = DateTime.Now;
            if (now < promo.StartDate || now > promo.EndDate)
            {
                HttpContext.Session.Remove("PromoCode");
                return (originalTotal, 0m, originalTotal, null, "Mã giảm giá đã hết hạn hoặc chưa được kích hoạt!");
            }

            if (!promo.IsActive)
            {
                HttpContext.Session.Remove("PromoCode");
                return (originalTotal, 0m, originalTotal, null, "Mã khuyến mãi đã bị vô hiệu hóa!");
            }

            // ✅ MaxUsage = 0 hoặc null => không giới hạn
            if (promo.MaxUsage.HasValue && promo.MaxUsage.Value > 0 && promo.UsageCount >= promo.MaxUsage.Value)
            {
                HttpContext.Session.Remove("PromoCode");
                return (originalTotal, 0m, originalTotal, null, "Mã giảm giá đã đạt giới hạn sử dụng!");
            }

            decimal final = originalTotal;

            if (promo.DiscountPercent.HasValue && promo.DiscountPercent.Value > 0)
                final -= originalTotal * promo.DiscountPercent.Value / 100m;

            if (promo.DiscountAmount.HasValue && promo.DiscountAmount.Value > 0)
                final -= Convert.ToDecimal(promo.DiscountAmount.Value);

            if (final < 0) final = 0;

            var discount = originalTotal - final;
            return (originalTotal, discount, final, promo.Code, null);
        }

        private void BindPromoViewBags(decimal cartTotal)
        {
            var promo = CalcPromo(cartTotal);
            ViewBag.OriginalTotal = promo.original;
            ViewBag.DiscountAmount = promo.discount;
            ViewBag.FinalTotal = promo.final;
            ViewBag.AppliedCode = promo.code;

            // nếu mã invalid => auto remove + báo cho user
            if (!string.IsNullOrWhiteSpace(promo.error))
                TempData["Error"] = promo.error;
        }

        // ========== CART ==========
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
                return Json(new { success = false, message = "Không tìm thấy sản phẩm!" });

            // ============================
            // 🔥 CHECK FLASH SALE
            // ============================
            var now = DateTime.Now;
            var flashSale = _context.FlashSales
                .FirstOrDefault(x =>
                    x.ProductId == productId &&
                    x.IsActive &&
                    x.StartTime <= now &&
                    x.EndTime > now);

            decimal finalPrice = product.NewPrice;

            // =========================================
            // 🔥 TRƯỜNG HỢP 1 — SẢN PHẨM ĐANG FLASH SALE
            // =========================================
            if (flashSale != null)
            {
                // Check tồn kho flash sale
                int remainingFsQty = flashSale.FlashQuantity - flashSale.SoldQuantity;

                if (remainingFsQty <= 0)
                    return Json(new { success = false, message = "Flash Sale đã bán hết!" });

                if (quantity > remainingFsQty)
                    return Json(new { success = false, message = $"Flash Sale chỉ còn {remainingFsQty} sản phẩm!" });

                // Giá đặc biệt
                finalPrice = flashSale.FlashPrice;
            }
            // =========================================
            // 🔥 TRƯỜNG HỢP 2 — KHÔNG FLASH SALE
            // =========================================
            else
            {
                if (product.WebQuantity <= 0)
                    return Json(new { success = false, message = "Sản phẩm hiện đang hết hàng!" });

                if (quantity > product.WebQuantity)
                    return Json(new { success = false, message = $"Chỉ còn {product.WebQuantity} sản phẩm!" });
            }

            // ============================
            // 🛒 ADD TO CART
            // ============================
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();

            cart.AddItem(new CartItem
            {
                ProductId = product.Id,
                Name = product.Name,
                Price = finalPrice,    // <= TỰ ĐỘNG FLASH SALE / NORMAL
                Quantity = quantity,
                ImageUrl = product.ImageUrl
            });

            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();

            // ✅ THÊM: bind số tiền sau giảm để hiển thị ra view
            BindPromoViewBags(cart.GetTotal());

            return View(cart);
        }

        public IActionResult RemoveFromCart(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart != null)
            {
                cart.RemoveItem(productId);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult IncreaseQuantityAjax(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            cart.IncreaseQuantity(productId);
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            // ✅ THÊM: trả về tổng tiền sau giảm
            var promo = CalcPromo(cart.GetTotal());

            return Json(new
            {
                success = true,
                quantity = item?.Quantity ?? 0,
                itemTotal = item?.Total ?? 0,

                // thêm mới (không phá JS cũ)
                originalTotal = promo.original,
                discount = promo.discount,
                total = promo.final,
                appliedCode = promo.code
            });
        }

        [HttpPost]
        public IActionResult DecreaseQuantityAjax(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            cart.DecreaseQuantity(productId);
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            // ✅ THÊM: trả về tổng tiền sau giảm
            var promo = CalcPromo(cart.GetTotal());

            return Json(new
            {
                success = true,
                quantity = item?.Quantity ?? 0,
                itemTotal = item?.Total ?? 0,

                // thêm mới (không phá JS cũ)
                originalTotal = promo.original,
                discount = promo.discount,
                total = promo.final,
                appliedCode = promo.code
            });
        }

        // ========== CHECKOUT ==========
        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart == null || !cart.Items.Any())
                return RedirectToAction("Index");

            var user = await _userManager.GetUserAsync(User);

            ViewBag.Addresses = await _context.UserAddresses
                .Where(x => x.UserId == user.Id)
                .OrderByDescending(x => x.IsDefault)
                .ToListAsync();

            // ✅ THÊM: bind số tiền sau giảm để hiển thị ra view
            BindPromoViewBags(cart.GetTotal());

            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Checkout(
             int SelectedAddressId,
             string notes,
             string paymentMethod)
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart == null || !cart.Items.Any())
                return RedirectToAction("Index");

            var user = await _userManager.GetUserAsync(User);

            var address = await _context.UserAddresses
                .FirstOrDefaultAsync(x => x.Id == SelectedAddressId && x.UserId == user.Id);

            if (address == null)
            {
                TempData["Error"] = "Vui lòng chọn địa chỉ giao hàng!";
                return RedirectToAction("Checkout");
            }

            string shippingAddress =
                $"{address.HoTen} | {address.DienThoai} | " +
                $"{address.DiaChi}, {address.PhuongXa}, {address.QuanHuyen}, {address.TinhThanh}";

            // ✅ THÊM: tính tổng sau giảm (không dùng double nữa)
            var promo = CalcPromo(cart.GetTotal());

            var order = new Order
            {
                UserId = user.Id,
                UserAddressId = address.Id,
                ShippingAddress = shippingAddress,
                Notes = notes,

                // ✅ dùng tổng đã áp mã
                TotalPrice = promo.final,

                OrderDate = DateTime.Now,
                OrderDetails = cart.Items.Select(i => new OrderDetail
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var payment = new Payment
            {
                OrderId = order.Id,
                PaymentMethod = paymentMethod,
                Amount = order.TotalPrice,
                Status = "Pending",
                TransactionId = Guid.NewGuid().ToString(),
                CustomerName = user.HoTen ?? user.UserName,
                PaymentDate = DateTime.Now
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            if (paymentMethod == "Momo")
                return Redirect(await GenerateMomoPaymentUrl(order, payment));

            if (paymentMethod == "VNPay")
                return Redirect(GenerateVNPayPaymentUrl(order, payment));

            HttpContext.Session.Remove("Cart");
            return RedirectToAction("OrderSuccess", new { orderId = order.Id });
        }

        // ========== MOMO ==========
        private async Task<string> GenerateMomoPaymentUrl(Order order, Payment payment)
        {
            string endpoint = "https://test-payment.momo.vn/v2/gateway/api/create";
            string partnerCode = "MOMO";
            string accessKey = "F8BBA842ECF85";
            string secretKey = "K951B6PE1waDMi640xX08PD3vg6EkVlz";

            string orderInfo = $"Thanh toan don hang {order.Id}";
            string redirectUrl = $"{Request.Scheme}://{Request.Host}/ShoppingCart/MomoReturn";
            string ipnUrl = redirectUrl;

            string orderId = Guid.NewGuid().ToString();
            string requestId = Guid.NewGuid().ToString();
            string amount = ((int)order.TotalPrice).ToString();

            string rawHash =
                $"accessKey={accessKey}&amount={amount}&extraData=&ipnUrl={ipnUrl}" +
                $"&orderId={orderId}&orderInfo={orderInfo}&partnerCode={partnerCode}" +
                $"&redirectUrl={redirectUrl}&requestId={requestId}&requestType=captureWallet";

            using var hmac = new System.Security.Cryptography.HMACSHA256(
                System.Text.Encoding.UTF8.GetBytes(secretKey));

            var signature = BitConverter
                .ToString(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(rawHash)))
                .Replace("-", "").ToLower();

            var payload = new
            {
                partnerCode,
                accessKey,
                requestId,
                amount,
                orderId,
                orderInfo,
                redirectUrl,
                ipnUrl,
                requestType = "captureWallet",
                extraData = "",
                signature
            };

            using var client = new HttpClient();
            var response = await client.PostAsJsonAsync(endpoint, payload);
            var content = await response.Content.ReadAsStringAsync();
            var json = JsonDocument.Parse(content);

            if (json.RootElement.TryGetProperty("payUrl", out var payUrl))
                return payUrl.GetString();

            return "/";
        }

        public async Task<IActionResult> MomoReturn(string orderId, string resultCode)
        {
            // Lấy payment đang chờ xử lý
            var payment = _context.Payments
                .OrderByDescending(p => p.Id)
                .FirstOrDefault(p => p.Status == "Pending");

            if (payment == null)
                return RedirectToAction("Checkout", new { error = "Không tìm thấy giao dịch thanh toán!" });

            // MoMo CANCEL -> KHÔNG có resultCode => hủy
            if (string.IsNullOrEmpty(resultCode))
            {
                payment.Status = "Failed";
                await _context.SaveChangesAsync();

                // ❗ Không xóa giỏ hàng, không trừ tồn kho
                TempData["Error"] = "❌ Bạn đã hủy thanh toán MoMo!";
                return RedirectToAction("Checkout");
            }

            // Trường hợp MoMo trả về mã lỗi
            if (resultCode != "0")
            {
                payment.Status = "Failed";
                await _context.SaveChangesAsync();

                TempData["Error"] = "❌ Thanh toán MoMo thất bại!";
                return RedirectToAction("Checkout");
            }

            // ===== THÀNH CÔNG =====
            payment.Status = "Success";
            await _context.SaveChangesAsync();

            await UpdateStockAfterPaymentSuccess(payment.OrderId);
            await UpdatePromotionUsageAfterSuccess();

            // Chỉ xóa giỏ hàng khi thành công
            HttpContext.Session.Remove("Cart");

            return RedirectToAction("OrderSuccess", new { orderId = payment.OrderId });
        }

        // ========== VNPAY ==========
        public string GenerateVNPayPaymentUrl(Order order, Payment payment)
        {
            string vnp_TmnCode = "YOUR_VNP_TMNCODE";
            string vnp_HashSecret = "YOUR_VNP_HASHSECRET";
            string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
            string vnp_Returnurl = $"{Request.Scheme}://{Request.Host}/ShoppingCart/VNPayReturn";

            var query = new SortedDictionary<string, string>
            {
                ["vnp_Version"] = "2.1.0",
                ["vnp_Command"] = "pay",
                ["vnp_TmnCode"] = vnp_TmnCode,
                ["vnp_Amount"] = ((int)order.TotalPrice * 100).ToString(),
                ["vnp_CreateDate"] = DateTime.Now.ToString("yyyyMMddHHmmss"),
                ["vnp_CurrCode"] = "VND",
                ["vnp_IpAddr"] = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1",
                ["vnp_Locale"] = "vn",
                ["vnp_OrderInfo"] = $"Thanh toan don hang {order.Id}",
                ["vnp_OrderType"] = "other",
                ["vnp_ReturnUrl"] = vnp_Returnurl,
                ["vnp_TxnRef"] = payment.Id.ToString()
            };

            string rawData = string.Join("&", query.Select(x => $"{x.Key}={x.Value}"));

            using var hmac = new System.Security.Cryptography.HMACSHA512(
                System.Text.Encoding.UTF8.GetBytes(vnp_HashSecret));

            string hash = BitConverter
                .ToString(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(rawData)))
                .Replace("-", "").ToLower();

            string queryString =
                string.Join("&", query.Select(x => $"{x.Key}={x.Value}")) +
                $"&vnp_SecureHash={hash}";

            return $"{vnp_Url}?{queryString}";
        }

        public async Task<IActionResult> VNPayReturn(string vnp_TxnRef, string vnp_ResponseCode)
        {
            var payment = _context.Payments.FirstOrDefault(p => p.Id == int.Parse(vnp_TxnRef));
            if (payment == null) return NotFound();

            payment.Status = vnp_ResponseCode == "00" ? "Success" : "Failed";
            await _context.SaveChangesAsync();

            if (payment.Status == "Success")
            {
                await UpdateStockAfterPaymentSuccess(payment.OrderId);
                await UpdatePromotionUsageAfterSuccess();
            }
            else
                await RestoreStockAfterPaymentFailed(payment.OrderId);

            HttpContext.Session.Remove("Cart");

            return RedirectToAction("OrderSuccess", new { orderId = payment.OrderId });
        }

        // ========== UTILS ==========
        public IActionResult OrderSuccess(int orderId)
        {
            ViewBag.Order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            ViewBag.Payment = _context.Payments.FirstOrDefault(p => p.OrderId == orderId);
            return View();
        }

        private async Task UpdatePromotionUsageAfterSuccess()
        {
            var code = HttpContext.Session.GetString("PromoCode");
            if (code == null) return;

            var promo = await _context.PromotionCodes.FirstOrDefaultAsync(p => p.Code == code);

            if (promo != null)
            {
                promo.UsageCount++;

                // ✅ MaxUsage = 0 hoặc null => không giới hạn
                if (promo.MaxUsage.HasValue && promo.MaxUsage.Value > 0 && promo.UsageCount >= promo.MaxUsage.Value)
                    promo.IsActive = false;

                await _context.SaveChangesAsync();
            }

            HttpContext.Session.Remove("PromoCode");
        }

        private async Task UpdateStockAfterPaymentSuccess(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            foreach (var item in order.OrderDetails)
            {
                item.Product.WebQuantity -= item.Quantity;
                item.Product.StockQuantity -= item.Quantity;
            }

            await _context.SaveChangesAsync();
        }

        private async Task RestoreStockAfterPaymentFailed(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            foreach (var item in order.OrderDetails)
            {
                item.Product.WebQuantity += item.Quantity;
                item.Product.StockQuantity += item.Quantity;
            }

            await _context.SaveChangesAsync();
        }

        // ==========================
        // ✅ GIỮ LẠI HÀM CŨ (KHÔNG XÓA)
        // (bạn đang dùng trước đó - mình để nguyên)
        // ==========================
        private double GetDiscountedTotal(double total)
        {
            var code = HttpContext.Session.GetString("PromoCode");
            if (code == null) return total;

            var promo = _context.PromotionCodes.FirstOrDefault(p => p.Code == code);
            if (promo == null) return total;

            if (promo.DiscountPercent.HasValue)
                total -= total * promo.DiscountPercent.Value / 100;

            if (promo.DiscountAmount.HasValue)
                total -= promo.DiscountAmount.Value;

            return total < 0 ? 0 : total;
        }

        // ========== ÁP DỤNG MÃ GIẢM GIÁ ==========
        [HttpPost]
        public IActionResult ApplyCoupon(string code, string? returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                TempData["Error"] = "Vui lòng nhập mã khuyến mãi!";
                return LocalRedirect(returnUrl ?? Url.Action("Index")!);
            }

            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart == null || !cart.Items.Any())
            {
                TempData["Error"] = "Giỏ hàng đang trống!";
                return LocalRedirect(returnUrl ?? Url.Action("Index")!);
            }

            var promo = _context.PromotionCodes
                .FirstOrDefault(p => p.Code.ToUpper() == code.ToUpper());

            if (promo == null)
            {
                TempData["Error"] = "Mã giảm giá không tồn tại!";
                return LocalRedirect(returnUrl ?? Url.Action("Index")!);
            }

            // Kiểm tra ngày hợp lệ
            var now = DateTime.Now;
            if (now < promo.StartDate || now > promo.EndDate)
            {
                TempData["Error"] = "Mã giảm giá đã hết hạn hoặc chưa được kích hoạt!";
                return LocalRedirect(returnUrl ?? Url.Action("Index")!);
            }

            // Check trạng thái
            if (!promo.IsActive)
            {
                TempData["Error"] = "Mã khuyến mãi đã bị vô hiệu hóa!";
                return LocalRedirect(returnUrl ?? Url.Action("Index")!);
            }

            // ✅ MaxUsage: 0 hoặc null => không giới hạn
            if (promo.MaxUsage.HasValue && promo.MaxUsage.Value > 0 && promo.UsageCount >= promo.MaxUsage.Value)
            {
                TempData["Error"] = "Mã giảm giá đã đạt giới hạn sử dụng!";
                return LocalRedirect(returnUrl ?? Url.Action("Index")!);
            }

            // Lưu mã giảm giá vào Session
            HttpContext.Session.SetString("PromoCode", promo.Code);

            TempData["Success"] = "🎉 Áp dụng mã giảm giá thành công!";
            return LocalRedirect(returnUrl ?? Url.Action("Index")!);
        }

        // ✅ THÊM MỚI: BỎ MÃ
        [HttpPost]
        public IActionResult RemoveCoupon(string? returnUrl = null)
        {
            HttpContext.Session.Remove("PromoCode");
            TempData["Success"] = "Đã bỏ mã khuyến mãi.";
            return LocalRedirect(returnUrl ?? Url.Action("Index")!);
        }
    }
}
