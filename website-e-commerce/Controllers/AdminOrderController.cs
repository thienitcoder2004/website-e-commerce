using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using website_e_commerce.Models;

namespace website_e_commerce.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminOrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminOrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Danh sách đơn hàng + Phân trang
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10; 

            var ordersQuery = _context.Orders
                .Include(o => o.ApplicationUser)
                .Include(o => o.Payments)
                .OrderByDescending(o => o.OrderDate);

            int totalItems = await ordersQuery.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var orders = await ordersQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

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

        // ✅ Cập nhật trạng thái thanh toán
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int orderId, string status)
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.OrderId == orderId);
            if (payment == null) return NotFound();

            payment.Status = status;
            await _context.SaveChangesAsync();

            if (status == "Success")
            {
                var order = await _context.Orders
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                    .FirstOrDefaultAsync(o => o.Id == orderId);

                if (order != null)
                {
                    foreach (var item in order.OrderDetails)
                    {
                        var product = item.Product;
                        if (product != null)
                        {
                            product.WebQuantity -= item.Quantity;
                            product.StockQuantity -= item.Quantity;

                            if (product.WebQuantity < 0) product.WebQuantity = 0;
                            if (product.StockQuantity < 0) product.StockQuantity = 0;
                        }
                    }

                    await _context.SaveChangesAsync();
                }
            }

            TempData["SuccessMessage"] = "Đã cập nhật trạng thái thanh toán.";
            return RedirectToAction(nameof(Details), new { id = orderId });
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
            var totalOrders = await _context.Orders.CountAsync();
            var totalRevenue = await _context.Payments
                .Where(p => p.Status == "Success")
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

            var payments = await _context.Payments
                .Where(p => p.Status == "Success" && p.PaymentDate >= startDate)
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
