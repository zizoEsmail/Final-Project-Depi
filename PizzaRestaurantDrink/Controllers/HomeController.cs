using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dblayer.Models;
using PizzaRestaurantDrink.ViewModels;

namespace PizzaRestaurantDrink.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProPizzResturentandDrinkDbContext _context;

        public HomeController(ProPizzResturentandDrinkDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "User");
            }

            HomeViewModel model = new HomeViewModel();
            model.Categories = _context.StockMenuCategoryTables.ToList();
            model.Items = _context.StockMenuItemTables
                                  .Include(i => i.StockMenuCategory)
                                  .Include(i => i.StockItem)
                                  .ToList();

            return View(model);
        }

        // ==========================================
        // ADD TO CART (DATABASE VERSION)
        // ==========================================
        public IActionResult AddToCart(int id)
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null) return RedirectToAction("Login", "User");

            // 1. Check if this User already has a Cart
            var cart = _context.CartTables.FirstOrDefault(c => c.UserId == userId);

            // If no cart exists, create one
            if (cart == null)
            {
                cart = new CartTable
                {
                    UserId = (int)userId
                };
                _context.CartTables.Add(cart);
                _context.SaveChanges(); // Save to get the new CartId
            }

            // 2. Check if this specific item is already in the cart details
            var cartItem = _context.CartDetailTables
                                   .FirstOrDefault(cd => cd.CartId == cart.CartId && cd.StockMenuItemId == id);

            if (cartItem != null)
            {
                // Item exists, just increase quantity
                cartItem.Quantity++;
                _context.Entry(cartItem).State = EntityState.Modified;
            }
            else
            {
                // Item does not exist, add it
                cartItem = new CartDetailTable
                {
                    CartId = cart.CartId,
                    StockMenuItemId = id,
                    Quantity = 1
                };
                _context.CartDetailTables.Add(cartItem);
            }

            _context.SaveChanges();

            return RedirectToAction("Cart");
        }

        // ==========================================
        // UPDATE QUANTITY (+/-)
        // ==========================================
        public IActionResult UpdateQuantity(int id, int change)
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null) return RedirectToAction("Login", "User");

            var cart = _context.CartTables.FirstOrDefault(c => c.UserId == userId);
            if (cart != null)
            {
                var cartItem = _context.CartDetailTables
                    .FirstOrDefault(cd => cd.CartId == cart.CartId && cd.StockMenuItemId == id);

                if (cartItem != null)
                {
                    // Apply change (e.g., +1 or -1)
                    cartItem.Quantity += change;

                    // If quantity drops to 0 or less, remove the item
                    if (cartItem.Quantity <= 0)
                    {
                        _context.CartDetailTables.Remove(cartItem);
                    }
                    else
                    {
                        _context.Entry(cartItem).State = EntityState.Modified;
                    }
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Cart");
        }

        // =================================
        // AJAX QUANTITY UPDATE (NO RELOAD)
        // =================================
        [HttpPost]
        public IActionResult UpdateQuantityAjax(int id, int change)
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null) return Json(new { success = false, message = "Login required" });

            var cart = _context.CartTables.FirstOrDefault(c => c.UserId == userId);
            if (cart != null)
            {
                var cartItem = _context.CartDetailTables
                    .Include(cd => cd.StockMenuItem)
                    .ThenInclude(smi => smi.StockItem)
                    .FirstOrDefault(cd => cd.CartId == cart.CartId && cd.StockMenuItemId == id);

                if (cartItem != null)
                {
                    // 1. Update Quantity
                    cartItem.Quantity += change;

                    if (cartItem.Quantity <= 0)
                    {
                        _context.CartDetailTables.Remove(cartItem);
                        _context.SaveChanges();

                        // FIX 1: Added (?? 0) to handle nullable UnitPrice
                        decimal newGrandTotal = _context.CartDetailTables
                            .Where(x => x.CartId == cart.CartId)
                            .Sum(x => x.Quantity * (x.StockMenuItem.StockItem.UnitPrice ?? 0));

                        return Json(new { success = true, isRemoved = true, grandTotal = newGrandTotal });
                    }
                    else
                    {
                        _context.Entry(cartItem).State = EntityState.Modified;
                        _context.SaveChanges();

                        // FIX 2: Added (?? 0) here as well
                        decimal itemTotal = cartItem.Quantity * (cartItem.StockMenuItem.StockItem.UnitPrice ?? 0);

                        // FIX 3: And here
                        decimal newGrandTotal = _context.CartDetailTables
                            .Where(x => x.CartId == cart.CartId)
                            .Sum(x => x.Quantity * (x.StockMenuItem.StockItem.UnitPrice ?? 0));

                        return Json(new
                        {
                            success = true,
                            isRemoved = false,
                            newQty = cartItem.Quantity,
                            itemTotal = itemTotal,
                            grandTotal = newGrandTotal
                        });
                    }
                }
            }
            return Json(new { success = false });
        }

        // ==========================================
        // VIEW CART PAGE
        // ==========================================
        public IActionResult Cart()
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null) return RedirectToAction("Login", "User");

            // Get the user's cart
            var cart = _context.CartTables.FirstOrDefault(c => c.UserId == userId);

            List<CartItemViewModel> cartList = new List<CartItemViewModel>();

            if (cart != null)
            {
                // Join tables to get Item Name, Price, Photo
                cartList = _context.CartDetailTables
                    .Where(cd => cd.CartId == cart.CartId)
                    .Include(cd => cd.StockMenuItem)
                    .ThenInclude(smi => smi.StockItem)
                    .Select(cd => new CartItemViewModel
                    {
                        StockMenuItemId = cd.StockMenuItemId,
                        Quantity = cd.Quantity,
                        ItemTitle = cd.StockMenuItem.StockItem.StockItemTitle,
                        UnitPrice = (decimal)cd.StockMenuItem.StockItem.UnitPrice,
                        PhotoPath = cd.StockMenuItem.StockItem.ItemPhotoPath
                    }).ToList();
            }

            return View(cartList);
        }

        // ==========================================
        // REMOVE FROM CART
        // ==========================================
        public IActionResult RemoveFromCart(int id)
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null) return RedirectToAction("Login", "User");

            var cart = _context.CartTables.FirstOrDefault(c => c.UserId == userId);
            if (cart != null)
            {
                var itemToRemove = _context.CartDetailTables
                    .FirstOrDefault(cd => cd.CartId == cart.CartId && cd.StockMenuItemId == id);

                if (itemToRemove != null)
                {
                    _context.CartDetailTables.Remove(itemToRemove);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Cart");
        }

        // ==========================================
        // CHECKOUT PAGE
        // ==========================================
        public IActionResult Checkout()
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null) return RedirectToAction("Login", "User");

            var viewModel = new CheckoutViewModel();
            viewModel.OrderTime = DateTime.Now;
            viewModel.ExpectedTime = DateTime.Now.AddMinutes(40);

            // 1. Get User Details
            var user = _context.UserTables
                .Include(u => u.UserAddressTables)
                .ThenInclude(ua => ua.AddressType) // Assuming you might check type later
                .FirstOrDefault(u => u.UserId == userId);

            if (user != null)
            {
                viewModel.FullName = (user.FirstName + " " + user.LastName).Trim();
                viewModel.PhoneNumber = user.ContactNo;

                // Get the most recent address (or default logic)
                var address = user.UserAddressTables.FirstOrDefault();
                if (address != null)
                {
                    viewModel.Address = address.FullAddress;
                    viewModel.AddressId = address.UserAddressId;
                }
            }

            // 2. Get Cart Items
            var cart = _context.CartTables.FirstOrDefault(c => c.UserId == userId);
            if (cart != null)
            {
                viewModel.CartItems = _context.CartDetailTables
                    .Where(cd => cd.CartId == cart.CartId)
                    .Include(cd => cd.StockMenuItem)
                    .ThenInclude(smi => smi.StockItem)
                    .Select(cd => new CartItemViewModel
                    {
                        StockMenuItemId = cd.StockMenuItemId,
                        Quantity = cd.Quantity,
                        ItemTitle = cd.StockMenuItem.StockItem.StockItemTitle,
                        UnitPrice = (decimal)(cd.StockMenuItem.StockItem.UnitPrice ?? 0),
                        PhotoPath = cd.StockMenuItem.StockItem.ItemPhotoPath
                    }).ToList();

                viewModel.SubTotal = viewModel.CartItems.Sum(x => x.TotalPrice);
            }
            else
            {
                viewModel.CartItems = new List<CartItemViewModel>();
            }

            return View(viewModel);
        }

        // ==========================================
        // PLACE ORDER
        // ==========================================
        [HttpPost]
        public IActionResult PlaceOrder()
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null) return RedirectToAction("Login", "User");

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // 1. Get Cart Data
                    var cart = _context.CartTables
                        .Include(c => c.CartDetails)
                        .ThenInclude(cd => cd.StockMenuItem)
                        .ThenInclude(smi => smi.StockItem)
                        .FirstOrDefault(c => c.UserId == userId);

                    if (cart == null || !cart.CartDetails.Any())
                    {
                        return RedirectToAction("Index");
                    }

                    // 2. Get User Info for the Order Header
                    var user = _context.UserTables.Include(u => u.UserAddressTables).FirstOrDefault(u => u.UserId == userId);
                    var address = user.UserAddressTables.FirstOrDefault();

                    if (user == null || address == null) return RedirectToAction("Checkout"); // Validation fail

                    // 3. Create Order Header
                    var newOrder = new OrderTable
                    {
                        OrderByUserId = userId,
                        OrderDateTime = DateTime.Now,
                        OrderTypeId = 1, // Assuming 1 = Delivery
                        OrderStatusId = 1, // Assuming 1 = Pending/Received
                        DeliveryAddressUserAddressId = address.UserAddressId,
                        OrderReceivedByFullName = (user.FirstName + " " + user.LastName).Trim(),
                        OrderReceivedByContactNo = user.ContactNo,
                        Description = "Order placed via Website"
                    };

                    _context.OrderTables.Add(newOrder);
                    _context.SaveChanges(); // Save to get the OrderId

                    // 4. Create Order Details (Items)
                    foreach (var cartItem in cart.CartDetails)
                    {
                        var orderItem = new OrderItemDetailTable
                        {
                            OrderId = newOrder.OrderId,
                            StockItemId = cartItem.StockMenuItem.StockItemId, // Link to the raw stock item
                            Qty = cartItem.Quantity,
                            UnitPrice = (double)(cartItem.StockMenuItem.StockItem.UnitPrice ?? 0),
                            DiscountAmount = 0
                        };
                        _context.OrderItemDetailTables.Add(orderItem);
                    }
                    _context.SaveChanges();

                    // 5. Delete Cart
                    _context.CartTables.Remove(cart); // Cascading delete will handle details usually, or remove details first
                    _context.SaveChanges();

                    transaction.Commit();

                    // 6. Set Success Notification
                    TempData["SuccessMessage"] = "Order Placed Successfully! Reference #" + newOrder.OrderId;

                    // Redirect to Home (or Order History if you prefer)
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // Log error
                    return RedirectToAction("Checkout");
                }
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}