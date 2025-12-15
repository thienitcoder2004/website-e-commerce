using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using website_e_commerce.Models;

namespace website_e_commerce.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class AdminOrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminOrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Danh sách đơn hàng + Phân trang
        public async Task<IActionResult> Index(int page = 1, string statusFilter = "All")
        {
            int pageSize = 10;

            // Start with the base query
            IQueryable<Order> ordersQuery = _context.Orders
                .Include(o => o.ApplicationUser)
                .Include(o => o.Payments)
                .OrderByDescending(o => o.OrderDate);

            // Apply status filter if needed
            if (statusFilter != "All")
            {
                ordersQuery = ordersQuery.Where(o => o.Payments.Any(p => p.Status == statusFilter));
            }

            int totalItems = await ordersQuery.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var orders = await ordersQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.StatusFilter = statusFilter; // Pass filter to view

            return View(orders);
        }

        // ✅ Chi tiết đơn hàng
        public async Task<IActionResult> Details(int id)
        {
            var order = await _context.Orders
                .Include(o => o.ApplicationUser)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .Include(o => o.Payments)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound();

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int orderId, string status)
        {
            // Kiểm tra và cập nhật trạng thái
            var order = await _context.Orders
                .Include(o => o.Payments)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                return NotFound();

            // Cập nhật trạng thái đơn hàng
            var payment = order.Payments?.FirstOrDefault();
            if (payment != null)
            {
                payment.Status = status;
                await _context.SaveChangesAsync();
            }

            // Trả về kết quả
            return Json(new { success = true, status = status });
        }


        // Lịch sử đơn hàng của người dùng
        public async Task<IActionResult> OrderHistory()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy userId từ claims (Identity)
            if (userId == null)
            {
                // Trả về lỗi hoặc chuyển hướng người dùng đến trang đăng nhập
                return RedirectToAction("Login", "Account");
            }

            // Lấy danh sách đơn hàng của người dùng
            var orders = await _context.Orders
                .Where(o => o.ApplicationUser.Id == userId) // Chỉ lấy đơn hàng của người dùng này
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product) // Đảm bảo sản phẩm được bao gồm trong đơn hàng
                .Include(o => o.Payments)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders); // Trả về View với danh sách đơn hàng
        }

        // ✅ Xoá đơn hàng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .Include(o => o.Payments)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound();

            // Xoá chi tiết đơn hàng
            if (order.OrderDetails != null && order.OrderDetails.Any())
                _context.OrderDetails.RemoveRange(order.OrderDetails);

            // Xoá thanh toán liên quan
            if (order.Payments != null && order.Payments.Any())
                _context.Payments.RemoveRange(order.Payments);

            // Xoá đơn hàng
            _context.Orders.Remove(order);

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Đã xoá đơn hàng #{id} thành công.";
            return RedirectToAction(nameof(Index));
        }

        // ✅ Trang thống kê doanh thu
        public async Task<IActionResult> Dashboard(string filter = "month")
        {
            // Nếu muốn: tổng đơn tính tất cả order
            var totalOrders = await _context.Orders.CountAsync();

            // Hoặc chỉ đếm đơn đã hoàn thành:
            // var totalOrders = await _context.Orders
            //     .Where(o => o.Payments.Any(p => p.Status == "Completed" || p.Status == "Success"))
            //     .CountAsync();

            // Lọc các payment được xem là đã thành công / hoàn thành
            var successfulPaymentsQuery = _context.Payments
                .Where(p => p.Status == "Completed" || p.Status == "Success");

            // Tổng doanh thu = sum các payment đã thành công / hoàn thành
            var totalRevenue = await successfulPaymentsQuery
                .SumAsync(p => (decimal?)p.Amount) ?? 0;

            var totalUsers = await _context.Users.CountAsync();

            ViewBag.TotalOrders = totalOrders;
            ViewBag.TotalRevenue = totalRevenue;
            ViewBag.TotalUsers = totalUsers;
            ViewBag.Filter = filter;

            // --- Tính thời gian lọc ---
            DateTime startDate = DateTime.Now;
            switch (filter.ToLower())
            {
                case "day":
                    startDate = DateTime.Now.AddDays(-7);
                    break;
                case "week":
                    startDate = DateTime.Now.AddDays(-30);
                    break;
                case "month":
                    startDate = DateTime.Now.AddMonths(-6);
                    break;
                case "year":
                    startDate = DateTime.Now.AddYears(-3);
                    break;
            }

            // Chỉ lấy các payment thành công/hoàn thành trong khoảng thời gian cần thống kê
            var payments = await successfulPaymentsQuery
                .Where(p => p.PaymentDate >= startDate)
                .ToListAsync();

            // --- Gom nhóm dữ liệu theo thời gian ---
            var chartData = filter.ToLower() switch
            {
                "day" => payments
                    .GroupBy(p => p.PaymentDate.Date)
                    .OrderBy(g => g.Key)
                    .Select(g => (Label: g.Key.ToString("dd/MM"), Value: g.Sum(x => x.Amount)))
                    .ToList(),

                "week" => payments
                    .GroupBy(p => System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                        p.PaymentDate, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday))
                    .OrderBy(g => g.Key)
                    .Select(g => (Label: $"Tuần {g.Key}", Value: g.Sum(x => x.Amount)))
                    .ToList(),

                "month" => payments
                    .GroupBy(p => new { p.PaymentDate.Year, p.PaymentDate.Month })
                    .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                    .Select(g => (Label: $"{g.Key.Month}/{g.Key.Year}", Value: g.Sum(x => x.Amount)))
                    .ToList(),

                "year" => payments
                    .GroupBy(p => p.PaymentDate.Year)
                    .OrderBy(g => g.Key)
                    .Select(g => (Label: g.Key.ToString(), Value: g.Sum(x => x.Amount)))
                    .ToList(),

                _ => new List<(string Label, decimal Value)>()
            };

            ViewBag.ChartLabels = chartData.Select(x => x.Label).ToList();
            ViewBag.ChartValues = chartData.Select(x => x.Value).ToList();

            return View();
        }

    }
}
