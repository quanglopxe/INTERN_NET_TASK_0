using System.ComponentModel.DataAnnotations;

namespace XuongMay_BE.Models
{
    public class EmployeeModel
    {
        [Required]
        public Guid EmpID { get; set; }

        [Required]
        [StringLength(100)]
        public string EmpName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
        public Guid LineID { get; set; }

    }
}
