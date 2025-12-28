using System;

namespace PizzaRestaurantDrink.ViewModels
{
    public class CartItemViewModel
    {
        public int StockMenuItemId { get; set; }
        public string ItemTitle { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string PhotoPath { get; set; }

        public decimal TotalPrice
        {
            get { return UnitPrice * Quantity; }
        }
    }
}