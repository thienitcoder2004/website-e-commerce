using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace website_e_commerce.Services
{
    public class EmailService
    {
        private readonly string _fromEmail = "thiendznro2010@gmail.com"; // đổi thành email của bạn
        private readonly string _appPassword = "teabywayvkjozuxn";   // dán mã ứng dụng ở đây

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var message = new MailMessage();
            message.From = new MailAddress(_fromEmail, "Website E-Commerce");
            message.To.Add(new MailAddress(toEmail));
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.Credentials = new NetworkCredential(_fromEmail, _appPassword);
                client.EnableSsl = true;
                await client.SendMailAsync(message);
            }
        }
    }
}
