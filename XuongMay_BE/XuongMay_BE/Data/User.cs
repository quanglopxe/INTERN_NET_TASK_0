using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XuongMay_BE.Data
{
    public enum UserRole
    {
        Supervisor = 0,
        Employee = 1,
        Customer = 2,
        Admin = 3
    }
    [Table("User")]
    public class User
    {
        [Key]
        public Guid UserID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }        
        [Required]
        public UserRole Role { get; set; }

    }
}
