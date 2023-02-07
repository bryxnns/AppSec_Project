using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebApplication3.ViewModels
{
    public class Register
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        // RegularExpression("\"^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{12,}$\"")
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Full_Name { get; set; }

        [Required]
        public string Credit_Card { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Mobile_No { get; set; }

        [Required]
        public string Delivery_Address { get; set; }

        [MaxLength(50), RegularExpression(@"^.*\.(jpg|jpeg)$")]
        public string? Image { get; set; }

        public string AboutMe { get; set; }
    }
}
