using Dblayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaRestaurantDrink.Models;
using PizzaRestaurantDrink.ViewModels;
using System.Diagnostics;

namespace PizzaRestaurantDrink.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // 1. We use your specific Context class name here
        private readonly ProPizzResturentandDrinkDbContext _context;

        public HomeController(ILogger<HomeController> logger, ProPizzResturentandDrinkDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();

            // 1. Fetch Categories
            model.Categories = _context.StockMenuCategoryTables.ToList();

            // 2. Fetch Items
            // CRITICAL: We use .Include() to grab the related data
            model.Items = _context.StockMenuItemTables
                                  .Include(i => i.StockMenuCategory) // Gets Category Name
                                  .Include(i => i.StockItem)         // Gets Name, Price, Image
                                  .ToList();

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}