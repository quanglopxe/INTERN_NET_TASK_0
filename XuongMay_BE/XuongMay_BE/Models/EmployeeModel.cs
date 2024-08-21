using System.ComponentModel.DataAnnotations;

namespace XuongMay_BE.Models
{
    public class EmployeeModel
    {
        [Required]
        public string EmpName { get; set; }
        [Required]
        public Guid LineID { get; set; }
        [Required]
        public Guid? UserID { get; set; }

    }
}
