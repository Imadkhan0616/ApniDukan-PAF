using System.ComponentModel.DataAnnotations;

namespace ApniDukan.Models
{
    public class SignInViewModel
    {
        [Required, StringLength(200)]
        [EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(100)]
        public string Password { get; set; }
    }
}