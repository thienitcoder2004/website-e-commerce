using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using website_e_commerce.Models;
using website_e_commerce.Repositories;

namespace website_e_commerce.Controllers
{
    public class StockHistoryController : Controller
    {
        private readonly IStockHistoryRepository _repo;

        public StockHistoryController(IStockHistoryRepository repo)
        {
            _repo = repo;
        }

        // ================== INDEX + FILTER =====================
        public IActionResult Index(string filterType, DateTime? from, DateTime? to)
        {
            var data = _repo.GetAll().AsQueryable();

            if (filterType == "day" && from != null)
            {
                data = data.Where(h => h.CreatedAt.Date == from.Value.Date);
            }
            else if (filterType == "week" && from != null)
            {
                var weekStart = from.Value.Date.AddDays(-(int)from.Value.DayOfWeek + 1);
                var weekEnd = weekStart.AddDays(6);
                data = data.Where(h => h.CreatedAt >= weekStart && h.CreatedAt <= weekEnd);
            }
            else if (filterType == "month" && from != null)
            {
                data = data.Where(h => h.CreatedAt.Month == from.Value.Month &&
                                        h.CreatedAt.Year == from.Value.Year);
            }
            else if (filterType == "range" && from != null && to != null)
            {
                data = data.Where(h => h.CreatedAt >= from && h.CreatedAt <= to);
            }

            return View(data.OrderByDescending(h => h.CreatedAt).ToList());
        }

        // ================== EXPORT EXCEL =====================
        public IActionResult ExportExcel(string filterType, DateTime? from, DateTime? to)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var stream = new MemoryStream();

            var data = _repo.GetAll().AsQueryable();

            if (filterType == "day" && from != null)
                data = data.Where(h => h.CreatedAt.Date == from.Value.Date);
            else if (filterType == "week" && from != null)
            {
                var weekStart = from.Value.Date.AddDays(-(int)from.Value.DayOfWeek + 1);
                var weekEnd = weekStart.AddDays(6);
                data = data.Where(h => h.CreatedAt >= weekStart && h.CreatedAt <= weekEnd);
            }
            else if (filterType == "month" && from != null)
                data = data.Where(h => h.CreatedAt.Month == from.Value.Month &&
                                        h.CreatedAt.Year == from.Value.Year);
            else if (filterType == "range" && from != null && to != null)
                data = data.Where(h => h.CreatedAt >= from && h.CreatedAt <= to);

            var list = data.OrderByDescending(h => h.CreatedAt).ToList();

            using (var package = new ExcelPackage(stream))
            {
                var ws = package.Workbook.Worksheets.Add("History");

                // Header
                ws.Cells["A1"].Value = "STT";
                ws.Cells["B1"].Value = "Sản phẩm";
                ws.Cells["C1"].Value = "Thao tác";
                ws.Cells["D1"].Value = "Số lượng";
                ws.Cells["E1"].Value = "Ghi chú";
                ws.Cells["F1"].Value = "Thời gian";

                int row = 2;
                foreach (var h in list)
                {
                    ws.Cells[row, 1].Value = row - 1;
                    ws.Cells[row, 2].Value = h.Product?.Name;
                    ws.Cells[row, 3].Value = h.ActionType == "Import" ? "Nhập kho" : "Xuất kho";
                    ws.Cells[row, 4].Value = h.Quantity;
                    ws.Cells[row, 5].Value = h.Note;
                    ws.Cells[row, 6].Value = h.CreatedAt.ToString("dd/MM/yyyy HH:mm");
                    row++;
                }

                package.Save();
            }

            stream.Position = 0;
            string fileName = $"{DateTime.Now:yyyy-MM-dd}_Lichsukhohang.xlsx";

            return File(
                stream,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName
            );
        }

        // ================== DETAILS =====================
        public IActionResult Details(int id)
        {
            var item = _repo.GetById(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // ================== DELETE =====================
        public IActionResult Delete(int id)
        {
            var item = _repo.GetById(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            _repo.Save();
            return RedirectToAction("Index");
        }

        // ================== STATISTICS =====================
        // ================== STATISTICS =====================
        public IActionResult Statistics(DateTime? from, DateTime? to)
        {
            var data = _repo.GetAll().AsQueryable();

            if (from != null) data = data.Where(h => h.CreatedAt >= from.Value.Date);
            if (to != null) data = data.Where(h => h.CreatedAt <= to.Value.Date.AddDays(1).AddSeconds(-1));

            // EF không hỗ trợ ?. => dùng ?: hoặc ?? 
            var stats = data
                .GroupBy(h => h.Product != null ? h.Product.Name : "N/A")
                .Select(g => new StockStatistics
                {
                    ProductName = g.Key,
                    TotalImport = g.Where(x => x.ActionType == "Import").Sum(x => x.Quantity),
                    TotalExport = g.Where(x => x.ActionType == "Export").Sum(x => x.Quantity)
                })
                .OrderByDescending(s => s.CurrentStock)
                .ToList();

            ViewBag.From = from?.ToString("yyyy-MM-dd");
            ViewBag.To = to?.ToString("yyyy-MM-dd");

            return View(stats);
        }

        // ================== EXPORT STATISTICS EXCEL =====================
        public IActionResult ExportStatisticsExcel(DateTime? from, DateTime? to)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var stream = new MemoryStream();

            var data = _repo.GetAll().AsQueryable();
            if (from != null) data = data.Where(h => h.CreatedAt >= from.Value.Date);
            if (to != null) data = data.Where(h => h.CreatedAt <= to.Value.Date.AddDays(1).AddSeconds(-1));

            var stats = data
                .GroupBy(h => h.Product != null ? h.Product.Name : "N/A")
                .Select(g => new StockStatistics
                {
                    ProductName = g.Key,
                    TotalImport = g.Where(x => x.ActionType == "Import").Sum(x => x.Quantity),
                    TotalExport = g.Where(x => x.ActionType == "Export").Sum(x => x.Quantity)
                })
                .OrderByDescending(s => s.CurrentStock)
                .ToList();

            using (var package = new ExcelPackage(stream))
            {
                var ws = package.Workbook.Worksheets.Add("StockStatistics");

                // Header
                ws.Cells["A1"].Value = "STT";
                ws.Cells["B1"].Value = "Sản phẩm";
                ws.Cells["C1"].Value = "Tổng nhập";
                ws.Cells["D1"].Value = "Tổng xuất";
                ws.Cells["E1"].Value = "Tồn hiện tại";

                int row = 2;
                for (int i = 0; i < stats.Count; i++)
                {
                    ws.Cells[row, 1].Value = i + 1;
                    ws.Cells[row, 2].Value = stats[i].ProductName;
                    ws.Cells[row, 3].Value = stats[i].TotalImport;
                    ws.Cells[row, 4].Value = stats[i].TotalExport;
                    ws.Cells[row, 5].Value = stats[i].CurrentStock;
                    row++;
                }

                package.Save();
            }

            stream.Position = 0;

            string fileName;
            if (from != null && to != null)
                fileName = $"StockStatistics_{from:yyyy-MM-dd}_to_{to:yyyy-MM-dd}.xlsx";
            else
                fileName = $"StockStatistics_{DateTime.Now:yyyy-MM-dd}.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

    }

    public class StockStatistics
    {
        public string ProductName { get; set; } = "";
        public int TotalImport { get; set; }
        public int TotalExport { get; set; }
        public int CurrentStock => TotalImport - TotalExport;
    }
}
