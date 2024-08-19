using System.ComponentModel.DataAnnotations;
using XuongMay_BE.Data;

namespace XuongMay_BE.Models
{
    public class RegisterRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public UserRole Role { get; set; }

    }
}