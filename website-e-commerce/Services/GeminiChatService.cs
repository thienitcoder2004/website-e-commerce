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
using System;

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

        // ==============================
        // 1. HÀM CHÍNH – TRẢ LỜI BOT
        // ==============================
        public async Task<string> GetReplyAsync(string message)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(message))
                    return "⚠️ Bạn vui lòng nhập nội dung tin nhắn nhé.";

                string lower = message.ToLower().Trim();

                // 🟩 1.1 Câu chào, cảm ơn, tạm biệt
                string[] greetings = { "hi", "hello", "xin chào", "chào", "hey", "alo", "chào shop", "chào bạn", "lô" };
                string[] thanks = { "cảm ơn", "cảm ơn shop", "thank", "tks", "thanks", "ok" };
                string[] goodbyes = { "tạm biệt", "bye", "hẹn gặp lại", "goodbye", "see you" };

                if (greetings.Any(g => lower == g || lower.StartsWith(g + " ")))
                    return "🤖 Xin chào! Mình là trợ lý HTM Shop 😊 Bạn muốn tìm sản phẩm nào hôm nay?";

                if (thanks.Any(t => lower.Contains(t)))
                    return "🤖 Không có gì đâu ạ 💖 Rất vui được giúp bạn! Bạn cần mình gợi ý thêm sản phẩm nào không?";

                if (goodbyes.Any(b => lower.Contains(b)))
                    return "👋 Hẹn gặp lại bạn nha! Chúc bạn một ngày tuyệt vời 🌸";

                // 🟦 1.2 Nếu có chữ “so sánh” → so sánh 2 sản phẩm
                if (lower.Contains("so sánh") || lower.Contains("so sanh"))
                    return await CompareProducts(message);   // dùng cả câu gốc để tách tên

                // 🟧 1.3 Tách giá “dưới xx triệu”
                decimal? maxPrice = ExtractPrice(message);

                // 🟨 1.4 Tìm sản phẩm theo từ khóa (name / category / brand)
                var products = await SearchProductsByKeywordsAsync(message, maxPrice);

                if (products.Any())
                {
                    return BuildProductListHtml(products);
                }

                // 🟥 1.5 Hoàn toàn không tìm thấy → gọi Gemini
                return await AskGeminiAsync(message);
            }
            catch
            {
                return "⚠️ Có lỗi khi xử lý dữ liệu hoặc kết nối AI.";
            }
        }

        // ===========================================
        // 2. TÌM SẢN PHẨM THEO TỪ KHÓA + GIÁ TỐI ĐA
        // ===========================================
        private async Task<List<Product>> SearchProductsByKeywordsAsync(string message, decimal? maxPrice)
        {
            // Chuẩn hóa: bỏ ký tự lạ, tách từ
            string clean = Regex.Replace(message.ToLower(), @"[^a-z0-9\s]", " ");
            var words = clean.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                             .Where(w => w.Length > 1)
                             .ToList();

            if (!words.Any())
                return new List<Product>();

            // một số stopword bỏ qua cho đỡ nhiễu
            string[] stopwords = { "so", "sanh", "voi", "với", "va", "và", "can", "cần", "mua",
                                   "xem", "san", "sản", "pham", "phẩm", "tat", "tất", "ca", "cả" };
            words = words.Where(w => !stopwords.Contains(w)).ToList();
            if (!words.Any())
                return new List<Product>();

            // Lấy toàn bộ sản phẩm lên để lọc (shop thường không quá nhiều sản phẩm)
            var allProducts = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand) // nếu không có Brand thì bỏ dòng này
                .ToListAsync();

            // Nếu có yêu cầu giá → lọc trước
            if (maxPrice.HasValue)
                allProducts = allProducts.Where(p => p.NewPrice <= maxPrice.Value).ToList();

            // Tính điểm match cho từng sản phẩm
            List<(Product Product, int Score)> scored = new();
            foreach (var p in allProducts)
            {
                string name = p.Name.ToLower();
                string cat = p.Category?.Name?.ToLower() ?? "";
                string brand = p.Brand?.Name?.ToLower() ?? "";

                int score = 0;
                foreach (var w in words)
                {
                    if (name.Contains(w) || cat.Contains(w) || brand.Contains(w))
                        score++;
                }

                if (score > 0)
                    scored.Add((p, score));
            }

            // Lấy nhiều nhất 6 sản phẩm, ưu tiên score cao, giá thấp
            var result = scored
                .OrderByDescending(s => s.Score)
                .ThenBy(s => s.Product.NewPrice)
                .Take(6)
                .Select(s => s.Product)
                .ToList();

            return result;
        }

        private string BuildProductListHtml(List<Product> products)
        {
            var result = new StringBuilder();
            result.Append("<b>🔍 Kết quả tìm kiếm sản phẩm:</b><br><br>");
            result.Append("<div style='display:grid;grid-template-columns:repeat(auto-fit,minmax(200px,1fr));gap:10px'>");

            foreach (var p in products)
            {
                string image = !string.IsNullOrEmpty(p.ImageUrl)
                    ? $"<img src='{p.ImageUrl}' width='160' style='border-radius:10px;margin-bottom:8px;box-shadow:0 2px 6px rgba(0,0,0,0.15)'><br>"
                    : "";

                result.Append($@"
<div style='border:1px solid #eee;border-radius:10px;padding:10px;text-align:center;background:#fff'>
    {image}
    <b>{p.Name}</b><br>
    <span style='color:green;font-weight:bold'>{p.NewPrice:N0}₫</span><br>
    <small>{p.Category?.Name}</small><br>
    <a href='/Product/Details/{p.Id}' target='_blank'
       style='display:inline-block;margin-top:6px;color:#007bff;text-decoration:none'>
       ➡️ Xem chi tiết
    </a>
</div>");
            }

            result.Append("</div>");
            return result.ToString();
        }

        // 💰 Hàm tách giá (VD: “dưới 20 triệu”)
        private decimal? ExtractPrice(string text)
        {
            var match = Regex.Match(text, @"(\d+)\s*(triệu|tr|trieu)");
            if (match.Success && decimal.TryParse(match.Groups[1].Value, out decimal val))
                return val * 1_000_000;
            return null;
        }

        // ===================================
        // 3. SO SÁNH 2 SẢN PHẨM TỪ CÂU GÕ
        // ===================================
        private async Task<string> CompareProducts(string message)
        {
            // Tách 2 cụm sản phẩm
            (string left, string right) = SplitTwoProductParts(message);

            if (string.IsNullOrWhiteSpace(left) || string.IsNullOrWhiteSpace(right))
            {
                return "⚠️ Bạn hãy nhập theo dạng: <b>\"so sánh iPhone 15 và Samsung S24\"</b> để mình biết 2 sản phẩm cần so sánh nhé.";
            }

            var allProducts = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();

            var first = FindBestProduct(allProducts, left);
            var second = FindBestProduct(allProducts, right);

            // Không tìm được gì cả
            if (first == null && second == null)
            {
                return "❌ Cả hai sản phẩm bạn yêu cầu hiện <b>không có trong cửa hàng HTM Shop</b>. Bạn thử tìm lại trong danh mục sản phẩm giúp mình nhé.";
            }

            // Chỉ tìm được 1 sản phẩm
            if (first == null || second == null)
            {
                if (first == null && second != null)
                {
                    return $"❌ Mình chỉ tìm được sản phẩm <b>{second.Name}</b>. " +
                           $"Sản phẩm còn lại hiện <b>không có trong cửa hàng HTM Shop</b> hoặc tên bạn nhập chưa đúng.";
                }
                if (first != null && second == null)
                {
                    return $"❌ Mình chỉ tìm được sản phẩm <b>{first.Name}</b>. " +
                           $"Sản phẩm còn lại hiện <b>không có trong cửa hàng HTM Shop</b> hoặc tên bạn nhập chưa đúng.";
                }
            }

            // Cả 2 cùng có → build bảng so sánh
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
🧩 <b>{second.Name}:</b> {second.Specifications}<br><br>";

            // Gọi AI để comment ngắn
            string aiOpinion = await AskGeminiAsync(
                $"Hãy so sánh {first.Name} và {second.Name} ngắn gọn, thân thiện, bằng tiếng Việt, có emoji và bullet dễ đọc."
            );

            html += $"<b>🤖 Đánh giá AI:</b><br>{SmartFormat(aiOpinion)}";
            return html;
        }

        // Tách câu thành 2 phần sản phẩm
        private (string left, string right) SplitTwoProductParts(string message)
        {
            string lower = message.ToLower();
            lower = lower.Replace("so sánh", "").Replace("so sanh", "").Trim();

            string[] seps = { " vs ", " với ", " voi ", " và ", " va ", "," };

            foreach (var sep in seps)
            {
                int idx = lower.IndexOf(sep, StringComparison.Ordinal);
                if (idx > 0)
                {
                    string left = lower.Substring(0, idx).Trim();
                    string right = lower.Substring(idx + sep.Length).Trim();
                    return (left, right);
                }
            }

            return (lower, "");
        }

        // Tạo danh sách từ khóa từ 1 cụm sản phẩm (hỗ trợ ip15 ⇒ iphone 15)
        private List<string> BuildKeywords(string phrase)
        {
            string clean = Regex.Replace(phrase.ToLower(), @"[^a-z0-9\s]", " ");
            var tokens = clean.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

            var extras = new List<string>();

            foreach (var t in tokens)
            {
                // ip15 ⇒ iphone + 15
                var m = Regex.Match(t, @"^ip(\d+)$");
                if (m.Success)
                {
                    extras.Add("iphone");
                    extras.Add(m.Groups[1].Value);
                }
            }

            tokens.AddRange(extras);
            return tokens.Distinct().Where(w => w.Length > 1).ToList();
        }

        // Chọn sản phẩm "phù hợp nhất" với cụm từ
        private Product? FindBestProduct(List<Product> allProducts, string phrase)
        {
            var keywords = BuildKeywords(phrase);
            if (!keywords.Any()) return null;

            Product? best = null;
            int bestScore = 0;

            foreach (var p in allProducts)
            {
                string name = p.Name.ToLower();
                int score = keywords.Count(k => name.Contains(k));

                if (score > bestScore)
                {
                    bestScore = score;
                    best = p;
                }
            }

            // yêu cầu khớp ít nhất 2 “keywords” để đỡ nhầm
            return bestScore >= 2 ? best : null;
        }

        // ===================================
        // 4. GỌI GEMINI API
        // ===================================
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
                            new
                            {
                                text = $"Bạn là trợ lý thân thiện của HTM Shop, trả lời ngắn gọn, tiếng Việt tự nhiên, có emoji nếu phù hợp: {message}"
                            }
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
            {
                var text = candidates[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString();
                return text ?? "⚠️ Không nhận được phản hồi từ Gemini.";
            }

            return "⚠️ Không nhận được phản hồi hợp lệ từ Gemini API.";
        }

        // 🎨 Format phản hồi AI cho dễ đọc
        private string SmartFormat(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            text = text.Replace("\n", "<br>")
                       .Replace("* ", "• ");

            return $"<div style='padding:6px 10px;line-height:1.6;background:#fafafa;border-radius:8px;margin-top:6px'>{text}</div>";
        }
    }
}
