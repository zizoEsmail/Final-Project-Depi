using System.ComponentModel.DataAnnotations;
using Dblayer.Models;

namespace PizzaRestaurantDrink.ViewModels
{
    // ================== LOGIN VIEW MODEL ==================
    public class LoginMV
    {
        [Required(ErrorMessage = "Required*")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Required*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    // ================== REGISTRATION VIEW MODEL ==================
    public class Reg_UserMV
    {
        public int UserID { get; set; }
        public int UserTypeID { get; set; }
        public int UserStatusID { get; set; }

        [Required(ErrorMessage = "Required*")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Required*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Required*")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Required*")]
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

    // ================== FORGOT PASSWORD VIEW MODEL ==================
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

    // ================== DASHBOARD VIEW MODEL ==================
    public class DashboardMV
    {
        public DashboardMV()
        {
            ProfileMV = new ProfileMV();
        }

        // This constructor was used in the old code to init data, 
        // but in .NET Core we usually load data in the Controller.
        // We will keep it simple for now to make the code compile.
        public DashboardMV(int userid)
        {
            ProfileMV = new ProfileMV();
        }

        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Not Match!")]
        public string ConfirmPassword { get; set; }

        public ProfileMV ProfileMV { get; set; }
    }

    // ================== PROFILE VIEW MODEL ==================
    public class ProfileMV
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNo { get; set; }

        // For Uploading
        public IFormFile UserPhoto { get; set; }
        // For Displaying
        public string UserPhotoPath { get; set; }

        public string EducationLevel { get; set; }
        public string ExperenceLevel { get; set; }

        // For Uploading
        public IFormFile EducationLastDegreePhoto { get; set; }
        public IFormFile ExperenceLastPhoto { get; set; }

        // For Displaying
        public string EducationLastDegreePhotoPath { get; set; }
        public string ExperenceLastPhotoPath { get; set; }
    }

    public class AccountRecoveryMV
    {
        [Required(ErrorMessage = "Required*")]
        public string UserName { get; set; }
    }
}
