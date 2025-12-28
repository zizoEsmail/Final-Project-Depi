using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dblayer.Models;
using PizzaRestaurantDrink.ViewModels;
using PizzaRestaurantDrink.HelperClass;

namespace PizzaRestaurantDrink.Controllers
{
    public class UserRecoveryController : Controller
    {
        private readonly ProPizzResturentandDrinkDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailHelper _emailHelper;

        public UserRecoveryController(ProPizzResturentandDrinkDbContext context, IWebHostEnvironment webHostEnvironment, IEmailHelper emailHelper)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _emailHelper = emailHelper;
        }

        // GET: AccountRecovery
        public IActionResult AccountRecovery()
        {
            // We use the AccountRecoveryMV that already exists in your UserViewModels.cs
            return View(new AccountRecoveryMV());
        }

        [HttpPost]
        public IActionResult AccountRecovery(AccountRecoveryMV model)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var user = _context.UserTables
                            .FirstOrDefault(u => u.Username.Trim() == model.UserName || u.EmailAddress == model.UserName.Trim());

                        if (user != null)
                        {
                            // 1. Generate Code and Save to DB
                            string code = DateTime.Now.ToString("yyyyMMddHHmmssmm") + model.UserName;

                            var recovery = new UserPasswordRecoveryTable
                            {
                                UserId = user.UserId,
                                OldPassword = user.Password,
                                RecoveryCode = code,
                                RecoveryCodeExpiryDateTime = DateTime.Now.AddDays(1),
                                RecoveryStatus = true
                            };

                            _context.UserPasswordRecoveryTables.Add(recovery);
                            _context.SaveChanges();

                            // 2. Prepare Email
                            var callbackUrl = Url.Action("ResetPassword", "User", new { recoverycode = code }, protocol: Request.Scheme);

                            // Get path to template in wwwroot
                            string path = Path.Combine(_webHostEnvironment.WebRootPath, "MailTemplates", "ForgotPasswordConfirmation.html");
                            string body = string.Empty;

                            if (System.IO.File.Exists(path))
                            {
                                using (StreamReader reader = new StreamReader(path))
                                {
                                    body = reader.ReadToEnd();
                                }
                                body = body.Replace("{ConfirmationLink}", callbackUrl);
                                body = body.Replace("{UserName}", user.EmailAddress);

                                // 3. Send Email
                                bool isSent = _emailHelper.Send(user.EmailAddress, "Reset Password", body, true);

                                if (isSent)
                                {
                                    transaction.Commit();
                                    ModelState.AddModelError(string.Empty, "Recovery Link Sent on your email address (" + user.EmailAddress + ")");
                                }
                                else
                                {
                                    transaction.Rollback();
                                    ModelState.AddModelError(string.Empty, "Email configuration error. Please check appsettings.");
                                }
                            }
                            else
                            {
                                transaction.Rollback();
                                ModelState.AddModelError(string.Empty, "Email Template not found.");
                            }
                        }
                        else
                        {
                            transaction.Rollback();
                            ModelState.AddModelError("UserName", "User Not Found!");
                        }
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    ModelState.AddModelError(string.Empty, "An error occurred. Please try again later.");
                }
            }
            return View(model);
        }
    }
}