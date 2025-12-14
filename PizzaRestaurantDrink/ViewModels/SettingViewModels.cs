using System.ComponentModel.DataAnnotations;
using Dblayer.Models;

namespace PizzaRestaurantDrink.ViewModels
{
    // ================== USER TYPE VIEW MODEL ==================
    public class CRU_UserTypeMV
    {
        public int UserTypeID { get; set; }

        [Required(ErrorMessage = "Required*")]
        public string UserType { get; set; }

        // Holds the list to display in the table
        public IEnumerable<UserTypeTable> listUserType { get; set; }
    }

    // ================== GENDER VIEW MODEL ==================
    public class CRU_GenderMV
    {
        public int GenderID { get; set; }

        [Required(ErrorMessage = "Required*")]
        public string GenderTitle { get; set; }

        public IEnumerable<GenderTable> listGender { get; set; }
    }

    // ================== USER STATUS VIEW MODEL ==================
    public class CRU_UserStatusMV
    {
        public int UserStatusID { get; set; }

        [Required(ErrorMessage = "Required*")]
        public string UserStatus { get; set; }

        public IEnumerable<UserStatusTable> listUserStatus { get; set; }
    }

    // ================== VISIBLE STATUS VIEW MODEL ==================
    public class CRU_VisibleStatusMV
    {
        public int VisibleStatusID { get; set; }

        [Required(ErrorMessage = "Required*")]
        public string VisibleStatus { get; set; }

        public IEnumerable<VisibleStatusTable> listVisibleStatus { get; set; }
    }

    // ================== USER ADDRESS VIEW MODEL ==================
    public class CRU_UserAddressMV
    {
        public int UserAddressID { get; set; }
        public int UserID { get; set; }

        [Required]
        public int AddressTypeID { get; set; }

        [Required(ErrorMessage = "Required*")]
        public string FullAddress { get; set; }

        [Required]
        public int VisibleStatusID { get; set; }

        public IEnumerable<UserAddressTable> listUserAddress { get; set; }
    }
}
