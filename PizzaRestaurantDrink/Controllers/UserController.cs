using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dblayer.Models;
using PizzaRestaurantDrink.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace PizzaRestaurantDrink.Controllers
{
    public class UserController : Controller
    {
        private readonly ProPizzResturentandDrinkDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(ProPizzResturentandDrinkDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // ======================= LOGIN =======================
        public IActionResult Login()
        {
            return View(new LoginMV());
        }

        [HttpPost]
        public IActionResult Login(LoginMV loginMV)
        {
            if (ModelState.IsValid)
            {
                // Note: Changed 'UserName' to 'Username' (lowercase 'n') based on your Context
                var user = _context.UserTables.FirstOrDefault(u =>
                    (u.EmailAddress == loginMV.UserName.Trim() || u.Username.Trim() == loginMV.UserName.Trim()) &&
                    u.Password.Trim() == loginMV.Password.Trim());

                if (user != null)
                {
                    if (user.UserStatusId == 1) // Note: Changed UserStatusID to UserStatusId
                    {
                        HttpContext.Session.SetInt32("UserID", user.UserId);
                        HttpContext.Session.SetInt32("UserTypeID", user.UserTypeId);
                        return RedirectToAction("Dashboard", "User");
                    }
                    else
                    {
                        var accountstatus = _context.UserStatusTables.Find(user.UserStatusId);
                        ModelState.AddModelError(string.Empty, "Account is " + accountstatus?.UserStatus);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Please Enter Correct User Name and Password!");
                }
            }
            return View(loginMV);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        // ======================= REGISTER =======================
        public IActionResult Register()
        {
            // 1. AUTO-FIX: Check if Genders exist in the connected database
            if (!_context.GenderTables.Any())
            {
                // If the list is empty, add them automatically!
                _context.GenderTables.Add(new GenderTable { GenderTitle = "Male" });
                _context.GenderTables.Add(new GenderTable { GenderTitle = "Female" });
                _context.SaveChanges();
            }

            // 2. Now fetch the list (It is guaranteed to have data now)
            ViewBag.GenderID = new SelectList(_context.GenderTables.ToList(), "GenderId", "GenderTitle");

            return View(new Reg_UserMV());
        }

        [HttpPost]
        public IActionResult Register(Reg_UserMV reg_UserMV)
        {
            reg_UserMV.UserTypeID = 4;
            reg_UserMV.RegisterationDate = DateTime.Now;
            reg_UserMV.UserStatusID = 1;

            if (ModelState.IsValid)
            {
                bool isExist = _context.UserTables.Any(u => u.Username.ToUpper().Trim() == reg_UserMV.UserName.ToUpper().Trim() ||
                                                            u.EmailAddress.ToUpper().Trim() == reg_UserMV.EmailAddress.ToUpper().Trim());

                if (isExist)
                {
                    ModelState.AddModelError("", "Username or Email already exists!");
                }
                else
                {
                    var user = new UserTable
                    {
                        UserTypeId = reg_UserMV.UserTypeID,
                        Username = reg_UserMV.UserName,
                        Password = reg_UserMV.Password,
                        FirstName = reg_UserMV.FirstName,
                        LastName = reg_UserMV.LastName,
                        ContactNo = reg_UserMV.ContactNo,
                        GenderId = reg_UserMV.GenderID,
                        EmailAddress = reg_UserMV.EmailAddress,
                        RegistrationDate = reg_UserMV.RegisterationDate, // Note: Context uses RegistrationDate
                        UserStatusId = reg_UserMV.UserStatusID
                    };

                    _context.UserTables.Add(user);
                    _context.SaveChanges();
                    return RedirectToAction("Login", "User");
                }
            }
            ViewBag.GenderID = new SelectList(_context.GenderTables.ToList(), "GenderId", "GenderTitle", reg_UserMV.GenderID);
            return View(reg_UserMV);
        }

        // ======================= RESET PASSWORD =======================
        // DISABLED: The table 'UserPasswordRecoveryTable' does not exist in your Context.
        // You must create this table in SQL and re-scaffold if you want this feature.

        public IActionResult ResetPassword(string recoverycode)
        {
            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetPassword(ForgotPasswordMV forgotPasswordMV)
        {
            // Feature disabled until database table is created
            return RedirectToAction("Login");
        }


        // ======================= DASHBOARD (GET) =======================
        public IActionResult Dashboard()
        {
            // 1. Security Check
            if (HttpContext.Session.GetInt32("UserTypeID") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            int userid = HttpContext.Session.GetInt32("UserID") ?? 0;

            // 2. Initialize the ViewModel
            var model = new DashboardMV();
            model.ProfileMV = new ProfileMV();

            // 3. Fetch Basic User Data from DB
            var user = _context.UserTables.Find(userid);
            if (user != null)
            {
                model.ProfileMV.UserID = user.UserId;
                model.ProfileMV.FirstName = user.FirstName;
                model.ProfileMV.LastName = user.LastName;
                model.ProfileMV.EmailAddress = user.EmailAddress;
                model.ProfileMV.ContactNo = user.ContactNo;
            }

            // 4. Fetch Extra Details (Photos, Education)
            var userDetail = _context.UserDetailTables.FirstOrDefault(u => u.UserId == userid);
            if (userDetail != null)
            {
                model.ProfileMV.UserPhotoPath = userDetail.PhotoPath;

                model.ProfileMV.EducationLevel = userDetail.EducationLevel;
                // Note: Check your UserDetailTable.cs to ensure this property exists. 
                // If you deleted it earlier because of errors, comment this line out.
                // model.ProfileMV.ExperenceLevel = userDetail.ExperienceLevel; 

                model.ProfileMV.EducationLastDegreePhotoPath = userDetail.EducationLastDegreeScanPath;
                model.ProfileMV.ExperenceLastPhotoPath = userDetail.LastExperienceScanPhotoPath;
            }

            return View(model);
        }

        // ======================= DASHBOARD (POST) =======================
        [HttpPost]
        public IActionResult Dashboard(DashboardMV dashboardMV)
        {
            if (HttpContext.Session.GetInt32("UserTypeID") == null)
            {
                return RedirectToAction("Login", "User");
            }

            int userid = HttpContext.Session.GetInt32("UserID") ?? 0;

            var user = _context.UserTables.Find(userid);
            var userDetail = _context.UserDetailTables.FirstOrDefault(u => u.UserId == userid);

            // 1. Password Change Logic
            if (!string.IsNullOrEmpty(dashboardMV.OldPassword))
            {
                if (user.Password == dashboardMV.OldPassword)
                {
                    if (dashboardMV.NewPassword?.Trim() == dashboardMV.ConfirmPassword?.Trim())
                    {
                        user.Password = dashboardMV.NewPassword;
                        _context.Entry(user).State = EntityState.Modified;
                        _context.SaveChanges();
                    }
                    else
                    {
                        ModelState.AddModelError("ConfirmPassword", "Passwords do not match!");
                        return View(dashboardMV); // Return with error
                    }
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "Old Password is Incorrect!");
                    return View(dashboardMV); // Return with error
                }
            }

            // 2. Profile Info Update
            if (dashboardMV.ProfileMV != null)
            {
                user.FirstName = dashboardMV.ProfileMV.FirstName;
                user.LastName = dashboardMV.ProfileMV.LastName;
                user.EmailAddress = dashboardMV.ProfileMV.EmailAddress;
                user.ContactNo = dashboardMV.ProfileMV.ContactNo;
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();

                // 3. Photo Upload Logic
                if (dashboardMV.ProfileMV.UserPhoto != null)
                {
                    string folder = "Content/ProfilePhoto"; // Will save to wwwroot/Content/ProfilePhoto
                    string fileName = $"{user.UserId}.jpg";
                    bool uploaded = UploadFile(dashboardMV.ProfileMV.UserPhoto, folder, fileName);

                    if (uploaded)
                    {
                        string photoPath = $"~/{folder}/{fileName}";
                        if (userDetail == null)
                        {
                            userDetail = new UserDetailTable
                            {
                                UserId = userid,
                                CreatedByUserId = userid,
                                UserDataProvidedDate = DateTime.Now,
                                PhotoPath = photoPath
                            };
                            _context.UserDetailTables.Add(userDetail);
                        }
                        else
                        {
                            userDetail.PhotoPath = photoPath;
                            userDetail.UserDataProvidedDate = DateTime.Now;
                            _context.Entry(userDetail).State = EntityState.Modified;
                        }
                        _context.SaveChanges();
                    }
                }
            }

            // 4. Update Education/Experience
            if (dashboardMV.ProfileMV != null)
            {
                // Ensure userDetail exists before trying to update it
                if (userDetail == null)
                {
                    userDetail = new UserDetailTable
                    {
                        UserId = userid,
                        CreatedByUserId = userid,
                        UserDataProvidedDate = DateTime.Now
                    };
                    _context.UserDetailTables.Add(userDetail);
                    _context.SaveChanges(); // Save to generate ID
                }

                if (!string.IsNullOrEmpty(dashboardMV.ProfileMV.EducationLevel))
                {
                    userDetail.EducationLevel = dashboardMV.ProfileMV.EducationLevel;
                    _context.Entry(userDetail).State = EntityState.Modified;
                }

                // Upload Education Photo
                if (dashboardMV.ProfileMV.EducationLastDegreePhoto != null)
                {
                    string folder = "Content/OtherFiles";
                    string fileName = $"Education_{userid}.jpg";
                    if (UploadFile(dashboardMV.ProfileMV.EducationLastDegreePhoto, folder, fileName))
                    {
                        userDetail.EducationLastDegreeScanPath = $"~/{folder}/{fileName}";
                        _context.Entry(userDetail).State = EntityState.Modified;
                    }
                }

                // Upload Experience Photo
                if (dashboardMV.ProfileMV.ExperenceLastPhoto != null)
                {
                    string folder = "Content/OtherFiles";
                    string fileName = $"Experience_{userid}.jpg";
                    if (UploadFile(dashboardMV.ProfileMV.ExperenceLastPhoto, folder, fileName))
                    {
                        userDetail.LastExperienceScanPhotoPath = $"~/{folder}/{fileName}";
                        _context.Entry(userDetail).State = EntityState.Modified;
                    }
                }
                _context.SaveChanges();
            }

            // SUCCESS! Redirect back to the GET method to reload the fresh data
            return RedirectToAction("Dashboard");
        }

        // ======================= HELPER FOR UPLOADS =======================
        private bool UploadFile(IFormFile file, string folderName, string fileName)
        {
            try
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                string uploadPath = Path.Combine(webRootPath, folderName);

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                string filePath = Path.Combine(uploadPath, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}