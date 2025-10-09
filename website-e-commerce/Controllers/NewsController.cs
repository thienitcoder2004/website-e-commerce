using Microsoft.AspNetCore.Mvc;
using website_e_commerce.Models;

namespace website_e_commerce.Controllers
{
    public class NewsController : Controller
    {
        // Dữ liệu mẫu (sau này có thể thay bằng database)
        private static readonly List<News> Articles = new()
        {
            new News
            {
                Id = 1,
                Title = "AMD có thể giới thiệu dòng GPU Radeon RX 8000-series thế hệ tiếp theo tại CES 2025",
                Author = "Trần Tấn Phát",
                PublishedDate = new DateTime(2025, 3, 13),
                Summary = "AMD được cho là sẽ giới thiệu dòng GPU Radeon RX 8000-series thế hệ tiếp theo tại CES 2025...",
                ImageUrl = "/images/news/25558-paqq3i3mrvft8p4zdr8h8m-1200-80-png-11zon.png",
                Content = @"
<p>Chúng ta đã biết rằng AMD đã bắt đầu kích hoạt (hoặc nói chính xác hơn là đặt vào vị trí) một số bộ xử lý đồ họa RDNA 4 của mình trong các driver Linux...</p>
<p>Hình như AMD sẽ ra mắt vi xử lý đồ họa Navi 48 tại triển lãm CES 2025...</p>
<p>Ra mắt một loạt bộ xử lý đồ họa mới dành cho máy tính để bàn gaming là một điều tuyệt vời...</p>
<p>Câu hỏi hiện nay là: Navi 44 và Navi 48 là gì?... </p>",
                IsPublished = true
            },

            new News
            {
                Id = 2,
                Title = "Vụ lừa đảo Nvidia RTX 4090 sử dụng GPU RTX 3080 Ti dán lại nhãn",
                Author = "Hải Đăng",
                PublishedDate = new DateTime(2025, 3, 13),
                Summary = "Vụ lừa đảo Nvidia RTX 4090 sử dụng GPU RTX 3080 Ti dán lại nhãn, chip nhớ giả mạo...",
                ImageUrl = "/images/news/25560-wtjsjem9ovszcdvbzutpms-1200-80-png-11zon.png",
                Content = @"
<p>Làm thế nào để làm giả một card đồ họa RTX 4090, một trong những card đồ họa tốt nhất hiện nay...</p>
<p>Các card đồ họa Frankenstein sử dụng bộ xử lý đồ họa di động và/hoặc bộ nhớ đã được sửa chữa không phải là điều mới...</p>
<p>Rất tiện, AD102 của Nvidia là tương thích pin-to-pin với GPU thế hệ trước GA102...</p>
<p>Sau đó, bạn cần tìm bộ nhớ, điều này không phải là vấn đề...</p>",
                IsPublished = true
            },

            new News
            {
                Id = 3,
                Title = "Intel chuẩn bị ra mắt dòng CPU Core Ultra 200 series với kiến trúc Arrow Lake",
                Author = "Phạm Huy Thanh Hải",
                PublishedDate = new DateTime(2025, 4, 20),
                Summary = "Intel được cho là sẽ giới thiệu dòng CPU Core Ultra 200 series dựa trên kiến trúc Arrow Lake với hiệu năng vượt trội hơn Raptor Lake...",
                ImageUrl = "/images/news/25412-untitled-design-4-dydf.png",
                Content = @"
<p>Intel đang chuẩn bị tung ra thế hệ vi xử lý Core Ultra 200 series, được xây dựng trên kiến trúc Arrow Lake hoàn toàn mới, hứa hẹn mang lại hiệu năng vượt trội hơn nhiều so với thế hệ Raptor Lake hiện tại.</p>
<p>Theo các thông tin rò rỉ, Arrow Lake sẽ sử dụng tiến trình Intel 20A (2nm class), tích hợp công nghệ RibbonFET và PowerVia giúp tăng hiệu quả năng lượng đáng kể.</p>
<p>Dòng CPU này dự kiến sẽ hỗ trợ socket LGA1851 và tương thích với bo mạch chủ chipset Z890, mang đến khả năng ép xung cao hơn, cũng như hỗ trợ RAM DDR5 với tốc độ lên đến 6400 MHz.</p>
<p>Bên cạnh đó, GPU tích hợp Xe-LPG Gen12.7 cũng được cải thiện mạnh, cho phép chơi game eSports nhẹ nhàng mà không cần card đồ họa rời. Dự kiến Intel sẽ chính thức công bố dòng sản phẩm này tại sự kiện Innovation 2025 vào quý IV năm nay.</p>",
                IsPublished = true
            },

            // ✅ Bài viết ID = 4 mới thêm
            new News
            {
                Id = 4,
                Title = "Các trận chiến vũ trụ đặc sắc của Star Wars sẽ đến với Halo MCC vào đầu năm 2024 như một phần của gói Battlefront",
                Author = "Trần Tấn Phát",
                PublishedDate = new DateTime(2025, 3, 13),
                Summary = "Các trận chiến vũ trụ đẹp mắt của Star Wars sẽ đến với Halo: The Master Chief Collection vào đầu năm 2024 như một phần của mod phổ biến Battlefront.",
                ImageUrl = "/images/news/25418-halo-mcc-star-wars-space-battles-mod-battlefront-pack-728x410.png",
                Content = @"
<p>Các trận chiến vũ trụ đẹp mắt của Star Wars sẽ đến với Halo: The Master Chief Collection vào đầu năm 2024 như một phần của mod phổ biến Battlefront.</p>
<p>Gói Battlefront là một mod phổ biến cho Halo MCC bao gồm các bản đồ đến từ Star Wars Battlefront 2 cùng với phương tiện và vũ khí khác nhau. 
Được xác nhận bởi đội ngũ phía sau dự án phổ biến này, các Trận chiến vũ trụ Star Wars sẽ được bao gồm trong gói này vào đầu năm 2024.</p>
<p>Theo đội ngũ phát triển, trận chiến vũ trụ là một trong những tính năng được đề xuất nhiều nhất cho gói Battlefront. 
Cùng với thông báo về việc ra mắt vào đầu năm tới, đội ngũ cũng đã phát hành một bản xem trước về Trận chiến vũ trụ Star Wars trong Halo: Reach như một phần của Master Chief Collection cùng với một số hình ảnh ấn tượng.</p>",
                IsPublished = true
            }
        };
            // ====================== HIỂN THỊ DANH SÁCH ======================
            public IActionResult Index()
            {
                var published = Articles
                    .Where(n => n.IsPublished)
                    .OrderByDescending(n => n.PublishedDate)
                    .ToList();

                return View(published);
            }

