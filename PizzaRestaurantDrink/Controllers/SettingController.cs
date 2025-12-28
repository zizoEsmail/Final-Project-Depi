using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dblayer.Models;
using PizzaRestaurantDrink.ViewModels;

namespace PizzaRestaurantDrink.Controllers
{
    public class SettingController : Controller
    {
        private readonly ProPizzResturentandDrinkDbContext _context;

        public SettingController(ProPizzResturentandDrinkDbContext context)
        {
            _context = context;
        }

        // ======================= USER TYPES =======================
        public IActionResult List_UserTypes(int? id)
        {
            if (HttpContext.Session.GetInt32("UserTypeID") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new CRU_UserTypeMV();
            if (id.HasValue && id > 0)
            {
                var editItem = _context.UserTypeTables.Find(id);
                if (editItem != null)
                {
                    model.UserTypeID = editItem.UserTypeId;
                    model.UserType = editItem.UserType;
                }
            }
            model.listUserType = _context.UserTypeTables.ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult List_UserTypes(CRU_UserTypeMV cRU_UserTypeMV)
        {
            if (HttpContext.Session.GetInt32("UserTypeID") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                var checkexist = _context.UserTypeTables
                    .FirstOrDefault(u => u.UserType == cRU_UserTypeMV.UserType && u.UserTypeId != cRU_UserTypeMV.UserTypeID);

                if (checkexist == null)
                {
                    if (cRU_UserTypeMV.UserTypeID == 0)
                    {
                        var usertype = new UserTypeTable { UserType = cRU_UserTypeMV.UserType };
                        _context.UserTypeTables.Add(usertype);
                    }
                    else
                    {
                        var usertype = _context.UserTypeTables.Find(cRU_UserTypeMV.UserTypeID);
                        if (usertype != null)
                        {
                            usertype.UserType = cRU_UserTypeMV.UserType;
                            _context.Entry(usertype).State = EntityState.Modified;
                        }
                    }
                    _context.SaveChanges();
                    return RedirectToAction("List_UserTypes", new { id = 0 });
                }
                else
                {
                    ModelState.AddModelError("UserType", "Already Exists!");
                }
            }
            cRU_UserTypeMV.listUserType = _context.UserTypeTables.ToList();
            return View(cRU_UserTypeMV);
        }

        // ======================= GENDERS =======================
        public IActionResult List_Genders(int? id)
        {
            if (HttpContext.Session.GetInt32("UserTypeID") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new CRU_GenderMV();
            if (id.HasValue && id > 0)
            {
                var item = _context.GenderTables.Find(id);
                if (item != null)
                {
                    model.GenderID = item.GenderId;
                    model.GenderTitle = item.GenderTitle;
                }
            }
            model.listGender = _context.GenderTables.ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult List_Genders(CRU_GenderMV cRU_GenderMV)
        {
            if (HttpContext.Session.GetInt32("UserTypeID") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                var checkexist = _context.GenderTables
                    .FirstOrDefault(g => g.GenderTitle == cRU_GenderMV.GenderTitle && g.GenderId != cRU_GenderMV.GenderID);

                if (checkexist == null)
                {
                    if (cRU_GenderMV.GenderID == 0)
                    {
                        var gender = new GenderTable { GenderTitle = cRU_GenderMV.GenderTitle };
                        _context.GenderTables.Add(gender);
                    }
                    else
                    {
                        var gender = _context.GenderTables.Find(cRU_GenderMV.GenderID);
                        if (gender != null)
                        {
                            gender.GenderTitle = cRU_GenderMV.GenderTitle;
                            _context.Entry(gender).State = EntityState.Modified;
                        }
                    }
                    _context.SaveChanges();
                    return RedirectToAction("List_Genders", new { id = 0 });
                }
                else
                {
                    ModelState.AddModelError("GenderTitle", "Already Exists!");
                }
            }
            cRU_GenderMV.listGender = _context.GenderTables.ToList();
            return View(cRU_GenderMV);
        }

        // ======================= USER STATUS =======================
        public IActionResult List_UserStatus(int? id)
        {
            if (HttpContext.Session.GetInt32("UserTypeID") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new CRU_UserStatusMV();
            if (id.HasValue && id > 0)
            {
                var item = _context.UserStatusTables.Find(id);
                if (item != null)
                {
                    model.UserStatusID = item.UserStatusId;
                    model.UserStatus = item.UserStatus;
                }
            }
            model.listUserStatus = _context.UserStatusTables.ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult List_UserStatus(CRU_UserStatusMV cRU_UserStatusMV)
        {
            if (HttpContext.Session.GetInt32("UserTypeID") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                var checkexist = _context.UserStatusTables
                    .FirstOrDefault(s => s.UserStatus.ToUpper() == cRU_UserStatusMV.UserStatus.ToUpper()
                                      && s.UserStatusId != cRU_UserStatusMV.UserStatusID);

                if (checkexist == null)
                {
                    if (cRU_UserStatusMV.UserStatusID == 0)
                    {
                        var status = new UserStatusTable { UserStatus = cRU_UserStatusMV.UserStatus };
                        _context.UserStatusTables.Add(status);
                    }
                    else
                    {
                        var status = _context.UserStatusTables.Find(cRU_UserStatusMV.UserStatusID);
                        if (status != null)
                        {
                            status.UserStatus = cRU_UserStatusMV.UserStatus;
                            _context.Entry(status).State = EntityState.Modified;
                        }
                    }
                    _context.SaveChanges();
                    return RedirectToAction("List_UserStatus", new { id = 0 });
                }
                else
                {
                    ModelState.AddModelError("UserStatus", "Already Exists!");
                }
            }
            cRU_UserStatusMV.listUserStatus = _context.UserStatusTables.ToList();
            return View(cRU_UserStatusMV);
        }

        // ======================= VISIBLE STATUS =======================
        public IActionResult List_VisibleStatus(int? id)
        {
            if (HttpContext.Session.GetInt32("UserTypeID") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new CRU_VisibleStatusMV();
            if (id.HasValue && id > 0)
            {
                var item = _context.VisibleStatusTables.Find(id);
                if (item != null)
                {
                    model.VisibleStatusID = item.VisibleStatusId;
                    model.VisibleStatus = item.VisibleStatus;
                }
            }
            model.listVisibleStatus = _context.VisibleStatusTables.ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult List_VisibleStatus(CRU_VisibleStatusMV cRU_VisibleStatusMV)
        {
            if (HttpContext.Session.GetInt32("UserTypeID") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                var checkexist = _context.VisibleStatusTables
                    .FirstOrDefault(s => s.VisibleStatus.ToUpper() == cRU_VisibleStatusMV.VisibleStatus.ToUpper()
                                      && s.VisibleStatusId != cRU_VisibleStatusMV.VisibleStatusID);

                if (checkexist == null)
                {
                    if (cRU_VisibleStatusMV.VisibleStatusID == 0)
                    {
                        var status = new VisibleStatusTable { VisibleStatus = cRU_VisibleStatusMV.VisibleStatus };
                        _context.VisibleStatusTables.Add(status);
                    }
                    else
                    {
                        var status = _context.VisibleStatusTables.Find(cRU_VisibleStatusMV.VisibleStatusID);
                        if (status != null)
                        {
                            status.VisibleStatus = cRU_VisibleStatusMV.VisibleStatus;
                            _context.Entry(status).State = EntityState.Modified;
                        }
                    }
                    _context.SaveChanges();
                    return RedirectToAction("List_VisibleStatus", new { id = 0 });
                }
                else
                {
                    ModelState.AddModelError("VisibleStatus", "Field Required*");
                }
            }
            cRU_VisibleStatusMV.listVisibleStatus = _context.VisibleStatusTables.ToList();
            return View(cRU_VisibleStatusMV);
        }

        // ======================= USER ADDRESS =======================
        public IActionResult List_UserAddress(int? id)
        {
            if (HttpContext.Session.GetInt32("UserTypeID") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            int userid = HttpContext.Session.GetInt32("UserID") ?? 0;
            var model = new CRU_UserAddressMV();

            if (id.HasValue && id > 0)
            {
                var item = _context.UserAddressTables.Find(id);
                if (item != null)
                {
                    model.UserAddressID = item.UserAddressId;
                    model.AddressTypeID = item.AddressTypeId;
                    model.FullAddress = item.FullAddress;
                    model.VisibleStatusID = item.VisibleStatusId; // FIXED: No '?? 0' needed
                }
            }

            model.listUserAddress = _context.UserAddressTables
                .Include(u => u.AddressType)
                .Include(u => u.VisibleStatus)
                .Where(u => u.UserId == userid)
                .ToList();

            ViewBag.AddressTypeID = new SelectList(_context.AddressTypeTables.ToList(), "AddressTypeId", "AddressType", model.AddressTypeID);
            ViewBag.VisibleStatusID = new SelectList(_context.VisibleStatusTables.ToList(), "VisibleStatusId", "VisibleStatus", model.VisibleStatusID);

            return View(model);
        }

        [HttpPost]
        public IActionResult List_UserAddress(CRU_UserAddressMV cRU_UserAddressMV)
        {
            if (HttpContext.Session.GetInt32("UserTypeID") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            int userid = HttpContext.Session.GetInt32("UserID") ?? 0;

            if (ModelState.IsValid)
            {
                var checkexist = _context.UserAddressTables
                    .FirstOrDefault(s => s.FullAddress.ToUpper() == cRU_UserAddressMV.FullAddress.ToUpper()
                                      && s.AddressTypeId == cRU_UserAddressMV.AddressTypeID
                                      && s.UserAddressId != cRU_UserAddressMV.UserAddressID
                                      && s.UserId == userid);

                if (checkexist == null)
                {
                    if (cRU_UserAddressMV.UserAddressID == 0) // CREATE
                    {
                        var newaddress = new UserAddressTable
                        {
                            UserId = userid,
                            AddressTypeId = cRU_UserAddressMV.AddressTypeID,
                            FullAddress = cRU_UserAddressMV.FullAddress,
                            VisibleStatusId = cRU_UserAddressMV.VisibleStatusID,
                            CreatedByUserId = userid // Verified: This is correct
                        };
                        _context.UserAddressTables.Add(newaddress);
                    }
                    else // UPDATE
                    {
                        var edituseraddress = _context.UserAddressTables.Find(cRU_UserAddressMV.UserAddressID);
                        if (edituseraddress != null)
                        {
                            edituseraddress.AddressTypeId = cRU_UserAddressMV.AddressTypeID;
                            edituseraddress.FullAddress = cRU_UserAddressMV.FullAddress;
                            edituseraddress.VisibleStatusId = cRU_UserAddressMV.VisibleStatusID;
                            _context.Entry(edituseraddress).State = EntityState.Modified;
                        }
                    }
                    _context.SaveChanges();
                    return RedirectToAction("List_UserAddress", new { id = 0 });
                }
                else
                {
                    ModelState.AddModelError("FullAddress", "Already Exists!");
                }
            }

            cRU_UserAddressMV.listUserAddress = _context.UserAddressTables
                .Include(u => u.AddressType)
                .Include(u => u.VisibleStatus)
                .Where(u => u.UserId == userid)
                .ToList();

            ViewBag.AddressTypeID = new SelectList(_context.AddressTypeTables.ToList(), "AddressTypeId", "AddressType", cRU_UserAddressMV.AddressTypeID);
            ViewBag.VisibleStatusID = new SelectList(_context.VisibleStatusTables.ToList(), "VisibleStatusId", "VisibleStatus", cRU_UserAddressMV.VisibleStatusID);

            return View(cRU_UserAddressMV);
        }
    }
}