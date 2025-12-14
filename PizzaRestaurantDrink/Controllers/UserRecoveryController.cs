using Microsoft.AspNetCore.Mvc;
using Dblayer.Models;
using PizzaRestaurantDrink.ViewModels;
using Microsoft.EntityFrameworkCore; // Required for transactions

namespace PizzaRestaurantDrink.Controllers
{
    public class UserRecoveryController : Controller
    {
        private readonly ProPizzResturentandDrinkDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment; // Required to find files in wwwroot

        public UserRecoveryController(ProPizzResturentandDrinkDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: AccountRecovery
        public IActionResult AccountRecovery()
        {
            return View(new AccountRecoveryMV());
        }

        [HttpPost]
        public IActionResult AccountRecovery(AccountRecoveryMV accountRecoveryMV)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        // 1. Find the user
                        var user = _context.UserTables.FirstOrDefault(u => u.Username.Trim() == accountRecoveryMV.UserName || u.EmailAddress == accountRecoveryMV.UserName.Trim());

                        if (user != null)
                        {
                            string code = DateTime.Now.ToString("yyyyMMddHHmmssmm") + accountRecoveryMV.UserName;

                            // ==============================================================================
                            // CRITICAL: This section is commented out because your Database 
                            // is missing the 'UserPasswordRecoveryTable'.
                            // You must create this table in SQL and re-scaffold to uncomment this.
                            // ==============================================================================
                            /*
                            var accountrecoverydetails = new UserPasswordRecoveryTable();
                            accountrecoverydetails.UserID = user.UserId;
                            accountrecoverydetails.OldPassword = user.Password;
                            accountrecoverydetails.RecoveryCode = code;
                            accountrecoverydetails.RecoveryCodeExpiryDateTime = DateTime.Now.AddDays(1);
                            accountrecoverydetails.RecoveryStatus = true;
                            _context.UserPasswordRecoveryTables.Add(accountrecoverydetails);
                            _context.SaveChanges();
                            */

                            // 2. Generate the Link
                            var callbackUrl = Url.Action("ResetPassword", "User", new { recoverycode = code }, protocol: Request.Scheme);

                            // 3. Read the Email Template
                            string body = string.Empty;
                            string webRootPath = _webHostEnvironment.WebRootPath; // This gets the wwwroot folder
                            string templatePath = Path.Combine(webRootPath, "MailTemplates", "ForgotPasswordConfirmation.html");

                            if (System.IO.File.Exists(templatePath))
                            {
                                using (StreamReader reader = new StreamReader(templatePath))
                                {
                                    body = reader.ReadToEnd();
                                }
                                body = body.Replace("{ConfirmationLink}", callbackUrl);
                                body = body.Replace("{UserName}", user.EmailAddress);
                            }
                            else
                            {
                                body = $"Please reset your password by clicking here: {callbackUrl}";
                            }

                            // 4. Send Email (Using local helper method)
                            bool IsSendEmail = SendEmail(user.EmailAddress, "Reset Password", body);

                            if (IsSendEmail)
                            {
                                transaction.Commit();
                                ModelState.AddModelError(string.Empty, "Recovery Link Sent on your email address(" + user.EmailAddress + ")");
                            }
                            else
                            {
                                transaction.Rollback();
                                ModelState.AddModelError(string.Empty, "Issue Sending Email. Please Try Again later.");
                            }
                        }
                        else
                        {
                            transaction.Rollback();
                            ModelState.AddModelError("UserName", "User Not Found!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ModelState.AddModelError(string.Empty, "An error occurred: " + ex.Message);
                }
            }
            return View(accountRecoveryMV);
        }

        // ======================= SIMPLE EMAIL HELPER =======================
        // Since you don't have the original HelperClass, this is a placeholder.
        // You need to install 'System.Net.Mail' or 'MailKit' to make this work for real.
        private bool SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                // TODO: CONFIGURE YOUR EMAIL SETTINGS HERE
                // var smtpClient = new SmtpClient("smtp.gmail.com")
                // {
                //     Port = 587,
                //     Credentials = new NetworkCredential("your-email@gmail.com", "your-password"),
                //     EnableSsl = true,
                // };
                // var mailMessage = new MailMessage
                // {
                //     From = new MailAddress("your-email@gmail.com"),
                //     Subject = subject,
                //     Body = body,
                //     IsBodyHtml = true,
                // };
                // mailMessage.To.Add(toEmail);
                // smtpClient.Send(mailMessage);

                // Return true for now to simulate success
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}