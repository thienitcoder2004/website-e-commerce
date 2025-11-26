using System.Security.Cryptography;
using System.Text;

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
            { "vnp_IpAddr", context.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1" },
            { "vnp_Locale", "vn" },
            { "vnp_OrderInfo", $"Thanh toán đơn hàng {paymentId}" },
            { "vnp_OrderType", "other" },
            { "vnp_ReturnUrl", returnUrl },
            { "vnp_TxnRef", paymentId.ToString() }
        };

        string rawData = string.Join("&", vnpParams.Select(x => $"{x.Key}={x.Value}"));

        using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(hashSecret));
        string hash = BitConverter.ToString(
            hmac.ComputeHash(Encoding.UTF8.GetBytes(rawData))
        ).Replace("-", "").ToLower();

        return $"{baseUrl}?{rawData}&vnp_SecureHash={hash}";
    }
}
