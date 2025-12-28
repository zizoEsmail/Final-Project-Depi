using System.Net;
using System.Net.Mail;

namespace PizzaRestaurantDrink.HelperClass
{
    // Interface to allow Injection
    public interface IEmailHelper
    {
        bool Send(string SenderEmail, string Subject, string Message, bool IsBodyHtml = false);
    }

    public class EmailHelper : IEmailHelper
    {
        private readonly IConfiguration _config;

        public EmailHelper(IConfiguration config)
        {
            _config = config;
        }

        public bool Send(string SenderEmail, string Subject, string Message, bool IsBodyHtml = false)
        {
            bool status = false;
            try
            {
                // Reading from appsettings.json
                string HostAddress = _config["EmailSettings:Host"];
                string FormEmailId = _config["EmailSettings:MailFrom"];
                string Password = _config["EmailSettings:Password"];
                string Port = _config["EmailSettings:Port"];

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(FormEmailId);
                mailMessage.Subject = Subject;
                mailMessage.Body = Message;
                mailMessage.IsBodyHtml = IsBodyHtml;
                mailMessage.To.Add(new MailAddress(SenderEmail));

                SmtpClient smtp = new SmtpClient();
                smtp.Host = HostAddress;
                smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                NetworkCredential networkCredential = new NetworkCredential();
                networkCredential.UserName = FormEmailId;
                networkCredential.Password = Password;

                smtp.Credentials = networkCredential;
                smtp.Port = Convert.ToInt32(Port);
                smtp.EnableSsl = true; // Gmail usually requires SSL

                smtp.Send(mailMessage);
                status = true;
                return status;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}