using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