            // ====================== CHI TIẾT ======================
            public IActionResult Detail(int id)
            {
                var article = GetArticleById(id);
                if (article == null) return NotFound();

                ViewBag.Related = Articles
                    .Where(a => a.Id != id)
                    .Take(3)
                    .ToList();

                return View(article);
            }

            // ====================== THÊM ======================
            [HttpGet]
            public IActionResult Add()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Add(News model)
            {
                if (!ModelState.IsValid) return View(model);

                model.Id = Articles.Any() ? Articles.Max(n => n.Id) + 1 : 1;
                model.PublishedDate = DateTime.Now;
                Articles.Add(model);

                TempData["Message"] = "Thêm bài viết thành công!";
                return RedirectToAction(nameof(Index));
            }

            // ====================== SỬA ======================
            [HttpGet]
            public IActionResult Update(int id)
            {
                var news = GetArticleById(id);
                if (news == null) return NotFound();
                return View(news);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Update(News model)
            {
                if (!ModelState.IsValid) return View(model);

                var existing = GetArticleById(model.Id);
                if (existing != null)
                {
                    existing.Title = model.Title;
                    existing.Author = model.Author;
                    existing.Summary = model.Summary;
                    existing.Content = model.Content;
                    existing.ImageUrl = model.ImageUrl;
                    existing.IsPublished = model.IsPublished;

                    TempData["Message"] = "Cập nhật bài viết thành công!";
                }

                return RedirectToAction(nameof(Index));
            }

            // ====================== XÓA ======================
            [HttpGet]
            public IActionResult Delete(int id)
            {
                var news = GetArticleById(id);
                if (news == null) return NotFound();
                return View(news);
            }

            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public IActionResult DeleteConfirmed(int id)
            {
                var news = GetArticleById(id);
                if (news != null)
                {
                    Articles.Remove(news);
                    TempData["Message"] = "Xóa bài viết thành công!";
                }
                return RedirectToAction(nameof(Index));
            }

            // ====================== HÀM HỖ TRỢ ======================
            private News GetArticleById(int id)
            {
                return Articles.FirstOrDefault(n => n.Id == id);
            }
        }

    }
