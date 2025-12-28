using System;
using System.Collections.Generic;

namespace PizzaRestaurantDrink.ViewModels
{
    public class CheckoutViewModel
    {
        // Bill Details
        public List<CartItemViewModel> CartItems { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DeliveryFee { get; set; } = 7.50m; // Fixed Fee
        public decimal GrandTotal => SubTotal + DeliveryFee;

        // User Validation Details
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int? AddressId { get; set; } // Needed to save to OrderTable

        // Logic Flags
        public bool HasFullName => !string.IsNullOrEmpty(FullName);
        public bool HasPhoneNumber => !string.IsNullOrEmpty(PhoneNumber);
        public bool HasAddress => !string.IsNullOrEmpty(Address);
        public bool CanPlaceOrder => HasFullName && HasPhoneNumber && HasAddress;

        // Timing
        public DateTime OrderTime { get; set; }
        public DateTime ExpectedTime { get; set; }
    }
}