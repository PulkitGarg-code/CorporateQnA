
using System.ComponentModel.DataAnnotations;

namespace CorporateQnA.Client.Models.View
{
    public class RegisterModel
    {
        public string ReturnUrl { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Designation { get; set; }
        [Required]

        public string ProfileImage { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords dont match")]
        public string ConfirmPassword { get; set; }
    }
}
