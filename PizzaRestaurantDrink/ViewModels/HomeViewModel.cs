using Dblayer.Models;

namespace PizzaRestaurantDrink.ViewModels
{
    public class HomeViewModel
    {
        // The list of categories (e.g. Pizza, Drink)
        public IEnumerable<StockMenuCategoryTable> Categories { get; set; }

        // The list of actual food items
        public IEnumerable<StockMenuItemTable> Items { get; set; }
    }
}