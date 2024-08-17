using System.ComponentModel.DataAnnotations;

namespace XuongMay_BE.Models
{
    public class EmployeeModel
    {
        [Required]
        public string EmpName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
