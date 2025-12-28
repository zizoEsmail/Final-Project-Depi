using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dblayer.Models;
using PizzaRestaurantDrink.ViewModels;
using PizzaRestaurantDrink.HelperClass;

namespace PizzaRestaurantDrink.Controllers
{
    public class UserController : Controller
    {
        private readonly ProPizzResturentandDrinkDbContext _context;
        private readonly IFileHelper _fileHelper;

        public UserController(ProPizzResturentandDrinkDbContext context, IFileHelper fileHelper)
        {
            _context = context;
            _fileHelper = fileHelper;
        }

        // ======================= LOGIN =======================
        public IActionResult Login()
        {
            // NEW CHECK: If already logged in, redirect to Dashboard
            if (HttpContext.Session.GetInt32("UserID") != null)
            {
                return RedirectToAction("Dashboard");
            }

            return View(new LoginMV());
        }

        [HttpPost]
        public IActionResult Login(LoginMV loginMV)
        {
            if (ModelState.IsValid)
            {
                var user = _context.UserTables.FirstOrDefault(u =>
                    (u.EmailAddress == loginMV.UserName.Trim() || u.Username.Trim() == loginMV.UserName.Trim()) &&
                    u.Password.Trim() == loginMV.Password.Trim());

                if (user != null)
                {
                    if (user.UserStatusId == 1) // Active
                    {
                        HttpContext.Session.SetInt32("UserID", user.UserId);
                        HttpContext.Session.SetInt32("UserTypeID", user.UserTypeId);
                        return RedirectToAction("Dashboard", "User");
                    }
                    else
                    {
                        var accountstatus = _context.UserStatusTables.Find(user.UserStatusId);
                        ModelState.AddModelError(string.Empty, "Account is " + (accountstatus?.UserStatus ?? "Inactive"));
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Username or Password");
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
            // NEW CHECK: If already logged in, redirect to Dashboard
            if (HttpContext.Session.GetInt32("UserID") != null)
            {
                return RedirectToAction("Dashboard");
            }

            if (!_context.GenderTables.Any())
            {
                _context.GenderTables.Add(new GenderTable { GenderTitle = "Male" });
                _context.GenderTables.Add(new GenderTable { GenderTitle = "Female" });
                _context.SaveChanges();
            }
            ViewBag.GenderID = new SelectList(_context.GenderTables.ToList(), "GenderId", "GenderTitle");
            return View(new Reg_UserMV());
        }

        [HttpPost]
        public IActionResult Register(Reg_UserMV reg_UserMV)
        {
            reg_UserMV.UserTypeID = 4; // Customer
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
                        RegistrationDate = reg_UserMV.RegisterationDate,
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

        // ======================= DASHBOARD (GET) =======================
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetInt32("UserTypeID") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            int userid = HttpContext.Session.GetInt32("UserID") ?? 0;
            var dashboard = new DashboardMV();

            // 1. Fetch User with all relations
            var user = _context.UserTables
                .Include(u => u.UserType)
                .Include(u => u.Gender)
                .Include(u => u.UserStatus)
                .Include(u => u.UserDetailTables)
                .FirstOrDefault(u => u.UserId == userid);

            if (user != null)
            {
                // 2. Map Entity to ViewModel
                dashboard.ProfileMV.UserID = user.UserId;
                dashboard.ProfileMV.UserTypeID = user.UserTypeId;
                dashboard.ProfileMV.UserType = user.UserType?.UserType;
                dashboard.ProfileMV.UserName = user.Username;
                dashboard.ProfileMV.FirstName = user.FirstName;
                dashboard.ProfileMV.LastName = user.LastName;
                dashboard.ProfileMV.FullName = $"{user.FirstName} {user.LastName}";
                dashboard.ProfileMV.ContactNo = user.ContactNo;
                dashboard.ProfileMV.EmailAddress = user.EmailAddress;
                dashboard.ProfileMV.GenderTitle = user.Gender?.GenderTitle;

                dashboard.ProfileMV.RegisterationDate = user.RegistrationDate ?? DateTime.Now;
                dashboard.ProfileMV.UserStatus = user.UserStatus?.UserStatus;
                dashboard.ProfileMV.UserStatusID = user.UserStatusId ?? 0;

                if (user.UserStatusChangeData != null)
                {
                    dashboard.ProfileMV.UserStatusChangeDate = user.UserStatusChangeData.Value.ToDateTime(TimeOnly.MinValue);
                }

                // 3. User Details
                var userDetail = user.UserDetailTables.FirstOrDefault();

                if (userDetail != null)
                {
                    dashboard.ProfileMV.UserDetailProvideDate = userDetail.UserDataProvidedDate ?? DateTime.Now;

                    // Updated Property Name
                    dashboard.ProfileMV.UserPhotoPath = userDetail.PhotoPath;

                    dashboard.ProfileMV.CNIC = userDetail.Cnic;
                    dashboard.ProfileMV.EducationLevel = userDetail.EducationLevel;
                    dashboard.ProfileMV.EducationLastDegreePhotoPath = userDetail.EducationLastDegreeScanPath;
                    dashboard.ProfileMV.ExperenceLastPhotoPath = userDetail.LastExperienceScanPhotoPath;
                }

                // 4. Personal Address
                var personalAddress = _context.UserAddressTables.FirstOrDefault(u => u.UserId == user.UserId);
                dashboard.ProfileMV.FullAddress = personalAddress?.FullAddress ?? string.Empty;

                // 5. Load List of Addresses
                var addressList = _context.UserAddressTables
                    .Include(a => a.AddressType)
                    .Include(a => a.VisibleStatus)
                    .Include(a => a.User)
                    .Where(a => a.VisibleStatusId == 1)
                    .ToList();

                foreach (var addr in addressList)
                {
                    dashboard.UserAddress.Add(new UserAddressMV
                    {
                        UserAddressID = addr.UserAddressId,
                        FullAddress = addr.FullAddress,
                        AddressType = addr.AddressType?.AddressType,
                        VisibleStatus = addr.VisibleStatus?.VisibleStatus,
                        UserName = addr.User?.Username
                    });
                }
            }

            return View(dashboard);
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

            // A. Password Change Logic
            if (!string.IsNullOrEmpty(dashboardMV.OldPassword))
            {
                if (user.Password == dashboardMV.OldPassword)
                {
                    if (dashboardMV.NewPassword?.Trim() == dashboardMV.ConfirmPassword?.Trim())
                    {
                        user.Password = dashboardMV.NewPassword;
                        _context.Entry(user).State = EntityState.Modified;
                        _context.SaveChanges();
                        ModelState.AddModelError("OldPassword", "Password Changed Successfully");
                    }
                    else
                    {
                        ModelState.AddModelError("ConfirmPassword", "Passwords do not match!");
                    }
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "Old Password is Incorrect!");
                }
            }

            // B. Profile Update Logic
            if (dashboardMV.ProfileMV != null)
            {
                user.FirstName = dashboardMV.ProfileMV.FirstName;
                user.LastName = dashboardMV.ProfileMV.LastName;
                user.EmailAddress = dashboardMV.ProfileMV.EmailAddress;
                user.ContactNo = dashboardMV.ProfileMV.ContactNo;
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();

                // Create UserDetail if it doesn't exist
                if (userDetail == null)
                {
                    userDetail = new UserDetailTable
                    {
                        UserId = userid,
                        CreatedByUserId = userid,
                        UserDataProvidedDate = DateTime.Now
                    };
                    _context.UserDetailTables.Add(userDetail);
                    _context.SaveChanges();
                }

                // 1. Profile Photo Upload
                if (dashboardMV.ProfileMV.UserPhoto != null)
                {
                    string folder = "Content/ProfilePhoto";
                    string fileName = $"{user.UserId}.jpg";
                    if (_fileHelper.UploadPhoto(dashboardMV.ProfileMV.UserPhoto, folder, fileName))
                    {
                        userDetail.PhotoPath = $"/{folder}/{fileName}";
                        userDetail.UserDataProvidedDate = DateTime.Now;
                        _context.Entry(userDetail).State = EntityState.Modified;
                    }
                }

                // 2. Education Update
                if (!string.IsNullOrEmpty(dashboardMV.ProfileMV.EducationLevel))
                {
                    userDetail.EducationLevel = dashboardMV.ProfileMV.EducationLevel;
                    _context.Entry(userDetail).State = EntityState.Modified;
                }

                // 4. Education Document Upload
                if (dashboardMV.ProfileMV.EducationLastDegreePhoto != null)
                {
                    string folder = "Content/OtherFiles";
                    string fileName = $"Education_{userid}.jpg";
                    if (_fileHelper.UploadPhoto(dashboardMV.ProfileMV.EducationLastDegreePhoto, folder, fileName))
                    {
                        userDetail.EducationLastDegreeScanPath = $"/{folder}/{fileName}";
                        _context.Entry(userDetail).State = EntityState.Modified;
                    }
                }

                // 5. Experience Document Upload
                if (dashboardMV.ProfileMV.ExperenceLastPhoto != null)
                {
                    string folder = "Content/OtherFiles";
                    string fileName = $"Experience_{userid}.jpg";
                    if (_fileHelper.UploadPhoto(dashboardMV.ProfileMV.ExperenceLastPhoto, folder, fileName))
                    {
                        userDetail.LastExperienceScanPhotoPath = $"/{folder}/{fileName}";
                        _context.Entry(userDetail).State = EntityState.Modified;
                    }
                }

                _context.SaveChanges();
                ModelState.AddModelError(string.Empty, "Profile Updated");
            }

            return RedirectToAction("Dashboard");
        }

        // ======================= RESET PASSWORD PLACEHOLDER =======================
        public IActionResult ResetPassword(string recoverycode)
        {
            return RedirectToAction("Login");
        }
    }
}