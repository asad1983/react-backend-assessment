using System.ComponentModel.DataAnnotations;

namespace React_Backend.Application.Models
{
    public class RegisterModel
    {

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; } = string.Empty;


        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [EmailAddress(ErrorMessage = "Please provide valie email")]
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please provide valie email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]

        [DataType(DataType.Password)]
        public string Password { get; set; }


        public bool isDoctor { get; set; } = false;

    }
}
