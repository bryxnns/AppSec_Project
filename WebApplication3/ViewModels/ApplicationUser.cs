using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.ViewModels
{
    public class ApplicationUser : IdentityUser
    {
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
