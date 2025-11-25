using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

public class MomoService
{
    private readonly IConfiguration _config;

    public MomoService(IConfiguration config)
    {
        _config = config;
    }

    public async Task<string> CreatePaymentAsync(long orderId, long amount, string orderInfo)
    {
        string endpoint = _config["Momo:Endpoint"];
        string partnerCode = _config["Momo:PartnerCode"];
        string accessKey = _config["Momo:AccessKey"];
        string secretKey = _config["Momo:SecretKey"];
        string redirectUrl = _config["Momo:RedirectUrl"];
        string ipnUrl = _config["Momo:IpnUrl"];

        string requestId = Guid.NewGuid().ToString();

        // ⭐ FIX: ORDERID PHẢI UNIQUE → DÙNG GUID
        string orderIdStr = $"{orderId}_{Guid.NewGuid().ToString().Substring(0, 8)}";

        string rawHash =
            $"accessKey={accessKey}&amount={amount}&extraData=&ipnUrl={ipnUrl}&orderId={orderIdStr}" +
            $"&orderInfo={orderInfo}&partnerCode={partnerCode}&redirectUrl={redirectUrl}" +
            $"&requestId={requestId}&requestType=captureWallet";

        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
        string signature = BitConverter.ToString(
            hmac.ComputeHash(Encoding.UTF8.GetBytes(rawHash))
        ).Replace("-", "").ToLower();

        var payload = new
        {
            partnerCode,
            accessKey,
            requestId,
            orderId = orderIdStr,
            amount = amount.ToString(),
            orderInfo,
            redirectUrl,
            ipnUrl,
            extraData = "",
            requestType = "captureWallet",
            signature
        };

        using var client = new HttpClient();
        var response = await client.PostAsJsonAsync(endpoint, payload);

        string content = await response.Content.ReadAsStringAsync();
        Console.WriteLine("🔎 MOMO RESPONSE RAW: " + content);

        var json = JsonDocument.Parse(content);

        if (!json.RootElement.TryGetProperty("payUrl", out var payUrl))
            throw new Exception("MoMo không trả về payUrl. Response: " + content);

        return payUrl.GetString();
    }
}
