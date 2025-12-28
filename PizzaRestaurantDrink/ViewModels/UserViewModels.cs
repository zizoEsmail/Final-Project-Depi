using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http; // Required for IFormFile

namespace PizzaRestaurantDrink.ViewModels
{
    // ================== LOGIN ==================
    public class LoginMV
    {
        [Required(ErrorMessage = "Required*")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Required*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    // ================== REGISTRATION ==================
    public class Reg_UserMV
    {
        public int UserID { get; set; }
        public int UserTypeID { get; set; }
        public int UserStatusID { get; set; }

        [Required(ErrorMessage = "Required*")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Required*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Required*")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Required*")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Required*")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Required*")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Required*")]
        public string ContactNo { get; set; }

        public int GenderID { get; set; }
        public DateTime RegisterationDate { get; set; }
    }

    // ================== FORGOT PASSWORD ==================
    public class ForgotPasswordMV
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Required*")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Required*")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Not Match!")]
        public string ConfirmPassword { get; set; }
    }

    // ================== DASHBOARD & PROFILE ==================
    public class DashboardMV
    {
        public DashboardMV()
        {
            ProfileMV = new UserProfileMV();
            UserAddress = new List<UserAddressMV>();
        }

        public UserProfileMV ProfileMV { get; set; }
        public List<UserAddressMV> UserAddress { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "Not Match!")]
        public string ConfirmPassword { get; set; }
    }

    public class UserProfileMV
    {
        public int UserID { get; set; }
        public string UserType { get; set; }
        public int UserTypeID { get; set; }
        public string UserName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string ContactNo { get; set; }
        public string GenderTitle { get; set; }
        public string EmailAddress { get; set; }
        public DateTime RegisterationDate { get; set; }

        [DataType(DataType.MultilineText)]
        public string FullAddress { get; set; }

        public string UserStatus { get; set; }
        public int UserStatusID { get; set; }
        public DateTime? UserStatusChangeDate { get; set; }

        public DateTime UserDetailProvideDate { get; set; }

        // --- CHANGED: Renamed from PhotoPath to UserPhotoPath to match your View ---
        public string UserPhotoPath { get; set; }
        // --------------------------------------------------------------------------

        public string CNIC { get; set; }
        public string EducationLevel { get; set; }
        public string ExperenceLevel { get; set; }
        public string EducationLastDegreePhotoPath { get; set; }
        public string ExperenceLastPhotoPath { get; set; }

        [Display(Name = "Profile Photo")]
        public IFormFile UserPhoto { get; set; }

        [Display(Name = "Education Last Degree Scan")]
        public IFormFile EducationLastDegreePhoto { get; set; }

        [Display(Name = "Experience Certificate Scan")]
        public IFormFile ExperenceLastPhoto { get; set; }
    }

    public class UserAddressMV
    {
        public int UserAddressID { get; set; }
        public string AddressType { get; set; }
        public string FullAddress { get; set; }
        public string VisibleStatus { get; set; }
        public string UserName { get; set; }
    }

    // ================== ACCOUNT RECOVERY ==================
    public class AccountRecoveryMV
    {
        [Required(ErrorMessage = "Required*")]
        [Display(Name = "User Name or Email")]
        public string UserName { get; set; }
    }
}