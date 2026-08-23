using System.ComponentModel.DataAnnotations;

namespace KweziHealth.Web.ViewModels
{
    // This ViewModel is used for the login form in the application. It contains properties for the username and password
    // This represents the SystemAdmin user who will be logging into the application.
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter your username.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}