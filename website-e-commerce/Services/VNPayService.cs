using System.Security.Cryptography;
using System.Text;
using System.Web;

public class VnPayService
{
    private readonly IConfiguration _config;

    public VnPayService(IConfiguration config)
    {
        _config = config;
    }

    public string CreatePaymentUrl(HttpContext context, decimal amount, int paymentId)
    {
        string baseUrl = _config["VNPay:BaseUrl"];
        string tmnCode = _config["VNPay:TmnCode"];
        string hashSecret = _config["VNPay:HashSecret"];
        string returnUrl = _config["VNPay:ReturnUrl"];

        var vnpParams = new SortedDictionary<string, string>
        {
            { "vnp_Version", "2.1.0" },
            { "vnp_Command", "pay" },
            { "vnp_TmnCode", tmnCode },
            { "vnp_Amount", ((long)amount * 100).ToString() },
            { "vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss") },
            { "vnp_CurrCode", "VND" },
            { "vnp_IpAddr", GetIpAddress(context) },
            { "vnp_Locale", "vn" },
            { "vnp_OrderInfo", $"Thanh toán đơn hàng {paymentId}" },
            { "vnp_OrderType", "other" },
            { "vnp_ReturnUrl", returnUrl },
            { "vnp_TxnRef", paymentId.ToString() }
        };

        // 🔥 BẮT BUỘC phải encode trước khi ký
        string rawData = string.Join("&", vnpParams.Select(k =>
            $"{k.Key}={HttpUtility.UrlEncode(k.Value)}"));

        string secureHash = HmacSHA512(hashSecret, rawData);

        // Build final payment URL
        string queryString = string.Join("&", vnpParams.Select(k =>
            $"{k.Key}={HttpUtility.UrlEncode(k.Value)}"));

        return $"{baseUrl}?{queryString}&vnp_SecureHash={secureHash}";
    }

    private static string HmacSHA512(string key, string data)
    {
        using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key));
        byte[] hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
        return BitConverter.ToString(hashValue).Replace("-", "").ToLower();
    }

    private static string GetIpAddress(HttpContext context)
    {
        return context.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";
    }
}
