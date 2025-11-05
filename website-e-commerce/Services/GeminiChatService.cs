using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using website_e_commerce.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace website_e_commerce.Services
{
    public class GeminiChatService
    {
        private readonly string _apiKey;
        private readonly ApplicationDbContext _context;

        public GeminiChatService(IConfiguration config, ApplicationDbContext context)
        {
            _apiKey = config["Gemini:ApiKey"];
            _context = context;
        }

        public async Task<string> GetReplyAsync(string message)
        {
            try
            {
                string lower = message.ToLower().Trim();

                // 🟩 1️⃣ Phản hồi tự nhiên
                string[] greetings = { "hi", "hello", "xin chào", "chào", "hey", "alo", "chào shop", "chào bạn" };
                string[] thanks = { "cảm ơn", "thank", "tks", "thanks" };
                string[] goodbyes = { "tạm biệt", "bye", "hẹn gặp lại", "goodbye", "see you" };

                if (greetings.Any(g => lower == g || lower.StartsWith(g + " ")))
                    return "🤖 Xin chào! Mình là trợ lý HTM Shop 😊 Bạn muốn tìm sản phẩm nào hôm nay?";

                if (thanks.Any(t => lower.Contains(t)))
                    return "🤖 Không có gì đâu ạ 💖 Rất vui được giúp bạn! Bạn cần mình gợi ý thêm sản phẩm nào không?";

                if (goodbyes.Any(b => lower.Contains(b)))
                    return "👋 Hẹn gặp lại bạn nha! Chúc bạn một ngày tuyệt vời 🌸";

                // ⚖️ 2️⃣ Nếu có yêu cầu so sánh
                if (lower.Contains("so sánh"))
                    return await CompareProducts(lower);

                // 💰 3️⃣ Tách giá nếu người dùng có nói “dưới xx triệu”
                decimal? maxPrice = ExtractPrice(message);

                // 🛒 4️⃣ Lọc sản phẩm theo TÊN (bỏ category)
                var query = _context.Products
                    .Include(p => p.Category)
                    .Where(p => p.Name.ToLower().Contains(lower))
                    .AsQueryable();

                // Nếu người dùng có nói "dưới xx triệu" → lọc thêm theo giá
                if (maxPrice.HasValue)
                    query = query.Where(p => p.NewPrice <= maxPrice.Value);

                // Nếu có “rẻ”, “phù hợp”, “tốt nhất” → sắp xếp theo giá tăng
                if (lower.Contains("rẻ") || lower.Contains("phù hợp") || lower.Contains("tốt nhất") || lower.Contains("gợi ý"))
                    query = query.OrderBy(p => p.NewPrice);

                var products = await query.Take(5).ToListAsync();

                if (products.Any())
                {
                    // Hiển thị danh sách sản phẩm dạng HTML đẹp
                    var result = "<b>🔍 Kết quả tìm kiếm sản phẩm:</b><br><br><div style='display:grid;grid-template-columns:repeat(auto-fit,minmax(200px,1fr));gap:10px'>";
                    foreach (var p in products)
                    {
                        string image = !string.IsNullOrEmpty(p.ImageUrl)
                            ? $"<img src='{p.ImageUrl}' width='160' style='border-radius:10px;margin-bottom:8px;box-shadow:0 2px 6px rgba(0,0,0,0.15)'><br>"
                            : "";

                        result += $@"
                        <div style='border:1px solid #eee;border-radius:10px;padding:10px;text-align:center;background:#fff'>
                            {image}
                            <b>{p.Name}</b><br>
                            <span style='color:green;font-weight:bold'>{p.NewPrice:N0}₫</span><br>
                            <small>{p.Category?.Name}</small><br>
                            <a href='/Product/Details/{p.Id}' target='_blank' style='display:inline-block;margin-top:6px;color:#007bff;text-decoration:none'>➡️ Xem chi tiết</a>
                        </div>";
                    }
                    result += "</div>";
                    return result;
                }

                // Nếu không có kết quả → nhờ Gemini trả lời
                return await AskGeminiAsync(message);
            }
            catch
            {
                return "⚠️ Có lỗi khi xử lý dữ liệu hoặc kết nối AI.";
            }
        }

        // 💰 Hàm tách giá (VD: “dưới 20 triệu”)
        private decimal? ExtractPrice(string text)
        {
            var match = Regex.Match(text, @"(\d+)\s*(triệu|tr|trieu)");
            if (match.Success)
                return decimal.Parse(match.Groups[1].Value) * 1_000_000;
            return null;
        }

        // ⚖️ So sánh 2 sản phẩm
        private async Task<string> CompareProducts(string message)
        {
            string lower = Regex.Replace(message.ToLower(), @"[^a-z0-9\s]", "");
            var words = lower.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var allProducts = await _context.Products.Include(p => p.Category).ToListAsync();

            Product? first = null, second = null;
            foreach (var product in allProducts)
            {
                string pName = product.Name.ToLower();
                int matchCount = words.Count(w => pName.Contains(w));
                if (matchCount >= 2 && first == null)
                    first = product;
                else if (matchCount >= 2 && second == null && product.Id != first?.Id)
                    second = product;
                if (first != null && second != null)
                    break;
            }

            if (first == null || second == null)
                return "❌ Không tìm thấy đủ sản phẩm để so sánh. Hãy thử gõ chính xác hơn (VD: iPhone 15 Pro Max và Samsung Galaxy S24 Ultra).";

            var html = $@"
<b>⚖️ So sánh giữa {first.Name} và {second.Name}</b><br><br>
<table style='border-collapse:collapse;width:100%;font-size:14px'>
<tr>
<td style='width:50%;vertical-align:top;padding:5px;border-right:1px solid #ccc;text-align:center'>
<img src='{first.ImageUrl}' width='120' style='border-radius:8px'><br>
<b>{first.Name}</b><br>
💰 {first.NewPrice:N0}₫<br>
🏷️ {first.Category?.Name}<br><br>
<a href='/Product/Details/{first.Id}' target='_blank'>➡️ Xem chi tiết</a>
</td>

<td style='width:50%;vertical-align:top;padding:5px;text-align:center'>
<img src='{second.ImageUrl}' width='120' style='border-radius:8px'><br>
<b>{second.Name}</b><br>
💰 {second.NewPrice:N0}₫<br>
🏷️ {second.Category?.Name}<br><br>
<a href='/Product/Details/{second.Id}' target='_blank'>➡️ Xem chi tiết</a>
</td>
</tr>
</table><br>
<b>📊 Thông số kỹ thuật nổi bật</b><br>
🧩 <b>{first.Name}:</b> {first.Specifications}<br><br>
🧩 <b>{second.Name}:</b> {second.Specifications}<br><br>
";

            // 🧠 Đánh giá AI
            string aiOpinion = await AskGeminiAsync(
                $"Hãy so sánh {first.Name} và {second.Name} ngắn gọn, thân thiện, bằng tiếng Việt, có emoji và bullet dễ đọc."
            );

            html += $"<b>🤖 Đánh giá AI:</b><br>{SmartFormat(aiOpinion)}";
            return html;
        }

        // 🤖 Gọi Gemini
        private async Task<string> AskGeminiAsync(string message)
        {
            using var client = new HttpClient();

            var body = new
            {
                contents = new[]
                {
                    new
                    {
                        role = "user",
                        parts = new[]
                        {
                            new { text = $"Bạn là trợ lý thân thiện của HTM Shop, trả lời ngắn gọn, tiếng Việt tự nhiên, có emoji nếu phù hợp: {message}" }
                        }
                    }
                }
            };

            var jsonBody = JsonSerializer.Serialize(body);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var endpoint = $"https://generativelanguage.googleapis.com/v1/models/gemini-2.5-flash:generateContent?key={_apiKey}";

            var response = await client.PostAsync(endpoint, content);
            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);

            if (doc.RootElement.TryGetProperty("error", out var error))
                return $"⚠️ Lỗi từ Gemini API: {error.GetProperty("message").GetString()}";

            if (doc.RootElement.TryGetProperty("candidates", out var candidates))
                return candidates[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString()
                       ?? "⚠️ Không nhận được phản hồi từ Gemini.";

            return "⚠️ Không nhận được phản hồi hợp lệ từ Gemini API.";
        }

        // 🎨 Format phản hồi AI cho dễ đọc
        private string SmartFormat(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            text = text.Replace("\n", "<br>")
                       .Replace("* ", "• ")
                       .Replace("**", "<b>")
                       .Replace("**", "</b>");

            return $"<div style='padding:6px 10px;line-height:1.6;background:#fafafa;border-radius:8px;margin-top:6px'>{text}</div>";
        }
    }
}
